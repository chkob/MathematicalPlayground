using System;
using System.Diagnostics;

namespace MathematicalPlayground.NumericalMethods
{
   public class LevenbergMarquardt
   {
      private MatrixNumeric m_matJacobian;
      private MatrixNumeric m_matResiduals;
      private MatrixNumeric m_matRegPar0;
      private Derivatives m_derDerivatives;
      private Parameter[] m_pRegressionParameters;
      private Parameter[] m_pObservedParameters;
      private Func<double> m_fFunction;
      private double[,] m_dData;
      private double m_dL0 = 100.0;
      private double m_dV = 10.0;

      public LevenbergMarquardt(Func<double> fFunction, ModelBase modModel, Parameter[] pObserved, double[,] dData, int nNumDerivativePoints)
      {
         Debug.Assert(dData.GetLength(0) == pObserved.Length + 1);

         m_dData = dData;
         m_pObservedParameters = pObserved;

         for (int i = 0; i < m_pObservedParameters.Length; i++)
         {
            m_pObservedParameters[i].IsSolvedFor = false;
         }

         modModel.ResetParameters();

         m_pRegressionParameters = modModel.GetSolvedForParameters();
         m_fFunction = fFunction;

         int nNumParameters = m_pRegressionParameters.Length;
         int nNumPoints = dData.GetLength(1);

         m_derDerivatives = new Derivatives(nNumDerivativePoints);
         m_matJacobian = new MatrixNumeric(nNumPoints, nNumParameters);
         m_matResiduals = new MatrixNumeric(nNumPoints, 1);
         m_matRegPar0 = new MatrixNumeric(nNumParameters, 1);
      }

      public LevenbergMarquardt(Func<double> fFunction, ModelBase modModel, Parameter[] pObserved, double[,] dData)
          : this(fFunction, modModel, pObserved, dData, 3)
      {

      }

      public void Iterate()
      {
         int nNumPoints = m_dData.GetLength(1);
         int nNumParameters = m_pRegressionParameters.Length;

         double dCurrentResidual = 0.0;

         for (int i = 0; i < nNumPoints; i++)
         {
            for (int j = 0; j < m_pObservedParameters.Length; j++)
            {
               m_pObservedParameters[j].Value = m_dData[j, i];
            }

            double dFunction = m_fFunction();
            double dResidual = m_dData[m_pObservedParameters.Length, i] - dFunction;

            m_matResiduals[i, 0] = dResidual;

            dCurrentResidual += dResidual * dResidual;

            for (int j = 0; j < nNumParameters; j++)
            {
               m_matJacobian[i, j] = m_derDerivatives.ComputePartialDerivative(m_fFunction, m_pRegressionParameters[j], 1, dFunction);
            }
         }

         for (int i = 0; i < nNumParameters; i++)
         {
            m_matRegPar0[i, 0] = m_pRegressionParameters[i];
         }

         MatrixNumeric matJacobianTrans = m_matJacobian.Transpose();
         MatrixNumeric matJTranResid = matJacobianTrans * m_matResiduals;
         MatrixNumeric matJTranJ = matJacobianTrans * m_matJacobian;
         MatrixNumeric matJTranJDiag = new MatrixNumeric(matJTranJ.NumRows, matJTranJ.NumRows);

         for (int i = 0; i < matJTranJ.NumRows; i++)
         {
            matJTranJDiag[i, i] = matJTranJ[i, i];
         }

         MatrixNumeric matDelta = null;
         MatrixNumeric matNewRegPars = null;

         double dNewResidual = dCurrentResidual + 1.0;

         m_dL0 /= m_dV;

         while (dNewResidual > dCurrentResidual)
         {
            dNewResidual = 0.0;

            m_dL0 *= m_dV;

            MatrixNumeric matLHS = matJTranJ + m_dL0 * matJTranJDiag;

            matDelta = matLHS.SolveFor(matJTranResid); ;

            matNewRegPars = m_matRegPar0 + matDelta;

            for (int i = 0; i < nNumParameters; i++)
            {
               m_pRegressionParameters[i].Value = matNewRegPars[i, 0];
            }

            for (int i = 0; i < nNumPoints; i++)
            {
               for (int j = 0; j < m_pObservedParameters.Length; j++)
               {
                  m_pObservedParameters[j].Value = m_dData[j, i];
               }

               double dFunction = m_fFunction();
               double dResidual = m_dData[m_pObservedParameters.Length, i] - dFunction;

               dNewResidual += dResidual * dResidual;
            }
         }

         m_dL0 /= m_dV;
      }
   }
}
