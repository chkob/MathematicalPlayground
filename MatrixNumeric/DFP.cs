using System;

namespace MathematicalPlayground.NumericalMethods
{
   public class DFP : QuasiNewtonBase
   {
      public DFP(Func<double> function, Parameter[] optimizationParameters, double inverseHessianResetMultiple, int numberOfDerivativePoints)
         : base(function, optimizationParameters, inverseHessianResetMultiple, numberOfDerivativePoints)
      {
      }

      public DFP(Func<double> function, Parameter[] optimizationParameters)
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

         MatrixNumeric XXt = ComputeQNProduct(positionDelta, positionDelta);
         double YtX = ComputeInnerProduct(gradientDelta, positionDelta);
         MatrixNumeric T1 = XXt / YtX;
         MatrixNumeric YYt = ComputeQNProduct(gradientDelta, gradientDelta);
         MatrixNumeric AYYtAt = _inverseHessian * YYt * _inverseHessian.Transpose();
         double YtAY = ComputeInnerProduct(gradientDelta, _inverseHessian * gradientDelta);
         MatrixNumeric T2 = AYYtAt / YtAY;
         MatrixNumeric matInverseHessianNew = _inverseHessian + T1 - T2;
         _inverseHessian = matInverseHessianNew;
      }
   }
}
