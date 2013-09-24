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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        // This is a texture we can render.
        Texture2D myTexture, paddle1, paddle2;

        // Set the coordinates to draw the sprite at.
        Vector2 spritePosition = Vector2.Zero;
        Vector2 pad1Position = Vector2.Zero;
        Vector2 pad2Position = Vector2.Zero;

        // Store some information about the sprite's motion.
        Vector2 spriteSpeed = new Vector2(150.0f, 150.0f);
        float paddleSpeed = 5.0f;
        KeyboardState keyPressed = Keyboard.GetState();

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            myTexture = Content.Load<Texture2D>("tennisball");
            paddle1 = Content.Load<Texture2D>("paddle");
            paddle2 = Content.Load<Texture2D>("paddle");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            UpdateSprite(gameTime);
            UpdatePaddles(gameTime);
            base.Update(gameTime);
        }
        void UpdateSprite(GameTime gameTime)
        {
            // Move the sprite by speed, scaled by elapsed time.
            spritePosition +=
                spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            float YDiff;
            int MaxX =
                graphics.GraphicsDevice.Viewport.Width - myTexture.Width;
            int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - myTexture.Height;
            int MinY = 0;
            int Pad1MinX = MinX + 10;
            int Pad1MaxX = Pad1MinX + paddle1.Width;
            int Pad2MaxX =
                MaxX  - 10;
            int Pad2MinX = Pad2MaxX - paddle1.Width;
            int MinYdiff = -1 * myTexture.Height;
            int MaxYdiff = paddle1.Height;

            // Check for bounce.
            if (spritePosition.X > MaxX)
            {
                this.Exit();
            }

            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }


            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }

            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }

            if (spritePosition.X > Pad1MinX && spritePosition.X < Pad1MaxX)
            {
                YDiff = pad1Position.Y - spritePosition.Y;
                if (YDiff > MinYdiff && YDiff < MaxYdiff && spriteSpeed.X < 0)
                {
                    spriteSpeed.X *= -1;
                }
            }

            else if (spritePosition.X > Pad2MinX && spritePosition.X < Pad2MaxX)
            {
                YDiff = pad2Position.Y - spritePosition.Y;
                if (YDiff > MinYdiff && YDiff < MaxYdiff && spriteSpeed.X > 0)
                {
                    spriteSpeed.X *= -1;
                }
            }
        }



        void UpdatePaddles(GameTime gameTime)
        {
            int MaxY2 =
                graphics.GraphicsDevice.Viewport.Height - paddle2.Height - (int)paddleSpeed;
            int MinY2 = (int)paddleSpeed;
            int MaxY1 =
                graphics.GraphicsDevice.Viewport.Height - paddle2.Height;
            keyPressed = Keyboard.GetState();
            pad1Position.Y = Math.Min(Math.Max(0.0f, spritePosition.Y), MaxY1);
            pad1Position.X = 10.0f;
            pad2Position.X = graphics.GraphicsDevice.Viewport.Width - paddle2.Width - 10.0f;

            if (keyPressed.IsKeyDown(Keys.Down))
            {
                if (pad2Position.Y < MaxY2)
                {
                    pad2Position.Y += paddleSpeed;
                }
            }
            if (keyPressed.IsKeyDown(Keys.Up))
            {
                if (pad2Position.Y > MinY2)
                {
                    pad2Position.Y -= paddleSpeed;
                }
            }
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw the sprite.
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spriteBatch.Draw(myTexture, spritePosition, Color.White);
            spriteBatch.Draw(paddle1, pad1Position, Color.White);
            spriteBatch.Draw(paddle2, pad2Position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
