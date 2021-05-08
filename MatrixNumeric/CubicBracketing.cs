using System;

namespace MathematicalPlayground.NumericalMethods
{
   public class CubicBracketing : BracketingBase
   {
      public CubicBracketing() : base() { }
      public override SearchPointCollection Bracket(Func<double, double> function, double startPosition, double step, int targetReturnPoints)
      {
         SearchPointCollection returnCollection = new SearchPointCollection();

         //returnCollection.MaxSize = targetReturnPoints
         returnCollection.MaxSize = 4;
         double delta = step;
         double currentPosition = startPosition;
         double functionValue = function(currentPosition);
         SearchPoint newSearchPoint = new SearchPoint(currentPosition, functionValue);
         returnCollection.Add(newSearchPoint);
         currentPosition += delta;
         functionValue = function(currentPosition);
         while (double.IsNaN(functionValue) || double.IsInfinity(functionValue))
         {
            delta /= 10.0;
            currentPosition = startPosition + delta;
            functionValue = function(currentPosition);
         }
         if (functionValue > newSearchPoint.Y)
         {
            newSearchPoint = new SearchPoint(currentPosition, functionValue);
            returnCollection.Add(newSearchPoint);
            delta *= -1.0;
            currentPosition = startPosition + delta;
            functionValue = function(currentPosition);
            newSearchPoint = new SearchPoint(currentPosition, functionValue);
            returnCollection.Add(newSearchPoint);
         }
         else
         {
            newSearchPoint = new SearchPoint(currentPosition, functionValue);
            returnCollection.Add(newSearchPoint);
         }

         //Get third point.
         currentPosition += delta;
         functionValue = function(currentPosition); newSearchPoint = new SearchPoint(currentPosition, functionValue);
         returnCollection.Add(newSearchPoint);

         //If not already bracketed, try quadratic.
         double offsetDistanceFromEstMin = 0.1;
         if (!returnCollection.IsBracketed() && !Cancel)
         {
            double newX = returnCollection.ParabolicMinYEstimate();
            if (!double.IsNaN(newX))
            {
               if (newX > returnCollection[2].X)
               {
                  newX += offsetDistanceFromEstMin * (newX - returnCollection[2].X);
                  functionValue = function(newX);
                  newSearchPoint = new SearchPoint(newX, functionValue);
                  returnCollection.Add(newSearchPoint);
               }
               else if (newX < returnCollection[0].X)
               {
                  newX += offsetDistanceFromEstMin * (newX - returnCollection[0].X);
                  functionValue = function(newX);
                  newSearchPoint = new SearchPoint(newX, functionValue);
                  returnCollection.Add(newSearchPoint);
               }
               else
               {
                  //The point is inside the bracket.
                  if ((newX - returnCollection[0].X) < (returnCollection[2].X - newX))
                  {
                     newX = returnCollection[0].X - Math.Abs(delta);
                     functionValue = function(newX);
                     newSearchPoint = new SearchPoint(newX, functionValue);
                     returnCollection.Add(newSearchPoint);
                  }
                  else
                  {
                     newX = returnCollection[2].X + Math.Abs(delta);
                     functionValue = function(newX);
                     newSearchPoint = new SearchPoint(newX, functionValue);
                     returnCollection.Add(newSearchPoint);
                  }
               }
               currentPosition = newX;
            }
            else
            {
               delta *= 2.0;
               currentPosition += delta;
               functionValue = function(currentPosition);
               newSearchPoint = new SearchPoint(currentPosition, functionValue);
               returnCollection.Add(newSearchPoint);
            }

            while (!returnCollection.IsBracketed() && !Cancel)
            {
               newX = returnCollection.CubicMinYEstimate(); if (!double.IsNaN(newX))
               {
                  if (newX > returnCollection[3].X)
                  {
                     newX += offsetDistanceFromEstMin * (newX - returnCollection[3].X);
                     functionValue = function(newX);
                     newSearchPoint = new SearchPoint(newX, functionValue);
                     returnCollection.Add(newSearchPoint);
                  }
                  else if (newX < returnCollection[0].X)
                  {
                     newX += offsetDistanceFromEstMin * (newX - returnCollection[0].X);
                     functionValue = function(newX);
                     newSearchPoint = new SearchPoint(newX, functionValue);
                     returnCollection.Add(newSearchPoint);
                  }
                  else
                  {
                     //The point is inside the bracket.
                     if ((newX - returnCollection[0].X) < (returnCollection[3].X - newX))
                     {
                        newX = returnCollection[0].X - Math.Abs(delta);
                        functionValue = function(newX);
                        newSearchPoint = new SearchPoint(newX, functionValue);
                        returnCollection.Add(newSearchPoint);
                     }
                     else
                     {
                        newX = returnCollection[3].X + Math.Abs(delta);
                        functionValue = function(newX);
                        newSearchPoint = new SearchPoint(newX, functionValue);
                        returnCollection.Add(newSearchPoint);
                     }
                  }
                  currentPosition = newX;
               }
               else
               {
                  delta *= 2.0; currentPosition += delta;
                  functionValue = function(currentPosition);
                  newSearchPoint = new SearchPoint(currentPosition, functionValue);
                  returnCollection.Add(newSearchPoint);
               }
            }
         }

         return returnCollection;
      }
   }
}
