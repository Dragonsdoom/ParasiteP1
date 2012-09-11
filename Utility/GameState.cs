using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1.Utility
{
    public abstract class GameState
    {
        protected Game game;

        public GameState(Game game)
        {
            this.game = game;
        }

        public virtual void Update()
        { }

        public virtual void Draw(SpriteBatch sb)
        { }

        public virtual void Draw3D()
        { }

    }
}
