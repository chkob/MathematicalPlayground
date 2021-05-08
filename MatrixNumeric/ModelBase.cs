using MathematicalPlayground.NumericalMethods.Attributes;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace MathematicalPlayground.NumericalMethods
{
   public class ModelBase
   {
      protected Parameter[] m_lstParameters;

      public ModelBase()
      {
      }

      public Parameter[] GetParameters()
      {
         if (m_lstParameters == null)
         {
            Collection<Parameter> colPars = new Collection<Parameter>();
            Type tType = this.GetType();
            PropertyInfo[] piInfos = tType.GetProperties();

            for (int i = 0; i < piInfos.Length; i++)
            {
               Type tProperty = piInfos[i].PropertyType;

               if (tProperty == typeof(Parameter) || tProperty.IsSubclassOf(typeof(Parameter)))
               {
                  HideFromReflectionListsAttribute[] atrHide = piInfos[i].GetCustomAttributes(typeof(HideFromReflectionListsAttribute), true) as HideFromReflectionListsAttribute[];

                  if (atrHide == null || atrHide.Length == 0 || atrHide[0].Hidden == false)
                  {
                     colPars.Add((Parameter)piInfos[i].GetValue(this, null));
                  }
               }
               else
               {
                  if (typeof(IEnumerable).IsAssignableFrom(tProperty))
                  {

                     HideFromReflectionListsAttribute[] atrHide = piInfos[i].GetCustomAttributes(typeof(HideFromReflectionListsAttribute), true) as HideFromReflectionListsAttribute[];

                     if (atrHide == null || atrHide.Length == 0 || atrHide[0].Hidden == false)
                     {

                        IEnumerable enProperty = piInfos[i].GetValue(this, null) as IEnumerable;

                        foreach (object obj in enProperty)
                        {
                           Parameter pPar = obj as Parameter;

                           if (pPar != null)
                           {
                              colPars.Add(pPar);
                           }
                           else
                           {
                              break;
                           }
                        }
                     }
                  }
               }
            }

            //m_lstParameters = (from par in colPars select par).Distinct().ToArray();
            m_lstParameters = colPars.Select(p => p).Distinct().ToArray();

         }

         return m_lstParameters;

      }



      public void ResetParameters()
      {
         m_lstParameters = null;
      }

      public Parameter[] GetSolvedForParameters()
      {
         Parameter[] pars = GetParameters();

         return (from par in pars where par.IsSolvedFor select par).ToArray();
      }

   }
}
