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
	public Texture2D bulletTexture;
	Vector2 bulletPos;
	Vector2 bulletSpeed;
	public List<Vector2> bulletsList = new List<Vector2>();
	List<Vector2> bulletSpeedList = new List<Vector2>();
	Rectangle bulletRec;
    Vector2 tempBull;
	Vector2 tempBullSpeed;

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
        double timeSinceLastBullet= 0;

        double totalSpeed = 10, angle;

        if ((mouse.LeftButton == ButtonState.Pressed) && (gameTime.TotalGameTime.TotalMilliseconds > (timeSinceLastBullet + 600)))
		{


			bulletPos = GlobalConst.PlayerPos;

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

            bulletsList.Add(bulletPos);
            bulletSpeedList.Add(bulletSpeed);

            timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
        }

        for (int i = 0; i < bulletsList.Count; i++)
        {

            tempBull.X = bulletsList.ElementAt(i).X;
            tempBull.Y = bulletsList.ElementAt(i).Y;

            tempBull.Y = tempBull.Y - bulletSpeedList[i].Y;
            tempBull.X = tempBull.X - bulletSpeedList[i].X;

            bulletsList[i] = tempBull;

        }


    }

	public void BulletSpeedUp()
	{
        
    }
}
