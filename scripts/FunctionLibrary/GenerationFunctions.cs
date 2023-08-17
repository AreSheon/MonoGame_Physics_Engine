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
        /// generate a regular polygon
        /// </summary>
        /// <param name="_V2pos">position of the center</param>
        /// <param name="_V2force"></param>
        /// <param name="length">length of side</param>
        /// <param name="mass">mass of polygon</param>
        /// <param name="VertNum">number of vertices in the polygon</param>
        /// <param name="ange">angle of polygon in radians, if want to enter degres multiply by Pi/180</param>
        /// <returns></returns>
        public static Rigidbody generateRegPolygon(Vector2 _V2pos, Vector2 _V2force, float length, float mass, int VertNum, float angle)
        {
            //need to calculate the angle
            //divide 
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
    }
}
