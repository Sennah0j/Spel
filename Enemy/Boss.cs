using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Boss
{
    Random slump = new Random();
    public List<Vector2> bossbulletSpeedList = new List<Vector2>();
    public Vector2 bossVec, bossBulletPos, bossBulletSpeed, tempBull;
    public Rectangle bulletRec;
    double timeSinceLastBullet, angle, totalSpeed = 10;

	public void Initizlize()
	{
		//bossVec.X = GlobalConst.WindowWidth / 2 - ((GlobalConst.BossTex.Width * 12) / 2);
		//bossVec.Y = GlobalConst.WindowHeight - GlobalConst.BossTex.Height * 12;
        bossVec.X = slump.Next(0,GlobalConst.WindowWidth - GlobalConst.BossTex.Width * 12);
        bossVec.Y = slump.Next(0, GlobalConst.WindowHeight - GlobalConst.BossTex.Height * 12);

        GlobalConst.BossVec = bossVec;
	}
	public void BossRecMeth()
	{
		GlobalConst.BossRec = new Rectangle(GlobalConst.BossVec.ToPoint(), new Point(GlobalConst.BossTex.Width * 12, GlobalConst.BossTex.Height * 12));
	}

	public void Shooting(GameTime gameTime)
	{
        
        if ((gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 290))
        {
            bossBulletPos.X = ((GlobalConst.BossVec.X) + ((GlobalConst.BossTex.Width)/ 2) * 12);
            bossBulletPos.Y = (GlobalConst.BossVec.Y + ((GlobalConst.BossTex.Height / 2)* 12));

            angle = Math.Atan((GlobalConst.PlayerPos.Y - bossBulletPos.Y) / (GlobalConst.PlayerPos.X - bossBulletPos.X));

            if (bossBulletPos.X > (GlobalConst.PlayerPos.X))
            {
                bossBulletSpeed.X = (float)Math.Round(Math.Cos(angle) * totalSpeed, 7);
                bossBulletSpeed.Y = (float)Math.Round(Math.Sin(angle) * totalSpeed, 7);
            }
            else
            {
                bossBulletSpeed.X = (float)Math.Round(Math.Cos(angle + Math.PI) * totalSpeed, 7);
                bossBulletSpeed.Y = (float)Math.Round(Math.Sin(angle + Math.PI) * totalSpeed, 7);
            }

            GlobalConst.BossBulletPos.Add(bossBulletPos);
            bossbulletSpeedList.Add(bossBulletSpeed);

            timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
            
        }

        for (int i = 0; i < GlobalConst.BossBulletPos.Count; i++)
        {

            tempBull.X = GlobalConst.BossBulletPos.ElementAt(i).X;
            tempBull.Y = GlobalConst.BossBulletPos.ElementAt(i).Y;

            tempBull.Y = tempBull.Y - bossbulletSpeedList[i].Y;
            tempBull.X = tempBull.X - bossbulletSpeedList[i].X;

            GlobalConst.BossBulletPos[i] = tempBull;

        }
    }

    public void BossBulletBoundary()
    {
        
        for (int i = 0; i < GlobalConst.BossBulletPos.Count; i++)
        {
            if (GlobalConst.BossBulletPos.ElementAt(i).X <= 0 || GlobalConst.BossBulletPos.ElementAt(i).X >= GlobalConst.WindowWidth)
            {
                GlobalConst.BossBulletPos.RemoveAt(i);
                bossbulletSpeedList.RemoveAt(i);
            }
        }

        for (int i = 0; i < GlobalConst.BossBulletPos.Count; i++)
        {
            if ((GlobalConst.BossBulletPos.ElementAt(i).Y <= 0) || (GlobalConst.BossBulletPos.ElementAt(i).Y >= GlobalConst.WindowHeight))
            {
                GlobalConst.BossBulletPos.RemoveAt(i);
                bossbulletSpeedList.RemoveAt(i);
            }
        }

        
    }

    public void BossHealthCheck()
    {
        for(int i = 0; i < GlobalConst.BulletsList.Count; i++) 
        {
            bulletRec = new Rectangle((int)GlobalConst.BulletsList[i].X, (int)GlobalConst.BulletsList[i].Y, GlobalConst.BulletTexture.Width, GlobalConst.BulletTexture.Height);

            if (bulletRec.Intersects(GlobalConst.BossRec))
            {
                GlobalConst.BossHealth -= 10;
                GlobalConst.BulletsList.RemoveAt(i);
                GlobalConst.BulletSpeedList.RemoveAt(i);
            }
        }
    }

}
