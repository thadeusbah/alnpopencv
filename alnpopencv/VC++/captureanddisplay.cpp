// captureanddisplay.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "cv.h"
#include "highgui.h"
#include <math.h>
#include <stdio.h>


void f( 
  IplImage* src, 
  IplImage* dst 
) {
  CvMemStorage* storage = cvCreateMemStorage(0);
  CvSeq* comp = NULL;

  cvPyrSegmentation( src, dst, storage, &comp, 4, 200, 50 );
  int n_comp = comp->total;

  for( int i=0; i<n_comp; i++ ) {
    CvConnectedComp* cc = (CvConnectedComp*) cvGetSeqElem( comp, i );
    // do_something_with( cc );
  }
  cvReleaseMemStorage( &storage );
}

int main()
{

  // Create a named window with a the name of the file.
  cvNamedWindow("tui", 1 );
  // Load the image from the given file name.
  IplImage* src = cvLoadImage( "anhthoai.jpg",1 );
  IplImage* dst = cvCreateImage( cvGetSize(src), src->depth, src->nChannels);
  CvMemStorage* storage = cvCreateMemStorage(0);
  CvSeq* comp = NULL;

  cvPyrSegmentation( src, dst, storage, &comp, 4, 200, 50 );
  int n_comp = comp->total;

  for( int i=0; i<n_comp; i++ ) {
    CvConnectedComp* cc = (CvConnectedComp*) cvGetSeqElem( comp, i );
    // do_something_with( cc );
  }
  cvReleaseMemStorage( &storage );

  // Show the image in the named window
  cvShowImage( "tui", dst );

  // Idle until the user hits the "Esc" key.
  while( 1 ) { if( cvWaitKey( 100 ) == 27 ) break; }

  // Clean up and don’t be piggies
  cvDestroyWindow( "tui" );
  cvReleaseImage( &src );
  cvReleaseImage( &dst );

}
