using MathematicalPlayground.NumericalMethods.Enums;

namespace MathematicalPlayground.NumericalMethods
{
   public class Parameter
   {
      private bool m_bIsSolvedFor = true;
      private double m_dValue;
      private double m_dDerivativeStep = 1e-2;

      private DerivativeStepType m_enDerivativeStepType = DerivativeStepType.Fractional;

      public Parameter()
          : base()
      {

      }

      public Parameter(double dValue)
          : this()
      {
         m_dValue = dValue;
      }

      public Parameter(double dValue, double dDerivativeStep)
          : this(dValue)
      {
         m_dDerivativeStep = dDerivativeStep;
      }

      public Parameter(double dValue, double dDerivativeStep, DerivativeStepType enStepSizeType)
          : this(dValue, dDerivativeStep)
      {
         m_enDerivativeStepType = enStepSizeType;
      }

      public Parameter(double dValue, double dDerivativeStep, DerivativeStepType enStepSizeType, bool bOptimize)
          : this(dValue, dDerivativeStep, enStepSizeType)
      {
         m_bIsSolvedFor = bOptimize;
      }

      public Parameter(Parameter pClone)
      {
         m_bIsSolvedFor = pClone.IsSolvedFor;
         m_dValue = pClone.Value;
         m_dDerivativeStep = pClone.DerivativeStep;
         m_enDerivativeStepType = pClone.DerivativeStepType;
      }

      public bool IsSolvedFor
      {
         get { return m_bIsSolvedFor; }
         set { m_bIsSolvedFor = value; }
      }

      public double Value
      {
         get { return m_dValue; }
         set { m_dValue = value; }
      }

      public double DerivativeStep
      {
         get { return m_dDerivativeStep; }
         set { m_dDerivativeStep = value; }
      }

      public double DerivativeStepSize
      {
         get
         {
            double dDerivStepSize;

            if (m_enDerivativeStepType == DerivativeStepType.Absolute)
            {
               dDerivStepSize = m_dDerivativeStep;
            }
            else
            {
               if (m_dValue != 0.0)
               {
                  dDerivStepSize = m_dDerivativeStep * m_dValue;
               }
               else
               {
                  dDerivStepSize = m_dDerivativeStep;
               }
            }

            return dDerivStepSize;
         }
      }

      public DerivativeStepType DerivativeStepType
      {
         get { return m_enDerivativeStepType; }
         set { m_enDerivativeStepType = value; }
      }

      public static implicit operator double(Parameter p)
      {
         return p.Value;
      }

      public override string ToString()
      {
         return "Parameter: Value:" + Value.ToString() + " IsSolvedFor:" + m_bIsSolvedFor.ToString();
      }
   }
}
