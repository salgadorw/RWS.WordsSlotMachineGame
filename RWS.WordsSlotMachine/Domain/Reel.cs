using System;
using System.Linq;

namespace RWS.WordsSlotMachine.Domain
{
    public class Reel : IReel
    {
        private readonly char[] letters;
        private int currentPosition;

        public Reel(string reelLetters, int? startIndex = null)
        {
            letters = reelLetters.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(s => s[0]).ToArray();
            currentPosition = startIndex ?? new Random().Next(letters.Length);
        }

        public char GetCurrentLetter()
        {
            return letters[currentPosition];
        }
        public void MoveReelForward()
        {
            currentPosition = (currentPosition + 1) % letters.Length;
        }
    }
}
