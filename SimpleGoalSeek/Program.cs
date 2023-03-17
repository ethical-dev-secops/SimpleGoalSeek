using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGoalSeek.Console
{
    /// <summary>
    /// Test Project
    /// </summary>
    class Program
    {
        
        static void Main(string[] args)
        {
            var seeker = new SimpleGoalSeekCalculator();



            Func<decimal,decimal> equation = (decimal x) =>    
            {
                return x * 3;
            };

            var result = seeker.BisectionGoalSeek(16.01m, equation);
            //var result = seeker.GoalSeek_WithEfficiency(8.01m, equation, 1);
            Console.WriteLine(result);

            Console.Read();
        }
    }


    public class DummyData
    {
        public static List<decimal> DummyDataSource = new List<decimal>()
        {
            1,
            2,
            4,
            6,
            9,
            12,
            15
        };
    }
}
