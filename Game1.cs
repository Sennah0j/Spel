using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D myship;
        Vector2 myship_pos;
        Vector2 myship_speed;
        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            myship_pos.X = 100;
            myship_pos.Y = 100;
            myship_speed.X = 2.5f;
            myship_speed.Y = 2.5f;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            myship = Content.Load<Texture2D>("Sprites/ship");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            /*
            if (myship_pos.Y == Window.ClientBounds.Height || myship_pos.Y == 0)
            {
                if (myship_speed.Y == -2.5f)
                {
                    myship_speed.Y = 2.5f;
                }
                else
                {
                    myship_speed.Y = -2.5f;
                }
            }

            if (myship_pos.X == Window.ClientBounds.Width || myship_pos.X == 0)
            {
                if (myship_speed.X == -2.5f)
                {
                    myship_speed.X = 2.5f;
                }
                else
                {
                    myship_speed.X = -2.5f;
                }
                
            }
            myship_pos = myship_pos + myship_speed;
            */
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Right))
                myship_pos.X = myship_pos.X + myship_speed.X;
            if (keyboardState.IsKeyDown(Keys.Left))
                myship_pos.X = myship_pos.X - myship_speed.X;

            if (keyboardState.IsKeyDown(Keys.Down))
                myship_pos.Y = myship_pos.Y + myship_speed.Y;
            if (keyboardState.IsKeyDown(Keys.Up))
                myship_pos.Y = myship_pos.Y - myship_speed.Y;




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(myship, myship_pos, Color.White);
            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}