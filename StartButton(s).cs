using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class StartButton
{
    MouseState mouseState = Mouse.GetState();
	Rectangle mouseRec;
    public Rectangle StartBtn()
	{
		return  new Rectangle(875, 450, 150, 70);
	}

	public void MouseRec(MouseState mouse)
	{
        GlobalConst.MouseRec = new Rectangle(mouse.X, mouse.Y, 5, 5);

    }
	public void InteractBtn(MouseState mouse)
	{
		if (GlobalConst.MouseRec.Intersects(StartBtn()) && mouse.LeftButton == ButtonState.Pressed)
		{
			GlobalConst.SeneStatus = 1;
		}
	}
}
