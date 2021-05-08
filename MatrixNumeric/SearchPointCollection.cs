using MathematicalPlayground.NumericalMethods.Enums;
using System.Collections.ObjectModel;
using System.Linq;

namespace MathematicalPlayground.NumericalMethods
{
   public class SearchPointCollection
      : Collection<SearchPoint>
   {
      private int _maxSize = 3;

      public SearchPointCollection()
         : base()
      {
      }

      protected override void InsertItem(int index, SearchPoint item)
      {
         while (index >= _maxSize)
         {
            SearchPoint worstPoint = (from pt in this orderby pt.Y descending select pt).ToArray()[0];
            base.Remove(worstPoint);
            index--;
         }

         base.InsertItem(index, item);
         SearchPoint[] points = (from pt in this orderby pt.X select pt).ToArray();
         for (int i = 0; i < points.Length; i++)
         {
            this[i] = points[i];
         }
      }

      public bool CanAdd(SearchPoint newPoint)
      {
         bool canAdd = (from pt in this where pt.X == newPoint.X select pt).Count() == 0;

         if (canAdd)
         {
            double currentMin = (from pt in this select pt.Y).Max();
            canAdd = newPoint.Y < currentMin;
         }
         return canAdd;
      }

      public bool CanAdd(double newX)
      {
         return (from pt in this where pt.X == newX select pt).Count() == 0;
      }

      public bool IsBracketed()
      {
         double min = (from pt in this select pt.Y).Min();
         bool result = min < this[0].Y && min < this[Count - 1].Y;
         if (!result)
         {
            double max = (from pt in this select pt.Y).Max(); result = min == max;
         }

         return result;
      }

      public double ParabolicMinYEstimate()
      {
         double result = double.NaN;
         if (Count == 3)
         {
            double[] x = (from pt in this select pt.X).ToArray();
            double[] y = (from pt in this select pt.Y).ToArray();
            result = Polynomial.ParabolicMin(x, y);
         }

         return result;
      }

      public double CubicMinYEstimate()
      {
         double result = double.NaN;
         if (Count == 4)
         {
            double[] x = (from pt in this select pt.X).ToArray();
            double[] y = (from pt in this select pt.Y).ToArray();
            CubicMinResults res = Polynomial.CubicMin(x, y);
            if (res.ResultType == CubicResultType.StandardSecondOrder || res.ResultType == CubicResultType.StandardThirdOrder)
            {
               result = res.LocalMinX;
            }
         }

         return result;
      }

      public int MaxSize { get { return _maxSize; } set { _maxSize = value; } }

      public SearchPointCollection Clone(int nMaxSize)
      {
         SearchPointCollection cloneCollection = new SearchPointCollection();
         cloneCollection.MaxSize = nMaxSize;
         for (int i = 0; i < Count; i++)
         {
            cloneCollection.Add(new SearchPoint(this[i].X, this[i].Y));
         }

         return cloneCollection;
      }

      public SearchPoint FindMinY()
      {
         return (from pt in this orderby pt.Y select pt).ToArray()[0];
      }
   }
}
