using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ParasiteP1
{
    /// <summary>
    /// This class draws an image that is meant to visually depict another object.
    /// This sprite does not implement drawing to a rectangle, it fully displays the texture.
    /// </summary>
    public class Sprite
    {
        public Vector2 position, origin;
        float rotation = 0;
        public float Rotation { get { return rotation; } set { rotation = value; } }

        public Texture2D texture;

        Color color = Color.White;
        public Color Color { get { return color; } set { color = value; } }
        float scale = 1;
        public float Scale { private get { return scale; } set { scale = value; } }
        int layerDepth = 0;
        public int LayerDepth { private get { return layerDepth; } set { layerDepth = value; } }
        bool isVisible = true;
        public bool IsVisible { protected get { return isVisible; } set { isVisible = value; } }

        /// <summary>
        /// Used to construct a basic sprite.
        /// </summary>
        public Sprite(Texture2D texture)
        {
            position = Vector2.Zero;
            this.texture = texture;
            origin = Vector2.Zero;
        }

        /// <summary>
        /// Used to construct a basic sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 position)
        {
            this.position = position;
            this.texture = texture;
            origin = Vector2.Zero;
        }

        /// <summary>
        /// Used to construct a more complex version of the sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 position, float rotation)
        {
            this.position = position;
            this.texture = texture;
            this.rotation = rotation;
        }

        /// <summary>
        /// Used to construct a more complex version of the sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 position, Vector2 origin, float rotation)
        {
            this.position = position;
            this.origin = origin;
            this.texture = texture;
            this.rotation = rotation;
        }

        public Vector2 GetTextureSize()
        {
            return new Vector2(texture.Width, texture.Height) * scale;
        }

        public void CenterOriginOnTexture()
        {
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public virtual void AddRotation(float rotation)
        {
            this.rotation += rotation;
        }

        public void Draw(SpriteBatch sb)
        {
            if (isVisible)
            {
                sb.Draw(texture, position, null, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
            }
        }
    }
}
