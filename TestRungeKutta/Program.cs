using MathematicalPlayground.NumericalMethods;
using MathematicalPlayground.NumericalMethods.ODE;
using System;

namespace MathematicalPlayground.TestRungeKutta
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");

         TestEuler();
         TestRungeKutta2Heun();
         TestRungeKutta2Ralston();
         TestRungeKutta2ImprovedPolygon();
         TestRungeKutta3();
         TestRungeKutta4();
         TestRungeKutta5Butcher();
         TestRungeKutta54Fehlberg();
      }

      public static void TestEuler()
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
      }

      public static void TestRungeKutta2Heun()
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
      }

      public static void TestRungeKutta2Ralston()
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
      }

      public static void TestRungeKutta2ImprovedPolygon()
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
      }

      public static void TestRungeKutta3()
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
      }

      public static void TestRungeKutta4()
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
      }

      public static void TestRungeKutta5Butcher()
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
      }

      public static void TestRungeKutta54Fehlberg()
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
      }
   }
}
