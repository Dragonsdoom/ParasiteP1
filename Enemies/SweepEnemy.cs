using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    class SweepEnemy : Enemy
    {
        public bool reverse = false;
        uint fireDelay = 100;
        uint lastFired = 0;
        //List<Bullet> bList = new List<Bullet>(); Moved to Static
        public new SweepEnemy parent; 
        public SweepEnemy(Vector2 position, Vector2 velocity, SweepEnemy parent1)
            : base()
        {
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Enemy01"), position); //Temp
            sprite.Rotation = 3.14159f;
            //sprite.Color = Color.DarkOrange; 
            this.Position = position;
            this.velocity = velocity;
            //this.child = this;
            parent = parent1;
        }

        public override void Update()
        {
            if (isDying)
            {
                //this.child
                return;
            }

            if (lastFired > fireDelay && !isDying)
            {
                lastFired = 0;
                ObjectManager.AddBullet(new SprayBullet(sprite.position + new Vector2(0, 18f), new Vector2(0, 4f), 2, null),false);
            }
            lastFired++;

           
            reverse = (this.Position.Y > 250); 
            if (this.parent != null)
            {
                reverse = (parent.reverse || reverse);
            }
            else if (reverse)
            {
                this.velocity.Y = this.velocity.Y - .04f;
            }
         
            Position += velocity;
            base.Update(); 

        }

        public override void Draw(SpriteBatch sb)
        {
            if (isDying) return;
            
            sprite.Draw(sb);

        }
    }
}
