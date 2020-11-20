using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UI.Models
{
    public static class HtmlHelperExtensions
    {
        private static string defaultImage = "/Contents/Image/white.jpg";
        private static string uploadsDirectory = "/Contents/Image/";
        public static string ImageBookOrDefault(int id, int numPic)
        {
            var imagePath = uploadsDirectory + +id + "_" + numPic + ".jpg";
            var imageSrc = File.Exists(HttpContext.Current.Server.MapPath(imagePath))
                               ? imagePath : defaultImage;

            return imageSrc;
        }

        public static int ConvertDecimaltoIntegerSalePrice(decimal khuyenmai, decimal giagoc)
        {
            int numIn;
            decimal numOut = 100 - khuyenmai * 100 / giagoc;
            numIn = Convert.ToInt32(numOut);
            return numIn;
        }
    }
}