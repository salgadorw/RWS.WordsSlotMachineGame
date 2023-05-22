namespace RWS.WordsSlotMachine.Domain
{
    public interface IReel
    {
        char GetCurrentLetter();
        void MoveReelForward();
    }
}