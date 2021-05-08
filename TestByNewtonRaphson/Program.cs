using MathematicalPlayground.NumericalMethods;
using MathematicalPlayground.TestByNewtonRaphson.Models;
using System;
using System.Collections.Generic;

namespace MathematicalPlayground.TestByNewtonRaphson
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");

         var result1 = CalcByNewtonRaphson(15);
         CalcByNewtonRaphson2(15);
      }

      private static string CalcByNewtonRaphson(int iNumIteration)
      {
         NonLinearExample rxn = new NonLinearExample();

         Func<double>[] funcs = new Func<double>[] { rxn.F1, rxn.F2, rxn.F3, rxn.F4 };

         NewtonRaphson nr = new NewtonRaphson(funcs, rxn);

         string strResults = "n\tE1\tE2\tE3\tE4\t\tF1\tF2\tF3\tF4\n";

         int nIteration = 0;

         strResults += (nIteration++).ToString() + "\t" + rxn.E1.Value.ToString() + "\t" + rxn.E2.Value.ToString() + "\t" + rxn.E3.Value.ToString() + "\t" + rxn.E4.Value.ToString() + "\t\t" + rxn.F1().ToString() + "\t" + rxn.F2().ToString() + "\t" + rxn.F3().ToString() + "\t" + rxn.F4().ToString() + "\n";

         for (int i = 0; i < iNumIteration; i++)
         {
            nr.Iterate();

            strResults += (nIteration++).ToString() + "\t" + rxn.E1.Value.ToString() + "\t" + rxn.E2.Value.ToString() + "\t" + rxn.E3.Value.ToString() + "\t" + rxn.E4.Value.ToString() + "\t\t" + rxn.F1().ToString() + "\t" + rxn.F2().ToString() + "\t" + rxn.F3().ToString() + "\t" + rxn.F4().ToString() + "\n";
         }

         return strResults;
      }

      private static void CalcByNewtonRaphson2(int iNumIteration)
      {
         Parameter E1 = new Parameter(1.0);
         Parameter E2 = new Parameter(1.0);
         Parameter E3 = new Parameter(1.0);
         Parameter E4 = new Parameter(1.0);

         Parameter[] pPars = new Parameter[] { E1, E2, E3, E4 };

         Func<double>[] fnFunctions = new Func<double>[]
         {
            () => 0.005 * (100.0 - E1 - 2.0 * E2) * (1.0 - E1 - E3) - 100.0 * E1,
            () => 500.0 * Math.Pow(100.0 - E1 - 2.0 * E2, 2.0) - 100.0 * E2,
            () => 0.5 * (100.0 - E1 - E3 - 2.0 * E4) - 100.0 * E3,
            () => 10000.0 * Math.Pow(100.0 * E3 - 2.0 * E4, 2.0) - 100.0 * E4
         };

         NewtonRaphson2 nr = new NewtonRaphson2(pPars, fnFunctions);

         for (int i = 0; i < iNumIteration; i++)
         {
            nr.Iterate();
         }
      }
   }
}
