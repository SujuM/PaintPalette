using System.Collections.Generic;
using System.Drawing;

namespace PaintPalette
{
    public interface ColorExtractor
    {
        List<Color> ExtractColors(int numberColors, Bitmap bitmap);
    }

    public class Palette
    {

        public static List<Color> GetColors(int numColors, string fileName, ColorExtractor extractor)
        {
            // Create a Bitmap object from an image file.
            //https://stackoverflow.com/questions/28161900/relative-path-when-creating-bitmap
            try
            {
                Bitmap myBitmap = new Bitmap(fileName);
                
                return extractor.ExtractColors(numColors, myBitmap);
            }
            catch (System.ArgumentException e)
            {
                throw new System.Exception("File was not found or could not be read into a bitmap.");
            }

        }
    }
}