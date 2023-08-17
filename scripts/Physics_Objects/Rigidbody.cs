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
    //might change to not be physics object
    public class Rigidbody
    {
        #region main body
        //simulated through collection of points
        //the order of these points doesn't matter, due to the triangles only being there for rigidity not mesh building(this requires specific orientation)
        Particle[] vertices;
        Triangle[] triangles;

        //position of ridgidBody
        Vector2 _V2pos;

        float dist = 50f;//this is temporary for testing

        //this is the position of the center of mass
        Vector2 _V2COM;

        #region initialization
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_V2Pos">position of the center of the object</param>
        /// <param name="_V2COM">position of the center of mass relative to the center of the object</param>
        /// <param name="_V2force">a constant force acting on the object</param>
        /// <param name="mass">mass of the object acting around the COM</param>
        /// <param name="vertices">array of the vertices of the object, set their positions relative to the center </param>
        public Rigidbody(Vector2 _V2Pos, Vector2 _V2COM ,Vector2 _V2force/*initial forces*/, float mass, Particle[] vertices)
        {
            this._V2COM = _V2COM;
            //these are general values, all of the simulation comes through the points            
            this._V2pos = _V2Pos;

            this.vertices = vertices;

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
        public void update(GraphicsDeviceManager _graphics, Vector2 inputForce)
        {
            //RBConstrain();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < triangles.Length; j++)
                {
                    triangles[j]._S1.Update(_graphics, inputForce);
                    triangles[j]._S2.Update(_graphics, inputForce);
                    triangles[j]._S3.Update(_graphics, inputForce);

                }
            }
            
            //RBConstrain();
            
        }

        public void DebugDraw(SpriteBatch _spriteBatch)
        {
            //vertices[0].DebugDraw(_spriteBatch);
            //_spriteBatch.DrawLine(vertices[0]._V2pos, vertices[vertices.Length-1]._V2pos, Color.Green);
            for (int i = 0; i < triangles.Length; i++)
            {
                triangles[i].DebugDraw(_spriteBatch);
                //_spriteBatch.DrawLine(vertices[i]._V2pos, vertices[i - 1]._V2pos, Color.Green);
            }
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
            _S1.DebugDraw(_spriteBatch);
            _S2.DebugDraw(_spriteBatch);
            _S3.DebugDraw(_spriteBatch);
        }
    }
}
