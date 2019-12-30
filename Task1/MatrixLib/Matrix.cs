using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;

namespace MatrixApp
{
    public class MatrixDimensionExeption : Exception
    {
        public MatrixDimensionExeption() : base() { }
        public MatrixDimensionExeption(string message) : base(message) { }
        public MatrixDimensionExeption(string message, Exception innerException) : base(message, innerException) { }
    }
    
    public class MatrixIndexOutOfRangeException : Exception
    {
        public MatrixIndexOutOfRangeException() : base() { }
        public MatrixIndexOutOfRangeException(string message) : base(message) { }
        public MatrixIndexOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }
    }

    [Serializable]
    public class Matrix : IEquatable<Matrix>, ICloneable
    {
        private double[,] array;
        public int Rows { get { return array.GetLength(0); } }
        public int Columns { get { return array.GetLength(1); } }

        public Matrix(int rows, int columns)
        {
            array = new double[rows, columns];
        }

        public Matrix(double[,] array)
        {
            this.array = array;
        }

        public double this[int i, int j]
        {
            get
            {
                try
                {
                    return array[i, j];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new MatrixIndexOutOfRangeException("Matrix index is out of range", e);
                }
            }
            set
            {
                try
                {
                    array[i, j] = value;
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new MatrixIndexOutOfRangeException("Matrix index is out of range", e);
                }
                
            }
        }

        public object Clone()
        {
            double[,] arrayCopy = new double[Rows, Columns];
            Array.Copy(array, arrayCopy, array.Length);
            return new Matrix(arrayCopy);
        }

        public void Show()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(array[i, j] + " ");
                }

                Console.WriteLine();
            }
        }

        public bool Equals(Matrix matrix)
        {
            if (array.GetLength(0) == matrix.array.GetLength(0) &&
                array.GetLength(1) == matrix.array.GetLength(1))
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (this[i, j] != matrix[i, j])
                            return false;
                    }
                }
                return true;
            }
            else throw new MatrixDimensionExeption("Matrixes have different dimensions");
        }
        

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            if (m1.array.GetLength(0) == m2.array.GetLength(0) &&
                m1.array.GetLength(1) == m2.array.GetLength(1))
            {
                Matrix matrix = new Matrix(m1.Rows, m1.Columns);
                for (int i = 0; i < matrix.Rows; i++)
                {
                    for (int j = 0; j < matrix.Columns; j++)
                    {
                        matrix[i, j] = m1[i, j] + m2[i, j];
                    }
                }
                return matrix;
            }
            else throw new MatrixDimensionExeption("Matrixes have different dimensions");
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            if (m1.array.GetLength(0) == m2.array.GetLength(0) &&
                m1.array.GetLength(1) == m2.array.GetLength(1))
            {
                Matrix result = new Matrix(m1.Rows, m1.Columns);
                for (int i = 0; i < result.Rows; i++)
                {
                    for (int j = 0; j < result.Columns; j++)
                    {
                        result[i, j] = m1[i, j] - m2[i, j];
                    }
                }
                return result;
            }
            else throw new MatrixDimensionExeption("Matrixes have different dimensions");
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1.array.GetLength(1) == m2.array.GetLength(0))
            {
                Matrix result = new Matrix(m1.Rows, m2.Columns);
                for (int i = 0; i < m1.Rows; i++)
                {
                    for (int j = 0; j < m2.Columns; j++)
                    {
                        result[i, j] = 0;
                        for (int k = 0; k < m1.Columns; k++)
                        {
                            result[i, j] += m1[i, k] * m2[k, j];
                        }

                    }
                }
                return result;
            } else 
                throw new MatrixDimensionExeption("Matrixes have wrong dimensions for multiplying");
        }

        public static Matrix operator *(Matrix matrix, double number)
        {
            Matrix result = new Matrix(matrix.Rows, matrix.Columns);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] *= number;
                }
            }
            return result;
        }

        public static Matrix operator *(double number, Matrix matrix)
        {
            return matrix * number;
        }

        public static Matrix operator /(Matrix matrix, double number)
        {
            Matrix result = new Matrix(matrix.Rows, matrix.Columns);
            for (int i = 0; i < result.Rows; i++)
            {
                for (int j = 0; j < result.Columns; j++)
                {
                    result[i, j] /= number;
                }
            }
            return result;
        }
    }
}
