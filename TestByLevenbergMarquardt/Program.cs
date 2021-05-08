using MathematicalPlayground.NumericalMethods;
using System;

namespace MathematicalPlayground.TestByLevenbergMarquardt
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");

         Calc(50);
      }

      private static void Calc(int numIteration)
      {
         double[] dTemperature = new double[] { 229.15, 247.85, 256.95, 267.15, 278.15, 285.25, 294.35, 307.95, 323.05, 337.85 };
         double[] dPressure = new double[] { 133.3224898, 666.6124489, 1333.224898, 2666.449795, 5332.899591, 7999.349386, 13332.24898, 26664.49795, 53328.99591, 101325.0922 };

         double[,] dZ = new double[2, dTemperature.Length];

         for (int i = 0; i < dTemperature.Length; i++)
         {
            dZ[0, i] = dTemperature[i];
            dZ[1, i] = dPressure[i];
         }

         LBExample gn = new LBExample();
         Parameter[] pPars = new Parameter[] { gn.T };
         LevenbergMarquardt nr = new LevenbergMarquardt(gn.VaporPressure, gn, pPars, dZ, 3);

         for (int i = 0; i < numIteration; i++)
         {
            nr.Iterate();
         }
      }
   }
}
