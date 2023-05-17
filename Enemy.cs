using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;


public class Enemy
{
    int points;
    
    public List<Vector2> tripodSpeedList = new List<Vector2>();
    public Vector2 tripod_pos, tripod_speed, tripodTest, tripod_pos2;
    public Texture2D tripod;

    public void SpawnEnemy()
    {
        Random slump = new Random();
        for (int i = 0; i < 5; i++)
        {
            tripod_pos.X = slump.Next(0, GlobalConst.WindowWidth - 50);
            tripod_pos.Y = slump.Next(0, GlobalConst.WindowHeight - 100);

            GlobalConst.TripodPosList.Add(tripod_pos);

            tripod_speed.X = slump.Next(-5, 5);
            tripod_speed.Y = slump.Next(-5, 5);
            tripodSpeedList.Add(tripod_speed);
        }


    }
    public void EnemySPeed()
    {
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            GlobalConst.TripodPosList[i] = GlobalConst.TripodPosList[i] + tripodSpeedList[i];
        }
    }
    public void EnemyRndLocation(int j)
    {
        Random slump = new Random();
        
        tripod_pos2.X = slump.Next(0, GlobalConst.WindowWidth - 50);
        tripod_pos2.Y = slump.Next(0, GlobalConst.WindowHeight - 400);

        GlobalConst.TripodPosList[j] = tripod_pos2;

       

    }

    public void BoundaryCheckEn()
    {
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            if (GlobalConst.TripodPosList[i].Y >= (GlobalConst.WindowHeight - tripod.Height) || GlobalConst.TripodPosList[i].Y <= 0 + tripod.Height)
            {
                tripodSpeedList[i] = -tripodSpeedList[i];
            }
        }
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            if (GlobalConst.TripodPosList[i].X >= (GlobalConst.WindowWidth - tripod.Width) || GlobalConst.TripodPosList[i].X <= 0 + tripod.Width)
            {
                tripodSpeedList[i] = -tripodSpeedList[i];
            }
        }
    }
    public void CheckCollisionEn()
    {
        
        
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            if (GlobalConst.TripodPosList[i].Y >= (GlobalConst.WindowHeight + tripod.Height))
            {
                points += 10;
            }
        }


    }
}
