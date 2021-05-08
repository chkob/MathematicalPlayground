using System;
using System.Diagnostics;
using System.Linq;

namespace MathematicalPlayground.NumericalMethods
{
   public class QuasiNewtonBase
   {
      protected BracketingBase _bracketingMethod = new CubicBracketing();
      protected RegionEliminationBase _regionEliminationMethod = new CubicRegionElimination();
      protected MatrixNumeric _inverseHessian;
      protected Parameter[] _solvedForParameters;
      protected Func<double> _optimizationFunction;
      protected Derivatives _derivatives;
      protected double[] _currentGradient;
      protected double[] _lastGradient;
      protected double[] _searchDirection;
      protected double[] _currentPosition;
      protected double[] _lastPosition;
      protected double _regionEliminationFraction = 1e-5;
      protected int _iterationsSinceReset = 0;
      protected double _functionValue;
      protected int _inverseHessianResetCount = -1;

      public QuasiNewtonBase(Func<double> optimizationFunction, Parameter[] optimizationParameters, double inverseHessianResetMultiple, int numberOfDerivativePoints)
      {
         _solvedForParameters = optimizationParameters;
         _optimizationFunction = optimizationFunction;
         _derivatives = new Derivatives(numberOfDerivativePoints);
         int numberOfParameters = _solvedForParameters.Length;
         _inverseHessian = new MatrixNumeric(numberOfParameters, numberOfParameters);
         ResetInverseHessian();
         _inverseHessianResetCount = (int)(inverseHessianResetMultiple * (double)_solvedForParameters.Length);
      }

      public QuasiNewtonBase(Func<double> optimizationFunction, Parameter[] optimizationParameters)
         : this(optimizationFunction, optimizationParameters, 2.0, 3)
      {
      }

      public bool Iterate()
      {
         int numberOfParameters = _solvedForParameters.Length;
         _currentPosition = (from d in _solvedForParameters select d.Value).ToArray();
         CalculateGradient();
         if (_inverseHessianResetCount > 0 && _iterationsSinceReset > _inverseHessianResetCount)
         {
            ResetInverseHessian();
         }

         if (_iterationsSinceReset > 0)
         {
            double dMoveDistance = 0.0;
            for (int i = 0; i < numberOfParameters; i++)
            {
               double dDistDiff = _currentPosition[i] - _lastPosition[i];
               dMoveDistance += dDistDiff * dDistDiff;
            }

            if (dMoveDistance == 0.0)
            {
               ResetInverseHessian();
            }
            else
            {
               UpdateInverseHessian();
            }
         }

         CalculateSearchDirection();

         bool succeeded = (from d in _searchDirection where double.IsNaN(d) select d).Count() == 0;
         if (succeeded)
         {
            SearchPointCollection colSearch = _bracketingMethod.Bracket(LineSearchFunction, 0.0, 1.0, _regionEliminationMethod.NumStartingPoints);
            RegionEliminationResults resRegionEliminate = _regionEliminationMethod.RegionEliminate(LineSearchFunction, colSearch, _regionEliminationFraction);

            if (resRegionEliminate.Succeeded)
            {
               _lastGradient = _currentGradient;
               _lastPosition = _currentPosition;
               MoveParametersToPosition(resRegionEliminate.XResult);
               _functionValue = resRegionEliminate.YResult;
            }

            _iterationsSinceReset++;
         }
         else
         {
            Debugger.Break();
         }

         return succeeded;
      }

      protected virtual void UpdateInverseHessian()
      {
         throw new NotImplementedException();
      }

      protected double LineSearchFunction(double length)
      {
         MoveParametersToPosition(length);
         return _optimizationFunction();
      }

      protected void MoveParametersToPosition(double length)
      {
         int numberOfParameters = _solvedForParameters.Length;
         for (int i = 0; i < numberOfParameters; i++)
         {
            _solvedForParameters[i].Value = _currentPosition[i] + length * _searchDirection[i];
         }
      }

      protected void CalculateSearchDirection()
      {
         _searchDirection = -1.0 * _inverseHessian * _currentGradient;
      }

      public void ResetInverseHessian()
      {
         int numberOfParameters = _solvedForParameters.Length;
         for (int i = 0; i < numberOfParameters; i++)
         {
            for (int j = 0; j < numberOfParameters; j++)
            {
               if (i == j)
               {
                  double dSecondDeriv = _derivatives.ComputePartialDerivative(_optimizationFunction, _solvedForParameters[i], 2);
                  if (dSecondDeriv != 0.0)
                  {
                     _inverseHessian[i, j] = Math.Abs(1.0 / dSecondDeriv);
                  }
                  else
                  {
                     _inverseHessian[i, j] = 1.0;
                  }
               }
               else
               {
                  _inverseHessian[i, j] = 0.0;
               }
            }
         }

         _iterationsSinceReset = 0;
      }

      protected MatrixNumeric ComputeQNProduct(double[] x, double[] g) //xgT
      {
         Debug.Assert(x.Length == g.Length);
         MatrixNumeric mRet = new MatrixNumeric(x.Length, x.Length);
         for (int i = 0; i < x.Length; i++)
         {
            for (int j = 0; j < x.Length; j++)
            {
               mRet[i, j] = x[i] * g[j];
            }
         }
         return mRet;
      }

      protected double ComputeInnerProduct(double[] left, double[] right)
      {
         Debug.Assert(left.Length == right.Length);
         double result = 0.0;
         for (int i = 0; i < left.Length; i++)
         {
            result += left[i] * right[i];
         }

         return result;
      }

      protected void CalculateGradient()
      {
         int numberOfParameters = _solvedForParameters.Length;
         _currentGradient = new double[numberOfParameters];
         for (int i = 0; i < numberOfParameters; i++)
         {
            _currentGradient[i] = _derivatives.ComputePartialDerivative(_optimizationFunction, _solvedForParameters[i], 1);
         }
      }

      public BracketingBase BracketingMethod
      {
         get
         {
            return _bracketingMethod;
         }
         set
         {
            _bracketingMethod = value;
         }
      }

      public RegionEliminationBase RegionEliminationMethod
      {
         get
         {
            return _regionEliminationMethod;
         }
         set
         {
            _regionEliminationMethod = value;
         }
      }

      public double RegionEliminationFraction
      {
         get { return _regionEliminationFraction; }
         set { _regionEliminationFraction = value; }
      }

      public double FunctionValue
      {
         get
         {
            return _functionValue;
         }
      }
   }
}
