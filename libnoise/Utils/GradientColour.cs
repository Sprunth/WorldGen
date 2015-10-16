using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace Noise.Utils
{
    public class GradientColour
    {
        public void AddGradientPoint(double point, Color colour)
        {
            int i = FindInsertionPoint(point);
            _points.Insert(i, new GradientPoint(point, colour));
        }

        private int FindInsertionPoint(double point)
        {
            int insertionPos;
            for (insertionPos = 0; insertionPos < _points.Count; insertionPos++)
            {
                if (point < _points[insertionPos].Point)
                {
                    // We found the array index in which to insert the new gradient point.
                    // Exit now.
                    break;
                }
                else if (point == _points[insertionPos].Point)
                {
                    // Each gradient point is required to contain a unique gradient
                    // position, so throw an exception.
                    throw new ArgumentException("Point already added");
                }
            }
            return insertionPos;
        }

        public Color GetColour(double point)
        {
            Debug.Assert(_points.Count > 2);

            // Find the first element in the gradient point array that has a gradient
            // position larger than the gradient position passed to this method.
            int indexPos;
            for (indexPos = 0; indexPos < _points.Count; indexPos++)
            {
                if (point < _points[indexPos].Point)
                {
                    break;
                }
            }

            // Find the two nearest gradient points so that we can perform linear
            // interpolation on the color.
            int index0 = MathsUtils.Clamp(indexPos - 1, 0, _points.Count - 1);
            int index1 = MathsUtils.Clamp(indexPos, 0, _points.Count - 1);

            // If some gradient points are missing (which occurs if the gradient
            // position passed to this method is greater than the largest gradient
            // position or less than the smallest gradient position in the array), get
            // the corresponding gradient color of the nearest gradient point and exit
            // now.
            if (index0 == index1)
            {
                return _points[index1].Colour;
            }

            // Compute the alpha value used for linear interpolation.
            double input0 = _points[index0].Point;
            double input1 = _points[index1].Point;
            double alpha = (point - input0) / (input1 - input0);


            // Now perform the linear interpolation given the alpha value.
            Color color0 = _points[index0].Colour;
            Color color1 = _points[index1].Colour;

            Color color2 = Color.FromArgb(
                (int)(Interpolation.LinearInterpolate((double)color0.R, (double)color1.R, alpha)),
                (int)(Interpolation.LinearInterpolate((double)color0.G, (double)color1.G, alpha)),
                (int)(Interpolation.LinearInterpolate((double)color0.B, (double)color1.B, alpha)));


            return color2;
        }


        List<GradientPoint> _points = new List<GradientPoint>();

        class GradientPoint
        {
            public GradientPoint(double point, Color colour)
            {
                Point = point;
                Colour = colour;
            }

            public double Point { get; set; }
            public Color Colour { get; set; }
        }
    }
}
