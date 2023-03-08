using Pong;
using System;
using System.Numerics;

public class Global
{
    
    static Vector2 playerPos;
    playerPos.X = 10;
    public static Vector2 PlayerPos
    {
        get { return playerPos; }
        set { playerPos = value; }
    }
}
