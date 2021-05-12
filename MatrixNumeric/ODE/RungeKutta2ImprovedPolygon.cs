using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MathematicalPlayground.NumericalMethods.ODE
{
   public class RungeKutta2ImprovedPolygon : RungeKuttaBase
   {
      public RungeKutta2ImprovedPolygon() : base() { }
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
         double[,] ks = new double[2, rungeKuttaParameters.Length];
         double x0 = xStart;
         double[] y0 = new double[rungeKuttaParameters.Length];
         double currentStep = step;

         while (currentX < xEnd)
         {
            if (currentX + currentStep > xEnd)
            {
               currentStep = xEnd - currentX;
            }

            x0 = currentX;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               y0[i] = rungeKuttaParameters[i];
            }

            //k0sx.Value = x0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[0, i] = rungeKuttaFunctions[i]();
            }

            //k1sx.Value = x0 + currentStep / 2.0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + ks[0, i] * currentStep / 2.0;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[1, i] = rungeKuttaFunctions[i]();
            }

            //FinalcurrentX += currentStep;
            row = new double[rungeKuttaParameters.Length + 1];
            currentX += currentStep;
            row[0] = currentX;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + ks[1, i] * currentStep;
               row[i + 1] = rungeKuttaParameters[i];
            }

            returnCollection.Add(row);
         }

         return returnCollection.ToArray();
      }

      public override double[][] IntegrateWithBreak(Parameter[] rungeKuttaParameters,
               Parameter x,
               Func<double>[] rungeKuttaFunctions,
               Func<bool>[] breakingFunctions,
               double xEnd,
               double step)
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
         double[,] ks = new double[2, rungeKuttaParameters.Length];
         double x0 = xStart;
         double[] y0 = new double[rungeKuttaParameters.Length];
         double currentStep = step;

         bool breaking = false;

         while (currentX < xEnd && !breaking)
         {
            if (currentX + currentStep > xEnd)
            {
               currentStep = xEnd - currentX;
            }

            x0 = currentX;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               y0[i] = rungeKuttaParameters[i];
            }

            //k0sx.Value = x0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[0, i] = rungeKuttaFunctions[i]();
            }

            //k1sx.Value = x0 + currentStep / 2.0;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + ks[0, i] * currentStep / 2.0;
            }

            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               ks[1, i] = rungeKuttaFunctions[i]();
            }

            //FinalcurrentX += currentStep;
            row = new double[rungeKuttaParameters.Length + 1];
            currentX += currentStep;
            row[0] = currentX;
            for (int i = 0; i < rungeKuttaParameters.Length; i++)
            {
               rungeKuttaParameters[i].Value = y0[i] + ks[1, i] * currentStep;
               row[i + 1] = rungeKuttaParameters[i];
            }

            returnCollection.Add(row);

            for (int j = 0; j < breakingFunctions.Length; j++)
            {
               breaking |= breakingFunctions[j]();

               if (breaking)
               {
                  break;
               }
            }
         }

         return returnCollection.ToArray();
      }
   }
}
