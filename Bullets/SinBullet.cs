using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    class SinBullet : Bullet
    {
        float orbitRadius = 15;
        float angularVelocity = 0.25f;
        float angle = 0;
        
        public SinBullet(Vector2 position, Vector2 velocity, float angle, Bullet parent)
            : base()
        {
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Shape01"), position); //Temp
            sprite.CenterOriginOnTexture();
            this.angle = angle;
            sprite.Scale = .5f;
            sprite.Color = Color.Chartreuse;
            this.Position = position;
            this.velocity = velocity;
            
        }

        public override void Update()
        {
            float x = (float)Math.Cos(angle) * orbitRadius + Position.X +1f;
            float y = velocity.Y + Position.Y;
            this.Position = new Vector2(x, y);
            angle += angularVelocity;
        }

        public override void Draw(SpriteBatch sb)
        {
            sprite.Draw(sb);
        }
    }
}
