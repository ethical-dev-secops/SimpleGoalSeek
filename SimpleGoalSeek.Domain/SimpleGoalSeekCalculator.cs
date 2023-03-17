using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleGoalSeek
{
    public class SimpleGoalSeekCalculator
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="desiredResult"></param>
        /// <returns>requiredInput to achieve desired result</returns>
        public decimal SeekNearestInputNumberAfterConditionIsFulfilled(List<decimal> dataSource, decimal desiredResult, Func<decimal, decimal> equation)
        {
            var solution = 0m;
            var result = 0m;
            var length = dataSource.Count;

            for (var i = 0; i < length; i++)
            {
                var input = dataSource.ElementAt(i);
                result = equation(input);
                if (result >= desiredResult)
                {
                    solution = input;
                    break;
                }
            }

            return solution;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="desiredResult"></param>
        /// <returns>requiredInput to achieve desired result</returns>
        public decimal SeekLowestPossibleNumberBeforeConditionIsFulfilled(List<decimal> dataSource, decimal desiredResult, Func<decimal, decimal> equation)
        {
            var solution = 0m;
            var oldResult = 0m;
            var result = 0m;
            var length = dataSource.Count;

            for(var i=0; i < length; i++)
            {
                result = equation(dataSource.ElementAt(i));
                if(result < desiredResult)
                {
                    oldResult = result;
                    continue;
                }
                else if(result > desiredResult)
                {
                    solution = oldResult;
                    break;
                }
            }

            return solution;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="desiredResult"></param>
        /// <returns>requiredInput to achieve desired result</returns>
        public decimal GoalSeek(decimal desiredResult, Func<decimal, decimal> equation)
        {
            var solution = 0m;
            var result = 0m;
            //var additive = increment;
            var iterations = 0;

            var multiplier = equation(desiredResult) - Math.Truncate(equation(desiredResult));

            var additive = Convert.ToDecimal(Math.Pow(Convert.ToDouble(equation(desiredResult)), 0.33));
            //var additive = multiplier * 10;

            for (var i = 0m; i < desiredResult + 1; i+= additive)
            {
                iterations++;
                result = equation(i);
                if (result < desiredResult)
                {
                    continue;
                }
                else if (result > desiredResult)
                {
                    //startFromPreviousIndex, but go into decimal place
                    i -= additive;
                    additive = additive * 0.1m;


                }
                else if (result == desiredResult)
                {
                    solution = i;
                    break;
                }
            }

            Console.WriteLine(iterations);
            return solution;
        }


        /// <summary>
        /// Efficient Goal Seeking Calculator, however accuracy is determined by the tolerance parameter.
        /// </summary>
        /// <param name="desiredResult"></param>
        /// <param name="equation"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public decimal BisectionGoalSeek(decimal desiredResult, Func<decimal, decimal> equation, decimal tolerance = 0.001m)
        {
            var min = desiredResult;
            var max = 1m;
            var c = 0m;
            var i = 1;
            var MaxIterations = 1000;
            var result = 0m;

            while (i <= MaxIterations)
            {
                c = (min + max) / 2;
                
                result = equation(c);
                var isFound = Math.Abs(result - desiredResult) < tolerance;
                if (result == desiredResult || isFound) //|| isFound)
                {
                    break;
                }
                
                if (result > desiredResult)
                {
                    min = c;
                }
                else 
                {
                    max = c;
                }

                i = i + 1;
            }

            //Console.WriteLine("Cycles: " + i);
            //Console.WriteLine("answer: " + c);
            return c;
        }
    }
}
