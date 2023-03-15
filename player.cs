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



public class player : Game1
{
    public void Variable()
    {
        
    }
     
    Vector2 playerPos, playerSpeed, myshipSpeedDown;
    float gravity = 0.2f, gravitySpeed = 0f;
    string blockFile = "File", blockRead, place;
    int points;
    
    
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
        splitBlockStr = blockRead.Split(',');
        if (playerPos.Y == float.Parse(splitBlockStr[2]))
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


        if (playerPos.Y >= Window.ClientBounds.Height - myship.Height)
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
        else if (((playerPos.Y == Window.ClientBounds.Height - myship.Height) && keyboardState.IsKeyDown(Keys.W) == true) || (playerPos.Y == 0 && keyboardState.IsKeyDown(Keys.S) == true))
        {
            playerSpeed.Y = 4f;
        }
        else
        {
            points = 420;
            gravitySpeed = 7f;
            gravity = 0.89f;
        }

        if (((playerPos.X == Window.ClientBounds.Width - myship.Width) && keyboardState.IsKeyDown(Keys.D) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.A) == true))
        {
            playerSpeed.X = 0f;
        }
        else if (((playerPos.X == Window.ClientBounds.Width - myship.Width) && keyboardState.IsKeyDown(Keys.A) == true) || (playerPos.X == 0 && keyboardState.IsKeyDown(Keys.D) == true))
        {
            playerSpeed.X = 4f;
        }

        if (keyboardState.IsKeyDown(Keys.E) == true)
        {
            using (StreamWriter writefile = new StreamWriter(blockFile))
            {

                writefile.Write("false" + "," + splitBlockStr[1] + "," + splitBlockStr[2]);
                writefile.Close();
            }
        }
    }
}

