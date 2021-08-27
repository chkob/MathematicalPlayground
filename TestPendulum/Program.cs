using MathematicalPlayground.NumericalMethods;
using MathematicalPlayground.NumericalMethods.ODE;
using System;
using System.Linq;

namespace MathematicalPlayground.TestPendulum
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");

         Func<double, double, double[][]>[] functionsNoBreak = new Func<double, double, double[][]>[]
         {
            TestEulerSHM,
            TestEulerDampHM,
            TestEulerForceHM,
            TestEulerDampedFourceHM
         };

         var resultsNoBreak = functionsNoBreak.Select(x => x.Invoke(1, 25)).ToList();
      }

      public static double[][] TestEulerSHM(double g, double l)
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(10.0);
         Parameter X2 = new Parameter(0.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X2,
            () => -1*Math.Sin(X1)
         };

         RungeKuttaBase ode = new Euler();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestEulerDampHM(double g, double l)
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(10.0);
         Parameter X2 = new Parameter(0.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X2,
            () => -1*(g/l)*Math.Sin(X1)
         };

         RungeKuttaBase ode = new Euler();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestEulerForceHM(double g, double l)
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(10.0);
         Parameter X2 = new Parameter(0.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };

         double extForce = 3.0;
         Func<double>[] functions = new Func<double>[]
         {
            () => X2,
            () => -1*Math.Sin(X1) + Math.Sin(extForce)
         };

         RungeKuttaBase ode = new Euler();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestEulerDampedFourceHM(double g, double l)
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(10.0);
         Parameter X2 = new Parameter(0.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };

         double extForce = 3.0;
         Func<double>[] functions = new Func<double>[]
         {
            () => X2,
            () => -1*(g/l)*Math.Sin(X1) + Math.Sin(extForce)
         };

         RungeKuttaBase ode = new Euler();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }
   }
}
