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
using ParasiteP1.Utility;

namespace ParasiteP1
{
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Vector2 ScreenSize = new Vector2(800,800);
        public static Vector2 ScreenCenter = new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2);

        InputController input;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth  = (int)ScreenSize.X;
            graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            Content.RootDirectory = "Content";

            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            input = new InputController(this);

            input.UseMouseNav = false;

            ContentStorageManager.Store<InputController>("input", input);
            ContentStorageManager.Store<Game1>("game", this);

            ContentStorageManager.Store<Texture2D>("Bullet00", Content.Load<Texture2D>("Bullet00"));
            ContentStorageManager.Store<Texture2D>("Ship00", Content.Load<Texture2D>("Ship00"));
            ContentStorageManager.Store<Texture2D>("Ship01", Content.Load<Texture2D>("Ship01"));
            ContentStorageManager.Store<Texture2D>("Ship02", Content.Load<Texture2D>("Ship02"));
            ContentStorageManager.Store<Texture2D>("Enemy01", Content.Load<Texture2D>("Enemy01"));
            ContentStorageManager.Store<Texture2D>("Enemy02", Content.Load<Texture2D>("Enemy02"));
            ContentStorageManager.Store<Texture2D>("Background00", Content.Load<Texture2D>("Background00"));

            ContentStorageManager.Store<Texture2D>("Shape00", Content.Load<Texture2D>("Shape00"));
            ContentStorageManager.Store<Texture2D>("Shape01", Content.Load<Texture2D>("Shape01"));
            ContentStorageManager.Store<Texture2D>("Shape02", Content.Load<Texture2D>("Shape02"));
            ContentStorageManager.Store<Texture2D>("parasite", Content.Load<Texture2D>("Parasite00"));

            ContentStorageManager.Store<SoundEffect>("music", Content.Load<SoundEffect>("music"));
            ContentStorageManager.Store<SoundEffect>("explosion", Content.Load<SoundEffect>("Explosion00"));
            ContentStorageManager.Store<SoundEffect>("hithurt", Content.Load<SoundEffect>("HitHurt00"));
            ContentStorageManager.Store<SoundEffect>("lasershoot", Content.Load<SoundEffect>("LaserShoot00"));
            ContentStorageManager.Store<SoundEffect>("powerup", Content.Load<SoundEffect>("PowerUp00"));
			
            StateManager.InitalizeStateManager<MainMenuState>(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            input.Update();
            StateManager.UpdateInActiveStates();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            StateManager.DrawInActiveStates(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
