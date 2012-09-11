using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
//using System.Diagnostics;

namespace ParasiteP1
{
    public enum DamageType {None,Water,Fire,Electrical}

    public class Bullet:ICollidable
    {
        public static int Count=0;
        public List<Bullet> Children = new List<Bullet>();
        Vector2 position;
        public short OldGrid { get { return oldGrid; } set { oldGrid = value; } }
        protected short oldGrid = -1;
        bool privateTeam = false;
        public bool team { get { return privateTeam; } set { privateTeam = value; } }
        public Vector2 Position { get { return position; } set { position = value; sprite.position = value; } }
        public Vector2 velocity;
        public bool IsDying = false;
        public bool isDying { get { return IsDying; } set { IsDying = value; } }
        protected uint spawnCount;
        protected float spawnTime, lifeTimer = 80;
        protected Sprite sprite;
        protected Bullet parent;
        protected DamageType Type;
        protected float DamageAmount = 10;

        public virtual void Update()
        {
            CollisionManager.GoodMoved(this);
            lifeTimer -= 1;
            if (lifeTimer < 0)
            {
                CollisionManager.DeleteMe(this);
                if (parent != null)
                    parent.Children.Remove(this);
                for (int i = 0;i<Children.Count;i++)
                    Children[i].lifeTimer = 1;
            }
            for (int i = 0; i < Children.Count; i++)
                Children[i].Update();

            List<ICollidable> coll = CollisionManager.GoodMoved(this);

           
        }

        public void Die()
        {
            if (isDying) return;
            ObjectManager.RemoveBullet(this);
            isDying = true;
            lifeTimer = 0;
        }

        ~Bullet()
        {
           // Count--;
            //if (Count % 500==0)Debug.Print(Count.ToString());
        }
        public virtual void Draw(SpriteBatch sb)
        {
            if (isDying) return;
            sprite.Draw(sb);
            foreach (Bullet item in Children)
            {
                item.Draw(sb);
            }
        }
    }
}
