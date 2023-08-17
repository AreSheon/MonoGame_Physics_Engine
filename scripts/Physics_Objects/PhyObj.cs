using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

//https://learn.microsoft.com/en-us/nuget/quickstart/create-and-publish-a-package-using-visual-studio?tabs=netcore-cli -> follow this once finished
namespace MonoGame_Physics_Engine.scripts.Physics_Objects
{
    public class PhyObj
    {
        
        public const float GravityCoe =.00008f; //this is for testing purely and is temporary

        //this is the quick data encapsulation syntax for c#
        //the {get;set;} in the interfaces make these fields required to be implemented in a class that inherits the interface
        public/*temp public*/ Vector2 _V2pos { get; set; }
        protected Vector2 _V2prePos { get; set; }
        protected Vector2 _V2velocity { get; set; }
        protected Vector2 _V2force { get; set; }

        protected float mass { get; set; }
        

        //collider _collider

        public PhyObj(Vector2 _V2startPos, Vector2 _V2force/*initial forces*/, Vector2 _V2prePos ,float mass)
        {
            this._V2pos = _V2startPos;
            this._V2force = _V2force;
            this._V2prePos = _V2prePos;
            this.mass = mass;

        }

        //note to self, I might need to create a separate  update that has a fixed update cycle(like fixed update in unity)
        //for testing this is globally accessible, might be able to change that in the future
        public virtual void simulate(Vector2 InputForce)
        {
            //simple verlet intergration;
            //calculate acceleration
            Vector2 acc = (_V2force+InputForce / mass)/* 0.0001f*/;
            /*temporary*/
            acc.Y += GravityCoe;

            //acc = Vector2.Zero;


            //find velocity
            _V2velocity = (_V2pos - _V2prePos);
            _V2prePos = _V2pos;
            _V2pos = _V2pos + _V2velocity + acc;
        }

        //this is a method used to constrain 2 physics objects at a certain distance or vector(distance and angle)
        //this will have 2 overrides

        #region constrain methods (might not be nessacary to have here)        
        //just set distance
        //essentially moves the points equal distance to get them back to the correct distance apart
        public virtual void constrain(PhyObj obj1, PhyObj obj2, float distance)
        {
            //get vector between obj1 and obj2
            //this is from obj2 -> obj1(doesn't matter which way round)
            Vector2 _V2Difference = obj1._V2pos - obj2._V2pos;
            float excess = distance - _V2Difference.Length();
            float SF = excess / _V2Difference.Length() / 2;//scale factor to change distances
            Vector2 Offset = _V2Difference * SF;
            //if to large move closer
            //if to small move further apart
            //float excessDist = distance - _V2VecBtw.Length();
            //move the points
            obj1._V2pos += Offset;
            obj2._V2pos -= Offset;

        }

        
        /*private float distOver(float trueDist, float targetDist, out float excessDist)
        {
            //this allows the function to be used in the selection statement but also means I'm not doing uneccassery maths or
        }*/

        #endregion 
        //the 2 check collisions would be done by a collider, but i just want to test the physics and this allows me to have bouncing things for a longer test
        //for wall bouncing for particles
        public virtual void ParticleWallBounce(GraphicsDeviceManager _graphics, float diameter)
        {
            //this now has a basic wall bouncing for particles
            //side walls
            //for the left wall
            if (_V2pos.X - (diameter / 2) < 0)
            {
                _V2pos = new Vector2(0 + (diameter / 2), _V2pos.Y);
                _V2prePos = new Vector2(_V2pos.X + _V2velocity.X, _V2prePos.Y);
            }
            else if (_V2pos.X + (diameter / 2) > _graphics.PreferredBackBufferWidth)//right wall
            {
                _V2pos = new Vector2(_graphics.PreferredBackBufferWidth - (diameter / 2), _V2pos.Y);
                _V2prePos = new Vector2(_V2pos.X + _V2velocity.X, _V2prePos.Y);
            }
            //top walls
            if (_V2pos.Y - (diameter / 2) < 0)
            {
                _V2pos = new Vector2(_V2pos.X, 0 + (diameter / 2));
                _V2prePos = new Vector2(_V2prePos.X, _V2pos.Y + _V2velocity.Y);
            }
            else if (_V2pos.Y + (diameter / 2) > _graphics.PreferredBackBufferHeight)//bottom wall
            {
                _V2pos = new Vector2(_V2pos.X, _graphics.PreferredBackBufferHeight - (diameter / 2));
                _V2prePos = new Vector2(_V2prePos.X, _V2pos.Y + _V2velocity.Y);
            }
        }
        //just general
        public virtual void checkCollisions()
        {

        }

        public virtual void DebugDraw(SpriteBatch _spriteBatch)
        {

        }
    }
}