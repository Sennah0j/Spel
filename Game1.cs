using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Pong
{

    public class Game1 : Game
    {



        string blockFile = "File", blockRead, place;



        double mathPow;
        static int points = 0, countNum = 0;
        bool wPress = false;
        float gravity, gravitySpeed, timeJump;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D platform;


        Texture2D coin;
        Texture2D tripod;
        static Vector2 playerPos, playerSpeed, myshipSpeedDown;

        Vector2 coin_pos;
        Vector2 tripod_pos, tripod_speed, tripodTest, tripod_pos2;
        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
        Rectangle rec_myship;
        Rectangle rec_coin;
        Rectangle platformVis;
        MouseState mouse;

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
            playerPos.X = 100;
            playerPos.Y = 100;
            playerSpeed.X = 5f;
            playerSpeed.Y = 5f;
            myshipSpeedDown.Y = 4f;
            tripod_speed.Y = 2f;
            tripod_speed.X = 0f;
            tripodTest.X = 0f;
            tripodTest.Y = 0f;
            tripod_pos2.X = 0;
            tripod_pos2.Y = 0;
            gravity = 0.2f;
            gravitySpeed = 0f;
            playerPos.X = 10;




            base.Initialize();

        }



        public static Vector2 PlayerSpeed
        {
            get { return playerSpeed; }
            set { playerSpeed = value; }
        }
        public static Vector2 PlayerPos
        {
            get { return playerPos; }
            set { playerPos = value; }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont2");

            // TODO: use this.Content to load your game content here
            coin = Content.Load<Texture2D>("Sprites/coin");
            Global.Myship = Content.Load<Texture2D>("Sprites/slime");
            tripod = Content.Load<Texture2D>("Sprites/tripod");

            platform = new Texture2D(GraphicsDevice, 1, 1);
            platform.SetData(new Color[] { Color.Black });



        }


        public void Jump()
        {

            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W) && (Global.Touch = true))
            {
                gravity = -2f;

            }
        }

        public void KeyInput()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
                playerPos.X = playerPos.X + playerSpeed.X;
            if (keyboardState.IsKeyDown(Keys.A))
                playerPos.X = playerPos.X - playerSpeed.X;

            //if (keyboardState.IsKeyDown(Keys.S))
            //myship_pos.Y = myship_pos.Y + myship_speed.Y;
            /*
            blockRead = File.ReadAllText(blockFile);
            Global.Split = blockRead.Split(',');
            if (playerPos.Y == float.Parse(Global.Split[2]))
            {
                
                gravity = 0f;
                points = 69;
                Global.Touch = true;
            }
            else
            {
                points = 420;
                
                
                gravity = 0.89f;
            }
            */

            if (playerPos.Y >= Window.ClientBounds.Height - Global.Myship.Height)
            {
                countNum = 0;
                playerSpeed.Y = 5f;
                myshipSpeedDown.Y = 0f;
                gravitySpeed = 0f;
                gravity = 0f;

                Global.Points = 69;

                Global.Touch = true;
            }
            else if (((playerPos.Y == Window.ClientBounds.Height - Global.Myship.Height) && keyboardState.IsKeyDown(Keys.W) == true) || (playerPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
            {
                playerSpeed.Y = 5f;
            }
            else
            {
                points = 420;
                gravitySpeed = 7f;
                gravity = 0.89f;
            }

            if (((playerPos.X >= Window.ClientBounds.Width - Global.Myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
            {
                playerSpeed.X = 0f;
            }
            else if (((playerPos.X >= Window.ClientBounds.Width - Global.Myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
            {
                playerSpeed.X = 5f;
            }

            if (keyboardState.IsKeyDown(Keys.E) == true)
            {
                using (StreamWriter writefile = new StreamWriter(blockFile))
                {

                    writefile.Write("false" + "," + Global.Split[1] + "," + Global.Split[2]);
                    writefile.Close();
                }
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

        public void Test()
        {
            player Player = new player();
            Player.KeyMovements();
        }
    
        protected override void Update(GameTime gameTime) 
        {
            

            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //KeyInput();

            Test();
            
            
            timeJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (keyboardState.IsKeyDown(Keys.W) && (Global.Touch  == true))
            {
                
                playerPos.Y -= Convert.ToSingle(Math.Pow(mathPow, 0.99));
                mathPow = Math.Pow(mathPow, 0.99);
            }

            if(keyboardState.IsKeyUp(Keys.W))
            {
                
                mathPow = 10;
                Global.Touch = false;
            }
            else if (timeJump >= 2)
            {

                Global.Touch = false;
            }
            playerPos.Y += (gravity * timeJump * gravitySpeed);




            //BoundaryCheckEn();

            //EnemySPeed();
            CheckCollisionEn();
            Cursor();


            base.Update(gameTime);
        }
        
        public void CheckCollisionEn()
        {
            foreach (Vector2 cn in coin_pos_list.ToList())
            {
                rec_myship = new Rectangle(Convert.ToInt32(playerPos.X), Convert.ToInt32(playerPos.Y), Global.Myship.Width, Global.Myship.Height);
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

        public void Cursor()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {

                blockRead = File.ReadAllText(blockFile);
                Global.Split = blockRead.Split(',');
                if (Global.Split[0] == "true") 
                {
                    
                }
                else
                {
                    using (StreamWriter writefile = new StreamWriter(blockFile))
                    {
                        place = "true";
                        writefile.Write(place + "," + mouse.Position.X + "," + mouse.Position.Y);
                        writefile.Close();
                    }
                }
            }
            
        }

        public Rectangle recta()
        {
            
            blockRead = File.ReadAllText(blockFile);
            Global.Split = blockRead.Split(",");
            //                   X   Y   Xlång   Ytjock
            return new Rectangle(int.Parse(Global.Split[1]), int.Parse(Global.Split[2]), 400, 50);
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

            
            _spriteBatch.DrawString(gameFont, "Poäng:" + Global.Points, new Vector2(10, 10), Color.White);
            _spriteBatch.Draw(Global.Myship, playerPos, Color.White);

            _spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }




   
}


