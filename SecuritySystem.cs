using System;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    /// <summary>
    /// Class responsible for solving the security chip sequence problem
    /// </summary>
    public class SecuritySystem
    {
        private List<ColorChip> _chips;
        private List<ColorChip> _bestSolution;
        private int _maxChipsUsed;

        public SecuritySystem(List<ColorChip> chips)
        {
            _chips = chips;
            _bestSolution = new List<ColorChip>();
            _maxChipsUsed = 0;
        }

        /// <summary>
        /// Finds the optimal solution using the most number of chips
        /// </summary>
        /// <returns>List of chips in the optimal sequence, or empty list if no solution exists</returns>
        public List<ColorChip> FindOptimalSolution()
        {
            var usedChips = new bool[_chips.Count];
            var currentSolution = new List<ColorChip>();
            
            FindSolution(usedChips, currentSolution);
            
            return _bestSolution;
        }

        /// <summary>
        /// Recursive method to find all possible solutions using backtracking
        /// </summary>
        private void FindSolution(bool[] usedChips, List<ColorChip> currentSolution)
        {
            // If we have a valid solution and it uses more chips than our current best
            if (IsValidSolution(currentSolution) && currentSolution.Count > _maxChipsUsed)
            {
                _bestSolution = new List<ColorChip>(currentSolution);
                _maxChipsUsed = currentSolution.Count;
            }

            // Try adding each unused chip
            for (int i = 0; i < _chips.Count; i++)
            {
                if (!usedChips[i])
                {
                    // Try the chip in both orientations
                    var chip = _chips[i];
                    
                    // Try normal orientation
                    if (CanAddChip(currentSolution, chip))
                    {
                        usedChips[i] = true;
                        currentSolution.Add(chip);
                        FindSolution(usedChips, currentSolution);
                        currentSolution.RemoveAt(currentSolution.Count - 1);
                        usedChips[i] = false;
                    }

                    // Try reversed orientation
                    var reversedChip = new ColorChip(chip.EndColor, chip.StartColor);
                    if (CanAddChip(currentSolution, reversedChip))
                    {
                        usedChips[i] = true;
                        currentSolution.Add(reversedChip);
                        FindSolution(usedChips, currentSolution);
                        currentSolution.RemoveAt(currentSolution.Count - 1);
                        usedChips[i] = false;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if a chip can be added to the current solution
        /// </summary>
        private bool CanAddChip(List<ColorChip> currentSolution, ColorChip chip)
        {
            if (currentSolution.Count == 0)
            {
                // First chip must start with Blue
                return chip.StartColor == Color.Blue;
            }

            // Check if the colors match with the last chip
            var lastChip = currentSolution[currentSolution.Count - 1];
            return lastChip.EndColor == chip.StartColor;
        }

        /// <summary>
        /// Validates if the current solution is complete and valid
        /// </summary>
        private bool IsValidSolution(List<ColorChip> solution)
        {
            if (solution.Count == 0)
                return false;

            // Check if it starts with Blue and ends with Green
            return solution[0].StartColor == Color.Blue && 
                   solution[solution.Count - 1].EndColor == Color.Green;
        }
    }
} 