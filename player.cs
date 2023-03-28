using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
public class Player
{

    
    public Vector2 playerSpeed, myshipSpeedDown;
    public Vector2 playerPos;
    public Texture2D myship;
    public double mathPow = 1;
    string blockRead, place;
    public string testStr, blockFile = "File";
    int points, countNum;
    public float timeJump, gravity, gravitySpeed;
    public bool touch;
    //public TimeSpan ElapsedGameTime { get; set; }

    public void Gravity(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        timeJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (keyboardState.IsKeyDown(Keys.W) && (touch == true))
        {

            playerPos.Y -= Convert.ToSingle(Math.Pow(mathPow, 0.99));
            mathPow = Math.Pow(mathPow, 0.99);
        }

        if (keyboardState.IsKeyUp(Keys.W))
        {

            mathPow = 10;
            touch = false;
        }
        else if (timeJump >= 2)
        {
            touch = false;
        }
        playerPos.Y = (gravity * timeJump * gravitySpeed) + playerPos.Y;

    }
    public void KeyMovements()
    {
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.D))
            playerPos.X = playerPos.X + playerSpeed.X;
        if (keyboardState.IsKeyDown(Keys.A))
            playerPos.X = playerPos.X - playerSpeed.X;

        //if (keyboardState.IsKeyDown(Keys.S))
        //myship_pos.Y = myship_pos.Y + myship_speed.Y;

        blockRead = File.ReadAllText(blockFile);
        Global.Split = blockRead.Split(',');
        if (playerPos.Y == float.Parse(Global.Split[2]))
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
        

        if (playerPos.Y >= Global.WindowHeight - myship.Height)
        {
            countNum = 0;
            playerSpeed.Y = 0f;
            myshipSpeedDown.Y = 0f;
            gravitySpeed = 0f;
            gravity = 0f;
            timeJump = 0f;
            points = 69;

            touch = true;
        }
        else if (((playerPos.Y == Global.WindowHeight - myship.Height) && keyboardState.IsKeyDown(Keys.W) == true) || (playerPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
        {
            playerSpeed.Y = 4f;
        }
        else
        {
            points = 420;
            gravitySpeed = 7f;
            gravity = 0.89f;
        }
        if (((playerPos.X == Global.WindowWidth - myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
        {
            playerSpeed.X = 0f;
        }
        else if (((playerPos.X == Global.WindowWidth - myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
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
