using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolinomeLib
{
    public class PolynomialIndexOutOfRangeException : Exception
    {
        public PolynomialIndexOutOfRangeException() : base() { }
        public PolynomialIndexOutOfRangeException(string message) : base(message) { }
        public PolynomialIndexOutOfRangeException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class Polynomial
    {
        private double[] coefficients;

        public int Power { get { return coefficients.Length; } }

        public Polynomial(params double[] coefficients)
        {
            this.coefficients = coefficients;
        }

        public Polynomial(int power)
        {
            coefficients = new double[power];
        }

        public double this[int index]
        {
            get
            {
                try
                {
                    return coefficients[index];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new PolynomialIndexOutOfRangeException("Matrix index is out of range", e);
                }
            }
            set
            {
                try
                {
                    coefficients[index] = value;
                }
                catch (IndexOutOfRangeException e)
                {
                    throw new PolynomialIndexOutOfRangeException("Matrix index is out of range", e);
                }
            }
        }

        public static Polynomial operator +(Polynomial pol1, Polynomial pol2)
        {
            int maxDegree = pol1.Power > pol2.Power ? pol1.Power : pol2.Power;
            double[] result = new double[maxDegree];
            for (int i = 0; i < pol1.Power; i++)
            {
                result[i] += pol1[i];
            }
            for (int i = 0; i < pol2.Power; i++)
            {
                result[i] += pol2[i];
            }
            return new Polynomial(result);
        }

        public static Polynomial operator -(Polynomial pol1, Polynomial pol2)
        {
            int maxDegree = pol1.Power > pol2.Power ? pol1.Power : pol2.Power;
            var result = new List<double>(maxDegree);
            for (int i = 0; i < maxDegree; i++)
                result[i] = 0;

            for (int i = 0; i < pol1.Power; i++)
                result[i] -= pol1[i];

            for (int i = 0; i < pol2.Power; i++)
                result[i] -= pol2[i];

            while (result.Last() == 0)
                result.RemoveAt(result.Count - 1);

            return new Polynomial(result.ToArray());
        }

        public static Polynomial operator *(Polynomial pol1, Polynomial pol2)
        {
            int maxDegree = pol1.Power + pol2.Power;
            var result = new double[maxDegree];
            for (int i = 0; i < pol1.Power; i++)
            {
                for (int j = 0; j < pol2.Power; j++)
                {
                    result[i + j] = pol1[i] * pol2[j];
                }
            }
            return new Polynomial(result);
        }
    }
}
