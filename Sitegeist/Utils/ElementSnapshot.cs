using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System.IO;

namespace Sitegeist.Utils
{
    /// <summary>
    /// Get a snapshot of an element on the page
    /// </summary>
    public class ElementSnapshot
    {
        /// <summary>
        /// Get a snapshot the page in it's current state
        /// </summary>
        /// <param name="webDriver">Selenium driver</param>
        /// <param name="element">Which element should be the focus of the snapshot</param>
        /// <param name="path">Path to save snapshot to</param>
        public void GenerateBitmap(ChromeDriver webDriver, IWebElement element, string path)
        {
            var bmp = GetCroppedBitmap(webDriver, element);
            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        /// <summary>
        /// Generate the bitmap, and crop it
        /// </summary>
        /// <param name="webDriver">Selenium driver</param>
        /// <param name="element">Which element should be the focus of the snapshot</param>
        /// <returns></returns>
        public Bitmap GetCroppedBitmap(ChromeDriver webDriver, IWebElement element)
        {
            var shot = webDriver.GetScreenshot();
            using (var mem = new MemoryStream(shot.AsByteArray))
            {
                var bmp = new Bitmap(mem);
                return CropAtRect(bmp,
                    new Rectangle(element.Location.X - element.Size.Width / 2 - 10,
                    element.Location.Y - element.Size.Height / 2 - 10,
                    element.Size.Width + 20, element.Size.Height + 20));
            }
        }

        /// <summary>
        /// Crop a bitmap 
        /// </summary>
        /// <param name="bmp">Bitmap to be processed</param>
        /// <param name="rect">Area to crop</param>
        /// <returns></returns>
        public Bitmap CropAtRect(Bitmap bmp, Rectangle rect)
        {
            Bitmap nb = new Bitmap(r.Width, r.Height);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(bmp, -r.X, -r.Y);
                return nb;
            }
        }
    }
}
