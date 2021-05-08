using System;
using System.Diagnostics;

namespace MathematicalPlayground.NumericalMethods
{
   public class IntervalHalvingRegionElimination : RegionEliminationBase
   {
      public IntervalHalvingRegionElimination() 
         : base()
      {
      }

      public override RegionEliminationResults RegionEliminate(Func<double, double> function, SearchPointCollection startPoint, double eliminationFraction)
      {
         Debug.Assert(startPoint.Count >= 2);
         Cancel = false;
         double result = 0.0;
         CalculateMissingValues(startPoint, function);
         SearchPointCollection searchPoints = new SearchPointCollection();
         searchPoints.MaxSize = 5; SearchPoint leftBracket = startPoint[0];
         SearchPoint rightBracket = startPoint[startPoint.Count - 1];
         searchPoints.Add(new SearchPoint(leftBracket.X, leftBracket.Y));
         double length = rightBracket.X - leftBracket.X;
         double stopRegionSize = length * eliminationFraction;
         SearchPoint newPoint; newPoint = new SearchPoint();
         newPoint.X = leftBracket.X + length / 4.0;
         newPoint.Y = function(newPoint.X);
         searchPoints.Add(newPoint);
         newPoint = new SearchPoint();
         newPoint.X = leftBracket.X + length / 2.0;
         newPoint.Y = function(newPoint.X);
         searchPoints.Add(newPoint);
         newPoint = new SearchPoint();
         newPoint.X = leftBracket.X + (3.0 / 4.0) * length;
         newPoint.Y = function(newPoint.X);
         searchPoints.Add(newPoint);
         searchPoints.Add(new SearchPoint(rightBracket.X, rightBracket.Y));
         while (!Cancel)
         { 
            if (searchPoints[1].Y < searchPoints[2].Y)
            {
               searchPoints[4] = searchPoints[2];
               searchPoints[2] = searchPoints[1];
               length = searchPoints[4].X - searchPoints[0].X;
               searchPoints[1] = new SearchPoint();
               searchPoints[1].X = searchPoints[0].X + length / 4.0;
               searchPoints[1].Y = function(searchPoints[1].X);
               searchPoints[3] = new SearchPoint();
               searchPoints[3].X = searchPoints[0].X + (3.0 / 4.0) * length;
               searchPoints[3].Y = function(searchPoints[3].X);
            }
            else if (searchPoints[3].Y < searchPoints[2].Y)
            {
               searchPoints[0] = searchPoints[2];
               searchPoints[2] = searchPoints[3];
               length = searchPoints[4].X - searchPoints[0].X;
               searchPoints[1] = new SearchPoint();
               searchPoints[1].X = searchPoints[0].X + length / 4.0;
               searchPoints[1].Y = function(searchPoints[1].X);
               searchPoints[3] = new SearchPoint();
               searchPoints[3].X = searchPoints[0].X + (3.0 / 4.0) * length; searchPoints[3].Y = function(searchPoints[3].X);
            }
            else
            {
               searchPoints[0] = searchPoints[1];
               searchPoints[4] = searchPoints[3];
               length = searchPoints[4].X - searchPoints[0].X;
               searchPoints[1] = new SearchPoint();
               searchPoints[1].X = searchPoints[0].X + length / 4.0;
               searchPoints[1].Y = function(searchPoints[1].X);
               searchPoints[3] = new SearchPoint(); searchPoints[3].X = searchPoints[0].X + (3.0 / 4.0) * length;
               searchPoints[3].Y = function(searchPoints[3].X);
            }
            
            if (length < stopRegionSize)
            {
               Cancel = true;
            }
         }
         
         result = (searchPoints[0].X + searchPoints[4].X) / 2.0;
         
         return new RegionEliminationResults(result, function(result));
      }
   }
}
