using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace ParasiteP1
{
    public class Enemy:ICollidable
    {
        protected Vector2 position; 
        public Vector2 Position
        { 
            get 
            {
                return position; 
            } 
            set 
            { 
                position = value; sprite.position = value; 
            } 
        }
        public short OldGrid { get { return oldGrid; } set{ oldGrid=value;} }
        protected short oldGrid = -1;
        public bool team { get { return false; } set{;} }
        public Vector2 velocity;
        public bool IsDying = false;
        public bool isDying { get { return IsDying; } set { IsDying = value; } }
        public uint spawnCount;
        public float spawnTime;
        public int type;
        public int health;
        public Sprite sprite;
        public Enemy parent;
        
        
        //public Enemy child;
       
        
        
        /* public Bullet Fire() {
            bullet = new Bullet;
            return bullet; 
        }*/ 
        //public void KillChildren()
        //{
        //    if (isDying) return;
        //    isDying = true;
        //    child.KillChildren();
        //    child.Die();
        //    child = null;
        //    CollisionManager.DeleteMe(child);
        //}
        public void Die()
        {
            if (isDying) return;
            isDying = true;
           // KillChildren();
            CollisionManager.DeleteMe(this);
        }
        public virtual void Update() 
        {
            if (isDying) return;
            List<ICollidable> coll = CollisionManager.BadMoved(this);
            for (int i = 0; i < coll.Count; i++)
            {
                if (coll[i].team && (coll[i].Position - Position).LengthSquared()<64 && !coll[i].isDying)
                {
                    //Assign Points Here
                    coll[i].Die();
                    Die();
                }
            }
        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (isDying) return;
            
        }
    }
}
