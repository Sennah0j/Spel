using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Plattforms
{
    int size;
    Rectangle platform1, platform2, platform3, PlayRec;
    string blockRead;
    List<Rectangle> rectanglesList = new List<Rectangle>();
    public bool recTouch = false, platformTouch = false;
    Vector2 playerPos;

    public Rectangle CreateRec(int X, int Y, int wide, int thic)
    {
        return PlayRec = new Rectangle(X, Y, wide, thic);
    }
    public Rectangle Platform1()
    {
        if (!File.Exists(GlobalConst.BlockFile))
        {
            using (StreamWriter writeFile = new StreamWriter(GlobalConst.BlockFile))
                writeFile.WriteLine(("200,345,400,10"));
        }
        blockRead = File.ReadAllText(GlobalConst.BlockFile);
        GlobalConst.Split = blockRead.Split(",");

        //                   X   Y   Xlång   Ytjock
        return platform1 = new Rectangle(GlobalConst.WindowWidth - 700, GlobalConst.WindowHeight / 2, 550, 10);

    }

    public Rectangle Platform2()
    {
        return platform2 = new Rectangle(400, GlobalConst.WindowHeight / 2, 550, 10);
    }
    public Rectangle Platform3()
    {
        return platform3 = new Rectangle((GlobalConst.WindowWidth / 2) - 400, (GlobalConst.WindowHeight / 15) * 11, 800, 10);
    }
    public Rectangle Platform4()
    {

        blockRead = File.ReadAllText(GlobalConst.BlockFile);
        GlobalConst.Split = blockRead.Split(",");
        //                   X   Y   Xlång   Ytjock
        return platform1 = new Rectangle(700, 1200, 550, 10);

    }
    public Rectangle Platform5()
    {
        return platform2 = new Rectangle(200, 1200, 400, 10);
    }
    public Rectangle Platform6()
    {
        return platform3 = new Rectangle(470, 170, 230, 50);
    }

    public bool CheckColissionPlat1()
    {

        if (GlobalConst.RecPlayer.Intersects(platform1))
        {
            GlobalConst.RecTouch = true;


            platformTouch = true;
            return true;
        }
        else
        {
            GlobalConst.RecTouch = false;
            platformTouch = false;
            return false;
        }

    }
    public bool CheckColissionPlat2()
    {
        if (GlobalConst.RecPlayer.Intersects(platform2))
        {
            GlobalConst.RecTouch = true;
            platformTouch = true;
            return true;
        }
        else
        {
            GlobalConst.RecTouch = false;
            platformTouch = false;
            return false;
        }
    }
    public bool CheckColissionPlat3()
    {
        if (GlobalConst.RecPlayer.Intersects(platform3))
        {
            GlobalConst.RecTouch = true;
            platformTouch = true;
            return true;
        }
        else
        {
            GlobalConst.RecTouch = false;
            platformTouch = false;
            return false;
        }

    }
}




    
