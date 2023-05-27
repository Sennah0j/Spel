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
    static double hitTimer;
    static int scale = 4; //1 = default, 2 = twice the size
    static string[] splitBlockStr;
    static int windowWidth, windowHeight, sceneStatus, health = 100;
    static string blockFile = "File", whichPlat;
    static Vector2 playerPos, restartPos, bossVec;
    static float timeJump, gravity;
    static bool recTouch, spawnEnemyBool, snapTouch = false, snapTouch2 = false, deathBtnPress, bossSpawn;
    static Rectangle recPlayer, startButton, mouseRec, plat1, plat2, plat3, bossRec;
    static Texture2D myShip, enemy, bossTex, bossShootTex, healthPack;
    static Color startButtonColor ;
    static List<Vector2> tripod_pos_list = new List<Vector2>();
    static List<Vector2> tripodSpeedList = new List<Vector2>();

    public static Texture2D HealthPack
    {
        get { return healthPack; }
        set { healthPack = value; }
    }
    public static bool BossSpawn
    {
        get { return bossSpawn; }
        set { bossSpawn = value; }
    }
    public static Vector2 BossVec
    {
        get { return bossVec; }
        set { bossVec = value; }
    }
    public static Rectangle BossRec
    {
        get { return bossRec; }
        set { bossRec = value; }
    }
    public static Texture2D BossTex
    {
        get { return bossTex; }
        set { bossTex = value; }
    }
    public static Texture2D BossShootTex
    {
        get { return bossShootTex; }
        set { bossShootTex = value; }
    }
    public static bool DeathBtnPress
    {
        get { return deathBtnPress; }
        set { deathBtnPress = value; }
    }
    public static double HitTimer
    {
        get { return hitTimer; }
        set { hitTimer = value; }
    }
    public static int Health
    {
        get { return health; }
        set { health = value; }
    }
    public static List<Vector2> TripodSpeedList
    {
        get { return tripodSpeedList; }
        set { tripodSpeedList = value; }
    }

    public static bool SnapTouch2
    {
        get { return snapTouch2; }
        set { snapTouch2 = value; }
    }
    public static string WhichPlat
    {
        get { return whichPlat; }
        set { whichPlat = value; }
    }
    public static Rectangle Plat3
    {
        get { return plat3; }
        set { plat3 = value; }
    }
    public static Rectangle Plat2
    {
        get { return plat2; }
        set { plat2 = value; }
    }
    public static Rectangle Plat1
    {
        get { return plat1; }
        set { plat1 = value; }
    }
    public static bool SnapTouch
    {
        get { return snapTouch; }
        set { snapTouch = value; }
    }
    public static bool SpawnEnemyBool
    {
        get { return spawnEnemyBool; }
        set { spawnEnemyBool = value; }
    }
    public static int SCALE
    {
        get { return scale; }
        set { scale = value; }
    }

    public static List<Vector2> TripodPosList
    {
        get { return tripod_pos_list; }
        set { tripod_pos_list = value; }
    }

    public static Color StartButtonColor
    {
        get { return startButtonColor; }
        set { startButtonColor = value; }
    }
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
    public static Texture2D Enemy
    {
        get { return enemy; }
        set { enemy = value; }
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
