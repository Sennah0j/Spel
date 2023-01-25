using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pong
{
    public class Game1 : Game
    {
        TimeSpan currentTime = DateTime.Now.TimeOfDay;
        int points = 0, countNum = 0;
        bool touch = false;
        float timeSinceLastShot = 0f;
        Classes Class = new Classes();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D platform;
        Texture2D myship;
        Texture2D coin;
        Texture2D tripod;
        Vector2 myshipPos, myship_speed, myshipSpeedDown;
        Vector2 coin_pos;
        Vector2 tripod_pos,tripod_speed,tripodTest,tripod_pos2, gravity;
        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
        Rectangle rec_myship;
        Rectangle rec_coin;
        Rectangle platformVis;
        
        SpriteFont gameFont;



        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public TimeSpan ElapsedGameTime { get; set; }

        protected override void Initialize()
        {
            Random slump = new Random();
            for (int i = 0; i < 5; i++)
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coin_pos_list.Add(coin_pos);
            }
            
            for (int i = 0; i < 5; i++)
            {
                tripod_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                tripod_pos.Y = slump.Next(0, Window.ClientBounds.Height - 400);

                tripod_pos_list.Add(tripod_pos);
            }

            // TODO: Add your initialization logic here
            myshipPos.X = 100;
            myshipPos.Y = 100;
            myship_speed.X = 4f;
            myship_speed.Y = 4f;
            myshipSpeedDown.Y = 4f;
            tripod_speed.Y = 2f;
            tripod_speed.X = 0f;
            tripodTest.X = 0f;
            tripodTest.Y = 0f;
            tripod_pos2.X = 0;
            tripod_pos2.Y = 0;
            gravity.Y = -1;
            gravity.X = 1;

            base.Initialize();
            
        }
        
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont2");

            // TODO: use this.Content to load your game content here
            coin = Content.Load<Texture2D>("Sprites/coin");
            myship = Content.Load<Texture2D>("Sprites/ship");
            tripod = Content.Load < Texture2D>("Sprites/tripod");

            platform = new Texture2D(GraphicsDevice, 1, 1);
            platform.SetData(new Color[] { Color.Black });

        }


        public void Jump(int sec)
        {
            GameTime gameTime = new GameTime();
            
            timeSinceLastShot += (float)gameTime.ElapsedGameTime.TotalSeconds;

            KeyboardState keyboardState = Keyboard.GetState();
            if ((keyboardState.IsKeyDown(Keys.W) && countNum <= sec) && (touch = true))
            {
                
                if (countNum == sec)
                {
                    touch = false;
                }
                else
                {
                    myshipSpeedDown.Y = -1 * timeSinceLastShot * -0.5f;
                    countNum++;
                }

            }
        }
        
        public void KeyInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
                myshipPos.X = myshipPos.X + myship_speed.X;
            if (keyboardState.IsKeyDown(Keys.A))
                myshipPos.X = myshipPos.X - myship_speed.X;

            //if (keyboardState.IsKeyDown(Keys.S))
            //myship_pos.Y = myship_pos.Y + myship_speed.Y;
            


            Jump(30);
            
            

            
            myshipPos.Y = myshipPos.Y + myshipSpeedDown.Y;

            if (myshipPos.Y == Window.ClientBounds.Height - myship.Height)
            {
                countNum = 0;
                myship_speed.Y = 0f;
                myshipSpeedDown.Y = 0f;
                touch = true;
            }
            
            else if (((myshipPos.Y == Window.ClientBounds.Height - myship.Height) && keyboardState.IsKeyDown(Keys.W) == true) || (myshipPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
            {
                myship_speed.Y = 4f;
            }

            else
            {
                myshipSpeedDown.Y = 4f;
            }

            if (((myshipPos.X == Window.ClientBounds.Width - myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (myshipPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
            {
                myship_speed.X = 0f;
            }
            else if (((myshipPos.X == Window.ClientBounds.Width - myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (myshipPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
            {
                myship_speed.X = 4f;
            }

        }
        public void EnemySPeed()
        {
            for (int i = 0; i < tripod_pos_list.Count; i++)
            {
                tripod_pos_list[i] = tripod_pos_list[i] + tripod_speed;
            }
        }
        public void EnemyRndLocation(int j) 
        {
            Random slump = new Random();
            for (int i = 0; i < 5; i++)
            {
                tripod_pos2.X = slump.Next(0, Window.ClientBounds.Width - 50);
                tripod_pos2.Y = slump.Next(0, Window.ClientBounds.Height - 400);

                tripod_pos_list[j] = tripod_pos2;
            }
        }
        public void BoundaryCheckEn()
        {
            for (int i = 0; i < tripod_pos_list.Count; i++)
            {
                if (tripod_pos_list[i].Y >= (Window.ClientBounds.Height + tripod.Height))
                {
                    EnemyRndLocation(i);
                }
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            
            // TODO: Add your update logic here
                
            //BoundaryCheckEn();

            EnemySPeed();
            CheckCollision();
            KeyInput();


            base.Update(gameTime);
        }
        
        public void CheckCollision()
        {
            foreach (Vector2 cn in coin_pos_list.ToList())
            {
                rec_myship = new Rectangle(Convert.ToInt32(myshipPos.X), Convert.ToInt32(myshipPos.Y), myship.Width, myship.Height);
                rec_coin = new Rectangle(Convert.ToInt32(cn.X), Convert.ToInt32(cn.Y), coin.Width, coin.Height);

                if (rec_myship.Intersects(rec_coin))
                {
                    points += 10;
                    coin_pos_list.Remove(cn);

                }
            }
            for (int i = 0; i < tripod_pos_list.Count; i++)
            {
                if (tripod_pos_list[i].Y >= (Window.ClientBounds.Height + tripod.Height))
                {
                    points += 10;
                    tripod_pos_list.RemoveAt(i);
                }
            }


        }

        public Rectangle recta()
        {
            return new Rectangle(50, 50, 200, 50);
        }
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.BurlyWood);
            
            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            
            _spriteBatch.Draw(platform, recta() , Color.Black);

            foreach (Vector2 cn in coin_pos_list)
            {
                _spriteBatch.Draw(coin, cn, Color.White);
            }
            foreach (Vector2 cn in tripod_pos_list)
            {
                _spriteBatch.Draw(tripod, cn, Color.White);
            }

            string text = timeSinceLastShot.ToString();
            _spriteBatch.DrawString(gameFont, "Poäng:" + points, new Vector2(10, 10), Color.White);
            _spriteBatch.Draw(myship, myshipPos, Color.White);

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }




    public class Keyboard
    {
        static KeyboardState currentKeyState;
        static KeyboardState previousKeyState;

        public static KeyboardState GetState()
        {
            currentKeyState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            previousKeyState = currentKeyState;
            
            return currentKeyState;
        }

        public static bool IsPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key);
        }

        public static bool HasBeenPressed(Keys key)
        {
            return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
        }
    }
}


