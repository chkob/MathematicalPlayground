using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MathematicalPlayground.NumericalMethods.ODE
{
   public class RungeKutta5Butcher : RungeKuttaBase
   {
      public RungeKutta5Butcher() : base() { }

      public override double[][] Integrate(Parameter[] rungeKuttaParameters, Parameter x, Func<double>[] rungeKuttaFunctions, double xEnd, double step)
      {
         Debug.Assert(rungeKuttaParameters.Length == rungeKuttaFunctions.Length);
         double xStart = x;
         double stepSize = (xEnd - xStart) / step;
         int integerStepSize = (int)stepSize;
         if (stepSize > (double)integerStepSize)
         {
            integerStepSize++;
         }

         integerStepSize++;
         Collection<double[]> returnCollection = new Collection<double[]>();
         double[] row = new double[rungeKuttaParameters.Length + 1];
         row[0] = x;
         for (int i = 0; i < rungeKuttaParameters.Length; i++)
         {
            row[i + 1] = rungeKuttaParameters[i];
         }

         returnCollection.Add(row);
         double currentX = xStart;
         double[,] ks = new double[6, rungeKuttaParameters.Length];
         double x0 = xStart;
         double[] y0 = new double[rungeKuttaParameters.Length];
         double currentStep = step;
         
         while (currentX < xEnd)
         {
            if (currentX + currentStep > xEnd)
            {
               currentStep = xEnd - currentX;
            }

            x0 = currentX; for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               y0[i] = rungeKuttaParameters[i];
            }

            //k0sx.Value = x0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[0, i] = rungeKuttaFunctions[i]();
            }

            //k1sx.Value = x0 + currentStep / 4.0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + (ks[0, i] / 4.0) * currentStep;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++) 
            {
               ks[1, i] = rungeKuttaFunctions[i]();
            }

            //k2sx.Value = x0 + currentStep / 4.0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + (ks[0, i] / 8.0 + ks[1, i] / 8.0) * currentStep;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[2, i] = rungeKuttaFunctions[i]();
            }

            //k3sx.Value = x0 + currentStep / 2.0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + (-ks[1, i] / 2.0 + ks[2, i]) * currentStep;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[3, i] = rungeKuttaFunctions[i]();
            }

            //k4sx.Value = x0 + currentStep * 3.0 / 4.0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++) 
            {
               rungeKuttaParameters[i].Value = y0[i] + (ks[0, i] * 3.0 / 16.0 + ks[3, i] * 9.0 / 16.0) * currentStep;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[4, i] = rungeKuttaFunctions[i]();
            }

            //k5sx.Value = x0 + currentStep;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + (-ks[0, i] * 3.0 / 7.0 + ks[1, i] * 2.0 / 7.0 + ks[2, i] * 12.0 / 7.0 - ks[3, i] * 12.0 / 7.0 + ks[4, i] * 8.0 / 7.0) * currentStep;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[5, i] = rungeKuttaFunctions[i]();
            }

            //FinalcurrentX += currentStep;
            row = new double[rungeKuttaParameters.Length + 1];
            row[0] = currentX;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + ((7.0 * ks[0, i] + 32.0 * ks[2, i] + 12.0 * ks[3, i] + 32.0 * ks[4, i] + 7.0 * ks[5, i]) / 90.0) * currentStep;
               row[i + 1] = rungeKuttaParameters[i];
            }

            returnCollection.Add(row);
         }
         return returnCollection.ToArray();
      }
   }
}
