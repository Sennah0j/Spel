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
    string blockRead;
    Player player = new Player();
    
    public  Rectangle Platform1()
    {

        blockRead = File.ReadAllText(GlobalConst.BlockFile);
        GlobalConst.Split = blockRead.Split(",");
        //                   X   Y   Xlång   Ytjock
        return new Rectangle(200, 350, 400, 50);
        
    }
    
    public Rectangle Platform2()
    {
        return new Rectangle(70 ,170, 230, 50);
    }
    public Rectangle Platform3()
    {
        return new Rectangle(470, 170, 230, 50);
    }

    public void CheckColission()
    {
        
    }

    
    

}
