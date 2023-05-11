using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


public class SceneChange
{
	public bool keyDown;
	public void ArrowSceneChange()
	{
        KeyboardState keyboardState = Keyboard.GetState();
		if(keyboardState.IsKeyDown(Keys.Right) && keyDown == false) 
		{
			GlobalConst.SeneStatus++;
			keyDown = true;

        }
		else if (keyboardState.IsKeyDown(Keys.Left) && keyDown == false)
		{
			GlobalConst.SeneStatus--;
			keyDown = true;
		}
		
		else if (keyboardState.IsKeyDown(Keys.Up)) 
		{
			keyDown = false;
		}
		if( GlobalConst.SeneStatus < 0 ) 
			GlobalConst.SeneStatus = 0;
		else if ( GlobalConst.SeneStatus > 3 )
			GlobalConst.SeneStatus = 3;
    }
}
