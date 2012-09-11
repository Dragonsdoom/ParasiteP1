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
    public class MainMenuState : GameState
    {
        Menu MainMenu;
        InputController input;
        Level level;
        Texture2D titleLogo;
        Rectangle Position;

        public MainMenuState(Game game)
            : base(game)
        {
            //Re-initialize score and lives for a new game.
            GUIVariables.score = 0;
            GUIVariables.lives = 3;

            input = ContentStorageManager.Get<InputController>("input");

            //Create and set buttons
            Dictionary<string, ImageButton.Callback> menuButtons = new Dictionary<string, ImageButton.Callback>();
            menuButtons.Add("MainMenuStart", startButton);
            menuButtons.Add("MainMenuQuit", quitButton);

            //initialize menu
            MainMenu = new Menu(game, menuButtons, HLayout.Left, VLayout.Bottom, new Vector2(0, 0), 10); // HLayout is CENTER for PC

            titleLogo = game.Content.Load<Texture2D>("ParasiteTitle");
            Position.X -= 658 / 2 - game.GraphicsDevice.PresentationParameters.BackBufferWidth / 2;
            Position.Y = 150;
            Position.Width = 658;
            Position.Height = 206;

            //Start a new level to scroll in the background
            level = new Level(1);
            level.GenerateLevel(level.current_level);
            AudioManager.QueueSound("music", true);
        }

        //Quit out of game
        public void quitButton()
        {
            game.Exit();
        }

        //Switch to game state
        public void startButton()
        {
            StateManager.QueueState<GameplayState>();
        }
        
        public override void Update()
        {
            MainMenu.Update();
            level.TitleUpdate();
            ObjectManager.Update();
            AudioManager.PlaySounds();
            
        }

        public override void Draw(SpriteBatch sb)
        {
            level.TitleDraw(sb);
            MainMenu.Draw(sb);
            sb.Draw(titleLogo, Position, Color.White);
        }

    }
}
