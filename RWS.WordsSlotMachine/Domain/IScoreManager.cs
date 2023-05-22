namespace RWS.WordsSlotMachine.Domain
{
    public interface IScoreManager
    {
        string GetLettersScoreData();
        int GetScore();
        void SumWordScore(string scoreLetter);
    }
}