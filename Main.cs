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


        
        string blockRead, place;
        bool F3TF = true, F3Click = false;
        string sceneName;
        int sceneChanger = 0;
        static int points = 0, countNum = 0;
        bool wPress = false;
                
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        Texture2D platform, touchPlat, startbtn, playerTexture, healthBarTex, deathBtn;
        
        
        Texture2D coin;
        
        Color backColor = Color.BurlyWood;
        

        Vector2 coin_pos, restartPos;
        Vector2 origin = new Vector2(GlobalConst.PlayerPos.X, GlobalConst.PlayerPos.Y);
        Vector2 bossOrigin = new Vector2(GlobalConst.BossVec.X, GlobalConst.BossVec.Y);
        List<Vector2> coin_pos_list = new List<Vector2>();
        
        public Rectangle rec_myship;
        Rectangle enemyRec, bulletRec;
       
        MouseState mouse;

        Player player = new Player();
        Plattforms plattformsClass = new Plattforms();
        Bullet bulletClass = new Bullet();
        Enemy EnemyClass = new Enemy();
        SceneChange SceneChangeClass = new SceneChange();
        StartButton StartButtonClass = new StartButton();    
        SpriteFont gameFont;
        Health healthClass = new Health();
        DeathBtn DeathBtnClass = new DeathBtn();
        Boss BossClass = new Boss();

        
        
        
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

            bulletClass.Vector2Def();
            

            
            EnemyClass.tripod_speed.Y = 2f;
            EnemyClass.tripod_speed.X = 0f;

            restartPos.X = 0f;
            restartPos.Y = GlobalConst.WindowHeight - 12 * 4;





            base.Initialize();
            
        }
        
        



        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameFont = Content.Load<SpriteFont>("Utskrift/GameFont2");

            // TODO: use this.Content to load your game content here
            coin = Content.Load<Texture2D>("Sprites/coin");
            player.myship = Content.Load<Texture2D>("Sprites/slime");
            player.playerShoot = Content.Load<Texture2D>("Sprites/SlimeShooting");
            GlobalConst.Enemy = Content.Load < Texture2D>("Sprites/tripod");
            bulletClass.bulletTexture = Content.Load < Texture2D>("Sprites/bullet");
            GlobalConst.BossTex = Content.Load<Texture2D>("Sprites/Boss");
            GlobalConst.BossShootTex = Content.Load<Texture2D>("Sprites/BossShoot");

            platform = new Texture2D(GraphicsDevice, 1, 1);
            platform.SetData(new Color[] { Color.Black });
            touchPlat = new Texture2D(GraphicsDevice, 1, 1);

            startbtn = new Texture2D(GraphicsDevice, 1, 1);
            startbtn.SetData(new Color[] { Color.White });

            playerTexture = new Texture2D(GraphicsDevice, 1, 1);
            playerTexture.SetData(new Color[] { Color.Red });

            healthBarTex = new Texture2D(GraphicsDevice, 1, 1);
            healthBarTex.SetData(new Color[] { Color.Red });

            deathBtn = new Texture2D(GraphicsDevice, 1, 1);
            deathBtn.SetData(new Color[] { Color.White });

        }



        public void BulletUpdate(GameTime gameTime)
        {
            bulletClass.bulletMethod(gameTime);
            bulletClass.BulletBondaryCheck();
        }

        public void PlattformSpawn()
        {
            plattformsClass.CheckColission();
            plattformsClass.Platform1();
            plattformsClass.Platform2();
            plattformsClass.Platform3();
            

        }
        
        public void BossMap()
        {
            BossClass.Initizlize();
        }

        protected override void Update(GameTime gameTime) 
        {
            
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            PlattformSpawn();
            player.KeyMovements(gameTime);
            player.Gravity(gameTime);
            

            BulletUpdate(gameTime);

            SceneChangeClass.ArrowSceneChange();

            EnemyClass.BoundaryCheckEn();
            EnemyClass.EnemySPeed();
            CheckCollisionEn();
            EnemyClass.CheckCollisionEn();
            StartButtonClass.MouseRec(mouse);
            healthClass.EnemyHit(gameTime);
            healthClass.HealthBar();

            BossMap();

            Cursor();
            player.playerRecUpdate();

            

            base.Update(gameTime);
        }

        
        
         
        public void CheckCollisionEn()
        {
            foreach (Vector2 enemy in GlobalConst.TripodPosList.ToList())
            {
                
                enemyRec = new Rectangle(Convert.ToInt32(enemy.X), Convert.ToInt32(enemy.Y), GlobalConst.Enemy.Width, GlobalConst.Enemy.Height);

                for(int i = 0; i < bulletClass.bulletsList.Count; i++)
                {
                    bulletRec = new Rectangle((int)bulletClass.bulletsList[i].X, (int)bulletClass.bulletsList[i].Y , bulletClass.bulletTexture.Width * 2, bulletClass.bulletTexture.Height * 2);
                    if (bulletRec.Intersects(enemyRec))
                    {

                        GlobalConst.TripodPosList.Remove(enemy);
                        GlobalConst.TripodSpeedList.Remove(enemy);
                        bulletClass.bulletsList.RemoveAt(i);
                        bulletClass.bulletSpeedList.RemoveAt(i);

                    }
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
                spriteBatch.DrawString(gameFont, "Enemy left: " + GlobalConst.TripodPosList.Count, new Vector2(10, 150), Color.White);
                spriteBatch.DrawString(gameFont, "Platform touch: " + plattformsClass.platformTouch + " Which Plat: " + GlobalConst.WhichPlat + " Snap Touch " + GlobalConst.SnapTouch, new Vector2(10, 170), Color.White);
                spriteBatch.DrawString(gameFont, "Health: " + GlobalConst.Health, new Vector2(10,190), Color.White);
                spriteBatch.DrawString(gameFont, "Hit timer: " + (int)GlobalConst.HitTimer, new Vector2(10, 210), Color.White);


            }
          
           
        }

        public void DeathScene()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(deathBtn, DeathBtnClass.Btn(), GlobalConst.StartButtonColor);
            spriteBatch.DrawString(gameFont, "RIP", new Vector2(GlobalConst.WindowWidth / 2 + 10, GlobalConst.WindowHeight / 3), Color.White);
            spriteBatch.DrawString(gameFont, "Play again", new Vector2((DeathBtnClass.Btn().X -5 + DeathBtnClass.Btn().Width / 2) - 10, (DeathBtnClass.Btn().Y + DeathBtnClass.Btn().Height / 2) - 10), Color.Black);
            DeathBtnClass.Interact(mouse);
        }

        public void StartScene()
        {

            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Draw(platform, plattformsClass.CreateRec(20, 20, 20, 20), Color.White);
            spriteBatch.Draw(startbtn, StartButtonClass.StartBtn(), GlobalConst.StartButtonColor);
            spriteBatch.DrawString(gameFont, "Play", new Vector2((StartButtonClass.StartBtn().X + StartButtonClass.StartBtn().Width / 2) - 10, (StartButtonClass.StartBtn().Y + StartButtonClass.StartBtn().Height / 2) - 10) , Color.Black);
            StartButtonClass.InteractBtn(mouse);

        }

        public void NextScene(int j)
        {


            if (GlobalConst.SpawnEnemyBool == true)
            {
                //error
                
                GlobalConst.PlayerPos = restartPos;
                EnemyClass.SpawnEnemy(j);
                player.IntiPlayerCont();
                GlobalConst.SpawnEnemyBool = false;
            }
            if(GlobalConst.TripodPosList.Count == 0)
            {
                LevelClear();
            }
            else
            {
                GraphicsDevice.Clear(backColor);

                spriteBatch.Draw(touchPlat, plattformsClass.Platform1(), backColor);
                spriteBatch.Draw(touchPlat, plattformsClass.Platform2(), backColor);
                spriteBatch.Draw(touchPlat, plattformsClass.Platform3(), backColor);
                spriteBatch.Draw(platform, plattformsClass.Platform1(), Color.Black);
                spriteBatch.Draw(platform, plattformsClass.Platform2(), Color.Black);
                spriteBatch.Draw(platform, plattformsClass.Platform3(), Color.Black);

                

                foreach (Vector2 cn in GlobalConst.TripodPosList)
                {
                    spriteBatch.Draw(GlobalConst.Enemy, cn, Color.White);
                }

                if (bulletClass.pressed == true)
                {
                    spriteBatch.Draw(player.playerShoot, GlobalConst.PlayerPos, null, Color.White, 0, origin, GlobalConst.SCALE, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(player.myship, GlobalConst.PlayerPos, null, Color.White, 0, origin, GlobalConst.SCALE, SpriteEffects.None, 0);
                }
               

                foreach (Vector2 bullets in bulletClass.bulletsList)
                {
                    spriteBatch.Draw(bulletClass.bulletTexture, bullets, null, Color.White, 0, origin, 2, SpriteEffects.None, 0);


                }
                spriteBatch.Draw(healthBarTex, healthClass.HealthBar(), Color.Red);
            }
            
        }

        public void LevelClear()
        {
            GraphicsDevice.Clear(Color.Black);
            //spriteBatch.Draw(platform, plattformsClass.CreateRec(20, 20, 20, 20), Color.White);
            spriteBatch.DrawString(gameFont, "Good Job", new Vector2(GlobalConst.WindowWidth / 2 + 10, GlobalConst.WindowHeight / 3), Color.White);
            spriteBatch.Draw(startbtn, StartButtonClass.StartBtn(), GlobalConst.StartButtonColor);
            spriteBatch.DrawString(gameFont, "Next", new Vector2((StartButtonClass.StartBtn().X + StartButtonClass.StartBtn().Width / 2) - 10, (StartButtonClass.StartBtn().Y + StartButtonClass.StartBtn().Height / 2) - 10), Color.Black);
            StartButtonClass.InteractBtn(mouse);
        }

        public void BossLevel()
        {
            GraphicsDevice.Clear(backColor);

            spriteBatch.Draw(touchPlat, plattformsClass.Platform1(), backColor);
            spriteBatch.Draw(touchPlat, plattformsClass.Platform2(), backColor);
            spriteBatch.Draw(touchPlat, plattformsClass.Platform3(), backColor);
            spriteBatch.Draw(platform, plattformsClass.Platform1(), Color.Black);
            spriteBatch.Draw(platform, plattformsClass.Platform2(), Color.Black);
            spriteBatch.Draw(platform, plattformsClass.Platform3(), Color.Black);

            spriteBatch.Draw(GlobalConst.BossTex, BossClass.bossVec, null, Color.White, 0, bossOrigin, 6, SpriteEffects.None, 0);
            foreach (Vector2 cn in GlobalConst.TripodPosList)
            {
                spriteBatch.Draw(GlobalConst.Enemy, cn, Color.White);
            }

            if (bulletClass.pressed == true)
            {
                spriteBatch.Draw(player.playerShoot, GlobalConst.PlayerPos, null, Color.White, 0, origin, GlobalConst.SCALE, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(player.myship, GlobalConst.PlayerPos, null, Color.White, 0, origin, GlobalConst.SCALE, SpriteEffects.None, 0);
            }


            foreach (Vector2 bullets in bulletClass.bulletsList)
            {
                spriteBatch.Draw(bulletClass.bulletTexture, bullets, null, Color.White, 0, origin, 2, SpriteEffects.None, 0);


            }
            spriteBatch.Draw(healthBarTex, healthClass.HealthBar(), Color.Red);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            
            
            // TODO: Add your drawing code here
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            

            if(GlobalConst.Health == 0)
            {
                GlobalConst.SeneStatus = -1;
                EnemyClass.DeleteEnemy();
                
            }


            if (GlobalConst.SeneStatus == 0)
            {
                StartScene();
                sceneName = "start";
            }
            else if ( GlobalConst.SeneStatus == 1)
            {
                
                NextScene(5);
                sceneName = "first";
            }
            else if (GlobalConst.SeneStatus == 2)
            {
                NextScene(10);
            }
            else if( GlobalConst.SeneStatus == 3)
            {
                BossLevel();
            }
            else if (GlobalConst.SeneStatus == -1)
            {
                
                DeathScene();
            }

            /*
            foreach (Vector2 cn in coin_pos_list)
            {
                spriteBatch.Draw(coin, cn, Color.White);
            }
            */

            //spriteBatch.Draw(platform, GlobalConst.RecPlayer, Color.Red);






           //spriteBatch.Draw(startbtn, GlobalConst.MouseRec, Color.OrangeRed);

            F3Info();
            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }




   
}


