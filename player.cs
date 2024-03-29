﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Threading;
using static System.Formats.Asn1.AsnWriter;


public class Player
{
    
    public Vector2 playerSpeed, myshipSpeedDown, restartPos;
    public Rectangle recMyShip;
    public Vector2 playerPos;
    public Texture2D myship, playerShoot;
    public double mathPow = 1;
    string blockRead, place;
    public string testStr, blockFile = "File";
    int points, countNum;
    public float timeJump, gravity = 0.2f, gravitySpeed;
    public bool touch;


    
    
    Plattforms plattformsClass = new Plattforms();
    //public TimeSpan ElapsedGameTime { get; set; }

    public float PlayerX()
    {
        return (playerPos.X);
        restartPos.X = 0f;
    }
    public float playerY()
    {
        return (playerPos.Y);
    }
    
    public void playerRecUpdate()
    {
        GlobalConst.RecPlayer = new Rectangle(playerPos.ToPoint(), new Point(myship.Width * GlobalConst.SCALE, myship.Height * GlobalConst.SCALE));
        GlobalConst.PlayerRecPlat = new Rectangle((int)playerPos.X, (int)playerPos.Y + myship.Height * GlobalConst.SCALE, myship.Width * GlobalConst.SCALE, 1);
    }
    public void IntiPlayerCont()
    {

        myshipSpeedDown.Y = 4f;
        playerPos.X = 0 + myship.Width * 4;
        playerPos.Y = GlobalConst.WindowHeight - myship.Height * 4;
        playerSpeed.X = 4f;
        playerSpeed.Y = 4f;
        GlobalConst.MyShip = myship;
    } 
    public void Gravity(GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        timeJump += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if ((keyboardState.IsKeyDown(Keys.W)) && (touch == true))
        {
            mathPow = Math.Pow(mathPow, 0.99999999);
            playerPos.Y -= Convert.ToSingle(Math.Pow(mathPow, 0.999999999999));
            
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

        GlobalConst.PlayerPos = playerPos;

    }

    
    public void KeyMovements(GameTime gameTime)
    {
       playerPos = GlobalConst.PlayerPos;
       KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.D))
            playerPos.X = (playerPos.X + playerSpeed.X);
        if (keyboardState.IsKeyDown(Keys.A))
            playerPos.X = (playerPos.X - playerSpeed.X);

        //if (keyboardState.IsKeyDown(Keys.S))
        //myship_pos.Y = myship_pos.Y + myship_speed.Y;
        
        blockRead = File.ReadAllText(blockFile);
        GlobalConst.Split = blockRead.Split(',');
        
        if (playerPos.Y == float.Parse(GlobalConst.Split[2]))
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





        if ((playerPos.Y >= GlobalConst.WindowHeight - myship.Height * 4))
        {
            


            if (GlobalConst.SnapTouch == false)
            {
                playerPos.Y = GlobalConst.WindowHeight - myship.Height * 4;

                //GlobalConst.PlayerPos = playerPos;
                GlobalConst.SnapTouch = true;
            }

            GlobalConst.RecTouch = true;
           
        }
        if (GlobalConst.RecTouch)
        {
            countNum = 0;
            playerSpeed.Y = 0f;
            myshipSpeedDown.Y = 0f;
            gravitySpeed = 0f;
            gravity = 0f;
            timeJump = 0f;
            touch = true;
        }

        //                                                                 SCALE
        else if (((playerPos.Y == GlobalConst.WindowHeight - myship.Height * 4) && keyboardState.IsKeyDown(Keys.W) == true) || (playerPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true) )
        {
            
            playerSpeed.Y = 4f;
           
        }
        
        else if (plattformsClass.CheckColission())
        {

        }

        else
        {
            GlobalConst.SnapTouch = false;
            points = 420;
            gravitySpeed = 7f;
            gravity = 0.89f;
            
        }
        if (((playerPos.X == GlobalConst.WindowWidth - myship.Width * 4) && keyboardState.IsKeyDown(Keys.D) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
        {
            
            playerSpeed.X = 0f;
        }
        else if (((playerPos.X == GlobalConst.WindowWidth - myship.Width * 4) && keyboardState.IsKeyDown(Keys.A) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
        {
            
            playerSpeed.X = 4f;
        }
        
        if (keyboardState.IsKeyDown(Keys.E) == true)
        {
            using (StreamWriter writefile = new StreamWriter(blockFile))
            {

                writefile.Write("false" + "," + GlobalConst.Split[1] + "," + GlobalConst.Split[2]);
                writefile.Close();
            }
        }

        GlobalConst.PlayerPos = playerPos;
    }
}
