using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.Util;
using System.Windows.Forms;
using Emgu.CV.UI;
using System.Drawing;

namespace StellerPositioning
{
    class Recognition
    {
        public Form1 form;

        public void ListStart(Image<Gray, Byte> img)
        {
            var bin = img.ThresholdAdaptive(new Gray(255), Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY, 11, new Gray(0));

            form.imageBox1.Image = bin;

            // flood fill
            form.listBox1.Items.Clear();
            var tmp = bin.Clone();
            var mask =new Image<Gray, Byte>(tmp.Width + 2, tmp.Height + 2);
            for ( int i = 0; i < tmp.Width; i ++ )
                for ( int j = 0; j < tmp.Height; j ++ )
                    if (tmp.Data[j, i, 0] == 255)
                    {
                        MCvConnectedComp comp = new MCvConnectedComp();
                        var seed = new Point(i, j);
                        CvInvoke.cvFloodFill(tmp, seed, new MCvScalar(0),
                            new MCvScalar(0), new MCvScalar(0), out comp,
                            Emgu.CV.CvEnum.CONNECTIVITY.EIGHT_CONNECTED,
                            Emgu.CV.CvEnum.FLOODFILL_FLAG.DEFAULT, mask.Ptr);
                        if (comp.area < 50)
                        {
                        }
                    }

            form.imageBox1.Image = tmp;
        }

        public void Match(Image<Gray, Byte> img1, Image<Gray, Byte> img2)
        {
            img2.ThresholdAdaptive(new Gray(255), Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY, 11, new Gray(0));

            double min = 999;

            for (int dx = -img2.Width / 2; dx <= img1.Width - img2.Width / 2; dx++)
                for (int dy = -img2.Height / 2; dy <= img1.Height - img2.Height / 2; dy++)
                    for (float r = 0; r < 2 * Math.PI; r += 0.1f)
                    {
                        var img2r = img2.Rotate(r, new Gray(0));
                        img1.ROI = new Rectangle(Math.Max(0, dx), Math.Max(0, dy),
                            (Math.Min(dx + img2r.Width, img1.Width) - Math.Max(dx, 0)),
                            (Math.Min(dy + img2r.Height, img2.Height) - Math.Max(dy, 0))
                            );
                        img2r.ROI = new Rectangle(Math.Max(0, -dx), Math.Max(0, -dy),
                            (Math.Min(dx + img2r.Width, img1.Width) - Math.Max(dx, 0)),
                            (Math.Min(dy + img2r.Height, img2.Height) - Math.Max(dy, 0))
                            );

                        var diff = img1.AbsDiff(img2r);
                        var avg = diff.GetAverage();

                        if (avg.Intensity < min)
                        {
                            form.imageBox1.Image = img2r;
                            min = avg.Intensity;
                        }

                        img1.ROI = new Rectangle();
                        img2.ROI = new Rectangle();
                    }
        }
    }
}
