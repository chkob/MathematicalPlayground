using System;
using System.Diagnostics;

namespace MathematicalPlayground.NumericalMethods
{
   public class GoldenSectionRegionElimination : RegionEliminationBase
   {
      private readonly double _sectionRatio;

      public GoldenSectionRegionElimination() : base() { _sectionRatio = (3.0 - Math.Sqrt(5.0)) / 2.0; }
      public override RegionEliminationResults RegionEliminate(Func<double, double> function, SearchPointCollection startPoint, double eliminationFraction)
      {
         Debug.Assert(startPoint.Count >= 2);
         Cancel = false;
         double result = 0.0;
         CalculateMissingValues(startPoint, function);
         SearchPointCollection searchPoints = new SearchPointCollection();
         searchPoints.MaxSize = 4;
         SearchPoint leftBracket = startPoint[0];
         SearchPoint rightBracket = startPoint[startPoint.Count - 1];
         searchPoints.Add(new SearchPoint(leftBracket.X, leftBracket.Y));
         double length = rightBracket.X - leftBracket.X;
         double stopRegionSize = length * eliminationFraction;
         SearchPoint newPoint; newPoint = new SearchPoint();
         newPoint.X = leftBracket.X + _sectionRatio * length;
         newPoint.Y = function(newPoint.X); searchPoints.Add(newPoint);
         newPoint = new SearchPoint();
         newPoint.X = leftBracket.X + (1.0 - _sectionRatio) * length;
         newPoint.Y = function(newPoint.X); searchPoints.Add(newPoint);
         searchPoints.Add(new SearchPoint(rightBracket.X, rightBracket.Y));
         
         while (!Cancel)
         {
            if (searchPoints[1].Y > searchPoints[2].Y)
            {
               searchPoints[0] = searchPoints[1];
               length = searchPoints[3].X - searchPoints[0].X;
               searchPoints[1] = searchPoints[2]; searchPoints[2] = new SearchPoint();
               searchPoints[2].X = searchPoints[0].X + (1.0 - _sectionRatio) * length; searchPoints[2].Y = function(searchPoints[2].X);
            }
            else
            {
               searchPoints[3] = searchPoints[2];
               length = searchPoints[3].X - searchPoints[0].X; searchPoints[2] = searchPoints[1];
               searchPoints[1] = new SearchPoint();
               searchPoints[1].X = searchPoints[0].X + _sectionRatio * length; searchPoints[1].Y = function(searchPoints[1].X);
            }
            
            if (length < stopRegionSize)
            {
               Cancel = true;
            }
         }
         
         result = (searchPoints[0].X + searchPoints[3].X) / 2.0;
         return new RegionEliminationResults(result, function(result));
      }
   }
}
