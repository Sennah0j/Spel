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
    
    public class Main : Game 
    {
        const int SCALE = 4; //1 = default, 2 = twice the size


        string blockRead, place;
        bool F3TF = true, F3Click = false;
        string sceneName;
        int sceneChanger = 0;
        static int points = 0, countNum = 0;
        bool wPress = false;
                
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D platform, touchPlat, startbtn;
        
        
        Texture2D coin;
        
        Color backColor = Color.BurlyWood;
        
        Vector2 coin_pos;
        Vector2 origin = new Vector2(GlobalConst.PlayerPos.X, GlobalConst.PlayerPos.Y);
        List<Vector2> coin_pos_list = new List<Vector2>();
        
        public Rectangle rec_myship;
        Rectangle rec_coin;
       
        MouseState mouse;

        Player player = new Player();
        Plattforms plattformsClass = new Plattforms();
        Bullet bulletClass = new Bullet();
        Enemy EnemyClass = new Enemy();
        SceneChange SceneChangeClass = new SceneChange();
        StartButton StartButtonClass = new StartButton();    
        SpriteFont gameFont;



        
        
        
        public Main()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
            
        }
   

        protected override void Initialize()
        {
            GlobalConst.WindowWidth = GraphicsDevice.DisplayMode.Width;
            GlobalConst.WindowHeight = GraphicsDevice.DisplayMode.Height;

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.IsFullScreen = true;
            graphics.ApplyChanges();



            Random slump = new Random();
            for (int i = 0; i < 5; i++)
            {
                coin_pos.X = slump.Next(0, Window.ClientBounds.Width - 50);
                coin_pos.Y = slump.Next(0, Window.ClientBounds.Height - 50);
                coin_pos_list.Add(coin_pos);
            }

           
          





            // TODO: Add your initialization logic here
            player.IntiPlayerCont();
            bulletClass.Vector2Def();
            EnemyClass.SpawnEnemy();

            
            EnemyClass.tripod_speed.Y = 2f;
            EnemyClass.tripod_speed.X = 0f;
            
            
            
            


            

            base.Initialize();
            
        }
        
        



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont2");

            // TODO: use this.Content to load your game content here
            coin = Content.Load<Texture2D>("Sprites/coin");
            player.myship = Content.Load<Texture2D>("Sprites/slime");
            EnemyClass.tripod = Content.Load < Texture2D>("Sprites/tripod");
            bulletClass.bulletTexture = Content.Load < Texture2D>("Sprites/bullet");

            platform = new Texture2D(GraphicsDevice, 1, 1);
            platform.SetData(new Color[] { Color.Black });
            touchPlat = new Texture2D(GraphicsDevice, 1, 1);

            startbtn = new Texture2D(GraphicsDevice, 1, 1);
            startbtn.SetData(new Color[] { Color.White });



        }



        public void BulletUpdate(GameTime gameTime)
        {
            bulletClass.bulletMethod(gameTime);
            bulletClass.BulletBondaryCheck();
        }

        public void PlattformSpawn()
        {
            plattformsClass.Platform1();
            plattformsClass.Platform2();
            plattformsClass.Platform3();
            plattformsClass.CheckColission();
        }
        
        

        protected override void Update(GameTime gameTime) 
        {
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            PlattformSpawn();
            player.Gravity(gameTime);
            player.KeyMovements(gameTime);

            BulletUpdate(gameTime);

            SceneChangeClass.ArrowSceneChange();

            EnemyClass.BoundaryCheckEn();
            EnemyClass.EnemySPeed();
            CheckCollisionEn();
            EnemyClass.CheckCollisionEn();
            


            Cursor();

     

            
            base.Update(gameTime);
        }
        
         
        public void CheckCollisionEn()
        {
            foreach (Vector2 cn in coin_pos_list.ToList())
            {
                GlobalConst.RecPlayer = new Rectangle(player.playerPos.ToPoint(), new Point(player.myship.Width * SCALE, player.myship.Height * SCALE));
                rec_coin = new Rectangle(Convert.ToInt32(cn.X), Convert.ToInt32(cn.Y), coin.Width, coin.Height);

                if (GlobalConst.RecPlayer.Intersects(rec_coin))
                {
                    points += 10;
                    coin_pos_list.Remove(cn);

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

        public void F3Info()
        {
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.F3) == true && (F3Click == false))
            {
                if (F3TF == true)
                    F3TF = false;
                else
                    F3TF = true;
                F3Click = true;

                
            }
            if( keyboardState.IsKeyUp(Keys.F3) == true)
            {
                F3Click = false;
            }


            if (F3TF == true)
            {
                spriteBatch.DrawString(gameFont, player.touch.ToString() + player.timeJump.ToString() + player.gravity.ToString() + player.mathPow, new Vector2(10, 10), Color.White);
                spriteBatch.DrawString(gameFont, "Y." + mouse.Y + " X." + mouse.X + " " + plattformsClass.recTouch, new Vector2(10, 30), Color.White);
                spriteBatch.DrawString(gameFont, "X." + player.playerPos.X + "Y." + player.playerPos.Y, new Vector2(10, 50), Color.White);
                spriteBatch.DrawString(gameFont, "Bullets: " + bulletClass.bulletsList.Count, new Vector2(10, 70), Color.White);
                spriteBatch.DrawString(gameFont, "Mouse press:  " + bulletClass.pressed, new Vector2(10, 90), Color.White);
                spriteBatch.DrawString(gameFont, "Scene: " + GlobalConst.SeneStatus + " " + SceneChangeClass.keyDown.ToString(), new Vector2(10, 110), Color.White);
                spriteBatch.DrawString(gameFont, "Scene name: " + sceneName , new Vector2(10, 130), Color.White);

            }
          
           
        }

        public void StartScene()
        {

            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Draw(platform, plattformsClass.CreateRec(20, 20, 20, 20), Color.White);
            spriteBatch.Draw(startbtn, StartButtonClass.StartBtn(), Color.White);

        }

        public void NextScene()
        {
            GraphicsDevice.Clear(backColor);

            spriteBatch.Draw(touchPlat, plattformsClass.Platform1(), backColor);
            spriteBatch.Draw(touchPlat, plattformsClass.Platform2(), backColor);
            spriteBatch.Draw(touchPlat, plattformsClass.Platform3(), backColor);
            spriteBatch.Draw(platform, plattformsClass.Platform1(), Color.Black);
            spriteBatch.Draw(platform, plattformsClass.Platform2(), Color.Black);
            spriteBatch.Draw(platform, plattformsClass.Platform3(), Color.Black);

            foreach (Vector2 cn in EnemyClass.tripod_pos_list)
            {
                spriteBatch.Draw(EnemyClass.tripod, cn, Color.White);
            }

            //Scale up not good
            spriteBatch.Draw(player.myship, GlobalConst.PlayerPos, null, Color.White, 0, origin, SCALE, SpriteEffects.None, 0);
            //spriteBatch.Draw(player.myship, player.playerPos, Color.White);

            foreach (Vector2 bullets in bulletClass.bulletsList)
            {
                spriteBatch.Draw(bulletClass.bulletTexture, bullets, Color.White);

            }
        }

        protected override void Draw(GameTime gameTime)
        {
            
            
            
            // TODO: Add your drawing code here
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            

           if (GlobalConst.SeneStatus == 0)
            {
                StartScene();
                sceneName = "start";
            }
           else if ( GlobalConst.SeneStatus == 1)
            {
                NextScene();
                sceneName = "first";
            }

            /*
            foreach (Vector2 cn in coin_pos_list)
            {
                spriteBatch.Draw(coin, cn, Color.White);
            }
            */










            F3Info();
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }




   
}


