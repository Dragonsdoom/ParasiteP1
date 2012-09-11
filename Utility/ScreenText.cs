using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ParasiteP1;
using ParasiteP1.Utility;

namespace ParasiteP1
{
    public class ScreenText
    {
        public delegate void Callback(object sender);
        SpriteFont Font;
        string Text;
        Vector2 Scale; //Sets the text scaling in the X and Y directions multaplicatively 
        Vector2 Position;

        public ScreenText()
        {
            Game1 g = ContentStorageManager.Get<Game1>("game");
            Font = g.Content.Load<SpriteFont>("Courier");
        }

        public ScreenText(string FontName, string Text, Vector2 Scale, Vector2 Position)
        {

            Game1 g =ContentStorageManager.Get<Game1>("game");
            this.Text = Text;
            Font=g.Content.Load<SpriteFont>(FontName);
            this.Scale = Scale;
            this.Position = Position;
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.DrawString(Font, Text, Position, Color.White, 0.0f, new Vector2(0, 0), Scale, SpriteEffects.None, 1.0f);
        }

        public void Draw(String Text, Vector2 Position, SpriteBatch sb)
        {
            sb.DrawString(Font, Text, Position, Color.White);
        }
        
        //Draw text centered horizontally (horrizontal positioning declared is ignored)
        public void DrawCentered(SpriteBatch sb)
        {
            Vector2 textSize = Font.MeasureString(Text);
            Vector2 textCenter = new Vector2(Game1.ScreenSize.X / 2, 50f);
            Vector2 centerPosition = new Vector2((textCenter.X - (textSize.X)), Position.Y);

            sb.DrawString(Font, Text, centerPosition, Color.White, 0.0f, new Vector2(0, 0), Scale, SpriteEffects.None, 1.0f);
        }
    }
}
