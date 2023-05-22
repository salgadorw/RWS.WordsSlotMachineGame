namespace RWS.WordsSlotMachine.Domain
{
    public interface IReelsManager
    {
        string CheckLetterByWord(string word);
        string GetReelsStringOnCurrentPositon();
    }
}