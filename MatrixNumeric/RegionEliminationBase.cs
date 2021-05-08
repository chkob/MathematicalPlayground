using System;

namespace MathematicalPlayground.NumericalMethods
{
   public class RegionEliminationBase
   {
      protected bool _cancel = false;
      protected int _numStartingPoints = 2;

      public RegionEliminationBase()
      {
      }

      public virtual RegionEliminationResults RegionEliminate(Func<double, double> function, SearchPointCollection startPoint, double eliminationFraction)
      {
         return new RegionEliminationResults();
      }

      public bool Cancel
      {
         get
         {
            bool result = false;
            lock (this)
            {
               result = _cancel;
            }
            return result;
         }
         set
         {
            lock (this)
            {
               _cancel = value;
            }
         }
      }

      protected void CalculateMissingValues(SearchPointCollection points, Func<double, double> function)
      {
         for (int i = 0; i < points.Count; i++)
         {
            if (double.IsNaN(points[i].Y))
            {
               points[i].Y = function(points[i].X);
            }
         }
      }

      public int NumStartingPoints
      {
         get
         {
            return _numStartingPoints;
         }
      }
   }
}
