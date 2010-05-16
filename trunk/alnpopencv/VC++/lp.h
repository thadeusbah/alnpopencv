#ifndef LP_H
#define LP_H
/* defines */

/* colours used by ssocr */
#define LP_BLACK 0
#define LP_WHITE 255

/* states */
#define FIND_DARK 0
#define FIND_LIGHT 1

/* boarder between dark and light */
#define MAXRGB 255 /* maximum RGB component value */
#define THRESHOLD 50.0
#define DARK 0
#define LIGHT 1
#define UNKNOWN 2

/* segments
 *
 *  1     -
 * 2 3   | |
 *  4     -
 * 5 6   | |
 *  7     -
 *
 *  */
#define HORIZ_UP 1
#define VERT_LEFT_UP 2
#define VERT_RIGHT_UP 4
#define HORIZ_MID 8
#define VERT_LEFT_DOWN 16
#define VERT_RIGHT_DOWN 32
#define HORIZ_DOWN 64
#define ALL_SEGS 127

/* digits */
#define D_ZERO (ALL_SEGS & ~HORIZ_MID)
#define D_ONE (VERT_RIGHT_UP | VERT_RIGHT_DOWN)
#define D_TWO (ALL_SEGS & ~(VERT_LEFT_UP | VERT_RIGHT_DOWN))
#define D_THREE (ALL_SEGS & ~(VERT_LEFT_UP | VERT_LEFT_DOWN))
#define D_FOUR (ALL_SEGS & ~(HORIZ_UP | VERT_LEFT_DOWN | HORIZ_DOWN))
#define D_FIVE (ALL_SEGS & ~(VERT_RIGHT_UP | VERT_LEFT_DOWN))
#define D_SIX (ALL_SEGS & ~VERT_RIGHT_UP)
#define D_SEVEN (HORIZ_UP | VERT_RIGHT_UP | VERT_RIGHT_DOWN)
#define D_EIGHT ALL_SEGS
#define D_NINE (ALL_SEGS & ~VERT_LEFT_DOWN)
#define D_UNKNOWN 0

#define NUMBER_OF_DIGITS 6 /* in this special case */
#define SPACES 1 /* for segment_display string */

/* to find segment need # of pixels */
#define NEED_PIXELS 1

/* ignore # of pixels when checking a column fo black or white */
#define IGNORE_PIXELS 0

#define DEBUG_IMAGE_NAME "car.bmp"

/* doubles are assumed equal when they differ less than EPSILON */
#define EPSILON 0.0000001

/* types */

typedef struct {
  int x1,y1,x2,y2,digit;
} digit_struct;

typedef enum channel_e {
  CHAN_ALL,
  CHAN_RED,
  CHAN_GREEN,
  CHAN_BLUE
} channel_t;

typedef enum fg_bg_e {
  FG,
  BG
} fg_bg_t;

typedef enum luminance_e {
  REC601,
  REC709,
  LINEAR,
  MINIMUM,
  MAXIMUM,
  RED,
  GREEN,
  BLUE
} luminance_t;

#define DEFAULT_LUM_FORMULA REC709 /* default luminance formula */

/* functions */

/* print usage */
void usage(char *name, FILE *f);

/* print help for luminance functions */
void print_lum_help(void);

/* print version */
void print_version(FILE *f);

/* parse luminance keyword */
luminance_t parse_lum(char *keyword);

/* print luminance keyword */
void print_lum_key(luminance_t lt, FILE *f);

/* set foreground color */
void set_fg_color(int color);

/* set background color */
void set_bg_color(int color);

/* set imlib color */
void ssocr_set_imlib_color(fg_bg_t color);

/* check if a pixel is set regarding current foreground/background colors */
int is_pixel_set(int value, double threshold);

/* set pixel if at least mask neighboring pixels (including the pixel to be set)
 * are set
 * a pixel is set if its luminance value is less than thresh
 * mask=1 is the standard dilation filter
 * mask=9 is the standard erosion filter */
IplImage set_pixels_filter(IplImage *source_image, double thresh,
                              luminance_t lt, int mask);

/* shortcut for dilation */
IplImage dilation(IplImage *source_image, double thresh, luminance_t lt);

/* shortcut for erosion */
IplImage erosion(IplImage *source_image, double thresh, luminance_t lt);

/* shortcut for closing */
IplImage closing(IplImage *source_image, double thresh, luminance_t lt,
                    int n);

/* shortcut for opening */
IplImage opening(IplImage *source_image, double thresh, luminance_t lt,
                    int n);

/* keep only pixels that have at least mask-1 neighbors set */
IplImage keep_pixels_filter(IplImage *source_image, double thresh,
                               luminance_t lt, int mask);

/* remove isolated pixels (shortcut for keep_pixels_filter with mask = 2) */
IplImage remove_isolated(IplImage *source_image, double thresh,
                            luminance_t lt);

/* grey stretching, i.e. lum<t1 => lum=0, lum>t2 => lum=100,
 * else lum=((lum-t1)*MAXRGB)/(t2-t1) */
IplImage grey_stretch(IplImage *source_image, double t1, double t2,
                         luminance_t lt);

/* use dynamic (aka adaptive) thresholding to create monochrome image */
IplImage dynamic_threshold(IplImage *source_image, double t,
                              luminance_t lt ,int ww, int wh);

/* make black and white */
IplImage make_mono(IplImage *source_image, double thresh, luminance_t lt);

/* set pixel to black (0,0,0) if R<T or G<T or R<T, T=thresh/100*255 */
IplImage rgb_threshold(IplImage *source_image, double thresh,
                          channel_t channel);

/* set pixel to black (0,0,0) if R<T, T=thresh/100*255 */
IplImage r_threshold(IplImage *source_image, double thresh);

/* set pixel to black (0,0,0) if G<T, T=thresh/100*255 */
IplImage g_threshold(IplImage *source_image, double thresh);

/* set pixel to black (0,0,0) if B<T, T=thresh/100*255 */
IplImage b_threshold(IplImage *source_image, double thresh);

/* make the border white */
IplImage white_border(IplImage *source_image, int width);

/* invert image */
IplImage invert(IplImage *source_image, double thresh, luminance_t lt);

/* shear the image
 * the top line is unchanged
 * the bottom line is moved offset pixels to the right
 * the other lines are moved yPos*offset/(height-1) pixels to the right
 * white pixels are inserted at the left side */
IplImage shear(IplImage *source_image, int offset);

/* rotate the image */
IplImage rotate(IplImage *source_image, double theta);

/* turn image to greyscale */
IplImage greyscale(IplImage *source_image, luminance_t lt);

/* crop image */
IplImage crop(IplImage *source_image, int x, int y, int w, int h);

/* adapt threshold to image values values */
double adapt_threshold(IplImage *image, double thresh, luminance_t lt, int x,
                       int y, int w, int h, int absolute_threshold,
                       int iterative_threshold, int verbose, int debug_output);

/* compute dynamic threshold value from the rectangle (x,y),(x+w,y+h) of
 * source_image */
double get_threshold(IplImage *source_image, double fraction, luminance_t lt,
                     int x, int y, int w, int h);

/* determine threshold by an iterative method */
double iterative_threshold(IplImage *source_image, double thresh,
                           luminance_t lt, int x, int y, int w, int h);

/* get minimum grey value */
double get_minval(IplImage *source_image, int x, int y, int w, int h,
                 luminance_t lt);

/* get maximum grey value */
double get_maxval(IplImage *source_image, int x, int y, int w, int h,
                 luminance_t lt);

int color;
/* compute luminance from RGB values */
//int get_lum(color, luminance_t lt);

/* compute luminance Y_709 from linear RGB values */
int get_lum_709(color);

/* compute luminance Y_601 from gamma corrected (non-linear) RGB values */
int get_lum_601(color);

/* compute luminance Y = (R+G+B)/3 */
int get_lum_lin(color);

/* compute luminance Y = min(R,G,B) as used in GNU Ocrad 0.14 */
int get_lum_min(color);

/* compute luminance Y = max(R,G,B) */
int get_lum_max(color);

/* compute luminance Y = R */
int get_lum_red(color);

/* compute luminance Y = G */
int get_lum_green(color);

/* compute luminance Y = B */
int get_lum_blue(color);

/* clip value thus that it is in the given interval [min,max] */
int clip(int value, int min, int max);

/* print image info */
void print_info(IplImage *source_image);


#endif LP_H