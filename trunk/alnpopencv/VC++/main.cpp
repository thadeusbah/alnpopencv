#include <stdio.h>
#include <glob.h>
#include <errno.h>
#include <stdlib.h>
#include <string.h>

#include <string>

#include <cv.h>
#include <highgui.h>
#include <math.h>

#define affine
#ifdef affine

// Declare the image data structures

const char* win1 = "Original Image";
const char* win2 = "Affine Transform";
const char* win3 = "Rotate And Scale";

int main( int argc, char *argv[] )
{
    // Get the directory we want to browse, use current one otherwise
    std::string dirPath = ".";
    if ( argc > 1 )
        dirPath = argv[1];

    // What files are we looking for?
    std::string filePattern = "*.jpg";
    // So the full pattern is
    std::string pattern = dirPath+"/"+filePattern;

    printf( "Using pattern: %s\n", pattern.c_str() );
    glob_t pglob;
    int glob_status = glob( pattern.c_str(), 0, 0, &pglob );
    if ( glob_status != 0 )
    {
            fprintf( stderr, "Can't glob '%s': %s\n", pattern.c_str(), strerror(errno) );
            exit( -1 );
    }
    else
    {
        for ( int i = 0; i < pglob.gl_pathc; i++ )
        {
            // This is your file

            char *file = pglob.gl_pathv[i];
            //printf( "File: %s\n", file );

            IplImage *img1, *img2, *img3;
            //img1 = cvLoadImage( file , 1 );
            img1 = cvLoadImage( file , CV_LOAD_IMAGE_GRAYSCALE ); //8bit-1ch
            img2 = cvCloneImage( img1 );
            img2->origin = img1->origin;

            cvZero( img2 );


            cvNamedWindow( win1 , CV_WINDOW_AUTOSIZE );
            cvShowImage( win1, img1 );
            cvMoveWindow( win1, 400, 0 );

            CvMat* rot_mat = cvCreateMat(2,3,CV_32FC1);
            CvMat* warp_mat = cvCreateMat(2,3,CV_32FC1);

            //COMPUTE WARP MATRIX
            CvPoint2D32f srcTri[3], dstTri[3];
            srcTri[0].x = 0;          //src Top left
            srcTri[0].y = 0;
            srcTri[1].x = img1->width - 1;    //src Top right
            srcTri[1].y = 0;
            srcTri[2].x = 0;          //src Bottom left
            srcTri[2].y = img1->height - 1;
            //- - - - - - - - - - - - - - -//
            dstTri[0].x = img1->width*0.0;    //dst Top left
            dstTri[0].y = img1->height*0.33;
            dstTri[1].x = img1->width*0.85; //dst Top right
            dstTri[1].y = img1->height*0.25;
            dstTri[2].x = img1->width*0.15; //dst Bottom left
            dstTri[2].y = img1->height*0.7;

            cvGetAffineTransform( srcTri , dstTri , warp_mat);
            //cvWarpAffine( img1 , img2 , warp_mat );
            //cvCopy( img2 , img1 );

            //COMPUTE ROTATION MATRIX
            CvPoint2D32f center1 = cvPoint2D32f( img1->width/2, img1->height/2);
            //double angle = -50.0;
            double angle = -4.0;
            //double scale = 0.6;
            double scale = 3;

            //cv2DRotationMatrix( center1 , angle , scale , rot_mat );
            //cvWarpAffine( img1 , img2 , rot_mat );

            //img3 = cvCreateImage( cvSize(img1->width * scale, img1->height * scale ), 8, 3 );
            img3 = cvCreateImage( cvSize(img1->width * scale, img1->height * scale ), IPL_DEPTH_8U , 1 );
            //img3->origin = img1->origin;
            //cvZero( img3 );

            //CvPoint2D32f center2 = cvPoint2D32f( img3->width/2, img3->height/2);
            //center of src image req'd, manually tweaked re-centering
            CvPoint2D32f center2 = cvPoint2D32f( img1->width/2 - 32 , img1->height/2 - 20 );

            //DO THE TRANSFORM:
            cv2DRotationMatrix( center2 , angle , scale , rot_mat );
            cvWarpAffine( img1 , img3 , rot_mat );

            cvSaveImage( "ScaledAndRotated.jpg" , img3 );

            cvNamedWindow( win2 , CV_WINDOW_AUTOSIZE );
            cvShowImage( win2 , img2 );
            cvMoveWindow( win2, 0, 350 );

            cvNamedWindow( win3 , CV_WINDOW_AUTOSIZE );
            cvShowImage( win3 , img3 );
            cvMoveWindow( win3, 400, 350 );

            cvReleaseImage( &img1 );
            cvReleaseImage( &img2 );
            cvReleaseImage( &img3 );

            cvReleaseMat( &rot_mat );
            cvReleaseMat( &warp_mat );

            cvWaitKey();

        }


    cvDestroyWindow( win1 );
    cvDestroyWindow( win2 );
    cvDestroyWindow( win3 );

    return 0;

    }
    globfree( &pglob );
}
#endif
