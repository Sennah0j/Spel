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
    public List<Vector2> tripod_pos_list = new List<Vector2>();
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

            tripod_pos_list.Add(tripod_pos);

            tripod_speed.X = slump.Next(-5, 5);
            tripod_speed.Y = slump.Next(-5, 5);
            tripodSpeedList.Add(tripod_speed);
        }


    }
    public void EnemySPeed()
    {
        for (int i = 0; i < tripod_pos_list.Count; i++)
        {
            tripod_pos_list[i] = tripod_pos_list[i] + tripodSpeedList[i];
        }
    }
    public void EnemyRndLocation(int j)
    {
        Random slump = new Random();
        for (int i = 0; i < 5; i++)
        {
            tripod_pos2.X = slump.Next(0, GlobalConst.WindowWidth - 50);
            tripod_pos2.Y = slump.Next(0, GlobalConst.WindowHeight - 400);

            tripod_pos_list[j] = tripod_pos2;
        }
    }
    public void BoundaryCheckEn()
    {
        for (int i = 0; i < tripod_pos_list.Count; i++)
        {
            if (tripod_pos_list[i].Y >= (GlobalConst.WindowHeight + tripod.Height))
            {
                EnemyRndLocation(i);
            }
        }
    }
    public void CheckCollisionEn()
    {
        
        
        for (int i = 0; i < tripod_pos_list.Count; i++)
        {
            if (tripod_pos_list[i].Y >= (GlobalConst.WindowHeight + tripod.Height))
            {
                points += 10;
            }
        }


    }
}
