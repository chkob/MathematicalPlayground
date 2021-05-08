using MathematicalPlayground.NumericalMethods;
using System;

namespace MathematicalPlayground.TestLinearRegression
{
   class Program
   {
      private static double[] dX = new double[] { 2.3601, 2.3942, 2.4098, 2.4268, 2.4443, 2.4552, 2.4689, 2.4885, 2.5093, 2.5287 };

      private static double[] dY = new double[] { 133.322, 666.612, 1333.22, 2666.45, 5332.9, 7999.35, 13332.2, 26664.5, 53329, 101325 };

      static void Main(string[] args)
      {
         Console.WriteLine("Hello World!");

         var order1stResult = Test1stOrder();
         var order2ndResult = Test2ndOrder();
         var order3rdResult = Test3rdOrder();
         var order4thResult = Test4thOrder();
         var order5thByPowResult = TestNthOrderByPow(5);
      }

      public static double[] Test1stOrder()
      {
         int nPolyOrder = 1;

         double[,] dZ = new double[dY.Length, nPolyOrder + 1];

         for (int i = 0; i < dY.Length; i++)
         {
            dZ[i, 0] = 1.0;
            dZ[i, 1] = dX[i];
         }

         double[] dCoefs = MatrixNumeric.Regress(dZ, dY);

         return dCoefs;
      }

      public static double[] Test2ndOrder()
      {
         int nPolyOrder = 2;

         double[,] dZ = new double[dY.Length, nPolyOrder + 1];

         for (int i = 0; i < dY.Length; i++)
         {
            dZ[i, 0] = 1.0;
            dZ[i, 1] = dX[i];
            dZ[i, 2] = dX[i] * dX[i];
         }

         double[] dCoefs = MatrixNumeric.Regress(dZ, dY);

         return dCoefs;
      }

      public static double[] Test3rdOrder()
      {
         int nPolyOrder = 3;

         double[,] dZ = new double[dY.Length, nPolyOrder + 1];

         for (int i = 0; i < dY.Length; i++)
         {
            dZ[i, 0] = 1.0;
            dZ[i, 1] = dX[i];
            dZ[i, 2] = dX[i] * dX[i];
            dZ[i, 3] = dX[i] * dX[i] * dX[i];
         }

         double[] dCoefs = MatrixNumeric.Regress(dZ, dY);

         return dCoefs;
      }

      public static double[] Test4thOrder()
      {
         int nPolyOrder = 4;

         double[,] dZ = new double[dY.Length, nPolyOrder + 1];

         for (int i = 0; i < dY.Length; i++)
         {
            dZ[i, 0] = 1.0;
            dZ[i, 1] = dX[i];
            dZ[i, 2] = dX[i] * dX[i];
            dZ[i, 3] = dX[i] * dX[i] * dX[i];
            dZ[i, 4] = dX[i] * dX[i] * dX[i] * dX[i];
         }

         double[] dCoefs = MatrixNumeric.Regress(dZ, dY);

         return dCoefs;
      }

      public static double[] TestNthOrderByPow(int nth)
      {
         int nPolyOrder = nth;

         double[,] dZ = new double[dY.Length, nPolyOrder + 1];

         for (int i = 0; i < dY.Length; i++)
         {
            for (int j = 0; j < nPolyOrder + 1; j++)
            {
               dZ[i, j] = Math.Pow(dX[i], (double)j);
            }
         }

         double[] dCoefs = MatrixNumeric.Regress(dZ, dY);

         return dCoefs;
      }
   }
}
