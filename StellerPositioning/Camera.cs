using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV.Structure;
using Emgu.Util;
using Emgu.CV;
using Emgu.CV.Util;
using System.Drawing;



namespace StellerPositioning
{
    class Camera
    {
        #region Variables and Properties

        /// <summary>
        /// Specify the pose of camera with three vectors
        /// and the location of the camera remains at the origin in this application.
        /// </summary>
        private MCvPoint3D32f u, v, n;
        public MCvPoint3D32f U
        {
            get { return u; }
            set { u = value; homographMatrix = null; }
        }
        public MCvPoint3D32f V
        {
            get { return v; }
            set { v = value; homographMatrix = null; }
        }
        public MCvPoint3D32f N
        {
            get { return n; }
            set { n = value; homographMatrix = null; }
        }

        /// <summary>
        /// Focal length
        /// </summary>
        public float focalLength = 1;

        public float scale = 1;

        HomographyMatrix homographMatrix = null;

        /// <summary>
        /// Attribute homographMatrix, used in projection.
        /// </summary>
        public HomographyMatrix HomographMatrix {
            get{
                if (homographMatrix == null)
                {
                    homographMatrix = new HomographyMatrix();
                    homographMatrix[0, 0] = U.x; homographMatrix[0, 1] = U.y; homographMatrix[0, 2] = U.z;
                    homographMatrix[1, 0] = V.x; homographMatrix[1, 1] = V.y; homographMatrix[1, 2] = V.z;
                    homographMatrix[2, 0] = N.x; homographMatrix[2, 1] = N.y; homographMatrix[2, 2] = N.z;
                }

                return homographMatrix;
            }
        }

        #endregion

        #region Utilities

        public void PointTo(Star star)
        {
            /*
             * v: up
             * u: right
             * n: back
             */
            if (star.Dec > 90 - 1e-9)
            {
                N = new MCvPoint3D32f(0, -1, 0);
                V = new MCvPoint3D32f(0, 0, 1);
                U = new MCvPoint3D32f(1, 0, 0);

                return; 
            }
            var starPos = star.Position.GetNormalizedPoint();
            starPos.x = -starPos.x; starPos.y = -starPos.y; starPos.z = -starPos.z;
            N = starPos;

            var up = new MCvPoint3D32f(0, 1, 0);
            U = up.CrossProduct(n).GetNormalizedPoint();
            V = N.CrossProduct(u).GetNormalizedPoint();   

        }

        /// <summary>
        /// Project the point according to camera position.
        /// </summary>
        /// <param name="p">Point position, in WC</param>
        /// <returns>pixel position</returns>
        public PointF Project(MCvPoint3D32f p)
        {
            //return new PointF(p.x, p.y);
            // Extend p
            Matrix<double> p_ex = new Matrix<double>(3, 1);
            p_ex[0, 0] = p.x;
            p_ex[1, 0] = p.y;
            p_ex[2, 0] = p.z;
            
            var rst = HomographMatrix * p_ex;
            rst[0, 0] = rst[0,0] / rst[2, 0] * scale; 
            rst[1, 0] = rst[1,0] / rst[2, 0] * scale;

            return new PointF((float)rst[0, 0], (float)rst[1, 0]);
        }

        /// <summary>
        /// Project a star
        /// </summary>
        /// <param name="star">the star</param>
        /// <returns>pixel position</returns>
        public PointF Project(Star star)
        {
            var starPos = star.Position.GetNormalizedPoint();

            return Project(starPos);
        }

        #endregion
    }
}
