using MonoGame_Physics_Engine.scripts.Physics_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Shapes;
using MonoGame_Physics_Engine.scripts.PreGenObj;

//could be this is inspired by the system in this paper:https://www.researchgate.net/profile/Thomas-Jakobsen-6/publication/228599597_Advanced_character_physics/links/5695317108ae3ad8e33d548d/Advanced-character-physics.pdf
//https://www.monogameextended.net/docs/features/collision/collision - real code modified from this
namespace MonoGame_Physics_Engine.scripts.Colliders
{
    /*public interface ICollider
    {
        public const int checkPointsNum = 50; //this is the number of points per edge

        //required by all classes inheriting this interface
        public Vector2 _V2COM { get; set; }//within interface this means it is a required component

        //the edges of the collider
        public Stick[] _Edges { get; set; }
        //corners of collider
        public Vector2[] Vertices { get; set; }//this replaces having to store stick objects, more space efficent

        //all the points the collision will be checked with
        protected collisionPoints[] CheckPoints { get; set; }

        //area around each point that gets checked
        protected float thickness { get; set; }

        //the radius of the circle for the broad checks
        protected float radius { get; set; }

        //this will be the same for all colliders, however for a UI collider it will not be needed as those will only need to check for mouse positions
        //these will be done separately
        collisionPoints[] DefaultGenerateCheckPoints(Vector2[] edges, Vector2[] Corners ,int NumOfPoints/*this is a set number, that is important for the workings of the collider, and only nessacary here)
        {
            collisionPoints[] tempPoints = new collisionPoints[(NumOfPoints * edges.Length) + Corners.Length];
            //so we generate for each edge including the leading  vertices
            for (int i = 0; i < edges.Length; i++)
            {
                collisionPoints[] edgePoints = new collisionPoints[NumOfPoints + 1];
                edgePoints[0] = new collisionPoints(Corners[i]);
                //for each edge we generate the points, as this is essentially a series of n terms per edge, where n = num of points
                for (int n = 1; n < NumOfPoints + 1; n++)
                {
                    edgePoints[n] = new collisionPoints((n * edges[i]) / (NumOfPoints + 1)); //should genreate the points
                }
                Enumerable.Concat(tempPoints, edgePoints);//concatinates both arrays
            }
            return tempPoints;

        }

        public virtual bool broadCheck(ICollider thisCol, ICollider otherCol)
        {
            //checks weather there is a chance that the objects are colliding(will be the same for basically all colliders)
            //this is much faster than doing a detailed check for each object, so if this returns true for any object we then proced to do a presice check
            if(Vector2.Distance(thisCol._V2COM, otherCol._V2COM) <= thisCol.radius + otherCol.radius)
            {
                return true;
            }
            return false;
        }

        collisionPoints[] GenerateCheckPoints(Stick[] sticks);

        //this will be dependant on which type of collider there is
        bool PreciseCheck(ICollider thisCol, ICollider otherCol);




        //if there is a collision, this resolves what happens to the collidiing objects
        void resolveCollision(ICollider thisCol, ICollider otherCol);



        //draws the collider
        void DebugDraw(SpriteBatch _spriteBatch);


    }

    //this is so it is easier to find which points are collision points and which points aren't

    public struct collisionPoints
    {
        //only need a position as they are just a reference to check each point
        Vector2 _V2COM;

        public collisionPoints(Vector2 V2pos)
        {
            _V2COM = V2pos;
        }
    }*/

    public interface ICollider : ICollisionActor//collision from the monogame.extended.collisions
    {
        public void DebugDraw(SpriteBatch _spriteBatch);
    }

    //2 basic standard colliders

    public class BoxCollider : ICollider
    {
        public bool _isTrigger { get; set; }

        //sets up the class
        public IShapeF Bounds { get; }//bounds of the rectangle

        GameObject _connectedObj { get; set; }

        public BoxCollider(RectangleF bounds, bool isTrigger, GameObject _connectedObj)
        {
            this.Bounds = bounds;
            _isTrigger = isTrigger;
            this._connectedObj = _connectedObj;
        }
        public virtual void DebugDraw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);
        }

        public virtual void OnCollision(CollisionEventArgs CollisionInfo)
        {
            if(!_isTrigger)
            {
                if(_connectedObj.rb != null)
                {
                    _connectedObj.rb.MoveRb(-CollisionInfo.PenetrationVector/2, 1);
                    //moves the ridgid body out, halves the penetration vector as the collisions look a bit more realistic then
                }
                else
                {
                    _connectedObj.Position -= CollisionInfo.PenetrationVector;//this bumbs the objects out of each other

                }
                
            }
        }
    }

    public class CircleCollider : ICollider
    {
        public bool _isTrigger { get; set; }

        GameObject _connectedObj { get; set; }

        //sets up the class
        public IShapeF Bounds { get; }//bounds of the rectangle

        public CircleCollider(CircleF bounds, bool isTrigger, GameObject _connectedObj)
        {
            this.Bounds = bounds;
            this._connectedObj = _connectedObj;
            this._isTrigger = isTrigger;
        }
        public virtual void DebugDraw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawRectangle((CircleF)Bounds, Color.Blue, 3);
        }

        public virtual void OnCollision(CollisionEventArgs CollisionInfo)
        {
            if (!_isTrigger)
            {
                _connectedObj.Position -= CollisionInfo.PenetrationVector;//this bumbs the objects out of each other
            }
        }
    }
    

    
}
