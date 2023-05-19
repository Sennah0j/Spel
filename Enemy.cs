﻿using Microsoft.Xna.Framework;
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
    
    
    public Vector2 tripod_pos, tripod_speed, tripodTest, tripod_pos2;
    public Texture2D tripod;

    public void SpawnEnemy(int j)
    {
        Random slump = new Random();
        for (int i = 0; i < j; i++)
        {
            tripod_pos.X = slump.Next(0, GlobalConst.WindowWidth - 50);
            tripod_pos.Y = slump.Next(0, GlobalConst.WindowHeight - 100);

            GlobalConst.TripodPosList.Add(tripod_pos);

            tripod_speed.X = slump.Next(-5, 5);
            tripod_speed.Y = slump.Next(-5, 5);
            GlobalConst.TripodSpeedList.Add(tripod_speed);
        }


    }
    public void EnemySPeed()
    {
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            GlobalConst.TripodPosList[i] = GlobalConst.TripodPosList[i] + GlobalConst.TripodSpeedList[i];
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
            if (GlobalConst.TripodPosList[i].Y >= (GlobalConst.WindowHeight - GlobalConst.Enemy.Height) || GlobalConst.TripodPosList[i].Y <= 0 + GlobalConst.Enemy.Height)
            {
                GlobalConst.TripodSpeedList[i] = -GlobalConst.TripodSpeedList[i];
            }
        }
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            if (GlobalConst.TripodPosList[i].X >= (GlobalConst.WindowWidth - GlobalConst.Enemy.Width) || GlobalConst.TripodPosList[i].X <= 0 + GlobalConst.Enemy.Width)
            {
                GlobalConst.TripodSpeedList[i] = -GlobalConst.TripodSpeedList[i];
            }
        }
    }
    public void CheckCollisionEn()
    {
        
        
        for (int i = 0; i < GlobalConst.TripodPosList.Count; i++)
        {
            if (GlobalConst.TripodPosList[i].Y >= (GlobalConst.WindowHeight + GlobalConst.Enemy.Height))
            {
                points += 10;
            }
        }


    }
}
