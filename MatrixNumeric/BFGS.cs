using System;

namespace MathematicalPlayground.NumericalMethods
{
   public class BFGS : QuasiNewtonBase
   {
      public BFGS(Func<double> function, Parameter[] optimizationParameters, double inverseHessianResetMultiple, int numberOfDerivativePoints)
         : base(function, optimizationParameters, inverseHessianResetMultiple, numberOfDerivativePoints)
      {
      }

      public BFGS(Func<double> function, Parameter[] optimizationParameters)
         : this(function, optimizationParameters, 2.0, 3)
      {
      }

      protected override void UpdateInverseHessian()
      {
         int numberOfParameters = _solvedForParameters.Length;
         double[] positionDelta = new double[numberOfParameters];
         double[] gradientDelta = new double[numberOfParameters];

         for (int i = 0; i < numberOfParameters; i++)
         {
            gradientDelta[i] = _currentGradient[i] - _lastGradient[i];
            positionDelta[i] = _currentPosition[i] - _lastPosition[i];
         }

         MatrixNumeric YXt = ComputeQNProduct(gradientDelta, positionDelta);
         double YtX = ComputeInnerProduct(gradientDelta, positionDelta);
         MatrixNumeric YXtOverYtX = YXt / YtX;
         MatrixNumeric IdentMinusYXtOverYtX = MatrixNumeric.Identity(numberOfParameters) - YXtOverYtX;
         MatrixNumeric T1 = IdentMinusYXtOverYtX.Transpose() * _inverseHessian * IdentMinusYXtOverYtX;
         MatrixNumeric XXt = ComputeQNProduct(positionDelta, positionDelta);
         MatrixNumeric T2 = XXt / YtX;
         MatrixNumeric matInverseHessianNew = T1 + T2;
         _inverseHessian = matInverseHessianNew;
      }
   }
}
