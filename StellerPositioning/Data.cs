using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using Emgu.CV.Structure;
using LumenWorks.Framework.IO.Csv;

namespace StellerPositioning
{
    class Star
    {

        #region Variables

        /// <summary>
        /// Location and Magnitude
        /// </summary>
        public float    RA{ get; private set;}
        public float    Dec{ get; private set; }
        public float    Magnitude { get; private set; }

        /*
         * Convert to Rad
         */
        public float RA_rad
        {
            get { return (float)(RA / (12 / Math.PI)); }
        }
        public float Dec_rad
        {
            get { return (float)(Dec / (180 / Math.PI)); }
        }

        /// <summary>
        /// Virtual distance of star
        /// Set to positive inf.
        /// </summary>
        public const float Dist = 1e10f;

        /// <summary>
        /// Get Cartesian Coordinate of a star
        /// </summary>
        public MCvPoint3D32f Position
        {
            get
            {
                float   x = (float)(Math.Cos(Dec_rad) * Math.Cos(RA_rad)),
                        y = (float)(Math.Sin(Dec_rad)),
                        z = (float)-(Math.Cos(Dec_rad) * Math.Sin(RA_rad));

                return new MCvPoint3D32f(x * Dist, y * Dist, z * Dist);
            }
        }
    

        /// <summary>
        /// "scale" setter
        /// scale:  number of pixels per degree in the map
        /// Projection Parameters
        /// </summary>
        static public float scale { get; set; }

        // Harvard Revised Catalog ID
        public String HR_name { get; private set; }
        public String Proper_name { get; private set; }

        #endregion

        #region Constructors and Interface

        public Star(float ra, float dec, float magnitude, string hr_name = "", string proper_name = "")
        {
            RA = ra; Dec = dec; Magnitude = magnitude;

            HR_name = hr_name;
            Proper_name = proper_name;
        }


        public Star() { }

        
        override public String ToString()
        {
            return HR_name + "," + Proper_name + String.Format(" RA:{0},Dec:{1},mag:{2}", RA.ToString(), Dec.ToString(), Magnitude.ToString());
        }

        #endregion
    }


    class Data
    {
        static List<Star> starList;

        public Data()
        {
            using (CsvReader csv = new CsvReader(new StreamReader("data/hygxyz.csv"), true))
            {
                int fieldCount = csv.FieldCount;
                
                string[] headers = csv.GetFieldHeaders();

                starList = new List<Star>();
                Console.WriteLine(headers.ToString());
                while (csv.ReadNextRecord())
                {
                    float   ra  = float.Parse(csv[7]),
                            dec = float.Parse(csv[8]),
                            mag = float.Parse(csv[13]);
                    // Harvad Revised Catalog ID
                    string hr_name = csv[3] + ",";
                    string proper_name = csv[5] + "," + csv[6];

                    starList.Add(new Star(ra, dec, mag, hr_name, proper_name));
                }
            }
        }

        public List<Star> GetBrightStars(int cnt)
        {
            var starListMag = starList;    
            
                // sort by Magnitude
            starListMag.Sort((x,y) => x.Magnitude.CompareTo(y.Magnitude));

            return starListMag.GetRange(0, cnt);

        }

    }
}
