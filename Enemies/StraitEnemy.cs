using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    class StraitEnemy:Enemy
    {
        uint fireDelay = 70;
        uint lastFired = 0;
        

        public StraitEnemy(Vector2 position, Vector2 velocity)
            :base()
        {
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Enemy02"), position); //Temp
            this.Position = position;
            sprite.Rotation = 3.14159f;
            this.velocity = velocity;
            sprite.Color = Color.DarkRed; 
            //this.child = null;
            this.parent = null; 
            
        }

        public override void Update()
        {
            if (lastFired > fireDelay && !isDying)
            {
                lastFired = 0;
                ObjectManager.AddBullet(new StraitBullet(sprite.position, new Vector2(0, 10f)),false);
            }
            lastFired++;
            
            Position += velocity;
            base.Update();
        }

        public override void Draw(SpriteBatch sb) {
           
            sprite.Draw(sb);
   
        }
    }
}
