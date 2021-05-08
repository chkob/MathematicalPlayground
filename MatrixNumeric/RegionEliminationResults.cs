namespace MathematicalPlayground.NumericalMethods
{
   public class RegionEliminationResults
   {
      private bool _succeeded = false;
      private double _xResult = double.NaN;
      private double _yResult = double.NaN;

      public RegionEliminationResults() { }

      public RegionEliminationResults(double result, double yResult)
      {
         _succeeded = true;
         _xResult = result;
         _yResult = yResult;
      }

      public RegionEliminationResults(bool succeeded)
      {
         _succeeded = succeeded;
      }

      public RegionEliminationResults(double xResult, double yResult, bool succeeded)
      {
         _succeeded = succeeded;
         _xResult = xResult;
         _yResult = yResult;
      }

      public bool Succeeded
      {
         get
         {
            return _succeeded;
         }
         set
         {
            _succeeded = value;
         }
      }

      public double XResult
      {
         get
         {
            return _xResult;
         }
         set
         {
            _xResult = value;
         }
      }

      public double YResult
      {
         get
         {
            return _yResult;
         }
         set
         {
            _yResult = value;
         }
      }
   }

}
