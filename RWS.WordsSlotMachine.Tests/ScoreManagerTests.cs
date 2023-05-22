using RWS.WordsSlotMachine.Domain;
using System.Collections.Generic;
using Xunit;

namespace RWS.WordsSlotMachine.Tests
{
    public class ScoreManagerTests
    {
        private readonly IScoreManager scoreManager;
        public ScoreManagerTests() {

            scoreManager = new ScoreManager(new Dictionary<char, int>() { {'A',5},{'B',2}, {'C',3}});
        }  
        
        [Fact]
        public void SumScoreTest()
        {
            scoreManager.SumWordScore("XX");
            Assert.Equal(0, scoreManager.GetScore());
            scoreManager.SumWordScore("AB");
            Assert.Equal(7, scoreManager.GetScore());
            scoreManager.SumWordScore("B");        
            Assert.Equal(9, scoreManager.GetScore());
            scoreManager.SumWordScore("CAB");
            Assert.Equal(19, scoreManager.GetScore());

        }
    }
}
