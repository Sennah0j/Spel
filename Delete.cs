using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

public class DeleteClass
{
	public void DeleteMethod()
	{
        foreach (Vector2 enemy in GlobalConst.TripodPosList.ToList())
        {

            GlobalConst.TripodPosList.Remove(enemy);
            GlobalConst.TripodSpeedList.Remove(enemy);
        }
        foreach(Vector2 pack in GlobalConst.PackPosList.ToList() )
        {
            GlobalConst.PackPosList.Remove(pack);
        }

    }
}
