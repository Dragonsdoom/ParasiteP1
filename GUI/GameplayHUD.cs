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
    public class GameplayHUD
    {
        ScreenText st = new ScreenText();
        Game1 g = ContentStorageManager.Get<Game1>("game");
        Texture2D livesIcon;
        public GameplayHUD()
        {
            livesIcon = g.Content.Load<Texture2D>("redOrb");
        }

        public void Update()
        {
            
        }

        public void Draw(SpriteBatch sb)
        {
            st.Draw("SCORE: " + GUIVariables.score.ToString(), new Vector2(10, 10), sb);
            for (int i = 0; i < GUIVariables.lives; i++)
            {
                sb.Draw(livesIcon, new Rectangle(10 + (50 * i), 750, 40, 40), Color.White);
            }
        }
    }
}
