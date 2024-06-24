using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ScottplotLayer
{
    public class Scatter
    {
        public Scatter(double[] x, double[] y, string[] titles)
        {
            X = x;
            Y = y;
            Titles = titles;
            Color = Color.Aqua;
        }

        public Scatter(double[] x, double[] y, string[] titles, Color color)
        {
            X = x;
            Y = y;
            Titles = titles;
            Color = color;
        }

        public double[] X { get; }
        public double[] Y { get; }
        public string[] Titles { get; }
        public Color Color { get; }
    }
}
