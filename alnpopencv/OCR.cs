using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;
using OpenCvSharp.Blob;

namespace OCRSharp
{
    class OCR
    {
        public IplImage src;
        public string OcrName = "gocr.exe";
        IplImage timg;
        IplImage pimg;
        CvBlobs blobs;
        public List<IplImage> plate = new List<IplImage>();
        public List<string> plateList = new List<string>();
        bool imageready = false;

        public bool LoadImage(string fname)
        {
            src = new IplImage(fname, LoadMode.Color);
            if (src != null)
                imageready = true;

            return imageready;
        }

        public void SetImage(IplImage tsrc)
        {
            if (src != null)
                Cv.ReleaseImage(src);
            src = tsrc;
        }

        public void PreProcess()
        {
            //Cv.NamedWindow("anhthoai", WindowMode.AutoSize);
            IplConvKernel element = Cv.CreateStructuringElementEx(21, 3, 10, 2, ElementShape.Rect, null);
            timg = new IplImage(src.Size, BitDepth.U8, 1);
            IplImage temp = timg.Clone();
            IplImage dest = timg.Clone();
            src.CvtColor(timg, ColorConversion.RgbaToGray);
            pimg = timg.Clone();
            //Cv.Threshold(pimg, pimg, 128, 255, ThresholdType.Binary | ThresholdType.Otsu);
            Cv.Smooth(timg, timg, SmoothType.Gaussian);
            Cv.MorphologyEx(timg, dest, temp, element, MorphologyOperation.TopHat, 1);

            Cv.Threshold(dest, timg, 180, 255, ThresholdType.Binary | ThresholdType.Otsu);
            //Cv.AdaptiveThreshold(dest, timg, 255, AdaptiveThresholdType.MeanC, ThresholdType.Binary,75, 0);
            Cv.Smooth(timg, dest, SmoothType.Median);
            Cv.Dilate(dest, dest, element, 2);

            /*using (CvWindow window = new CvWindow("BoundingRect", WindowMode.AutoSize))
            {
                window.Image = dest;
                CvWindow.WaitKey(0);
            }*/
            //Cv.ShowImage("anhthoai", dest);
            Cv.ReleaseImage(temp);
            Cv.ReleaseImage(dest);

        }

        public int FindPlates()
        {
            IplImage labelImg = new IplImage(src.Size, CvBlobLib.DepthLabel, 1);
            blobs = new CvBlobs();
            plate.Clear();
            CvBlobLib.Label(timg, labelImg, blobs);
            CvBlobLib.FilterByArea(blobs, 600, 3000);
            IplImage srctemp = src.Clone();
            CvBlobLib.RenderBlobs(labelImg, blobs, src, srctemp, RenderBlobsMode.BoundingBox | RenderBlobsMode.Angle);

            foreach (var item in blobs)
            {

                item.Value.SetImageROItoBlob(pimg);
                // ratio values of plate between 3.5 and 5.4 
                double ratio = (double)item.Value.Rect.Width / item.Value.Rect.Height;
                double angle = (double)item.Value.CalcAngle();
                //if (ratio > 3.5 && ratio < 5.4 && angle > -15 && angle < 15)
                if (ratio > 1 && ratio < 6 && angle > -15 && angle < 15)
                {
                    //                    IplImage platetemp = new IplImage(new CvSize(pimg.ROI.Width, pimg.ROI.Height), pimg.Depth, pimg.NChannels);
                    IplImage platetemp = new IplImage(new CvSize(140, 27), pimg.Depth, pimg.NChannels);
                    Cv.Resize(pimg, platetemp);
                    //                    Cv.Copy(pimg, platetemp);
                    plate.Add(platetemp);
                    src.Rectangle(item.Value.Rect, new CvScalar(0, 0, 255), 2, LineType.Link4);
                }
            }

            //            CvBlobLib.RenderBlobs(labelImg, blobs, src, src, RenderBlobsMode.BoundingBox);
            src.ResetROI();

            return plate.Count;

        }

        public void ReadPlates()
        {
            plateList.Clear();
            int i = 1;
            foreach (var plateimg in plate)
            {
                plateimg.SaveImage("tmp.pgm");
                plateList.Add(" #) " + RunOcr());
                i++;
            }

        }

        string RunOcr()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo(OcrName, "tmp.pgm");
            processStartInfo.UseShellExecute = false;
            processStartInfo.ErrorDialog = false;
            //            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            //            process.CreateNoWindow = true;
            bool processStarted = process.Start();

            StreamWriter inputWriter = process.StandardInput;
            StreamReader outputReader = process.StandardOutput;
            StreamReader errorReader = process.StandardError;

            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
            //            return (errorReader.ReadToEnd()+" "+ process.StandardOutput.ReadToEnd());        
        }
    }
}
