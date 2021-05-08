using System;

namespace MathematicalPlayground.NumericalMethods
{
   public class BracketingBase
   {
      public BracketingBase() { }

      public virtual SearchPointCollection Bracket(Func<double, double> function, double startPosition, double step, int targetReturnPoints)
      {
         throw new NotImplementedException();
      }

      public virtual SearchPointCollection Bracket(Func<double, double> function, double startPosition, double step)
      {
         return Bracket(function, startPosition, step, 2);
      }
      protected bool _cancel = false;
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
   }
}
