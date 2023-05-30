using RWS.WordsSlotMachine.Domain;
using RWS.WordsSlotMachine.Infrastructure;
using Xunit;

namespace RWS.WordsSlotMachine.Tests
{
    public class WordsTreeTests
    {
        private const string TEST_WORD = "parallel";
        private const string TEST_WORD1 = "parallelo";
        private const string TEST_WORD2 = "paraallel";
        private const string TEST_WORD5 = "aapparaaloel";
        private const string TEST_WORD3 = "aaparaalolel";
        private const string TEST_WORD4 = "baparaallel";

        private readonly IGameDataRepository gameDataRepository = new GameDataRepository();

        [Fact]
        public void WordTreeInsertTest()
        {
            WordsTree wordsTree = new();
            wordsTree.Insert(TEST_WORD);
            wordsTree.Insert(TEST_WORD1);
            wordsTree.Insert(TEST_WORD2);
            wordsTree.Insert(TEST_WORD3);
            wordsTree.Insert(TEST_WORD4);
            wordsTree.Insert(TEST_WORD5);

            Assert.True(wordsTree.Search(TEST_WORD));
            Assert.True(wordsTree.Search(TEST_WORD1));
            Assert.True(wordsTree.Search(TEST_WORD2));
            Assert.True(wordsTree.Search(TEST_WORD3));
            Assert.True(wordsTree.Search(TEST_WORD4));
            Assert.True(wordsTree.Search(TEST_WORD5));
        }

        [Fact]
        public void WordTreeDeleteTest()
        {
            WordsTree wordsTree = new();
            wordsTree.Insert(TEST_WORD);
            wordsTree.Insert(TEST_WORD1);
            wordsTree.Insert(TEST_WORD2);
            wordsTree.Insert(TEST_WORD3);
            wordsTree.Insert(TEST_WORD4);
            wordsTree.Insert(TEST_WORD5);

            Assert.True(wordsTree.Search(TEST_WORD));
            Assert.True(wordsTree.Search(TEST_WORD2));
            Assert.True(wordsTree.Search(TEST_WORD3));
            Assert.True(wordsTree.Search(TEST_WORD4));

            wordsTree.Delete(TEST_WORD);
            wordsTree.Delete(TEST_WORD4);
            wordsTree.Delete(TEST_WORD2);
            wordsTree.Delete(TEST_WORD3);

            Assert.False(wordsTree.Search(TEST_WORD4));
            Assert.False(wordsTree.Search(TEST_WORD3));
            Assert.False(wordsTree.Search(TEST_WORD2));
            Assert.True(wordsTree.Search(TEST_WORD1));
            Assert.True(wordsTree.Search(TEST_WORD5));

            wordsTree.Delete(TEST_WORD1);
            wordsTree.Delete(TEST_WORD5);

            Assert.False(wordsTree.Search(TEST_WORD1));
            Assert.False(wordsTree.Search(TEST_WORD5));

            Assert.Equal(0, wordsTree.GetWordsTreeNodes.NextLetterWord.Letter);
            Assert.Empty(wordsTree.GetWordsTreeNodes.NextWordsLetters);
        }

        [Fact]
        public void WordTreeDelete_ALL_Test()
        {
            IWordsTree wordsTree = gameDataRepository.GetWords();
            var words = gameDataRepository.Words;

            words.ForEach(w => wordsTree.Delete(w));
            Assert.Equal(0, wordsTree.GetWordsTreeNodes.NextLetterWord.Letter);
            Assert.Empty(wordsTree.GetWordsTreeNodes.NextWordsLetters);
        }
    }
}