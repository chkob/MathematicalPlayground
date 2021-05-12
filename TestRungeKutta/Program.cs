using MathematicalPlayground.NumericalMethods;
using MathematicalPlayground.NumericalMethods.ODE;
using System;
using System.Linq;

namespace MathematicalPlayground.TestRungeKutta
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");

         Func<double[][]>[] functionsNoBreak = new Func<double[][]>[]
         {
            TestEuler,
            TestRungeKutta2Heun,
            TestRungeKutta2Ralston,
            TestRungeKutta2ImprovedPolygon,
            TestRungeKutta3,
            TestRungeKutta4,
            TestRungeKutta5Butcher,
            TestRungeKutta54Fehlberg
         };

         var resultsNoBreak = functionsNoBreak.Select(x => x.Invoke()).ToList();

         Func<double[][]>[] functionsWithBreak = new Func<double[][]>[]
         {
            TestEulerWithBreaking,
         };

         var resultsWithBreak = functionsWithBreak.Select(x => x.Invoke()).ToList();
      }

      public static double[][] TestEuler()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[] 
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new Euler();
         
         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestEulerWithBreaking()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         Func<bool>[] breakingFunctions = new Func<bool>[]
         {
            () => X1 < 2.0 * X2,
            () => 2.0 * X1 + X2 > 10
         };

         RungeKuttaBase ode = new Euler();

         double[][] dRes = ode.IntegrateWithBreak(parameters, T, functions, breakingFunctions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta2Heun()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[] 
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta2Heun();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta2Ralston()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta2Ralston();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta2ImprovedPolygon()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta2ImprovedPolygon();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta3()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta3();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta4()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta4();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta5Butcher()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta5Butcher();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }

      public static double[][] TestRungeKutta54Fehlberg()
      {
         Parameter T = new Parameter(0.0);
         Parameter X1 = new Parameter(0.0);
         Parameter X2 = new Parameter(4.0);
         Parameter[] parameters = new Parameter[] { X1, X2 };
         Func<double>[] functions = new Func<double>[]
         {
            () => X1 - 2.0 * X2,
            () => 2.0 * X1 + X2
         };

         RungeKuttaBase ode = new RungeKutta54Fehlberg();

         double[][] dRes = ode.Integrate(parameters, T, functions, 3.3, 0.1);

         return dRes;
      }
   }
}
