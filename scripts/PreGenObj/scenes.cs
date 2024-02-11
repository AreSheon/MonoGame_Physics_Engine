using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Physics_Engine.scripts.PreGenObj
{
    //this will have to be a thing the user overwrites if they want to change the scenes
    public enum scenes //will need to fill in themselves
    {
        //load = 0,//this is the scene for when the game is first loaded so all save states can be found etc
        menu = 0,
        gameplay = 1,
        gameOver = 2,
        //etc
    };
}
