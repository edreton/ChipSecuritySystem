using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Example 1: Simple case from README
            Console.WriteLine("Example 1: Simple case from README");
            Console.WriteLine("================================");
            RunExample(new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Orange, Color.Purple)
            });

            Console.WriteLine("\n\nExample 2: Complex case with multiple possible solutions");
            Console.WriteLine("=====================================================");
            // Example 2: More complex case with multiple possible solutions
            RunExample(new List<ColorChip>
            {
                new ColorChip(Color.Blue, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Red),
                new ColorChip(Color.Red, Color.Green),
                new ColorChip(Color.Blue, Color.Purple),
                new ColorChip(Color.Purple, Color.Orange),
                new ColorChip(Color.Orange, Color.Yellow),
                new ColorChip(Color.Yellow, Color.Green)   
            });
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void RunExample(List<ColorChip> chips)
        {
            // Create and run the security system
            var securitySystem = new SecuritySystem(chips);
            var solution = securitySystem.FindOptimalSolution();

            // Display the results
            Console.WriteLine("\nAvailable chips:");
            foreach (var chip in chips)
            {
                Console.WriteLine($"[{chip}]");
            }

            Console.WriteLine("\nSolution found:");
            if (solution.Count > 0)
            {
                Console.Write("Blue ");
                foreach (var chip in solution)
                {
                    Console.Write($"[{chip}] ");
                }
                Console.WriteLine("Green");
                Console.WriteLine($"\nTotal chips used: {solution.Count}");
            }
            else
            {
                Console.WriteLine("No valid solution found!");
            }
        }
    }
}
