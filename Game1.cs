using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D myship;
        Texture2D coin;
        Texture2D tripod;
        Vector2 myship_pos;
        Vector2 myship_speed;
        Vector2 coin_pos;
        Vector2 tripod_pos;
        Vector2 tripod_speed;
        Vector2 tripodTest;
        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
        



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public void TripodAdd()
        {
            Random slump = new Random();
            for (int i = 0; i < 5; i++)
            {
                tripod_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                tripod_pos.Y = slump.Next(0, Window.ClientBounds.Height - 400);

                tripod_pos_list.Add(tripod_pos);
            }
        }

        protected override void Initialize()
        {
            Random slump = new Random();
            for (int i = 0; i < 5; i++)
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coin_pos_list.Add(coin_pos);
            }
            TripodAdd();
           

            // TODO: Add your initialization logic here
            myship_pos.X = 100;
            myship_pos.Y = 100;
            myship_speed.X = 4f;
            myship_speed.Y = 4f;
            tripod_speed.Y = 2f;
            tripod_speed.X = 0f;
            tripodTest.X = 0f;
            tripodTest.Y = 0f;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myship = Content.Load<Texture2D>("Sprites/ship");
            coin = Content.Load<Texture2D>("Sprites/coin");
            tripod = Content.Load < Texture2D>("Sprites/tripod");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here



            for(int i = 0; i < tripod_pos_list.Count; i++)
            {
                
                tripod_pos_list[i] = tripod_pos_list[i] + tripod_speed;
            } 
            for (int i = 0; i < tripod_pos_list.Count; i++)
            { 
                if (tripod_pos_list[i].Y == Window.ClientBounds.Height - tripod.Height)
                {
                    tripod_pos_list.RemoveAt(i);
                    TripodAdd();
                }
            }

 
            
            


            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
                myship_pos.X = myship_pos.X + myship_speed.X;
            if (keyboardState.IsKeyDown(Keys.A))
                myship_pos.X = myship_pos.X - myship_speed.X;

            if (keyboardState.IsKeyDown(Keys.S))
                myship_pos.Y = myship_pos.Y + myship_speed.Y;
            if (keyboardState.IsKeyDown(Keys.W))
                myship_pos.Y = myship_pos.Y - myship_speed.Y;
            

            if (((myship_pos.Y == Window.ClientBounds.Height - myship.Height) && keyboardState.IsKeyDown(Keys.S) == true) || (myship_pos.Y == 0 && keyboardState.IsKeyDown(Keys.W)))
            {
                myship_speed.Y = 0f;
            }
            else if (((myship_pos.Y == Window.ClientBounds.Height - myship.Height) && keyboardState.IsKeyDown(Keys.W)==true) || (myship_pos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
            {
                myship_speed.Y = 4f;
            }

            if (((myship_pos.X == Window.ClientBounds.Width - myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (myship_pos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
            {
                myship_speed.X = 0f;
            }
            else if (((myship_pos.X == Window.ClientBounds.Width - myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (myship_pos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
            {
                myship_speed.X = 4f;
            }






            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);
            

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            foreach (Vector2 cn in coin_pos_list)
            {
                _spriteBatch.Draw(coin, cn, Color.White);
            }
            foreach (Vector2 cn in tripod_pos_list)
            {
                _spriteBatch.Draw(tripod, cn, Color.White);
            }
            

            _spriteBatch.Draw(myship, myship_pos, Color.White);
            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}