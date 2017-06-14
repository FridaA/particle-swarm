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

        public SwarmVisualizer(ParticleSwarm swarm)
        {
            this._swarm = swarm;

            InitializeComponent();

            timer = new Timer(); 
            timer.Tick += new EventHandler(this.timer_Tick);
            timer.Interval = 500;
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
                    toInterval(p.Position.X, 0, _Width), toInterval(p.Position.Y, 0, _Height));
                Rectangle rec = new Rectangle(pnt, p.Size);
                formGraphics.FillEllipse(brush, rec);
            }
            brush.Dispose();
            formGraphics.Dispose();
        }

        //TODO: Resize of window

        private void timer_Tick(object sender, EventArgs e)
        {
            _swarm.UpdateParticles();
            Invalidate();
        }

        private static int toInterval(float x, int min, int max)
        {
            //TODO: method should take old interval min, max and new interval min, max
            // from [oldMin, oldMax] to [newMin, newMax]
            //Right now: from [0,1] to [min, max]
            // (oldValue - oldMin)*newRange/oldRange + newMin
            return (int) (x * (max - min) + min);
        }
    }
}
