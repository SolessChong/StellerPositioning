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
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace StellerPositioning
{
    class Overlay
    {
        public void Process()
        {
            var fns = Directory.GetFiles("data/overlay");
            foreach( var fn in fns){
                Image<Bgr, Byte> img = new Image<Bgr,byte>(fn);

                for (float r = -0.2f; r < 0.2; r += 0.005f)
                {
                    for (int dx = -10; dx <= 10; dx++)
                    {
                        var img1 = img.Rotate(r, new Bgr(255, 255, 255));
                    }
                }
            }
        }



    }
}
