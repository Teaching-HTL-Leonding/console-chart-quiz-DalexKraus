using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ConsoleChartQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            var animalAttacks = new List<ChartEntry>();

            var header = Console.ReadLine();
            var categories = header.Split('\t');
            var groupColumnIdx = Array.IndexOf(categories, args[0]);
            var numericColumnIdx = Array.IndexOf(categories, args[1]);
            
            // Read the content of stdin
            while (true)
            {
                var currentLine = Console.ReadLine();
                if (string.IsNullOrEmpty(currentLine))
                    break;

                var lineSegments = currentLine.Split('\t');
                var numericColumnValue = int.Parse(lineSegments[numericColumnIdx]);
                animalAttacks.Add(new ChartEntry(lineSegments[groupColumnIdx], numericColumnValue));
            }

            // Print desired data
            var groupedEntries = animalAttacks.GroupBy(entry => entry.EntryTitle)
                .Select(entry => new ChartEntry(entry.Key, entry.Sum(e => e.EntryCount)))
                .OrderByDescending(entry => entry.EntryCount)
                .ToList();
            
            var highestAttackCount = groupedEntries.Max(entry => entry.EntryCount);
            foreach (var entry in groupedEntries)
            {
                var percentage = (int) ((double) entry.EntryCount / highestAttackCount * 100);
                Console.Write($"{entry.EntryTitle,40} |");

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"{GetBarForPercentage(percentage)}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// Returns a string containing an ascii-representation of a progress-bar,
        /// which progress is determined by the given parameter.
        /// </summary>
        /// <param name="percentage">The progress of the progress-bar in percent</param>
        /// <returns>A string containing the bar</returns>
        private static string GetBarForPercentage(int percentage)
        {
            return new string('█', percentage) + new string(' ', (100 - percentage));
        }
    }
}