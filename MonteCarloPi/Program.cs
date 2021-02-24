using System;
using System.Collections.Generic;
using System.Threading;

namespace MenteCarloPi
{
    class Program
    {

        static void Main(string[] args)
        {
            var primes = Tuple.Create(2, 3, 5, 7, 11, 13, 17, 19);
            Console.WriteLine("Prime numbers less than 20: " +
                              "{0}, {1}, {2}, {3}, {4}, {5}, {6}, and {7}",
                primes.Item1, primes.Item2, primes.Item3,
                primes.Item4, primes.Item5, primes.Item6,
                primes.Item7, primes.Rest.Item1);


            var maxCount = 100;
            var times = 20;
            for (int i = 0; i < times; i++)
            {   Console.WriteLine($"第{i+1}/{times}次计算:");
                CalculatePi(maxCount);
                maxCount = maxCount * 2;
            }
            Console.ReadKey();
        }

        private static void CalculatePi(int maxCount)
        {
            double[] Xs = new double[maxCount];
            double[] Ys = new double[maxCount];
            int CountInCircle = 0;
            int CountOutOfCircle = 0;
            int CountJustOnCircle = 0;
            Console.WriteLine($"Generating {maxCount} points...");
            for (int i = 0; i < maxCount; i++)
            {
                Xs[i] = GetRandomNumber();
                Ys[i] = GetRandomNumber();
                if (func(Xs[i]) > Ys[i])
                {
                    CountInCircle++;
                }
                else if (func(Xs[i]) < Ys[i])
                {
                    CountOutOfCircle++;
                }
                else
                {
                    CountJustOnCircle++;
                    Console.WriteLine($"X:{Xs[i]}Y:{Ys[i]}");
                }
            }
            Console.WriteLine($"落在扇形内点数量:{CountInCircle}");
            Console.WriteLine($"落在扇形外点数量:{CountOutOfCircle}");
            Console.WriteLine($"落在扇形上数量:{CountJustOnCircle}");
            Console.WriteLine($"PI估算值为【{4.0 * CountInCircle / (CountInCircle + CountOutOfCircle)}】");
            Console.WriteLine("***********************************************************************");
        }

        static double func(double x)//x<=1
        {
            return Math.Sqrt(1 - Math.Pow(x, 2));
        }

        static double GetRandomNumber()
        {
            var r = RandomProvider.GetThreadRandom();
            var randomNumber = r.NextDouble();
            return randomNumber;
        }
        /// <summary>
        /// Provides a threadsafe random number, from Jon Skeet's C# in Depth:
        /// http://csharpindepth.com/Articles/Chapter12/Random.aspx
        /// </summary>
        internal static class RandomProvider
        {
            private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random>(() =>
                new Random(Interlocked.Increment(ref seed))
            );

            private static int seed = Environment.TickCount;

            public static Random GetThreadRandom()
            {
                return randomWrapper.Value;
            }
        }
    }
}
