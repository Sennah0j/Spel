using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;



public class Player
{
    KeyboardState keyboardState = Keyboard.GetState();

    Vector2 playerSpeed, myshipSpeedDown;
    public Vector2 playerPos;
    double mathPow;
    string blockRead, place;
    int points, countNum;
    float timeJump, gravity, gravitySpeed;
    public TimeSpan ElapsedGameTime { get; set; }

    public void Gravity(GameTime gameTime)
    {
        Global.PlayerPos = playerPos;

        timeJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.W) && (Global.Touch == true))
        {

            playerPos.Y -= Convert.ToSingle(Math.Pow(mathPow, 0.99));
            mathPow = Math.Pow(mathPow, 0.99);
        }

        if (keyboardState.IsKeyUp(Keys.W))
        {

            mathPow = 10;
            Global.Touch = false;
        }
        else if (timeJump >= 2)
        {

            Global.Touch = false;
        }
        playerPos.Y += (gravity * timeJump * gravitySpeed);
    }

    public void KeyMovements()
    {


        if (keyboardState.IsKeyDown(Keys.D))
            playerPos.X = Global.PlayerPos.X + playerSpeed.X;
        if (keyboardState.IsKeyDown(Keys.A))
            playerPos.X = Global.PlayerPos.X - playerSpeed.X;
        Global.PlayerPos = playerPos;

        //if (keyboardState.IsKeyDown(Keys.S))
        //myship_pos.Y = myship_pos.Y + myship_speed.Y;

        //blockRead = File.ReadAllText(blockFile);
        //Global.Split = blockRead.Split(',');
        /*if (playerPos.Y == float.Parse(Global.Split[2]))
        {

            gravity = 0f;
            points = 69;
            touch = true;
        }
        else
        {
            points = 420;


            gravity = 0.89f;
        }
        */

        if (Global.PlayerPos.Y >= Global.WindowHeight - Global.Myship.Height)
        {
            countNum = 0;
            playerSpeed.Y = 0f;
            myshipSpeedDown.Y = 0f;
            //gravitySpeed = 0f;
            Global.Gravity = 0f;
            Global.TimeJump = 0f;
            points = 69;

            Global.Touch = true;
        }

        else if (((Global.PlayerPos.Y == Global.WindowHeight - Global.Myship.Height) && keyboardState.IsKeyDown(Keys.W) == true) || (playerPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
        {
            playerSpeed.Y = 4f;
        }

        else
        {
            points = 420;
            //gravitySpeed = 7f;
            Global.Gravity = 0.89f;
        }

        if (((Global.PlayerPos.X == Global.WindowWidth - Global.Myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
        {
            playerSpeed.X = 0f;
        }
        else if (((Global.PlayerPos.X == Global.WindowWidth - Global.Myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
        {
            playerSpeed.X = 4f;
        }

        /*
        if (keyboardState.IsKeyDown(Keys.E) == true)
        {
            using (StreamWriter writefile = new StreamWriter(blockFile))
            {

                writefile.Write("false" + "," + Global.Split[1] + "," + Global.Split[2]);
                writefile.Close();
            }
        }
        */
    }
}

