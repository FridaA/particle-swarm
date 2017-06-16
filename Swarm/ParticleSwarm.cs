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

        public ParticleSwarm(int numberOfParticles)
        {
            random = new Random(); //TODO: set interval here!
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

            //TODO: Initialize numberOfParticles particles in the swarm
            //      Give random initial position and velocity in interval [min, max]

            for (int i = 0; i < numberOfParticles; i++)
            {
                Particle p = new Particle(getRandomPosition(0, 1), getRandomVelocity(0, 1));
                p.BoundVelocity();
                Particles.Add(p);
            }
            //3 manual particles
            /*this.Particles.Add(
                new Particle(new Vector2(0.2f, 0.5f), new Vector2(0.2f, 0.1f)));
            this.Particles.Add(
                new Particle(new Vector2(0.2f, 0.2f), new Vector2(0.1f, 0.2f)));
            this.Particles.Add(
                new Particle(new Vector2(0.4f, 0.4f), new Vector2(-0.1f, -0.2f)));*/
        }

        private float getRandomPositionValue(float min, float max)
        {
            return (float) (min + random.NextDouble() * (max - min));
        }

        private Vector2 getRandomPosition(float min, float max)
        {
            return new Vector2(getRandomPositionValue(min, max), getRandomPositionValue(min, max));
        }

        private float getRandomVelocityValue(float min, float max)
        { 
            float alpha = 1f;

            float range = max - min;
            double value = alpha/Particle.timeStep * (random.NextDouble()*range - range/2);
            return (float)value;
        }

        private Vector2 getRandomVelocity(float min, float max)
        {
            return new Vector2(getRandomVelocityValue(min, max), getRandomVelocityValue(min, max));
        }
    }
}
