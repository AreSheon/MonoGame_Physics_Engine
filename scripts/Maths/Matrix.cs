using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Physics_Engine.scripts.Maths
{
    //might need to edit various things
    public struct SqrMatrix
    {
        private float[,] mat;//basically just the grid reference of the value
        public float[,] Mat { get {  return mat; } }

        private float[,] invMat;
        public float[,] InvMat { get { return invMat; } }

        //this doesn't need to be float as its just 1s, 0s, so it is an int to save space
        private int[,] identityMat;


        /// <summary>
        /// by row
        /// </summary>
        /// <param name="Mat"></param>
        /// <exception cref="Exception"></exception>
        public SqrMatrix(float[,] Mat)
        {
            if(Mat.GetLength(0) == Mat.GetLength(1))
            {
                this.mat = Mat;
                this.invMat = Mat;
                this.identityMat = null;//to get around needing to assign the value. as it is generated with the
                identityMat = GenerateId(Mat.GetLength(0));
            }
            throw new Exception("need square matirx(width height need to be equal)");
            
        }
        private int[,] GenerateId(int n)
        {
            int[,] id = new int[n,n];
            //loops through new matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        id[i, j] = 1;
                    }
                    else
                    {
                        id[i, j] = 0;
                    }
                }
            }
            return id;
        }
        /*private float[,] GenerateInv(float[,] mat, int n)
        {
            float[,] inv = new float[n, n];
        }*/

        #region operators
        public static Vector2 operator *(SqrMatrix mat, Vector2 vec)
        {
            if(mat.Mat.GetLength(0) == 2)
            {
                float x = (vec.X * mat.Mat[0,0]) + (vec.Y * mat.Mat[0,1]);
                float y = (vec.X * mat.Mat[1, 0]) + (vec.Y * mat.Mat[1, 1]);
                return new Vector2 (x, y);
            }
            else
            {
                throw new Exception("need 2*2 matrix");
            }
        }
        #endregion

        /// <summary>
        /// generates a matrix to rotate anticlockwise
        /// </summary>
        /// <param name="angle">angle in radians</param>
        /// <returns></returns>
        public SqrMatrix RotMat2d(float angle)
        {   
            return new SqrMatrix(new float[2, 2] { { MathF.Cos(angle), -MathF.Sin(angle) }, { MathF.Sin(angle), -MathF.Cos(angle) } });
        }
        public Vector2 RotPoint(Vector2 point, float angle)
        {
            return RotMat2d(angle) * point;
        }
        
        
    }
}
