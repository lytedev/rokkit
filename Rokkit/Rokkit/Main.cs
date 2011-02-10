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
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;

namespace Rokkit
{
    /// <summary>
    /// Main Game class.
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite BlueRocket;
        Sprite OrangeRocket;

        static public World World;

        List<Entity> Entities = new List<Entity>();

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            World = new World(Settings.Gravity);
            World.ContactManager.OnBroadphaseCollision += Collision;
            base.Initialize();
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 848;
            graphics.ApplyChanges();
        }

        private void Collision(ref FixtureProxy fp1, ref FixtureProxy fp2)
        {

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            BlueRocket = new Sprite(Content.Load<Texture2D>("Sprites/Projectiles/blrocket"), 16, 16, 0, 0, 0);
            OrangeRocket = new Sprite(Content.Load<Texture2D>("Sprites/Projectiles/ojrocket"), 16, 16, 0, 0, 0);

            Entities.Clear();
            Entities.Add(new Entity(BlueRocket, new Vector2(400, 16), Vector2.Zero, EntityFlags.CanFall | EntityFlags.CanMove | EntityFlags.CanCollide)); 
            Entities.Add(new Entity(OrangeRocket, new Vector2(402, 128), Vector2.Zero, EntityFlags.CanCollide));
            Entities[1].Velocity = new Vector2(1, -200.8f);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            World.Step(((float)gameTime.ElapsedGameTime.TotalMilliseconds / (1000 / 30)) * Settings.StepSpeed);

            Input(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// Handles user input. 
        /// </summary>
        /// <param name="gameTime"></param>
        void Input(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();

            if (ks.IsKeyDown(Settings.Restart))
            {
                LoadContent();
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(0x00, 0x00, 0x00, 0xff));
            spriteBatch.Begin();

            foreach (Entity e in Entities)
            {
                e.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
