using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    public class SprayBullet : Bullet
    {
        public SprayBullet(Vector2 position, Vector2 velocity,uint spawnCount,Bullet parent)
            : base()
        {
            constructor(position, velocity, spawnCount,parent);
        } 
        
        public SprayBullet(Vector2 position, Vector2 velocity): base()
        {
            constructor(position, velocity, 1,this);
        }
        
        private void constructor(Vector2 position, Vector2 velocity, uint spawnCount,Bullet parent)
        {
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Shape01"), position); //Temp
            sprite.CenterOriginOnTexture();
            sprite.Scale = .50f;
            sprite.Color = Color.Aquamarine;
            this.spawnCount = spawnCount - 1;
            this.Position = position;
            this.velocity = velocity;
            this.parent = parent;

            while (spawnCount > 0 && this.spawnCount>0)
            {
                Vector2 newVelocity = new Vector2(velocity.Y / (float)spawnCount / 3f, velocity.Y);
                    
                    //new Vector2( (float)Math.Cos(angle),-1* (float)Math.Sin(angle));
                ObjectManager.AddBullet(new SprayBullet(position + newVelocity, newVelocity, 1, this),true);
                newVelocity.X *= -1;

                    //new Vector2( (float)Math.Cos(angle),-1* (float)Math.Sin(angle));
                ObjectManager.AddBullet(new SprayBullet(position + newVelocity, newVelocity, 1, this),true);
                spawnCount--;
            }
        }
       
        public override void Update()
        {
            Position += velocity;
            base.Update();
        }
    }
}
