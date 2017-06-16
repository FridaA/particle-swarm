using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;

namespace Swarm
{
    public static class ScalarField
    {
        private static float _a = 0.5f;
        private static float _b = 0.5f;
 
        //TODO: Implement more interesting scalar fields
        public static float Paraboloid(Vector2 p)
        {
            return Paraboloid(p, _a, _b, 1, 1);
        }
        public static float Paraboloid(Vector2 p, float a, float b)
        {
            return Paraboloid(p, a, b, 1, 1);
        }

        public static float Paraboloid(Vector2 p, float a, float b, float c, float d)
        {
            return (p.X - a) * (p.X - a)/c + (p.Y - b) * (p.Y - b)/d;
        }

        public static float Trig(Vector2 p)
        {
            return (float)(Math.Sin(0.3*p.X)*Math.Cos(0.3*p.Y));
        }

        public static float Test(Vector2 p, float time)
        {
            return 0;
        }

        public static float GoldsteinPrice(Vector2 p)
        {


            double x = Math.Pow(p.X + p.Y + 1, 2);
            double y = 19 - 14*p.X + 3*Math.Pow(p.X, 2) - 14*p.Y + 6*p.X*p.Y + 3*Math.Pow(p.Y, 2);
            double z = Math.Pow(2*p.X - 3*p.Y,2);
            double w = 18 - 32*p.X + 12*Math.Pow(p.X, 2) + 48*p.Y - 36*p.X*p.Y + 27*Math.Pow(p.Y,2);
            return (float)((1 + x * y) * (30 + z * w));
        }


    }
}
