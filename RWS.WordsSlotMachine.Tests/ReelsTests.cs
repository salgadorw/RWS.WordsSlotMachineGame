using RWS.WordsSlotMachine.Domain;
using Xunit;

namespace RWS.WordsSlotMachine.Tests
{
    public class ReelsTests
    {
        private const string REEL_DATA = "A B";
        private readonly IReel reel;

        public ReelsTests() {

            reel = new Reel(REEL_DATA, 0);
        }

        [Fact]
        public void ReelRollTest()
        {
            reel.MoveReelForward();
            Assert.Equal('B',reel.GetCurrentLetter());
        }

        [Fact]
        public void ReelRollInfinityTest()
        {
            reel.MoveReelForward();
            reel.MoveReelForward();

            Assert.Equal('A', reel.GetCurrentLetter());
        }
    }
}
