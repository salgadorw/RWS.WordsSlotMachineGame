using RWS.WordsSlotMachine.Domain;
using System.Linq;
using Xunit;

namespace RWS.WordsSlotMachine.Tests
{
    public class ReelsManagerTests
    {
        private readonly string[] REELS_DATA = {  "A B" ,  "C D" };
        private const string ASSERT_ALL = "ACBD";
        private const string ASSERT_ALL2 = "ABCD";
        private const string ASSERT_ONE = "XCJT";
        private const string ASSERT_THRE = "ADCB";
        private const string ASSERT_FALSE = "XXXX";

        private readonly IReelsManager manager;
        public ReelsManagerTests() {

            manager = new ReelsManager(REELS_DATA.ToList(), 0);
        }              

        [Fact]
        public void CheckLetterByWord_FindALL_Test()
        {
            var resultTest = manager.GetReelsStringOnCurrentPositon();
            manager.CheckLetterByWord(ASSERT_ALL);
            Assert.Equal(resultTest, manager.GetReelsStringOnCurrentPositon());
            manager.CheckLetterByWord(ASSERT_ALL2);
            Assert.Equal(resultTest, manager.GetReelsStringOnCurrentPositon());
        }

        [Fact]
        public void CheckLetterByWord_FindSome_Test()
        {
            var resultTest = "AD";
            manager.CheckLetterByWord(ASSERT_ONE);
            Assert.Equal(resultTest, manager.GetReelsStringOnCurrentPositon());
            manager.CheckLetterByWord(ASSERT_THRE);
            Assert.Equal(resultTest, manager.GetReelsStringOnCurrentPositon());
        }

        [Fact]
        public void CheckLetterByWord_NotFind_Test()
        {
            var resultTest = manager.GetReelsStringOnCurrentPositon();
            manager.CheckLetterByWord(ASSERT_FALSE);
            Assert.Equal(resultTest, manager.GetReelsStringOnCurrentPositon());
        }
    }
}
