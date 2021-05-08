using MathematicalPlayground.NumericalMethods.Enums;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MathematicalPlayground.NumericalMethods
{
   public class Polynomial
   {
      public static double[] SolvePolyCoefs(double[] x, double[] y)
      {
         Debug.Assert(x.Length > 0);
         Debug.Assert(x.Length == y.Length);
         MatrixNumeric m = new MatrixNumeric(x.Length, x.Length);
         Parallel.For(0, x.Length, i =>
         {
            m[i, 0] = 1.0;
            for (int j = 1; j < x.Length; j++)
            {
               m[i, j] = m[i, j - 1] * x[i];
            }
         });

         return m.SolveFor(y);
      }

      public static double ParabolicMin(double[] x, double[] y)
      {
         double result = double.NaN;
         Debug.Assert(x.Length == 3);
         Debug.Assert(y.Length == 3);
         double[] values = SolvePolyCoefs(x, y);
         if (values[2] > 0)
         {
            result = -values[1] / (2.0 * values[2]);
         }

         return result;
      }

      public static double[] Regress(double[,] z, double[] y)
      {
         //y=a0 z1 + a1 z1 +a2 z2 + a3 z3 +...
         //Z is the functional values.
         //Z index 0 is a row, the variables go across index 1.
         //Y is the summed value.
         //returns the coefficients.

         Debug.Assert(z != null && y != null);
         Debug.Assert(z.GetLength(0) == y.GetLength(0));
         MatrixNumeric zMatrix = z;
         MatrixNumeric zTransposeMatrix = zMatrix.Transpose();
         MatrixNumeric leftHandSide = zTransposeMatrix * zMatrix;
         MatrixNumeric rightHandSide = zTransposeMatrix * y;
         MatrixNumeric coefsMatrix = leftHandSide.SolveFor(rightHandSide);
         return coefsMatrix;
      }

      public static double[] RegressPolynomial(double[] x, double[] y, int polynomialOrder)
      {
         double[,] z = new double[y.Length, polynomialOrder + 1];
         for (int i = 0; i < y.Length; i++)
         {
            z[i, 0] = 1.0;
            for (int j = 1; j < polynomialOrder + 1; j++)
            {
               z[i, j] = Math.Pow(x[i], j);
            }
         }

         double[] coefs = Polynomial.Regress(z, y);
         return coefs;
      }

      public static double EvaluatePolynomial(double[] coefs, double x)
      {
         double result = coefs[0];
         for (int i = 1; i < coefs.Length; i++)
         {
            result += coefs[i] * Math.Pow(x, i);
         }
         return result;
      }

      public static CubicMinResults CubicMin(double[] x, double[] y)
      {
         //0 is local min value X
         //1 is cubic value at min value X
         //2 is local max value X
         //3 is a status <0 means it failed.

         CubicMinResults result = new CubicMinResults();
         result.LocalMinX = 0.0;
         Debug.Assert(x.Length == 4);
         Debug.Assert(y.Length == 4);
         double[] values = SolvePolyCoefs(x, y);

         //  d is values[3]
         //  c is values[2]
         //  b is values[1]
         //  a is values[0]
         //  y=dx3+cx2+bx+a
         //  y'=3dx2+2cx+b which is equal to 0 at min and max.
         //  y''=6dx+2c which is greater than 0 at the min.
         //  Quad equation is (-b +- sqrt(b2-4ac))/2a
         //  Here, a=3d, b=2c, c=b which gives
         //  x = (-2c +- sqrt(4c2 - 12db))/6d
         //  Substituting x into y'' gives
         //  y''=6d((-2c +- sqrt(4c2 - 12db))/6d)+2c
         //  y''=-2c +- sqrt(4c2 - 12db) + 2c
         //  y''=+-sqrt(4c2 - 12db)
         //  y'' is positive at the local min, the sqrt is always positive, so the
         //  + solution is the local min and the - solution is the local max.
         //Make sure it is a cubic, i.e. d>0

         if (values[3] != 0.0 && (values[2] == 0 || Math.Abs(values[3] / values[2]) > 1e-10))
         {
            //Compute the square root term.

            double squareRoot = 4.0 * values[2] * values[2] - 12.0 * values[3] * values[1];
            if (double.IsNaN(squareRoot) || double.IsInfinity(squareRoot))
            {
               Debugger.Break();
            }

            //If the square root term is negative, then there are no real roots.

            if (squareRoot > 0)
            {
               result.LocalMinX = (-2.0 * values[2] + Math.Sqrt(squareRoot)) / (6.0 * values[3]);
               result.LocalMaxX = (-2.0 * values[2] - Math.Sqrt(squareRoot)) / (6.0 * values[3]);
               result.ResultType = CubicResultType.StandardThirdOrder;
            }
            else
            {
               result.ResultType = CubicResultType.ThirdOrderImaginaryRoots;
            }
         }
         else //This is a second order, not a third.
         {
            if (values[2] > 0)
            {
               result.LocalMinX = -values[1] / (2.0 * values[2]);
               result.ResultType = CubicResultType.StandardSecondOrder;
            }
            else
            {
               result.ResultType = CubicResultType.SecondOrderNegativeSecondDerivative;
            }
         }
         if (double.IsNaN(result.LocalMinX))
         {
            throw new ArithmeticException("Line search returned NaN");
         }

         return result;
      }
   }
}
