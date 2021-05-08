using MathematicalPlayground.NumericalMethods;
using System;

namespace MathematicalPlayground.TestByNewtonRaphson.Models
{
   public class NonLinearExample : ModelBase
   {
      private Parameter m_pE1 = new Parameter(1.0);
      private Parameter m_pE2 = new Parameter(1.0);
      private Parameter m_pE3 = new Parameter(1.0);
      private Parameter m_pE4 = new Parameter(1.0);

      public NonLinearExample()
          : base()
      {
      }

      public double F1()
      {
         return 0.005 * (100.0 - m_pE1 - 2.0 * m_pE2) * (1.0 - m_pE1 - m_pE3) - 100.0 * m_pE1;
      }

      public double F2()
      {
         return 500.0 * Math.Pow(100.0 - m_pE1 - 2.0 * m_pE2, 2.0) - 100.0 * m_pE2;
      }

      public double F3()
      {
         return 0.5 * (100.0 - m_pE1 - m_pE3 - 2.0 * m_pE4) - 100.0 * m_pE3;
      }

      public double F4()
      {
         return 10000.0 * Math.Pow(100.0 * m_pE3 - 2.0 * m_pE4, 2.0) - 100.0 * m_pE4;
      }

      public Parameter E1
      {
         get { return m_pE1; }
         set { m_pE1 = value; }
      }

      public Parameter E2
      {
         get { return m_pE2; }
         set { m_pE2 = value; }
      }

      public Parameter E3
      {
         get { return m_pE3; }
         set { m_pE3 = value; }
      }

      public Parameter E4
      {
         get { return m_pE4; }
         set { m_pE4 = value; }
      }
   }
}
