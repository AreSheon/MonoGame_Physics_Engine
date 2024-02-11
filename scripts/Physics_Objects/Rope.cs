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
    public class Rope //technically this is a sort of soft body, but this is the only one in the engine so no need to have a whole separate soft body class
    {
        private List<Particle> vertices = new List<Particle>();

        private List<RopeSegment> segments = new List<RopeSegment>();
        private float offset;

        private Particle _StartPart;

        private Particle _EndPart;
        
        //this is so we can toggle the start and end particle being simulated
        bool is_StParStatic{get;set;}
        bool is_EndParStatic{get;set;}

        public int SegNum{get; set;}//number of segments, can be adjusted in real time to allow adjustment in rope length

        private Vector2 _V2StartPos;
        private Vector2 _V2EndPos;

        float partMass;//mass of each particles

        public Rope(Vector2 _V2StartPos, Vector2 _V2EndPos,int SegNum, float offset, float partMass, bool is_StParStatic, bool is_EndParStatic)
        {
            this._V2StartPos = _V2StartPos;
            this.SegNum = SegNum;
            updateRope(SegNum);
            this.offset = offset;
            vertices[vertices.Count - 1]._V2pos = _V2EndPos;
            vertices[vertices.Count - 1]._V2prePos = _V2EndPos;
            
        }

        public void updateRope(int value)
        {
            for(int i = 0; i < value; i++)
            {
                //creates new particle
                Particle CurPart = new Particle(new Vector2(_V2StartPos.X, _V2StartPos.Y + offset),Vector2.Zero, new Vector2(_V2StartPos.X, _V2StartPos.Y + offset), partMass);
                try//trys to add new segment using last val in the vertices list(error if empty)
                {
                    segments.Add(new RopeSegment(vertices[vertices.Count - 1], CurPart));
                    //adds new particle to vertices List
                    vertices.Add(CurPart);
                }catch//if the vertices list is empty
                {
                    //add particle to vertex list
                    vertices.Add(CurPart);
                    //subtracts i by one and reloops
                    i--;
                }
            }

            
        }

        public void Update(GraphicsDeviceManager _graphics, Vector2 inputForce)
        {
            for (int i = 0; i < 5; i++)
            {
                segments[0].constrainEnds(is_StParStatic);
                for (int j = 1; j < segments.Count - 2; j++)//leaves start and end to update separatly
                {
                    segments[i].Update(_graphics, inputForce);
                }
                segments[segments.Count-1].constrainEnds(is_EndParStatic);
            }
        }

        public void DebugDraw(SpriteBatch _spriteBatch)
        {
            foreach(RopeSegment segment in segments)
            {
                segment.DebugDraw(_spriteBatch, Color.Blue);
            }
        }

        class RopeSegment : Stick//so I can use the constrain method
        {

            public RopeSegment(Particle _P1, Particle _P2) : base(_P1,_P2)
            {
                this._P1 = _P1;
                this._P2 = _P2;

                Distance = Vector2.Distance(_P1._V2pos, _P2._V2pos);
                XDist = -_P1._V2pos.X + _P2._V2pos.X;
                YDist = -_P1._V2pos.Y + _P2._V2pos.Y;

            }

            public void constrainEnds(bool isConstrained)
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
                _P1._V2pos += Offset * Convert.ToInt32(!isConstrained);//if constrained will offset otherwise won't(bools also 1 or 0)
                _P2._V2pos -= Offset + (Offset * Convert.ToInt32(isConstrained));//if constrained will offset normally otherwise double offset(bools also 1 or 0)
            }

            
        }

    }
}