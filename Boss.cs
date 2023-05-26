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
	public Vector2 bossVec;

	public void Initizlize()
	{
		bossVec.X = GlobalConst.WindowWidth / 2 + (GlobalConst.BossRec.Width / 2);
		bossVec.Y = GlobalConst.WindowHeight - GlobalConst.BossRec.Height;

		GlobalConst.BossVec = bossVec;
	}
	public void Class1()
	{
		GlobalConst.BossRec = new Rectangle(GlobalConst.BossVec.ToPoint(), new Point(GlobalConst.BossTex.Width * 6, GlobalConst.BossTex.Height * 6));
	}
}
