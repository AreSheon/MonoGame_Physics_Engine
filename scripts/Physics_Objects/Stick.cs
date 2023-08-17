using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MonoGame_Physics_Engine.scripts.Physics_Objects
{
    // note make a standard object interface so we have set functions like update and start
    public class Stick
    {
        public Particle _P1;
        public Particle _P2;

        private float Distance;
        //don't want to change the distance once set by outside scources so it is read only
        public float distance { get { return Distance; } }

        
        public Stick(Particle _P1, Particle _P2)
        {
            this._P1 = _P1;
            this._P2 = _P2;

            Distance = Vector2.Distance(_P1._V2pos, _P2._V2pos);
        }

        public void Update(GraphicsDeviceManager _graphics, Vector2 inputForce,float diameter = 0f/*this allows for an optional argument in the method*/)
        {

            constrain();
            _P1.simulate(inputForce);
            _P2.simulate(inputForce);
            constrain();
            _P1.ParticleWallBounce(_graphics);
            _P2.ParticleWallBounce(_graphics);
            constrain();
        }

        private void constrain()
        {
            //1. get current distance of the particles
            //get vector between _P1 and _P2
            //this is from _P2 -> _P1(doesn't matter which way round)
            Vector2 _V2Difference = _P1._V2pos - _P2._V2pos;
            float excess = distance - _V2Difference.Length();
            float SF = excess / _V2Difference.Length() / 2;//scale factor to change distances
            Vector2 Offset = _V2Difference * SF;
            //if to large move closer
            //if to small move further apart
            //float excessDist = distance - _V2VecBtw.Length();
            //move the points
            _P1._V2pos += Offset;
            _P2._V2pos -= Offset;

        }

        public void DebugDraw(SpriteBatch _spriteBatch)
        {
            _P1.DebugDraw(_spriteBatch);
            _P2.DebugDraw(_spriteBatch);
            _spriteBatch.DrawLine(_P1._V2pos, _P2._V2pos, Color.LimeGreen, 1);
        }
    }
}
