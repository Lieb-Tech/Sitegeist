using OpenQA.Selenium;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Sitegeist.Utils
{
    public class SnapshotCompare
    {
        public static Bitmap Difference(Screenshot snapshot0, Screenshot snapshot1)
        {
            Bitmap bmp0, bmp1;
            using (var ms = new MemoryStream(snapshot0.AsByteArray))
            {
                bmp0 = new Bitmap(ms);
            }
            using (var ms = new MemoryStream(snapshot1.AsByteArray))
            {
                bmp1 = new Bitmap(ms);
            }

            return getDifferencBitmap(bmp0, bmp1, Color.Red);
        }
        public static Bitmap getDifferencBitmap(Bitmap bmp1, Bitmap bmp2, Color diffColor)
        {
            Size s1 = bmp1.Size;
            Size s2 = bmp2.Size;
            if (s1 != s2) return null;

            Bitmap bmp3 = new Bitmap(s1.Width, s1.Height);

            for (int y = 0; y < s1.Height; y++)
                for (int x = 0; x < s1.Width; x++)
                {
                    Color c1 = bmp1.GetPixel(x, y);
                    Color c2 = bmp2.GetPixel(x, y);
                    if (c1 == c2) bmp3.SetPixel(x, y, c1);
                    else bmp3.SetPixel(x, y, diffColor);
                }
            return bmp3;
        }
    }
}
