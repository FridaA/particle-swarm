using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using OpenTK;

namespace Swarm
{
    public class ParticleSwarm
    {
        public List<Particle> Particles;

        public Vector2 BestInSwarm = new Vector2(float.MaxValue, float.MaxValue);

        private Random random;

        public float IntervalMax { get; set; }
        public float IntervalMin { get; set; }

        public ParticleSwarm(int numberOfParticles)
        {
            random = new Random(); 
            IntervalMin = 0f;
            IntervalMax = 1f;
            initializeSwarm(numberOfParticles);
        }

        public ParticleSwarm(int numberOfParticles, float min, float max)
        {
            random = new Random();
            IntervalMin = min;
            IntervalMax = max;
            initializeSwarm(numberOfParticles);
        }

        public void UpdateParticles()
        {
            foreach (Particle p in this.Particles)
            {
                p.EvaluatedValue = Particle.EvaluateParticle(p.Position);
                p.UpdateBestPosition();
                UpdateBestInSwarm(p);
                p.UpdateVelocity(this.BestInSwarm);
                p.UpdatePosition();
            }
        }

        public void UpdateParticles(float t)
        {
            foreach (Particle p in this.Particles)
            {
                p.EvaluatedValue = Particle.EvaluateParticle(p.Position, t);
                p.UpdateBestPosition();
                UpdateBestInSwarm(p);
                p.UpdateVelocity(this.BestInSwarm);
                p.UpdatePosition();
            }
        }

        private void UpdateBestInSwarm(Particle p)
        {
            if (p.EvaluatedValue < Particle.EvaluateParticle(this.BestInSwarm))
            {
                this.BestInSwarm = p.Position;
            }
        }

        private void initializeSwarm(int numberOfParticles)
        {
            Particles = new List<Particle>();

            for (int i = 0; i < numberOfParticles; i++)
            {
                //TODO: Place particles within a circle around a center point (a,b)
                //TODO: Get point (a,b) from mouse click in windows form
                Particle p = new Particle(getRandomPosition(), getRandomVelocity());
                p.BoundVelocity();
                Particles.Add(p);
            }
        }

        private float getRandomPositionValue()
        {
            return (float) (IntervalMin + random.NextDouble() * (IntervalMax - IntervalMin));
        }

        private Vector2 getRandomPosition()
        {
            return new Vector2(getRandomPositionValue(), getRandomPositionValue());
        }

        private float getRandomVelocityValue()
        { 
            float alpha = 1f;

            float range = IntervalMax - IntervalMin;
            double value = alpha/Particle.timeStep * (random.NextDouble()*range - range/2);
            return (float)value;
        }

        private Vector2 getRandomVelocity()
        {
            return new Vector2(getRandomVelocityValue(), getRandomVelocityValue());
        }
    }
}
