namespace MathematicalPlayground.NumericalMethods
{
   public class SearchPoint
   {
      private double _x = double.NaN;
      private double _y = double.NaN;
      
      public SearchPoint()
      {
      }
      
      public SearchPoint(double x)
      {
         _x = x;
      }
      
      public SearchPoint(double x, double y)
      {
         _x = x;
         _y = y;
      }

      public double X
      {
         get
         {
            return _x;
         }
         set
         {
            _x = value;
         }
      }
      
      public double Y
      {
         get
         {
            return _y;
         }
         set
         {
            _y = value;
         }
      }
      
      public override string ToString()
      {
         return "SearchPoint X:" + _x.ToString() + " Y:" + _y.ToString();
      }
   }
}
