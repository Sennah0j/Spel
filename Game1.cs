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
        

        string blockRead, place;
        

        
        static int points = 0, countNum = 0;
        bool wPress = false;
                
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        Texture2D platform, touchPlat;
        Texture2D myship;
        
        Texture2D coin;
        Texture2D tripod;
        Color backColor = Color.BurlyWood;
        
        Vector2 coin_pos;
        Vector2 tripod_pos,tripod_speed,tripodTest,tripod_pos2;
        List<Vector2> coin_pos_list = new List<Vector2>();
        List<Vector2> tripod_pos_list = new List<Vector2>();
        public Rectangle rec_myship;
        Rectangle rec_coin;
        Rectangle platformVis;
        MouseState mouse;
        Player player = new Player();
        Plattforms plattformsClass = new Plattforms();

        SpriteFont gameFont;


        
        
        
        public Game1()
        {

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }
   

        protected override void Initialize()
        {
            GlobalConst.WindowWidth = Window.ClientBounds.Width;
            GlobalConst.WindowHeight = Window.ClientBounds.Height;
            

            
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
            player.IntiPlayerCont();
            tripod_speed.Y = 2f;
            tripod_speed.X = 0f;
            tripodTest.X = 0f;
            tripodTest.Y = 0f;
            tripod_pos2.X = 0;
            tripod_pos2.Y = 0;
            
            
            


            

            base.Initialize();
            
        }
        
        



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont2");

            // TODO: use this.Content to load your game content here
            coin = Content.Load<Texture2D>("Sprites/coin");
            player.myship = Content.Load<Texture2D>("Sprites/slime");
            tripod = Content.Load < Texture2D>("Sprites/tripod");

            platform = new Texture2D(GraphicsDevice, 1, 1);
            platform.SetData(new Color[] { Color.Black });
            touchPlat = new Texture2D(GraphicsDevice, 1, 1);
            


        }



        

        public void PlattformSpawn()
        {
            plattformsClass.Platform1();
            plattformsClass.Platform2();
            plattformsClass.Platform3();
            plattformsClass.CheckColission();
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
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            PlattformSpawn();
            player.Gravity(gameTime);
            player.KeyMovements();

            

            BoundaryCheckEn();
            EnemySPeed();

            CheckCollisionEn();
            Cursor();

     

            
            base.Update(gameTime);
        }
        
        public void CheckCollisionEn()
        {
            foreach (Vector2 cn in coin_pos_list.ToList())
            {
                GlobalConst.RecMyship = new Rectangle(Convert.ToInt32(player.playerPos.X), Convert.ToInt32(player.playerPos.Y), player.myship.Width, player.myship.Height);
                rec_coin = new Rectangle(Convert.ToInt32(cn.X), Convert.ToInt32(cn.Y), coin.Width, coin.Height);

                if (GlobalConst.RecMyship.Intersects(rec_coin))
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
                    
                }
            }


        }

        public void Cursor()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
            {

                blockRead = File.ReadAllText(GlobalConst.BlockFile);
                GlobalConst.Split = blockRead.Split(',');
                if (GlobalConst.Split[0] == "true") 
                {
                    
                }
                else
                {
                    using (StreamWriter writefile = new StreamWriter(GlobalConst.BlockFile))
                    {
                        place = "true";
                        writefile.Write(place + "," + mouse.Position.X + "," + mouse.Position.Y);
                        writefile.Close();
                    }
                }
            }
            
        }

        
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(backColor);
            
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            
            spriteBatch.Draw(touchPlat, plattformsClass.Platform1() ,backColor);
            spriteBatch.Draw(touchPlat, plattformsClass.Platform2(), backColor);
            spriteBatch.Draw(touchPlat, plattformsClass.Platform3(), backColor);
            spriteBatch.Draw(platform, plattformsClass.Platform4(), Color.Black);
            spriteBatch.Draw(platform, plattformsClass.Platform5(), Color.Black);
            spriteBatch.Draw(platform, plattformsClass.Platform6(), Color.Black);
            /*
            foreach (Vector2 cn in coin_pos_list)
            {
                spriteBatch.Draw(coin, cn, Color.White);
            }
            */
            foreach (Vector2 cn in tripod_pos_list)
            {
                spriteBatch.Draw(tripod, cn, Color.White);
            }

            
            spriteBatch.DrawString(gameFont, "Poäng:" + player.testStr + player.touch.ToString() + player.timeJump.ToString() + player.gravity.ToString() + player.mathPow , new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(gameFont, "Y." + mouse.Y + " X."+ mouse.X + " " +plattformsClass.recTouch, new Vector2(10, 30), Color.White);
            spriteBatch.DrawString(gameFont, "X." + player.playerPos.X + "Y." + player.playerPos.Y, new Vector2(10, 50), Color.White);
            spriteBatch.Draw(player.myship, player.playerPos, Color.White);

            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }




   
}


