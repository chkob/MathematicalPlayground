using System;

namespace MathematicalPlayground.NumericalMethods.Exceptions
{
   public class SingularMatrixException : ArithmeticException
   {
      public SingularMatrixException()
          : base("Invalid operation on a singular matrix.")
      {
      }
   }
}
