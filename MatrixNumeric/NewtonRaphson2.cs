using System;
using System.Diagnostics;

namespace MathematicalPlayground.NumericalMethods
{
   public class NewtonRaphson2
   {
      private MatrixNumeric m_matJacobian;
      private MatrixNumeric m_matFunction;
      private MatrixNumeric m_matX0;
      private Derivatives m_derDerivatives;
      private Parameter[] m_pParameters;
      private Func<double>[] m_fFunctions;

      public NewtonRaphson2(Parameter[] pParameters, Func<double>[] fnFunctions, int nNumDerivativePoints)
      {
         m_pParameters = pParameters;
         m_fFunctions = fnFunctions;
         int nNumFunctions = m_fFunctions.Length;
         int nNumParameters = m_pParameters.Length;

         Debug.Assert(nNumParameters == nNumFunctions);

         m_derDerivatives = new Derivatives(nNumDerivativePoints);
         m_matJacobian = new MatrixNumeric(nNumFunctions, nNumParameters);
         m_matFunction = new MatrixNumeric(nNumFunctions, 1);
         m_matX0 = new MatrixNumeric(nNumFunctions, 1);
      }

      public NewtonRaphson2(Parameter[] pParameters, Func<double>[] fnFunctions)
          : this(pParameters, fnFunctions, 3)
      {
      }

      public NewtonRaphson2(Func<double>[] fnFunctions, ModelBase modModel, int nNumDerivativePoints)
          : this(modModel.GetSolvedForParameters(), fnFunctions, nNumDerivativePoints)
      {
      }

      public NewtonRaphson2(Func<double>[] fnFunctions, ModelBase modModel)
          : this(fnFunctions, modModel, 3)
      {
      }

      public void Iterate()
      {
         int nNumFunctions = m_fFunctions.Length;
         int nNumParameters = m_pParameters.Length;
         for (int i = 0; i < nNumFunctions; i++)
         {
            m_matFunction[i, 0] = m_fFunctions[i]();
            m_matX0[i, 0] = m_pParameters[i];
         }

         for (int i = 0; i < nNumFunctions; i++)
         {
            for (int j = 0; j < nNumParameters; j++)
            {
               m_matJacobian[i, j] = m_derDerivatives.ComputePartialDerivative(m_fFunctions[i], m_pParameters[j], 1, m_matFunction[i, 0]);
            }
         }

         MatrixNumeric matNewXs = m_matX0 - m_matJacobian.SolveFor(m_matFunction);

         for (int i = 0; i < nNumFunctions; i++)
         {
            m_pParameters[i].Value = matNewXs[i, 0];
         }
      }
   }
}
