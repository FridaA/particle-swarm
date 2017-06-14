using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenTK;

namespace Swarm
{
    public class Particle
    {
        public Vector2 Position { get; set; } //TODO: Make sure particles cannot move so they get out of sight?
        public Vector2 Velocity { get; set; } 
        public Vector2 BestPosition { get; set; }
        public float EvaluatedValue { get; set; } 

        private static int PARTICLE_SIZE = 3;
        private static float TIME_STEP = 0.1f;
        private static float MAX_VELOCITY = 0.3f;

        public Size Size
        {
            get 
            {
                return new Size(PARTICLE_SIZE, PARTICLE_SIZE);
            }
            set
            {
                if (value.GetType() == typeof(Int32))
                {
                    PARTICLE_SIZE = Convert.ToInt32(value);
                }
            }
        }

        public Particle(Vector2 pos, Vector2 vel)
        {
            this.Position = pos;
            this.Velocity = vel;
            this.BestPosition = new Vector2(float.MaxValue, float.MaxValue);
        }

        public void UpdatePosition()
        {
            float x = this.Position.X + this.Velocity.X * TIME_STEP;
            float y = this.Position.Y + this.Velocity.Y * TIME_STEP;

            this.Position = new Vector2(x, y);
        }

        public static float EvaluateParticle(Vector2 p)
        {
            return ScalarField.Paraboloid(p);
        }

        public void UpdateBestPosition()
        {
            if (this.EvaluatedValue < EvaluateParticle(this.BestPosition))
            {
                this.BestPosition = this.Position;
            }
        }

        public void UpdateVelocity(Vector2 bestInSwarm)
        { 
            float x = this.Velocity.X + (this.BestPosition.X - this.Position.X) / TIME_STEP + (bestInSwarm.X - this.Position.X) / TIME_STEP;
            float y = this.Velocity.Y + (this.BestPosition.Y - this.Position.Y) / TIME_STEP + (bestInSwarm.Y - this.Position.Y) / TIME_STEP;

            this.Velocity = new Vector2(x, y);
            BoundVelocity();
        }

        public void BoundVelocity()
        {
            if (this.Velocity.Length > MAX_VELOCITY)
            {
                this.Velocity = this.Velocity.Normalized() * MAX_VELOCITY;
            }
        }
    }
}
