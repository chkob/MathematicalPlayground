using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace MathematicalPlayground.NumericalMethods.ODE
{
   public class Euler : RungeKuttaBase
   {
      public Euler() : base() { }
      public override double[][] Integrate(Parameter[] rungeKuttaParameters, Parameter x, Func<double>[] rungeKuttaFunctions, double xEnd, double step)
      {
         double xStart = x; double stepSize = (xEnd - xStart) / step;
         int integerStepSize = (int)stepSize;
         if (stepSize > (double)integerStepSize)
         {
            integerStepSize++;
         }

         integerStepSize++;
         Collection<double[]> returnCollection = new Collection<double[]>();
         double[] row = new double[rungeKuttaParameters.Length + 1];
         row[0] = xStart;
         for (int i = 0; i < rungeKuttaParameters.Length; i++)
         {
            row[i + 1] = rungeKuttaParameters[i];
         }
         
         returnCollection.Add(row);

         double currentX = x;
         double[] dCurrentFs = new double[rungeKuttaParameters.Length];
         double currentStep = step;
         
         while (currentX < xEnd)
         {
            if (currentX + currentStep > xEnd)
            {
               currentStep = xEnd - currentX;
            }
            
            x.Value = currentX;
            
            for (int j = 0; j < rungeKuttaParameters.Length; j++)
            {
               dCurrentFs[j] = rungeKuttaFunctions[j]();
            }
            
            row = new double[rungeKuttaParameters.Length + 1];
            
            for (int j = 0; j < rungeKuttaParameters.Length; j++)
            {
               rungeKuttaParameters[j].Value = rungeKuttaParameters[j] + dCurrentFs[j] * currentStep;
               row[j + 1] = rungeKuttaParameters[j];
            }
            
            currentX += currentStep;
            row[0] = currentX;
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
         double xStart = x; double stepSize = (xEnd - xStart) / step;
         int integerStepSize = (int)stepSize;
         if (stepSize > (double)integerStepSize)
         {
            integerStepSize++;
         }

         integerStepSize++;
         Collection<double[]> returnCollection = new Collection<double[]>();
         double[] row = new double[rungeKuttaParameters.Length + 1];
         row[0] = xStart;
         for (int i = 0; i < rungeKuttaParameters.Length; i++)
         {
            row[i + 1] = rungeKuttaParameters[i];
         }

         returnCollection.Add(row);

         double currentX = x;
         double[] dCurrentFs = new double[rungeKuttaParameters.Length];
         double currentStep = step;

         bool breaking = false;

         while (currentX < xEnd && !breaking)
         {
            if (currentX + currentStep > xEnd)
            {
               currentStep = xEnd - currentX;
            }

            x.Value = currentX;

            for (int j = 0; j < rungeKuttaParameters.Length; j++)
            {
               dCurrentFs[j] = rungeKuttaFunctions[j]();
            }

            row = new double[rungeKuttaParameters.Length + 1];

            for (int j = 0; j < rungeKuttaParameters.Length; j++)
            {
               rungeKuttaParameters[j].Value = rungeKuttaParameters[j] + dCurrentFs[j] * currentStep;
               row[j + 1] = rungeKuttaParameters[j];
            }

            currentX += currentStep;
            row[0] = currentX;
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
