using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    public class StraitBullet : Bullet
    {
        public StraitBullet(Vector2 position, Vector2 velocity)
            : base()
        {
            sprite = new Sprite(Utility.ContentStorageManager.Get<Texture2D>("Shape01"), position); //Temp
            sprite.CenterOriginOnTexture();
            sprite.Scale = .5f;
            sprite.Color = Color.Chartreuse;
            this.Position = position;
            this.velocity = velocity;
        }

        public override void  Update()
        {
            Position += velocity;
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            sprite.Draw(sb);
        }
    }
}
