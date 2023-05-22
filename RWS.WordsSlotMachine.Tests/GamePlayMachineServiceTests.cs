using RWS.WordsSlotMachine.Domain;
using RWS.WordsSlotMachine.Application;
using Xunit;
using Moq;

namespace RWS.WordsSlotMachine.Tests
{
    public class GamePlayMachineServiceTests
    {
        private  IGamePlayMachineService service;
        private readonly Mock<IScoreManager> scoreManagerMock;
        private readonly Mock<IReelsManager> reelsManagerMock;
        private readonly Mock<IWordsTree> wordTreeMock;
        private readonly Mock<IWordsTree> trieMock;

        private const string INPUT = "èéü'cã";
        private const string NORMALIZED_INPUT = "EEUCA";
        private const string REPLICATED_WORD_MESSAGE = "Word already used!";
        private const string NOT_EXISTENT_WORD_MESSAGE = "Word not available!";
        public GamePlayMachineServiceTests() {
                    
            scoreManagerMock = new Mock<IScoreManager>();
            reelsManagerMock = new Mock<IReelsManager>();
            wordTreeMock = new Mock<IWordsTree>();
            trieMock = new Mock<IWordsTree>();
        }

        [Fact]
        public void PlayGame_NotExistentWordTest()
        {
            //arrange
            wordTreeMock.Setup(wordTreeMock => wordTreeMock.Search(It.IsAny<string>())).Returns(false);
            trieMock.Setup(wordTreeMock => wordTreeMock.Search(It.IsAny<string>())).Returns(true);
            service = new GamePlayMachineService(reelsManagerMock.Object, scoreManagerMock.Object, wordTreeMock.Object, trieMock.Object);

            //act
            var result = service.PlayGame(INPUT);         

            //assert
            Assert.False(result.PlayStatus);
            Assert.Equal(NOT_EXISTENT_WORD_MESSAGE, result.PlayStatusMessage);
        }

        [Fact]
        public void PlayGame_ReplicatedWordTest()
        {
            //arrange
            wordTreeMock.Setup(wordTreeMock => wordTreeMock.Search(It.IsAny<string>())).Returns(true);
            trieMock.Setup(wordTreeMock => wordTreeMock.Search(It.IsAny<string>())).Returns(true);
            service = new GamePlayMachineService(reelsManagerMock.Object, scoreManagerMock.Object, wordTreeMock.Object, trieMock.Object);

            //act
            var result = service.PlayGame(INPUT);

            //assert
            Assert.False(result.PlayStatus);
            Assert.Equal(REPLICATED_WORD_MESSAGE, result.PlayStatusMessage);
        }

        [Fact]
        public void PlayGame_SuccessTest()
        {
            //arrange
            scoreManagerMock.Setup(s => s.GetScore()).Returns(2);
            reelsManagerMock.Setup(s => s.CheckLetterByWord(It.IsAny<string>())).Callback<string>(s =>
            {
                //assert
                Assert.Equal(NORMALIZED_INPUT, s);
            }).Returns(NORMALIZED_INPUT);
            reelsManagerMock.Setup(s=> s.GetReelsStringOnCurrentPositon()).Returns(INPUT);
            wordTreeMock.Setup(wordTreeMock => wordTreeMock.Search(It.IsAny<string>())).Returns(true);
            trieMock.Setup(wordTreeMock => wordTreeMock.Search(It.IsAny<string>())).Returns(false);
            service = new GamePlayMachineService(reelsManagerMock.Object, scoreManagerMock.Object, wordTreeMock.Object, trieMock.Object);

            //act
            var result = service.PlayGame(INPUT);

            //assert
            Assert.True(result.PlayStatus);
            Assert.Equal(2, service.GetGameDisplayData().GameScoreInfo);
            Assert.Equal(INPUT, service.GetGameDisplayData().ReelsDisplayInfo);
        }
    }
}
