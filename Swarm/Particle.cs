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

        private int particleSize = 3;
        public static float timeStep = 0.04f;
        private float maxVelocity = 0.1f;

        public Size Size
        {
            get 
            {
                return new Size(particleSize, particleSize);
            }
            set
            {
                if (value.GetType() == typeof(Int32))
                {
                    particleSize = Convert.ToInt32(value);
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
            float x = this.Position.X + this.Velocity.X * timeStep;
            float y = this.Position.Y + this.Velocity.Y * timeStep;

            this.Position = new Vector2(x, y);
        }

        public static float EvaluateParticle(Vector2 p)
        {
            return ScalarField.Trig(p);
        }

        public static float EvaluateParticle(Vector2 p, float time)
        {
            return ScalarField.Test(p, time);
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
            //Vill egentligen inte hitta optima utan hänga ihop bara
            //BEräkna medelpunkt hos svärmen
            //vilken generell riktning har de andra?
            //kollisionskontroll
            float x = this.Velocity.X + (this.BestPosition.X - this.Position.X) / timeStep + (bestInSwarm.X - this.Position.X) / timeStep;
            float y = this.Velocity.Y + (this.BestPosition.Y - this.Position.Y) / timeStep + (bestInSwarm.Y - this.Position.Y) / timeStep;

            this.Velocity = new Vector2(x, y);
            BoundVelocity();
        }

        public void BoundVelocity()
        {
            if (this.Velocity.Length > maxVelocity)
            {
                this.Velocity = this.Velocity.Normalized() * maxVelocity;
            }
        }
    }
}
