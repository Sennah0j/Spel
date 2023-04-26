using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    public void Vector2Def()
	{
		
	}
	public void bulletMethod()
	{
        KeyboardState keyboardState = Keyboard.GetState();
        MouseState mouse = Mouse.GetState();

        if (mouse.LeftButton == ButtonState.Pressed)
		{
			bulletPos.X = playerClass.playerPos.X;
			bulletPos.Y = playerClass.playerPos.Y;
			bulletsList.Add(bulletPos);

			bulletSpeed.Y = ((mouse.Position.Y - bulletPos.Y) / (mouse.Position.X - bulletPos.X)) * bulletPos.X;
			bulletSpeed.X = 2f;
			bulletSpeedList.Add(bulletSpeed);
		}
		for (int i = 0; i < bulletsList.Count; i++)
		{

			tempBull.X = bulletsList.ElementAt(i).X;
			tempBull.Y = bulletsList.ElementAt(i).Y;

			tempBull.Y = tempBull.Y - bulletSpeed.Y;
		}
		
    }
}
