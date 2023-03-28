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
public static class Global
{
    static string[] splitBlockStr;
    static int windowWidth, windowHeight;
   
    static Vector2 playerPos;
    static float timeJump, gravity;
    static bool touch;


    public static string[] Split
    {
        
        get { return splitBlockStr; }
        set { splitBlockStr = value; }
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

    public static int WindowWidth
    {
        get { return windowWidth; }
        set { windowWidth = value; }
    }

    public static int WindowHeight
    {
        get { return windowHeight; }
        set { windowHeight = value; }
    }
}
