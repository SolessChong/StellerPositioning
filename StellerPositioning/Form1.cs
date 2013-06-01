using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace StellerPositioning
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        #region Global Variables

        List<Star> brightStars;
        Image<Gray, Byte> canvas;
        int height = 3000,
            width = 3000;


        Camera camera = new Camera();


        #endregion

        public Form1()
        {
            InitializeComponent();

            AllocConsole();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Projection.scale = 5000;

            brightStars = (new Data()).GetBrightStars(10000);
            
            canvas = new Image<Gray, byte>(new Size(width, height));

            // point to the celestial north pole
            camera = new Camera();
            camera.scale = 500;

            camera.PointTo(new Star(0, 90, 0));

        }

        void FreshImage(float centerRA, float centerDec)
        {
            toolStripStatusLabel1.Text = "centerRA:"+centerRA.ToString() + ", centerDec:" + centerDec.ToString();

            canvas.Dispose();
            canvas = new Image<Gray, byte>(new Size(width, height));

            camera.PointTo(new Star(centerRA, centerDec, 0));

            for (int i = 0; i < 1000; i++)
            {
                var star = brightStars[i];
                drawStar(canvas, star.RA, star.Dec, centerRA, centerDec, width / 2, height / 2, new Gray(50));

            }

            drawStar(canvas, 11.005f, 61.75f, centerRA, centerDec, width / 2, height / 2, new Gray(255));
            drawStar(canvas, 11.0167f, 56.366f, centerRA, centerDec, width / 2, height / 2, new Gray(255));
            drawStar(canvas, 11.8835f, 53.683f, centerRA, centerDec, width / 2, height / 2, new Gray(255));
            drawStar(canvas, 12.25f, 57.0167f, centerRA, centerDec, width / 2, height / 2, new Gray(255));
            drawStar(canvas, 12.9f, 5595f, centerRA, centerDec, width / 2, height / 2, new Gray(255));
            drawStar(canvas, 13.383f, 54.916f, centerRA, centerDec, width / 2, height / 2, new Gray(255));
            drawStar(canvas, 13.783f, 49.3f, centerRA, centerDec, width / 2, height / 2, new Gray(255));

            imageBox1.Image = canvas;
        }

        void drawStar(Image<Gray, Byte> canvas, float RA, float Dec, float centerRA, float centerDec, int x0, int y0, Gray gray)
        {
            /*
            Star star = new Star(RA, Dec, 0);
            var centerPos = Projection.GetPos3(star, centerRA, centerDec);
            centerPos.X += x0; centerPos.Y += y0;
            canvas.Draw(new CircleF(centerPos, 5), gray, 2);
            */
            
            Star star = new Star(RA, Dec, 0);
            var pixelPos = camera.Project(star);
            pixelPos.X += x0; pixelPos.Y += y0;

            canvas.Draw(new CircleF(pixelPos, 5), gray, 2);
            
        }

        private void imageBox1_MouseMove(object sender, MouseEventArgs e)
        {

            float centerRA = (float)((float)e.X / imageBox1.Width) * 24f;
            float centerDec = (float)((float)e.Y / imageBox1.Height) * 90f;

            FreshImage( centerDec, centerRA);
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            FreshImage((float)trackBarRA.Value / 10, trackBarDec.Value);
        }
    }
}
