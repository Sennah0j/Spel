using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;


public class Health
{
    Rectangle enemyRec, healthBar;
    double hitTimmer = 0;
    bool hitTruFalse;
    public void EnemyHit(GameTime gameTime)
    {
        
        foreach (Vector2 enemy in GlobalConst.TripodPosList.ToList())
        {
            enemyRec = new Rectangle(Convert.ToInt32(enemy.X), Convert.ToInt32(enemy.Y), GlobalConst.Enemy.Width, GlobalConst.Enemy.Height);
            if (enemyRec.Intersects(GlobalConst.RecPlayer) && (gameTime.TotalGameTime.TotalSeconds > hitTimmer + 2))
            {
                GlobalConst.Health -= 20;
                hitTimmer = gameTime.TotalGameTime.TotalSeconds;
                
                
            }
            else if (!enemyRec.Intersects(GlobalConst.RecPlayer))
            {
                hitTruFalse = false;
            }
            
        }
        
        GlobalConst.HitTimer = (hitTimmer + 4) - gameTime.TotalGameTime.TotalSeconds;
        
        
    }

    public Rectangle HealthBar()
    {
        return healthBar = new Rectangle(10, GlobalConst.WindowHeight - (GlobalConst.Health * 2 + 10), 20, +GlobalConst.Health * 2);
    }
}
