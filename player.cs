using Microsoft.Xna.Framework.Input;
using Pong;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;



public class player : Game
{

     
   Vector2 playerPos, playerSpeed, myshipSpeedDown;
    
   string blockRead, place;
   int points, countNum;
        
    
    
    public void KeyMovements()
    {
        
        KeyboardState keyboardState = Keyboard.GetState();
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

        if (Global.PlayerPos.Y >= Window.ClientBounds.Height - Global.Myship.Height)
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
        else if (((Global.PlayerPos.Y == Window.ClientBounds.Height - Global.Myship.Height) && keyboardState.IsKeyDown(Keys.W) == true) || (playerPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
        {
            playerSpeed.Y = 4f;
        }
        else
        {
            points = 420;
            //gravitySpeed = 7f;
            Global.Gravity = 0.89f;
        }

        if (((Global.PlayerPos.X == Window.ClientBounds.Width - Global.Myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
        {
            playerSpeed.X = 0f;
        }
        else if (((Global.PlayerPos.X == Window.ClientBounds.Width - Global.Myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
        {
            playerSpeed.X = 4f;
        }

        /*if (keyboardState.IsKeyDown(Keys.E) == true)
        {
            using (StreamWriter writefile = new StreamWriter(blockFile))
            {

                writefile.Write("false" + "," + Global.Split[1] + "," + Global.Split[2]);
                writefile.Close();
            }
        }*/
    }
}

