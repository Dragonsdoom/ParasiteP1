using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    public class OrbitBullet : Bullet
    {
        float orbitRadius = 80;
        float angularVelocity = 0.15f;
        float angle = 0;
        public OrbitBullet(Vector2 position, Vector2 velocity, float angle, Bullet parent)
            : base()
        {
            this.parent = parent;
            this.angle = angle;
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Shape01"), parent.Position); //Temp
            sprite.CenterOriginOnTexture();
            sprite.Scale = .5f;
            this.Position = parent.Position;
            this.velocity = parent.velocity;
        }

        public override void Update()
        {
            Position = parent.Position;
            
            float x = (float)Math.Cos(angle) * orbitRadius + Position.X;
            float y = (float)Math.Sin(angle) * orbitRadius + Position.Y;
            this.Position = new Vector2(x, y);
            angle += angularVelocity;
            if (orbitRadius < 250)
            {
                orbitRadius += 1;
            }
            base.Update();
        }
    }
}
