using MathematicalPlayground.NumericalMethods.Exceptions;
using System;
using System.Diagnostics;

namespace MathematicalPlayground.NumericalMethods
{
   public class ParabolicRegionElimination : RegionEliminationBase
   {
      public ParabolicRegionElimination()
         : base()
      {
         _numStartingPoints = 3;
      }
      public override RegionEliminationResults RegionEliminate(Func<double, double> function, SearchPointCollection startPoint, double eliminationFraction)
      {
         Debug.Assert(startPoint.Count >= 2);
         bool succeeded = true; Cancel = false;
         SearchPointCollection searchPoints = startPoint.Clone(3);
         CalculateMissingValues(searchPoints, function);
         if (searchPoints.Count < 3)
         {
            double midPoint = (searchPoints[0].X + searchPoints[1].X) / 2.0;
            double functionValue = function(midPoint);
            searchPoints.Add(new SearchPoint(midPoint, functionValue));
         }

         double stopRegionSize = (searchPoints[2].X - searchPoints[0].X) * eliminationFraction;
         while (!Cancel)
         {
            try
            {
               double newX = searchPoints.ParabolicMinYEstimate();
               if (!double.IsNaN(newX))
               {
                  if (searchPoints.CanAdd(newX))
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
                  }
               }
               else
               {
                  Cancel = true; succeeded = false;
               }
               
               if (searchPoints[2].X - searchPoints[0].X < stopRegionSize)
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
