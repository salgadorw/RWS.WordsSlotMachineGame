using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RWS.WordsSlotMachine.Domain
{
    public class ReelsManager : IReelsManager
    {
        private readonly Reel[] reels;
        public ReelsManager(List<string> reelsData, int? startIndex = null)
        {
            reels = new Reel[reelsData.Count];

            foreach (string reel in reelsData)
            {
                reels[reelsData.IndexOf(reel)] = new Reel(reel, startIndex);
            }
        }
        public string GetReelsStringOnCurrentPositon()
        {
            return string.Join("", reels.Select(s => s.GetCurrentLetter()));
        }

        public string CheckLetterByWord(string word)
        {
            var result = new StringBuilder();
            foreach (char letter in word)
            {
                foreach (Reel reelsToTry in reels)
                {
                    if (reelsToTry.GetCurrentLetter().Equals(letter))
                    {
                        result.Append(letter);
                        reelsToTry.MoveReelForward();
                        break;
                    }

                }
            }
            return result.ToString();
        }
    }
}
