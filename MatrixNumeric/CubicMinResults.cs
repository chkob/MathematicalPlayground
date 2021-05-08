using MathematicalPlayground.NumericalMethods.Enums;

namespace MathematicalPlayground.NumericalMethods
{
   public class CubicMinResults
   {
      private double _localMinX;
      private double _localMaxX;
      private CubicResultType _resultType;
      
      public CubicMinResults() { }
      
      public double LocalMinX
      {
         get
         {
            return _localMinX;
         }
         set
         {
            _localMinX = value;
         }
      }

      public double LocalMaxX
      {
         get
         {
            return _localMaxX;
         }
         set
         {
            _localMaxX = value;
         }
      }
      
      public CubicResultType ResultType
      {
         get
         {
            return _resultType;
         }
         set
         {
            _resultType = value;
         }
      }
   }
}
