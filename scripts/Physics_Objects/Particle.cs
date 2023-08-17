using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MonoGame_Physics_Engine.scripts.Physics_Objects
{
    //inherits from physics object
    //perfectly elastic atm
    public class Particle : PhyObj
    {
        //this will be drawn with a simple point
        private float diameter;


        public Particle(Vector2 _V2startPos, Vector2 _V2force/*initial forces*/, Vector2 _V2PrePos, float mass, float diameter = 5) : base( _V2startPos, _V2force, _V2PrePos, mass) 
        {
            //_V2pos = _V2startPos;
            //_V2prePos = Vector2.One * 100;
            //this._V2force = _V2force;
            this.diameter = diameter;
        }
        public override void simulate(Vector2 inputForce)
        {
            //just implements the base class used in the phy_object
            base.simulate(inputForce);
            
        }

        //for testing the particle will bounce of walls
        //this would be done by the collider, however I'm just testing the physics at the moment
        
        public override void ParticleWallBounce(GraphicsDeviceManager _graphics, float diameter = 0f/*this allows for an optional argument in the method*/)
        {         
            base.ParticleWallBounce(_graphics, this.diameter);
        }
        /// <summary>
        /// draws a point representing the particle
        /// </summary>
        /// <param name="_spriteBatch"></param>
        public override void DebugDraw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawPoint(_V2pos, Color.Green, diameter);
        }
    }

    
}
