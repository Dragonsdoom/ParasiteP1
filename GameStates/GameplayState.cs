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
    class GameplayState : GameState
    {
        InputController input;
        bool pausedGame;

        GameplayHUD HUD = new GameplayHUD();
        ScreenText st;//used to display PAUSE while paused

        ControllableShip player;
        Parasite parasite;
        Level level;

        Menu pauseMenu;

        public GameplayState(Game game)
            : base(game)
        {
            Game1 g = (Game1)game;

            input = ContentStorageManager.Get<InputController>("input");

            st = new ScreenText("Courier", "PAUSE", new Vector2(2, 2), new Vector2(300, 300));//initialize the PAUSE text

            //Create and set pause buttons
            Dictionary<string, ImageButton.Callback> menuButtons = new Dictionary<string, ImageButton.Callback>();            
            menuButtons.Add("PauseResume", resumeButton);
            menuButtons.Add("MainMenuQuit", quitButton);

            //initialize menu
            pauseMenu = new Menu(g, menuButtons, HLayout.Center, VLayout.Bottom, new Vector2(300, 300), 10);

            player = new ControllableShip(new Vector2(Game1.ScreenSize.X / 2, Game1.ScreenSize.Y / 2), new Vector2(10, 10));
            parasite = new Parasite(player.Position, new Vector2(8,8));
            parasite.controlShip = player;
            level = new Level(1);
            // this method (level.Generate()) will be called when the level is needed to change. For now, its here.
            level.GenerateLevel(level.current_level);

        }

        //Quit out of game
        public void quitButton()
        {
            AudioManager.StopLoopedSound("music");
            StateManager.QueueState<MainMenuState>();
        }

        //Switch to game state
        public void resumeButton()
        {
            pausedGame = false;
        }

        public override void Update()
        {
            if (!pausedGame)
            {
                ObjectManager.Update();
                player.Update();
                parasite.Update();
                level.Update();
                AudioManager.PlaySounds();

                if (input.Esc)
                {
                    pausedGame = true;
                }
            }
            else
            {
                if (input.Esc)
                {
                    pausedGame = false;
                }
                else
                {
                    pauseMenu.Update();
                }
            }

            if (GUIVariables.lives <= 0)
            {
                AudioManager.StopLoopedSound("music");
                StateManager.QueueState<GameOverState>();
            }
        }
        public override void Draw(SpriteBatch sb)
        {
                level.Draw(sb); 	//Draw first

                ObjectManager.Draw(sb);

                if (player != null)
                {
                    player.Draw(sb); 
                }
                
                parasite.Draw(sb);
                HUD.Draw(sb);
                if (pausedGame)
                {
                    st.DrawCentered(sb);
                    pauseMenu.Draw(sb);
                }      
        }
    }

    
}
