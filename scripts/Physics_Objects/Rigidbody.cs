using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame_Physics_Engine.scripts.PreGenObj;

namespace MonoGame_Physics_Engine.scripts.Physics_Objects
{

    //change to add force and singular update after coppying in this version of the code

    //might change to not be physics object
    public class Rigidbody//might make this part of the component class idk
    {
        #region main body
        //simulated through collection of points
        //the order of these points doesn't matter, due to the triangles only being there for rigidity not mesh building(this requires specific orientation)
        Particle[] vertices;
        Triangle[] triangles;

        //position of ridgidBody
        public Vector2 _V2COM { get; set; }

        //float dist = 50f;//this is temporary for testing

        //this is the position of the center of mass

        Vector2 _V2vert0StartPos;

        public bool wallBounce { get; set; }//weather the ridgid body bounces off of edge of screen

        public float rotation
        {
            get
            {
                return currentAngle(relativePosition(vertices[0]), _V2vert0StartPos);
            }
        }
            //this needs to be fed to the game object, this is calculated by gaining the angle from the first vertex from its original pos

        #region initialization
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_V2Pos">position of the center of the object and center of mass</param>
        /// <param name="_V2force">a constant force acting on the object</param>
        /// <param name="mass">mass of the object acting around the COM</param>
        /// <param name="vertices">array of the vertices of the object, set their positions relative to the center </param>
        public Rigidbody(Vector2 _V2Pos, Vector2 _V2COM ,Vector2 _V2force/*initial forces*/, float mass, Particle[] vertices)
        {
            //these are general values, all of the simulation comes through the points            
            this._V2COM = _V2Pos;

            this.vertices = vertices;


            _V2vert0StartPos = relativePosition(vertices[0]);

            this.triangles = GenerateTriangles(vertices);//creates the tringles
        }
        private Triangle[] GenerateTriangles(Particle[] vertices)
        {
            //I could calculate the number of triangles per rigidbody however that is unnessacery as this will be run out of runtime
            List<Triangle> generatedTris = new List<Triangle>();
            //missing the last 2 vertices as we will add those into a triangle at the end
            for (int i = 0; i < vertices.Length-2; i +=2)
            {
                generatedTris.Add(new Triangle(vertices[i], vertices[i + 1], vertices[i + 2]));
            }
            //creates final triangle
            generatedTris.Add(new Triangle(vertices[0], vertices[vertices.Length-1], vertices[vertices.Length-2]));

            return generatedTris.ToArray();
            
        }
        #endregion

        #region Component Interface functions to make it happy.
        // Unnessacary in this class only here so Rigidbody class is a component
        /*public void Load()
        {

        }
        public void Update()
        {

        }*/
        #endregion

        public void Update(GraphicsDeviceManager _graphics, Vector2 inputForce)
        {
            //RBConstrain();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < triangles.Length; j++)
                {
                    triangles[j]._S1.Update(_graphics, inputForce, wallBounce);
                    triangles[j]._S2.Update(_graphics, inputForce, wallBounce);
                    triangles[j]._S3.Update(_graphics, inputForce, wallBounce);
                    //needs to update COM
                    _V2COM = GetCOM();

                }
            }
            
            //RBConstrain();
            
        }
        //moves rb set distance
        public void MoveRb(Vector2 _V2dir, float displacement)
        {
            foreach (var vertex in vertices)
            {
                vertex._V2pos += _V2dir * displacement;
                vertex._V2prePos += _V2dir * displacement;
            }
        }
        //moves rb by increasing velocity in direction given
        public void MoveRbVelo(Vector2 _V2dir, float velocity)
        {
            foreach (var vertex in vertices)
                {
                    vertex._V2pos += _V2dir * velocity;
                }
        }
        public void AddTorque(float TurningForceMag, bool inPlace/*does the rb rotate in place or move with the torque*/)
        {

            //for use when spinning in place
            //gets COM before movement
            Vector2 OriginalPos = GetCOM();

            //adds an equal turning force to each vertex
            foreach (Particle vert in vertices)
            {
                //need to find the vector perpendicular relative position of the vertices
                
                Vector2 TorqueForce = Vector2.Zero;
                Vector2 _V2vertPos = relativePosition(vert);

                //only need to add to 1 vertex(first vertex)

                TorqueForce.X = (TurningForceMag/4) / MathF.Sqrt(1 + (MathF.Pow(_V2vertPos.X, 2) / MathF.Pow(_V2vertPos.Y, 2)));
                TorqueForce.Y = (TorqueForce.X * _V2vertPos.X) / MathF.Abs(_V2vertPos.Y);//needed abs val as would flip when the relative pos flips from 1 to -1

                //adds force
                vert.simulate(TorqueForce);//might try to see if I can just call the update function for RB - cant as force dir isnt the same
                
            }
            //gets COM after
            Vector2 newPos = GetCOM();

            //moves forward on its own, will correct with selection
            if (inPlace)
            {
                //finds displacement between the to positions
                Vector2 offset = OriginalPos - newPos;//new to old pos
                foreach (Particle vert in vertices)//moves each back to their original position
                {
                    vert._V2pos += offset;
                }
            }


            
            /*
            Particle vert = vertices[0];
            Vector2 TorqueForce = Vector2.Zero;
            Vector2 _V2vertPos = relativePosition(vert);

            //only need to add to 1 vertex(first vertex)

            TorqueForce.X = TurningForceMag / MathF.Sqrt(1 + (MathF.Pow(_V2vertPos.X, 2) / MathF.Pow(_V2vertPos.Y, 2)));
            TorqueForce.Y = (TorqueForce.X * _V2vertPos.X) / MathF.Abs(_V2vertPos.Y);//needed abs val as would flip when the relative pos flips from 1 to -1

            //adds force
            vert.simulate(TorqueForce);*/


        }

        public void Draw(SpriteBatch _spriteBatch)
        {

        }

        public void DebugDraw(SpriteBatch _spriteBatch)
        {
            //vertices[0].DebugDraw(_spriteBatch);
            //_spriteBatch.DrawLine(vertices[0]._V2COM, vertices[vertices.Length-1]._V2COM, Color.Green);
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i].DebugDraw(_spriteBatch);
                //_spriteBatch.DrawLine(vertices[i]._V2COM, vertices[i - 1]._V2COM, Color.Green);
            }
        }

        float currentAngle(Vector2 _V2vert1Pos, Vector2 _V2StartPos)
        {
            float dot = Vector2.Dot(_V2vert1Pos, _V2StartPos);
            //find the angle throu reversing the dot
            return MathF.Acos(dot/(_V2vert1Pos.Length() * _V2StartPos.Length()));
        }

        Vector2 relativePosition(Particle _vertex)//relative position of particle to COM(the position of the reidgidbody)
        {
            return _vertex._V2pos - _V2COM;
        }
        Vector2 GetCOM()
        {
            //returns the position of the center of mass
            Vector2 COM = Vector2.Zero;
            float ResulantWeight = 0;
            foreach (Particle particle in vertices)
            {
                COM += particle.mass * particle._V2pos;
            }
            foreach (Particle particle in vertices)
            {
                ResulantWeight += particle.mass;
            }
            //the formular requires sums of values, reason for both loops

            return COM/ResulantWeight;

        }
        Vector2 setRealPos(Vector2 _V2tempPos, Vector2 _V2COM)//this is for when generating the ridgid body, sets the real position of the particle so the user can enter the positions relative to the COM
        {
            return _V2tempPos +  _V2COM;
        }

        //inefficient initial iteration
        #endregion
        
        

    }

    //The sticks all have to have common vertices, however that will be handled by an algorithm in the ridgidbody
    public struct Triangle
    {
        public Stick _S1;
        public Stick _S2;
        public Stick _S3;
        

        public Triangle(Particle _P1, Particle _P2, Particle _P3)
        {
            _S1 = new Stick(_P1, _P2);
            _S2 = new Stick(_P2, _P3);
            _S3 = new Stick(_P3, _P1);
        }
        public void DebugDraw(SpriteBatch _spriteBatch)
        {
            _S1.DebugDraw(_spriteBatch, Color.LimeGreen);
            _S2.DebugDraw(_spriteBatch, Color.LimeGreen);
            _S3.DebugDraw(_spriteBatch, Color.LimeGreen);
        }
    }
}
