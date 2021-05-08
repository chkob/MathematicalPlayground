using System;

namespace MathematicalPlayground.NumericalMethods.ODE
{
   public class RungeKuttaBase
   {
      public RungeKuttaBase() { }
      public virtual double[][] Integrate(Parameter[] rungeKuttaParameters, Parameter x, Func<double>[] rungeKuttaFunctions, double xEnd, double step)
      {
         throw new NotImplementedException();
      }
   }
}
