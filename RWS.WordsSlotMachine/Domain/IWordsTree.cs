namespace RWS.WordsSlotMachine.Domain
{
    public interface IWordsTree
    {
        IWordsTreeNode GetWordsTreeNodes { get; }

        void Delete(string s);
        void Insert(string s);
        bool Search(string s);
    }
}