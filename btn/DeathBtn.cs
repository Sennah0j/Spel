﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

public class DeathBtn
{
    MouseState mouseState = Mouse.GetState();
    public Rectangle deathBtn;
	public Rectangle Btn()
	{
        return deathBtn = new Rectangle((GlobalConst.WindowWidth / 2) - 75, (GlobalConst.WindowHeight / 2) - 35, 150, 70);
    }

    public void MouseRec(MouseState mouse)
    {
        GlobalConst.MouseRec = new Rectangle(mouse.X, mouse.Y, 10, 10);

    }

    public void Interact(MouseState mouse)
    {
        if (GlobalConst.MouseRec.Intersects(Btn()) && mouse.LeftButton == ButtonState.Pressed)
        {
            GlobalConst.DeathBtnPress = true;
            GlobalConst.SeneStatus = 0;
            GlobalConst.Health = 100;
        }

        if (GlobalConst.MouseRec.Intersects(Btn()))
        {
            GlobalConst.StartButtonColor = Color.White;
        }
        else
        {
            GlobalConst.StartButtonColor = Color.Gray;
        }
    }
}
