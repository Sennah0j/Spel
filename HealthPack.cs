using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class Pack
{
	Rectangle HealthRec;
	Vector2 packPos;
	
	public void Drop(Vector2 spawnPos)
	{
		packPos.X = spawnPos.X;
		packPos.Y = spawnPos.Y;

        GlobalConst.PackPosList.Add(packPos);
	}

	public void TouchCheck()
	{
		foreach(Vector2 pack in GlobalConst.PackPosList.ToList()) 
		{
			HealthRec = new Rectangle((int)pack.X, (int)pack.Y, GlobalConst.HealthPack.Width, GlobalConst.HealthPack.Height);

            if (GlobalConst.RecPlayer.Intersects(HealthRec))
			{
                GlobalConst.PackPosList.Remove(pack);
				if(!(GlobalConst.Health == 100))
                    GlobalConst.Health += 20;
				else if(!(GlobalConst.Health  > 100))
					GlobalConst.Health = 100;
            }
		}
	}
}
