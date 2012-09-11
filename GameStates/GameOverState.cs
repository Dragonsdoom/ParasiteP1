using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class GameOverState : GameState
    {
        InputController input;
        Texture2D credits;

        public GameOverState(Game game) : base(game)
        {
            credits = game.Content.Load<Texture2D>("GameOverScreen");
            input = ContentStorageManager.Get<InputController>("input");
            
        }

        public override void Update()
        {

            if (input.Fire)
            {
                //AudioManager.StopLoopedSound("music");
                StateManager.QueueState<MainMenuState>();
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(credits, new Rectangle(0, 0, 800, 800), Color.White);
        }


    }
}
