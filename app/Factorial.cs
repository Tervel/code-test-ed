using System;
using System.Linq;
using System.Threading.Tasks;

namespace app {
    public class Factorial {
        /// <summary>
        /// Calculates the factorial of a given number, using a user determined number of threads.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="threadNumber"></param>
        /// <returns></returns>
        private static int Factorialise (long number, int threadNumber) {
            // Make as many parallel tasks as our threadNumber
            // And make them operate on separate subsets of data
            var parallelTasks =
                Enumerable.Range(1, threadNumber)
                            .Select(i =>
                                Task.Factory.StartNew(() =>
                                    multiply(number, i, threadNumber)))
                            .ToArray();

            // after all tasks are done...
            Task.WaitAll(parallelTasks);

            // ... take the partial results and multiply them together
            var finalResult = 1;
            foreach (var partialResult in parallelTasks.Select(task => task.Result)) {
                finalResult *= partialResult;
            }

            return finalResult;
        }

        /// <summary>
        /// Multiplies all the integers up to upperBound, with a step equal to threadNumber
        /// starting from a different int
        /// </summary>
        /// <param name="upperBound"></param>
        /// <param name="startFrom"></param>
        /// <param name="threadNumber"></param>
        /// <returns></returns>
        private static int multiply (long upperBound, int startFrom, int threadNumber) {
            var result = 1;

            for (var i = startFrom; i <= upperBound; i += threadNumber)
                result *= i;

            return result;
        }

        /// <summary>
        /// Factorialisation function taken from stackoverflow:
        /// https://stackoverflow.com/questions/18911262/parallel-calculation-of-biginteger-factorial
        /// </summary>
        /// <param name="args"></param>
        public static void MainTwo(string[] args) {
            Console.Write("Enter a number to factorialise\n>>>  ");
            var number = int.Parse(Console.ReadLine());

            Console.Write("Enter the number of threads to run\n>>>  ");
            var threadNumber = int.Parse(Console.ReadLine());

            Console.WriteLine("The result is: {0}\nRunning {1} threads",
                Factorialise(number, threadNumber), threadNumber);
            Console.ReadLine();
        }
    }
}
