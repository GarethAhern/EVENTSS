using System.Drawing;

namespace EventsAndInvitesLogic
{
    public static class ButtonDisplayLogic
    {
        /// <summary>
        /// Use to darken BackgroundImage so it is clear that button is disabled
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static Bitmap DarkenBitMap(Bitmap original)
        {
            Rectangle r = new Rectangle(0, 0, original.Width, original.Height);
            int alpha = 80;
            using (Graphics g = Graphics.FromImage(original))
            {
                using (Brush cloud_brush = new SolidBrush(Color.FromArgb(alpha, Color.Black)))
                {
                    g.FillRectangle(cloud_brush, r);
                }
            }
            return original;
        }
        /// <summary>
        /// Alternative way to darken BackgroundImage so it is clear that button is disabled
        /// function found on http://www.switchonthecode.com/tutorials/csharp-tutorial-convert-a-color-image-to-grayscale
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        private static Bitmap MakeGrayscale(Bitmap original)
        {
            //make an empty bitmap the same size as original
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    //get the pixel from the original image
                    Color originalColor = original.GetPixel(i, j);

                    //create the grayscale version of the pixel
                    int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .3)
                        + (originalColor.B * .3));



                    //int grayScale = (int)((originalColor.R * .3) + (originalColor.G * .59)
                    //    + (originalColor.B * .11));

                    //create the color object
                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    //set the new image's pixel to the grayscale version
                    newBitmap.SetPixel(i, j, newColor);
                }
            }

            return newBitmap;
        }
    }
}
