using MathematicalPlayground.NumericalMethods.Exceptions;
using System;
using System.Diagnostics;

namespace MathematicalPlayground.NumericalMethods
{
   public class CubicRegionElimination : RegionEliminationBase
   {
      public CubicRegionElimination()
         : base()
      {
         _numStartingPoints = 4;
      }

      public override RegionEliminationResults RegionEliminate(Func<double, double> function, SearchPointCollection startPoint, double eliminationFraction)
      {
         Debug.Assert(startPoint.Count >= 2);
         bool succeeded = true; Cancel = false;
         SearchPointCollection searchPoints = startPoint.Clone(4);
         CalculateMissingValues(searchPoints, function);
         while (searchPoints.Count < 4)
         {
            double midPoint = (searchPoints[0].X + searchPoints[1].X) / 2.0;
            double functionValue = function(midPoint);
            searchPoints.Add(new SearchPoint(midPoint, functionValue));
         }

         double stopRegionSize = (searchPoints[3].X - searchPoints[0].X) * eliminationFraction;

         while (!Cancel)
         {
            try
            {
               double newX = searchPoints.CubicMinYEstimate();
               if (!double.IsNaN(newX))
               {
                  double functionValue = function(newX);
                  SearchPoint newPoint = new SearchPoint(newX, functionValue);
                  if (searchPoints.CanAdd(newPoint))
                  {
                     searchPoints.Add(newPoint);
                  }
                  else
                  {
                     Cancel = true;
                  }
               }
               else
               {
                  Cancel = true;
                  succeeded = false;
               }

               if (searchPoints[3].X - searchPoints[0].X < stopRegionSize)
               {
                  Cancel = true;
               }
            }
            catch (SingularMatrixException)
            {
               Cancel = true;
            }
         }

         SearchPoint minimumPoint = searchPoints.FindMinY();
         return new RegionEliminationResults(minimumPoint.X, minimumPoint.Y, succeeded);
      }
   }
}
