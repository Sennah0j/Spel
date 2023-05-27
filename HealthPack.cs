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
	Vector2 packPos;
	public List<Vector2> packPosList = new List<Vector2>();
	public void Drop(Vector2 spawnPos)
	{
		packPos.X = spawnPos.X;
		packPos.Y = spawnPos.Y;

        packPosList.Add(packPos);
	}
}
