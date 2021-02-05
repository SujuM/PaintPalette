using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaintPalette;
using System.Collections.Generic;
using System.Drawing;

namespace PaletteTest
{
    [TestClass]
    public class KmeansTest
    {
        private const string PATH = "C:\\Users\\suju1\\source\\repos\\PaintPalette\\PaletteTest\\rose.JPG";

        [TestMethod]
        public void TestNumberColors()
        {
            Kmeans kmeans = new Kmeans();

            List<Color> colors = Palette.GetColors(5, PATH, kmeans);

            Assert.AreEqual(5, colors.Count);

            colors = Palette.GetColors(7, PATH, kmeans);

            Assert.AreEqual(7, colors.Count);
        }
    }
}
