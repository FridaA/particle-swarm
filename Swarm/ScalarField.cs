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
        private static float a = 0.5f;
        private static float b = 0.5f;
 
        //TODO: Implement more interesting scalar fields
        public static float Paraboloid(Vector2 p)
        {
            return (p.X - a) * (p.X - a) + (p.Y - b) * (p.Y - b);
        }
    }
}
