using RWS.WordsSlotMachine.CrossCutting;
using RWS.WordsSlotMachine.Domain;

namespace RWS.WordsSlotMachine.Application
{
    public class GamePlayMachineService : IGamePlayMachineService
    {
        private readonly IReelsManager reels;
        private readonly IScoreManager score;
        private readonly IWordsTree avaiableWords;
        private readonly IWordsTree tries;

        private const string REPLICATED_WORD_MESSAGE = "Word already used!";
        private const string NOT_EXISTENT_WORD_MESSAGE= "Word not available!";


        private readonly GameDataDisplayInfoDTO gameDataDisplayInfoDTO = new();
        public GamePlayMachineService(IReelsManager reels, IScoreManager score, IWordsTree availableWords, IWordsTree tries)
        {

            this.reels = reels;
            this.score = score;
            avaiableWords = availableWords;
            this.tries = tries;

            gameDataDisplayInfoDTO.ScoresLettersInfo = score.GetLettersScoreData();

        }

        public PlayGameStatusResult PlayGame(string input)
        {
            input = input.NormalizeWord();

            var playStatus = new PlayGameStatusResult();
            if (avaiableWords.Search(input))
            {
                if (!tries.Search(input))
                {
                    var reelsStrikeLetter = reels.CheckLetterByWord(input);

                    if (!string.IsNullOrEmpty(reelsStrikeLetter))
                    {
                        score.SumWordScore(reelsStrikeLetter);
                        tries.Insert(input);
                        playStatus.PlayStatus = true;
                    }
                }
                else
                    playStatus.PlayStatusMessage = REPLICATED_WORD_MESSAGE;
            }
            else
                playStatus.PlayStatusMessage = NOT_EXISTENT_WORD_MESSAGE;

            return playStatus;

        }
        public GameDataDisplayInfoDTO GetGameDisplayData()
        {
            gameDataDisplayInfoDTO.ReelsDisplayInfo = reels.GetReelsStringOnCurrentPositon();
            gameDataDisplayInfoDTO.GameScoreInfo = score.GetScore();

            return gameDataDisplayInfoDTO;
        }
    }
}
