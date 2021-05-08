using MathematicalPlayground.NumericalMethods.Enums;

namespace MathematicalPlayground.NumericalMethods
{
   public class LUDecompositionResults
   {
      private MatrixNumeric m_matL;
      private MatrixNumeric m_matU;
      private int[] m_nPivotArray;
      private LUDecompositionResultStatus m_enuStatus = LUDecompositionResultStatus.OK;

      public LUDecompositionResults()
      {
      }

      public LUDecompositionResults(MatrixNumeric matL, MatrixNumeric matU, int[] nPivotArray, LUDecompositionResultStatus enuStatus)
      {
         m_matL = matL;
         m_matU = matU;
         m_nPivotArray = nPivotArray;
         m_enuStatus = enuStatus;
      }

      public MatrixNumeric L
      {
         get { return m_matL; }
         set { m_matL = value; }
      }

      public MatrixNumeric U
      {
         get { return m_matU; }
         set { m_matU = value; }
      }

      public int[] PivotArray
      {
         get { return m_nPivotArray; }
         set { m_nPivotArray = value; }
      }

      public LUDecompositionResultStatus Status
      {
         get { return m_enuStatus; }
         set { m_enuStatus = value; }
      }
   }
}
