using System;

namespace MathematicalPlayground.NumericalMethods.Attributes
{
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public sealed class HideFromReflectionListsAttribute : Attribute
   {
      private bool m_bHidden = true;

      public HideFromReflectionListsAttribute() : base() { }

      public HideFromReflectionListsAttribute(bool Hidden)
          : this()
      {
         m_bHidden = Hidden;
      }

      public bool Hidden
      {
         get { return m_bHidden; }
      }
   }
}
