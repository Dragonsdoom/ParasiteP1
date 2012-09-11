using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ParasiteP1
{
    class OrbitEnemy:Enemy
    {
        float orbitRadius = 4;
        float angularVelocity = 0.15f;
        float angle = 0;
        uint fireDelay = 35;
        uint lastFired = 0;
        List<Bullet> bList = new List<Bullet>(); 
        public OrbitEnemy(Enemy parent, float angle, float orbitRadius)
            : base() 
        {
            this.parent = parent;
            this.angle = angle;
            this.orbitRadius = orbitRadius; 
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Ship01"), parent.Position); //Temp
            sprite.Rotation = 3.14159f; 
            this.Position = parent.Position;
            this.velocity = parent.velocity;
        }

        public override void Update()
        {
            if (isDying) return;
            if (lastFired > fireDelay)
            {
                lastFired = 0;
                ObjectManager.AddBullet(new SinBullet(sprite.position, new Vector2(0, 7f), 0f, null),false);
                ObjectManager.AddBullet(new SinBullet(sprite.position, new Vector2(0, 7f), MathHelper.Pi, null),false);
            }
            lastFired++;

            if (parent == null) {
                Position = new Vector2(Position.X, Position.Y - 1);
            }
            Position = parent.Position;

            float x = (float)Math.Cos(angle) * orbitRadius + Position.X;
            float y = (float)Math.Sin(angle) * orbitRadius + Position.Y;
            this.Position = new Vector2(x, y);
            angle += angularVelocity;
            base.Update();
        }

        public override void Draw(SpriteBatch sb) {
            if (isDying) return;
            sprite.Draw(sb);

        }
    }
}
