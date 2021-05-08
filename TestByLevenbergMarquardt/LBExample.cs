using MathematicalPlayground.NumericalMethods;
using System;

namespace MathematicalPlayground.TestByLevenbergMarquardt
{
   public class LBExample : ModelBase
   {
      private Parameter m_pA = new Parameter(20.0);
      private Parameter m_pB = new Parameter(5000.0);
      private Parameter m_pC = new Parameter(1.0);
      private Parameter m_pT = new Parameter(1.0);

      public LBExample()
          : base()
      {
      }

      public Parameter A
      {
         get { return m_pA; }
         set { m_pA = value; }
      }

      public Parameter B
      {
         get { return m_pB; }
         set { m_pB = value; }
      }

      public Parameter C
      {
         get { return m_pC; }
         set { m_pC = value; }
      }

      public Parameter T
      {
         get { return m_pT; }
         set { m_pT = value; }
      }

      public double VaporPressure()
      {
         return Math.Exp(m_pA - m_pB / (m_pT + m_pC));
      }
   }
}
