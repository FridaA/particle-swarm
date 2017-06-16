using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Swarm
{
    public partial class SwarmVisualizer : Form
    {
        private ParticleSwarm _swarm;
        private Timer timer;
        private float time;

        public SwarmVisualizer(ParticleSwarm swarm)
        {
            this._swarm = swarm;

            InitializeComponent();

            time = 0;

            timer = new Timer(); 
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Interval = 20;
            timer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            SolidBrush brush = new SolidBrush(Color.Black);
            Graphics formGraphics = this.CreateGraphics();

            foreach (Particle p in _swarm.Particles)
            {
                //TODO: if window is resized, keep drawing on the whole area
                Point pnt = new Point(
                    (int)toInterval(p.Position.X, _swarm.IntervalMin, _swarm.IntervalMax, 0, _Width), 
                    (int)toInterval(p.Position.Y, _swarm.IntervalMin, _swarm.IntervalMax, 0, _Height));
                Rectangle rec = new Rectangle(pnt, p.Size);
                formGraphics.FillEllipse(brush, rec);
            }
            brush.Dispose();
            formGraphics.Dispose();
        }

        //TODO: Resize of window

        private void timer_Tick(object sender, EventArgs e)
        {
            time += timer.Interval;
            _swarm.UpdateParticles();
            Invalidate();
        }

        private static float toInterval(float x, float min, float max)
        {
            return toInterval(x, 0, 1, min, max);
        }
        private static float toInterval(float x, float oldMin, float oldMax, float newMin, float newMax)
        { 
            float oldRange = oldMax - oldMin;
            float newRange = newMax - newMin;
            return (x - oldMin) * newRange / oldRange + newMin;
        }

        //TODO: Create control for configuration:
        //Set numberOfParticles
        //Set size of circle used for initialization of swarm

        //TODO: Replace local minima using a mouse click
    }
}
