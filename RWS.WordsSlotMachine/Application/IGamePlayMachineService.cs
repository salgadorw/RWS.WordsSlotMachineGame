namespace RWS.WordsSlotMachine.Application
{
    public interface IGamePlayMachineService
    {
        GameDataDisplayInfoDTO GetGameDisplayData();
        PlayGameStatusResult PlayGame(string input);
    }
}