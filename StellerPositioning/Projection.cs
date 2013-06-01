using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace StellerPositioning
{
    class Projection
    {

        /**
         * Camera Parameters
         */
        public static float scale = 1;
        /// <summary>
        /// Field of View, degree.
        /// </summary>
        public static float FoV = 40f;

        /**
         * Coordinate Projections
         */

        /// <summary>
        /// Gnomonic Projection, NASA Version
        /// </summary>
        /// <param name="star">Target Star</param>
        /// <param name="centerRA">Camera Center Right Ascent</param>
        /// <param name="centerDec">Camera Center Declination</param>
        /// <returns></returns>
        public static PointF GetGnomonicPos1(Star star, float centerRA, float centerDec)
        {

            float Dec = star.Dec, RA = star.RA;

            /**
             * 
             * http://irsa.ipac.caltech.edu/IRASdocs/surveys/coordproj.html
             * 
             * scale:  number of pixels per degree in the map
             * alpha, delta:  Equatorial coordinates of a given position
             * alpha0, delta0:  Equatorial coordinates of the map center
             * A = cos(delta) x cos(alpha - alpha0)
             * F = scale x (180/pi)/[sin(delta0) x sin(delta) + A x cos(delta0)]
             * 
             */
            float ra2rad = (float)(12 / Math.PI);
            float deg2rad = (float)(180 / Math.PI);
            float delta = Dec / deg2rad, alpha = RA / ra2rad;
            float delta0 = centerDec / deg2rad, alpha0 = centerRA / ra2rad;

            float A = (float)(Math.Cos(delta) * Math.Cos(alpha - alpha0));
            float F = (float)(scale * (180 / Math.PI) / Math.Sin(delta0) * Math.Sin(delta) + A * Math.Cos(delta0));

            float line = -F * (float)(Math.Cos(delta0) * Math.Sin(delta) - A * Math.Sin(delta0));
            float sample = -F * (float)(Math.Cos(delta) * Math.Sin(alpha - alpha0));

            return new PointF(sample, line);
        }


        public static PointF GetGnomonicPos2(Star star, float centerRA, float centerDec)
        {

            float lambda = (float)(star.Dec / (180 / Math.PI)), phi = (float)(star.RA / (12 / Math.PI));
            float lambda0 = (float)(centerDec / (180 / Math.PI)), phi1 = (float)(centerRA / (12 / Math.PI));

            float cosc = (float)(Math.Sin(phi1) * Math.Sin(phi) + Math.Cos(phi1) * Math.Cos(phi) * Math.Cos(lambda - lambda0));
            float x = (float)(Math.Cos(phi) * Math.Sin(lambda - lambda0) / cosc);
            float y = (float)((Math.Cos(phi1) * Math.Sin(phi) - Math.Sin(phi1) * Math.Cos(phi) * Math.Cos(lambda - lambda0)) / cosc);

            x *= scale; y *= scale;

            return new PointF(x,y);
        }

        public static PointF GetPos3(Star star, float centerRA, float centerDec)
        {
            if ((star.Dec - centerDec) + FoV / 2 > 90)
                // out of view
                return new PointF(10000, 10000);

            float RA = (float)((star.RA - centerRA)/(12/Math.PI));
            float Dec = (float)((star.Dec - centerDec) / (180 / Math.PI));

            float x = (float)(Math.Cos(Dec) * Math.Cos(RA));
            float y = (float)(Math.Cos(Dec) * Math.Sin(RA));
            x *= scale;
            y *= scale;

            return new PointF(x, y);
        }
    }
}
