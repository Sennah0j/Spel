using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;

public class StartButton
{
    MouseState mouseState = Mouse.GetState();
	public Rectangle mouseRec, startBtn;
	Enemy EnemyClass = new Enemy();
	Player player = new Player();
	Boss BossClass = new Boss();
    public Rectangle StartBtn()
	{
		return startBtn = new Rectangle((GlobalConst.WindowWidth / 2) - 75, (GlobalConst.WindowHeight / 2) - 35, 150, 70);
	}

	public void MouseRec(MouseState mouse)
	{
        GlobalConst.MouseRec = new Rectangle(mouse.X, mouse.Y, 10, 10);

    }
	public void InteractBtn(MouseState mouse)
	{
		if (GlobalConst.MouseRec.Intersects(StartBtn()) && mouse.LeftButton == ButtonState.Pressed && GlobalConst.DeathBtnPress == false)
		{
			if (GlobalConst.SeneStatus == 0)
			{
                GlobalConst.SeneStatus = 1;
				GlobalConst.Health = 100;
                GlobalConst.SpawnEnemyBool = true;
            }
				


			else if (GlobalConst.SeneStatus == 1)	
			{
				GlobalConst.SeneStatus = 2;
                GlobalConst.SpawnEnemyBool = true;
            }
            

            else if (GlobalConst.SeneStatus == 2)
			{
                GlobalConst.SeneStatus = 3;
                BossClass.Initizlize();
                GlobalConst.BossHealth = 200;
            }

			else if (GlobalConst.SeneStatus == 3) 
			{
				GlobalConst.SeneStatus = 0;
				GlobalConst.DeathBtnPress = true;

            }
				
			

            
        }
		else if(mouse.LeftButton == ButtonState.Released)
		{
			GlobalConst.DeathBtnPress = false;
		}

		if(GlobalConst.MouseRec.Intersects(StartBtn()))
		{
			GlobalConst.StartButtonColor = Color.White;
		}
		else
		{
            GlobalConst.StartButtonColor = Color.Gray;
        }
	}
}
