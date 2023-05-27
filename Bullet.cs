using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;

public class Bullet
{
	Player playerClass = new Player();
	
	Vector2 bulletPos;
	Vector2 bulletSpeed;
	
	Rectangle bulletRec;
    Vector2 tempBull;
	Vector2 tempBullSpeed;
    public  bool pressed = false;
    double timeSinceLastBullet = 0;
    public void Vector2Def()
	{
	

    }
	public void bulletMethod(GameTime gameTime)
	{
        bulletPos = GlobalConst.PlayerPos;
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();
        tempBullSpeed.Y = 2f;
        tempBullSpeed.X = 2f;
        
        

        double totalSpeed = 10, angle;

        if ((mouse.LeftButton == ButtonState.Pressed) && (pressed == false) && (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet+ 500))
		{

            

            bulletPos.X = ((GlobalConst.PlayerPos.X) + (GlobalConst.RecPlayer.Width / 2));
            bulletPos.Y = (GlobalConst.PlayerPos.Y + (GlobalConst.RecPlayer.Height / 2));

            angle = Math.Atan((mouse.Position.Y - bulletPos.Y) / (mouse.Position.X - bulletPos.X));

            if (bulletPos.X > (mouse.Position.X))
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle) * totalSpeed, 7);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle) * totalSpeed, 7);
            }
            else
            {
                bulletSpeed.X = (float)Math.Round(Math.Cos(angle + Math.PI) * totalSpeed, 7);
                bulletSpeed.Y = (float)Math.Round(Math.Sin(angle + Math.PI) * totalSpeed, 7);
            }

            GlobalConst.BulletsList.Add(bulletPos);
            GlobalConst.BulletSpeedList.Add(bulletSpeed);

            timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
            pressed = true;
        }

        if(mouse.LeftButton == ButtonState.Released)
        {
            pressed = false;
        }

        for (int i = 0; i < GlobalConst.BulletsList.Count; i++)
        {

            tempBull.X = GlobalConst.BulletsList.ElementAt(i).X;
            tempBull.Y = GlobalConst.BulletsList.ElementAt(i).Y;

            tempBull.Y = tempBull.Y - GlobalConst.BulletSpeedList[i].Y;
            tempBull.X = tempBull.X - GlobalConst.BulletSpeedList[i].X;

            GlobalConst.BulletsList[i] = tempBull;

        }


    }

	public void BulletBondaryCheck()
	{
        for(int i = 0; i < GlobalConst.BulletsList.Count; i++)
        {
            if (GlobalConst.BulletsList.ElementAt(i).X <= 0 || GlobalConst.BulletsList.ElementAt(i).X >= GlobalConst.WindowWidth)
            {
                GlobalConst.BulletsList.RemoveAt(i);
                GlobalConst.BulletSpeedList.RemoveAt(i);
            }
        }    

        for(int i = 0; i < GlobalConst.BulletsList.Count; i++)
        {
            if ((GlobalConst.BulletsList.ElementAt(i).Y <= 0) || (GlobalConst.BulletsList.ElementAt(i).Y >= GlobalConst.WindowHeight))
            {
                GlobalConst.BulletsList.RemoveAt(i);
                GlobalConst.BulletSpeedList.RemoveAt(i);
            }
        }
        
    }
}
