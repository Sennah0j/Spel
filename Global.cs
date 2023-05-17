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
public static class GlobalConst
{
    static string[] splitBlockStr;
    static int windowWidth, windowHeight, sceneStatus;
    static string blockFile = "File";
    static Vector2 playerPos;
    static float timeJump, gravity;
    static bool recTouch;
    static Rectangle recPlayer, startButton, mouseRec;
    static Texture2D myShip;

    
    public static Rectangle MouseRec
    {
        get { return mouseRec; }
        set { mouseRec = value; }
    }
    public static Rectangle StartButton
    {
        get { return startButton; }
        set { startButton = value; }
    }
    public static int SeneStatus
    {
        get { return sceneStatus; }
        set { sceneStatus = value; }
    }
    public static Texture2D MyShip
    {
        get { return myShip; }
        set { myShip = value; }
    }
    public static Rectangle RecPlayer
    {
        get { return recPlayer; }
        set { recPlayer = value; }
    }
    public static string[] Split
    {
        
        get { return splitBlockStr; }
        set { splitBlockStr = value; }
    }

    public static string BlockFile
    {
        get { return blockFile; }
        set { blockFile = value; }
    }

    public static float TimeJump
    {
        get { return timeJump; }
        set { timeJump = value; }
    }

    public static bool RecTouch
    {
        get { return recTouch; }
        set { recTouch = value; }
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
