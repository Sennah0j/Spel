using Pong;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
public class Global
{
    static string[] splitBlockStr;
    static Texture2D myship;
    static Vector2 playerPos;
    static float timeJump, gravity;
    static bool touch;

    public static string[] Split
    {
        
        get { return splitBlockStr; }
        set { splitBlockStr = value; }
    }

    public static Texture2D Myship
    {
        get { return myship; }
        set { myship = value; }
    }

    public static float TimeJump
    {
        get { return timeJump; }
        set { timeJump = value; }
    }

    public static bool Touch
    {
        get { return touch; }
        set { touch = value; }
    }

    public static float Gravity
    {
        get { return gravity; }
        set { gravity = value; }
    }

    public static Vector2 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
   
}
