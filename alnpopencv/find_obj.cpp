/*
 * Further Information : Automatic Recognition of License Template
 * Author: T.Nguyen Anh
 * anhthoai@hotmail.com
 */

#include "stdafx.h"
#include <cv.h>
#include <highgui.h>
#include <stdio.h>
#include <math.h>
#include <string.h>
#include "lp.h"

int thresh = 50;
CvPoint origin;
CvPoint p0,p1;
IplImage* img = 0;
IplImage* img0 = 0;
IplImage* lp = 0;
IplImage* lp_gray =0;
IplImage* sub = 0;
IplImage* res=0;

IplImage *hsv; 
IplImage *hue; 
IplImage *mask; 
IplImage *backproject; 
IplImage *histimg;
IplImage* image = 0;
CvHistogram *hist;
IplImage *keep_pixels_filter(IplImage *source_image, double thresh,int mask);
IplImage *remove_isolated(IplImage *source_image, double thresh);
void Extract_LicensePlate(void);
void gray_template(IplImage* source_image);
void adp_threshold(IplImage* gray_image);
void Histogram(IplImage* image);
void find_connected_components(
							   IplImage* mask,
							   int poly1_hull0 =1,
							   float perimScale =4,
							   int* num			= NULL,
							   CvRect* bbs		= NULL,
							   CvPoint* centers	= NULL
							   );



//approx.threshold - the bigger it is, the sipler is the boundary
#define CVCONTOUR_APPROX_LEVEL 2
//How many iterations of erosion and/or dilation there should be
#define CVCLOSE_ITR 1
CvScalar hsv2rgb( float hue );

int hdims = 16;
float hranges_arr[] = {0, 180};
float* hranges = hranges_arr; 

CvMemStorage* storage = 0;
CvRect selection;
const char* WindowName = "Automatic Recognition of License Template";
const char* LicensePlate = "License Template";
static CvPoint pt[4], *rect = pt;

const CvScalar white = CV_RGB(0xff,0xff,0xff);
const CvScalar black = CV_RGB(0x00,0x00,0x00);
const CvScalar green = CV_RGB(0,250,0);
const CvScalar orange = CV_RGB(250,150,0);
const CvScalar red = CV_RGB(250,0,0);
const CvScalar blue = CV_RGB(0,0,250);

// finds a cosine of angle between vectors
// from pt0->pt1 and from pt0->pt2 
double angle( CvPoint* pt1, CvPoint* pt2, CvPoint* pt0 )
{
    double dx1 = pt1->x - pt0->x;
    double dy1 = pt1->y - pt0->y;
    double dx2 = pt2->x - pt0->x;
    double dy2 = pt2->y - pt0->y;
    return (dx1*dx2 + dy1*dy2)/sqrt((dx1*dx1 + dy1*dy1)*(dx2*dx2 + dy2*dy2) + 1e-10);
}

IplImage* keep_pixels_filter(IplImage *source_image, double thresh, int mask)
{
  IplImage* new_image; /* construct filtered image here */
  IplImage* current_image; /* save image pointer */
  int height, width; /* image dimensions */
  int x,y,i,j; /* iteration variables */
  int set_pixel; /* should  pixel be set or not? */
  int lum; /* luminance value of pixel */

  /* create a new image */
  height = source_image->height;
  width = source_image->width;
  new_image = cvCloneImage(source_image);

  /* draw white (background) rectangle to clear new image */

  ssocr_set_imlib_color(BG);
  //imlib_image_draw_rectangle(0, 0, width, height);


  /* check for every pixel if it should be set in filtered image */
  for(x=0; x<width; x++) {
    for(y=0; y<height; y++) {
      set_pixel=0;
      //imlib_image_query_pixel(x, y, &color);
      lum = get_lum(&color, lt);
      if(is_pixel_set(lum, thresh)) { /* only test neighbors of set pixels */
        for(i=x-1; i<=x+1; i++) {
          for(j=y-1; j<=y+1; j++) {
            if(i>=0 && i<width && j>=0 && j<height) { /* i,j inside image? */
              imlib_image_query_pixel(i, j, &color);
              lum = get_lum(&color, lt);
              if(is_pixel_set(lum, thresh)) {
                set_pixel++;
              }
            }
          }
        }
      }
      /* set pixel if at least mask pixels around it are set */
      /* mask = 1 keeps all pixels */
      if(set_pixel > mask) {
        /* draw a black (foreground) pixel */
        imlib_context_set_image(new_image);
        ssocr_set_imlib_color(FG);
        imlib_image_draw_pixel(x,y,0);
        imlib_context_set_image(*source_image);
      } else {
        /* draw a white (background) pixel */
        imlib_context_set_image(new_image);
        ssocr_set_imlib_color(BG);
        imlib_image_draw_pixel(x,y,0);
        imlib_context_set_image(*source_image);
      }
    }
  }

  /* return filtered image */
  return new_image;
}

IplImage* remove_isolated(IplImage *source_image, double thresh)
{
	return keep_pixels_filter(source_image, thresh, 1);
}
// returns sequence of squares detected on the image.
// the sequence is stored in the specified memory storage
CvSeq* findSquares4( IplImage* img, CvMemStorage* storage )
{
    CvSeq* contours;
	static CvScalar colors[] = { {{0,0,255}}, {{0,128,255}}, {{0,255,255}}, 
	{{0,255,0}}, {{255,128,0}}, {{255,255,0}}, {{255,0,0}}, {{255,0,255}} };
    int i, c, l, N = 11;
    CvSize sz = cvSize( img->width & -2, img->height & -2 );
    IplImage* timg = cvCloneImage( img ); // make a copy of input image
    IplImage* gray = cvCreateImage( sz, 8, 1 ); 
    IplImage* pyr = cvCreateImage( cvSize(sz.width/2, sz.height/2), 8, 3 );
    IplImage* tgray;
    CvSeq* result;
    double s, t;
    // create empty sequence that will contain points -
    // 4 points per square (the square's vertices)
    CvSeq* squares = cvCreateSeq( 0, sizeof(CvSeq), sizeof(CvPoint), storage );
    
    // select the maximum ROI in the image
    // with the width and height divisible by 2
    cvSetImageROI( timg, cvRect( 0, 0, sz.width, sz.height ));
    
    // down-scale and upscale the image to filter out the noise
    cvPyrDown( timg, pyr, 7 );
    cvPyrUp( pyr, timg, 7 );
    tgray = cvCreateImage( sz, 8, 1 );
    
    // find squares in every color plane of the image
    for( c = 0; c < 3; c++ )
    {
        // extract the c-th color plane
        cvSetImageCOI( timg, c+1 );
        cvCopy( timg, tgray, 0 );
        
        // try several threshold levels
        for( l = 0; l < N; l++ )
        {
            // hack: use Canny instead of zero threshold level.
            // Canny helps to catch squares with gradient shading   
            if( l == 0 )
            {
                // apply Canny. Take the upper threshold from slider
                // and set the lower to 0 (which forces edges merging) 
                cvCanny( tgray, gray, 0, thresh, 5 );
                // dilate canny output to remove potential
                // holes between edge segments 
                cvDilate( gray, gray, 0, 1 );
            }
            else
            {
                // apply threshold if l!=0:
                //     tgray(x,y) = gray(x,y) < (l+1)*255/N ? 255 : 0
                cvThreshold( tgray, gray, (l+1)*255/N, 255, CV_THRESH_BINARY );
            }
            
            // find contours and store them all as a list
            cvFindContours( gray, storage, &contours, sizeof(CvContour),
                CV_RETR_LIST, CV_CHAIN_APPROX_SIMPLE, cvPoint(0,0) );
            
            // test each contour
            while( contours )
            {
                // approximate contour with accuracy proportional
                // to the contour perimeter
                result = cvApproxPoly( contours, sizeof(CvContour), storage,
                    CV_POLY_APPROX_DP, cvContourPerimeter(contours)*0.02, 0 );
                // square contours should have 4 vertices after approximation
                // relatively large area (to filter out noisy contours)
                // and be convex.
                // Note: absolute value of an area is used because
                // area may be positive or negative - in accordance with the
                // contour orientation
                if( result->total == 4 &&
                    fabs(cvContourArea(result,CV_WHOLE_SEQ)) > 1000 &&
					fabs(cvContourArea(result,CV_WHOLE_SEQ)) < 15000 &&
                    cvCheckContourConvexity(result) )
                {
                    s = 0;
                    
                    for( i = 0; i < 5; i++ )
                    {
                        // find minimum angle between joint
                        // edges (maximum of cosine)
                        if( i >= 2 )
                        {
                            t = fabs(angle(
                            (CvPoint*)cvGetSeqElem( result, i ),
                            (CvPoint*)cvGetSeqElem( result, i-2 ),
                            (CvPoint*)cvGetSeqElem( result, i-1 )));
                            s = s > t ? s : t;
                        }
                    }
                    
                    // if cosines of all angles are small
                    // (all angles are ~90 degree) then write quandrange
                    // vertices to resultant sequence 
                    if( s < 0.3 )
                        for( i = 0; i < 4; i++ )
						{
                            cvSeqPush( squares,
                                (CvPoint*)cvGetSeqElem( result, i ));
						}
                }
                
                // take the next contour
                contours = contours->h_next;
            }
        }
    }
    
    // release all the temporary images
    cvReleaseImage( &gray );
    cvReleaseImage( &pyr );
    cvReleaseImage( &tgray );
    cvReleaseImage( &timg );
    
    return squares;
}


// the function draws all the squares in the image
void drawSquares( IplImage* img, CvSeq* squares )
{
    CvSeqReader reader;
    IplImage* cpy = cvCloneImage( img );
    int i;
    
    // initialize reader of the sequence
    cvStartReadSeq( squares, &reader, 0 );
    
    // read 4 sequence elements at a time (all vertices of a square)
    for( i = 0; i < 4; i += 4 )//squares->total
    {
        //static CvPoint pt[4], *rect = pt;
        int count = 4;
        
        // read 4 vertices
        CV_READ_SEQ_ELEM( pt[0], reader );
        CV_READ_SEQ_ELEM( pt[1], reader );
        CV_READ_SEQ_ELEM( pt[2], reader );
        CV_READ_SEQ_ELEM( pt[3], reader );
		        
        // draw the square as a closed polyline 
        cvPolyLine( cpy, &rect, &count, 1, 1, CV_RGB(0,255,0), 3, CV_AA, 0 );
    }
		
	
    // show the resultant image
    cvShowImage( WindowName, cpy );
    cvReleaseImage( &cpy );
}


char* names[] = { "im41.jpg", 0 };

int main(int argc, char** argv)
{
    int i, c;
    // create memory storage that will contain all the dynamic data
    storage = cvCreateMemStorage(0);

    for( i = 0; names[i] != 0; i++ )
    {
        // load i-th image
        img0 = cvLoadImage( names[i], 1 );
        if( !img0 )
        {
            printf("Couldn't load %s\n", names[i] );
            continue;
        }
        img = cvCloneImage( img0 );
        
        // create window and a trackbar (slider) with parent "image" and set callback
        // (the slider regulates upper threshold, passed to Canny edge detector) 
        cvNamedWindow( WindowName, 1 );
        
        // find and draw the squares
        drawSquares( img, findSquares4( img, storage ) );
		//lp= cvLoadImage("b.jpg",0);
		
		Extract_LicensePlate();
			//cvSaveImage("c.jpg",lp);
			//drawSquares(lp,findSquares4( lp, storage ));
		gray_template(lp);
		adp_threshold(lp_gray);
			//drawSquares( lp, findSquares4( lp, storage ) );
			//Extract_LicensePlate();
			//Histogram(lp);
		/*find_connected_components(
							   lp_gray,
							   4,
							   1.0,
							   NULL,
							   NULL,
							   NULL
							   );*/

      
        // Also the function cvWaitKey takes care of event processing
        c = cvWaitKey(0);
        // release both images
        cvReleaseImage( &img );
        cvReleaseImage( &img0 );
        // clear memory storage - reset free space position
        cvClearMemStorage( storage );
        if( (char)c == 27 )
            break;
    }
    
    cvDestroyWindow( WindowName );
    
    return 0;
}
void Extract_LicensePlate()
{
	for(int j=0;j<4;j++)
		{
			printf("Diem %d co toa do x=%d and y=%d\n",j,pt[j].x,pt[j].y);
		
		}
			
		selection.x=pt[0].x;
		selection.y=pt[0].y;
		selection.width=CV_IABS(pt[0].x-pt[2].x);
		selection.height=CV_IABS(pt[0].y-pt[2].y);
		lp = cvCloneImage(img);
		/*
		cvSetImageROI( lp, selection );
		cvNamedWindow(LicensePlate, 1);
		cvShowImage(LicensePlate, lp);*/
		// or
		CvMat *sub=cvCreateMat(selection.width,selection.height,lp->depth);
		cvGetSubRect(img, sub, selection);
		res=cvGetImage(sub, lp);
		cvNamedWindow(LicensePlate, 1);
		cvShowImage(LicensePlate, lp);
}

void Histogram(IplImage* img1)
{
	IplImage* image=0;
	image = cvCreateImage( cvGetSize(img1), 8, 3 );
	int i, bin_w, c;
	hsv = cvCreateImage( cvGetSize(img1), 8, 3 );
	hue = cvCreateImage( cvGetSize(img1), 8, 1 );
	mask = cvCreateImage( cvGetSize(img1), 8, 1 );
	hist = cvCreateHist( 1, &hdims, CV_HIST_ARRAY, &hranges, 1 );
	histimg = cvCreateImage( cvSize(320,200), 8, 3 );
	cvZero( histimg );

	cvCopy( img1, image, 0 );
	cvCvtColor( image, hsv, CV_BGR2HSV );
	cvSaveImage("a.jpg",hsv);

	int _vmin = 10, _vmax = 256, smin = 30;

	cvInRangeS( hsv, cvScalar(0,smin,MIN(_vmin,_vmax),0),
				cvScalar(180,256,MAX(_vmin,_vmax),0), mask );
	cvSplit( hsv, hue, 0, 0, 0 );

	float max_val = 0.f;
	cvCalcHist( &hue, hist, 0, mask );
	cvGetMinMaxHistValue( hist, 0, &max_val, 0, 0 );
	cvConvertScale( hist->bins, hist->bins, max_val ? 255. / max_val : 0., 0 );


	cvZero( histimg );
	bin_w = histimg->width / hdims;
	for( i = 0; i < hdims; i++ )
	{
		int val = cvRound( cvGetReal1D(hist->bins,i)*histimg->height/255 );
		CvScalar color = hsv2rgb(i*180.f/hdims);
		cvRectangle( histimg, cvPoint(i*bin_w,histimg->height),
					 cvPoint((i+1)*bin_w,histimg->height - val),
					 color, -1, 8, 0 );
	}
	cvNamedWindow("HISTOGRAM",1);
	cvShowImage("HISTOGRAM",histimg);
}

CvScalar hsv2rgb( float hue )
{
    int rgb[3], p, sector;
    static const int sector_data[][3]=
        {{0,2,1}, {1,2,0}, {1,0,2}, {2,0,1}, {2,1,0}, {0,1,2}};
    hue *= 0.033333333333333333333333333333333f;
    sector = cvFloor(hue);
    p = cvRound(255*(hue - sector));
    p ^= sector & 1 ? 255 : 0;

    rgb[sector_data[sector][0]] = 255;
    rgb[sector_data[sector][1]] = 0;
    rgb[sector_data[sector][2]] = p;

    return cvScalar(rgb[2], rgb[1], rgb[0],0);
}

void gray_template(IplImage* lp)
{
	lp_gray = cvCreateImage(cvGetSize(lp), 8 ,1);
	//cvCopy(lp, gray_image, 0);
	cvCvtColor( lp, lp_gray, CV_BGR2GRAY );
	cvNamedWindow("Gray Template",1);
	cvShowImage("Gray Template", lp_gray);
}

void adp_threshold(IplImage* adp_lp)
{
	IplImage* Iat=0;
	int threshold_type = CV_THRESH_BINARY;
	int adaptive_method = CV_ADAPTIVE_THRESH_GAUSSIAN_C;//CV_ADAPTIVE_THRESH_MEAN_C;//:;
	int block_size= 255;
	double offset = 0.5;
	Iat= cvCreateImage(cvSize(adp_lp->width ,adp_lp->height),IPL_DEPTH_8U,1);
	/* Threshold */
	cvAdaptiveThreshold(adp_lp,Iat,255,adaptive_method,
		threshold_type,block_size,offset);
	cvNamedWindow("Adaptive Threshold");
	cvShowImage("Adaptive Threshold", Iat);
	//cvWaitKey(0);
	//cvReleaseImage(&Iat);
	//cvDestroyWindow("Adaptive Threshold");
}

void find_connected_components(
							   IplImage* mask,
							   int poly1_hull0,
							   float perimScale,
							   int		*num,
							   CvRect*	bbs,
							   CvPoint* centers
							   )
{
	static CvMemStorage*	mem_storage = NULL;
	static CvSeq*			contours    = NULL;
	// CLEAN UP RAW MASK
	cvMorphologyEx(mask, mask, 0, 0, CV_MOP_OPEN, CVCLOSE_ITR);
	cvMorphologyEx(mask, mask, 0, 0, CV_MOP_CLOSE, CVCLOSE_ITR);
	//FIND CONTOURS AROUND BIGGER REGIONS
	if(mem_storage == NULL)
	{
		mem_storage = cvCreateMemStorage(0);
	}
	else
	{
		cvClearMemStorage(mem_storage);
	}
	CvContourScanner scanner= cvStartFindContours(
		mask,
		mem_storage,
		sizeof(CvContour),
		CV_RETR_EXTERNAL,
		CV_CHAIN_APPROX_SIMPLE
		);
	CvSeq* c;
	int numCont = 0;
	while((c=cvFindNextContour(scanner))!=NULL)
	{
		double len =cvContourPerimeter(c);
		// calculate perimeter len threshold
		double q=(mask->height+mask->width)/perimScale;
		//Get rid of blob if its perimeter is too small
		if(len<q)
		{
			cvSubstituteContour(scanner, NULL);
		}
		else
		{
			//Smooth  its edges if its large enough
			CvSeq* c_new;
			if(poly1_hull0)
			{
				//Polygonal approximation
				c_new=cvApproxPoly(
					c,
					sizeof(CvContour),
					mem_storage,
					CV_POLY_APPROX_DP,
					CVCONTOUR_APPROX_LEVEL,
					0
					);
			}
			else
			{
				//Covex Hull of segmentation
				c_new=cvConvexHull2(
					c,
					mem_storage,
					CV_CLOCKWISE,
					1
					);
			}
			cvSubstituteContour(scanner,c_new);
			numCont++;
		}
		contours = cvEndFindContours(&scanner);
		//Just some convenience variables
		const CvScalar CVX_WHITE = CV_RGB(0xff,0xff,0xff);
		const CvScalar CVX_BLACK = CV_RGB(0x00,0x00,0x00);
		//Paint the found regions back into the image
		cvZero(mask);
		IplImage *maskTemp;
		//calc ccenter of mass and/or bounding rectagles
		if(num ==NULL)
		{
			//USER WNATS TO COLLECT STATISTICS
			int N = *num;
			int numFilled=0;
			int i=0;
			CvMoments moments;
			double M00, M01, M10;
			maskTemp=cvCloneImage(mask);
			for(i=0,c=contours;c!=NULL;c=c->h_next,i++)
			{
				if(i<N)
				{
					//Only process up to *num of them
					cvDrawContours(
						maskTemp,
						c,
						CVX_WHITE,
						CVX_BLACK,
						-1,
						CV_FILLED,
						8
						);
					//Find center of each contour
					if(centers!=NULL)
					{
						cvMoments(maskTemp,&moments,1);
						M00=cvGetSpatialMoment(&moments,0,0);
						M10=cvGetSpatialMoment(&moments,1,0);
						M01=cvGetSpatialMoment(&moments,0,1);
						centers[i].x=(int)(M10/M00);
						centers[i].y=(int)(M01/M00);
					}
					//Bounding rectangles around blobs
					if(bbs != NULL)
					{
						bbs[i]=cvBoundingRect(c);
					}
					cvZero(maskTemp);
					numFilled++;
				}
				//Draw filled contours into mask
				cvDrawContours(
					mask,
					c,
					CVX_WHITE,
					CVX_BLACK,
					-1,
					CV_FILLED,
					8
					);
			}
			*num =numFilled;
			cvReleaseImage(&maskTemp);
		}
		//else just draw processed contours into the mask
		else
		{
			//the user doesn't want statistics, just draw the contours
			for(c=contours;c!=NULL;c=c->h_next)
			{
				cvDrawContours(
					mask,
					c,
					CVX_WHITE,
					CVX_BLACK,
					-1,
					CV_FILLED,
					8
					);
			}
		}
	}
}






