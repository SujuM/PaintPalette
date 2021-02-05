using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace PaintPalette
{
    public class Kmeans : ColorExtractor
    {
        private int NumberIterations { get; } = 5;

        public Kmeans() { 
        
        }

        public Kmeans(int numberIterations)
        {
            NumberIterations = numberIterations;
        }

        public List<Color> ExtractColors(int numberColors, Bitmap bitmap)
        {
            ArrayList allColors = new ArrayList();

            //Step1: load pixel data
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    Color pixel = bitmap.GetPixel(i, j);
                    allColors.Add(pixel);
                }
            }

            //Step2: Get k number of random centroids from pixel data.
            int size = allColors.Count;

            Random randomNum = new Random();

            List<Color> centroids = new List<Color>();
            List<ArrayList> groups = new List<ArrayList>();

            for (int i = 0; i < numberColors; i++)
            {
                //Assign random index.
                centroids.Add((Color)allColors[randomNum.Next(0, allColors.Count)]);
                groups.Add(new ArrayList());
            }

            //Step3: For each pixel, calculate Euclidean distance to each centroid.
            //                  Assign each pixel to the closest group.
            int repetiton = 5;
            
            do
            {
                for (int i = 0; i < size; i++)
                {
                    Color current = (Color)allColors[i];
                    List<double> distances = new List<double>();

                    for (int j = 0; j < centroids.Count; j++) {
                        //Add a distance between each centroid and current color to the list.
                        distances.Add(GetDistance(centroids[j], current));
                    }

                    //Add current color to the groups[the index of minimum distance in distances].
                    groups[GetIndexOfMinimum(distances)].Add(current);
                }

                for (int i = 0; i < centroids.Count; i++)
                {
                    centroids[i] = UpdateCentroid(groups[i]);
                }

                repetiton--;

            } while (repetiton > 0);

            return centroids;
        }

        static int GetIndexOfMinimum(List<double> numbers) {
            double minimum = numbers[0];
            double current = numbers[0];

            for (int i = 0; i < numbers.Count; i++) {
                current = numbers[i];

                if (minimum > current) {
                    minimum = current;
                }
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                if (minimum.Equals(numbers[i])) {
                    return i;
                }
            }

            return 0;
        }

        static Color UpdateCentroid(ArrayList group)
        {
            int red = 0;
            int green = 0;
            int blue = 0;

            //Add up all colors in the group.
            for (int i = 0; i < group.Count; i++)
            {
                red += ((Color)group[i]).R;
                green += ((Color)group[i]).G;
                blue += ((Color)group[i]).B;
            }

            Color updatedCentre = Color.FromArgb(
                (int)Math.Round((double)(red / group.Count)),
                (int)Math.Round((double)(green / group.Count)),
                (int)Math.Round((double)(blue / group.Count))
            );


            return updatedCentre;
        }


        static double GetDistance(Color centroid, Color currentColor)
        {
            double square = 0;

            int distanceR = centroid.R - currentColor.R;
            int distanceG = centroid.G - currentColor.G;
            int distanceB = centroid.B - currentColor.B;

            square = Math.Pow(distanceR, 2) + Math.Pow(distanceG, 2) + Math.Pow(distanceB, 2);

            return Math.Sqrt(square);
        }

}
}
