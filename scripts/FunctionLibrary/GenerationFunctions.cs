using MonoGame_Physics_Engine.scripts.Physics_Objects;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace MonoGame_Physics_Engine.scripts.FunctionLibrary
{
    public static class GenerationFunctions
    {
        #region Rigidbody generation
        //later add center of mass parameter

        /// <summary>
        /// generate a square
        /// </summary>
        /// <param name="_V2Pos"> this is the position for the center of square</param>
        /// <param name="length">length of a side</param>
        /// <param name="mass">mass of object</param>
        /// <returns></returns>
        public static Rigidbody generateSquare(Vector2 _V2pos, Vector2 _V2force, float length, float mass, float angle)
        {
            //generates the vertices
            Particle[] vertices =
            {
                new Particle(new Vector2(_V2pos.X + length/2, _V2pos.Y + length/2),Vector2.Zero,new Vector2(_V2pos.X + length/2, _V2pos.Y + length/2), mass/4),
                new Particle(new Vector2(_V2pos.X - length/2, _V2pos.Y + length/2),Vector2.Zero,new Vector2(_V2pos.X - length/2, _V2pos.Y + length/2), mass/4),
                new Particle(new Vector2(_V2pos.X - length/2, _V2pos.Y - length/2),Vector2.Zero,new Vector2(_V2pos.X - length/2, _V2pos.Y - length/2), mass/4),
                new Particle(new Vector2(_V2pos.X + length/2, _V2pos.Y - length/2),Vector2.Zero,new Vector2(_V2pos.X + length/2, _V2pos.Y - length/2), mass/4)
            };
            return new Rigidbody(_V2pos, _V2force, _V2pos, mass, vertices);
        }
        public static Rigidbody generateRectange(Vector2 _V2pos, Vector2 _V2force, float length, float height, float mass, float angle)
        {
            //generates the vertices
            Particle[] vertices =
            {
                new Particle(new Vector2(_V2pos.X + length/2, _V2pos.Y + height/2),Vector2.Zero,new Vector2(_V2pos.X + length/2, _V2pos.Y + height/2), mass/4),
                new Particle(new Vector2(_V2pos.X - length/2, _V2pos.Y + height/2),Vector2.Zero,new Vector2(_V2pos.X - length/2, _V2pos.Y + height/2), mass/4),
                new Particle(new Vector2(_V2pos.X - length/2, _V2pos.Y - height/2),Vector2.Zero,new Vector2(_V2pos.X - length/2, _V2pos.Y - height/2), mass/4),
                new Particle(new Vector2(_V2pos.X + length/2, _V2pos.Y - height/2),Vector2.Zero,new Vector2(_V2pos.X + length/2, _V2pos.Y - height/2), mass/4)
            };
            return new Rigidbody(_V2pos, _V2force, _V2pos, mass, vertices);
        }
        /// <summary>
        /// generate a regular polygon!!!NEED TO FINISH!!!
        /// </summary>
        /// <param name="_V2pos">position of the first vertex</param>
        /// <param name="_V2force"></param>
        /// <param name="length">length of side</param>
        /// <param name="mass">mass of polygon</param>
        /// <param name="VertNum">number of vertices in the polygon</param>
        /// <param name="ange">angle of polygon in radians, if want to enter degres multiply by Pi/180</param>
        /// <returns></returns>
        public static Rigidbody generateRegPolygon(Vector2 _V2pos, Vector2 _V2force, float length, float mass, int VertNum, float RotationAngle)
        {
            Particle[] vertices = new Particle[VertNum];
            Vector2[] _V2vertices = new Vector2[VertNum];
            //need to calculate the angle
            float SumAng = (VertNum - 2) * 180;
            SumAng *= (MathF.PI/180);
            //calculate interior angles
            float intAng = SumAng/VertNum;
            float exAng = 180 - intAng;
            //firstVertex
            //find apothem
            float apothem = MathF.Tan(intAng/2) * (length/2);//perpendicular distance from center to edge
            // initial Vertex
            Vector2 _V2start = new Vector2(_V2pos.X + (length/2), _V2pos.Y + apothem/*plus height as down is positive*/);
            //offset with vectors
            for(int i = 1; i < VertNum; i++)
            {
                //generate a vector with angle offset of int angle using dot product
                //the magnitudes = length
                //angle = 
            }

            //we have ext angles

            //divide
            return null;

        }

        public static Rigidbody generateRbFromVec2Arr(Vector2 _V2pos, Vector2 _V2force, float mass ,Vector2[] points/*relitive to position*/)
        {
            /*Particle[] vertices = new Particle[points.length];
            for(int i = 0; i < points.length; i++)
            {
                new Particle(points[i] + _V2COM/*the position of the vertice in world space*//*,Vector2.Zero, points[i] + _V2COM, mass/4);
            }*/
            //return new Rigidbody(_V2COM, _V2force, _V2COM, mass, vertices);
            return null;
        }

        public static Rigidbody generateEqlTriangle(Vector2 _V2pos, Vector2 _V2force, float length, float mass, float angle)
        {
            float height = MathF.Sqrt(MathF.Pow(length, 2) - MathF.Pow(length/2, 2));

            Particle[] vertices =
            {
                new Particle(new Vector2(_V2pos.X, _V2pos.Y - (height/2)),Vector2.Zero,new Vector2(_V2pos.X, _V2pos.Y - (height/2)), mass/4),
                new Particle(new Vector2(_V2pos.X - length/2, _V2pos.Y + height/2),Vector2.Zero,new Vector2(_V2pos.X - length/2, _V2pos.Y + height/2), mass/4),
                new Particle(new Vector2(_V2pos.X + length/2, _V2pos.Y + height/2),Vector2.Zero,new Vector2(_V2pos.X + length/2, _V2pos.Y + height/2), mass/4),
            };
            return new Rigidbody(_V2pos, _V2force, _V2pos, mass, vertices);
        }

        public static Rigidbody generateIsoTriangle(Vector2 _V2pos, Vector2 _V2force, float baseLength, float height, float mass, float angle)
        {
            Particle[] vertices =
            {
                new Particle(new Vector2(_V2pos.X, _V2pos.Y - (height/2)),Vector2.Zero,new Vector2(_V2pos.X, _V2pos.Y - (height/2)), mass/4),
                new Particle(new Vector2(_V2pos.X - baseLength/2, _V2pos.Y + height/2),Vector2.Zero,new Vector2(_V2pos.X - baseLength/2, _V2pos.Y + height/2), mass/4),
                new Particle(new Vector2(_V2pos.X + baseLength/2, _V2pos.Y + height/2),Vector2.Zero,new Vector2(_V2pos.X + baseLength/2, _V2pos.Y + height/2), mass/4),
            };
            return new Rigidbody(_V2pos, _V2force, _V2pos, mass, vertices);
        }
        #endregion
        #region Rope generation
        //this will contain a bunch of function that generate different types of ropes
        public static Rope genSimpleRope(Vector2 _V2pos, int segmentNum, float length)
        {
            return new Rope(_V2pos, new Vector2(_V2pos.Y, _V2pos.Y + length) ,segmentNum, length/segmentNum, 5, false, false);
            
        }

        #endregion
    }
}
