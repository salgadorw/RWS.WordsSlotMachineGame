using System.Collections.Generic;

namespace RWS.WordsSlotMachine.Domain
{
    public interface IWordsTreeNode
    {
        char Letter { get; set; }
        IWordsTreeNode NextLetterWord { get; set; }
        Dictionary<char,IWordsTreeNode> NextWordsLetters { get; set; }
        IWordsTreeNode PreviousLetterNode { get; set; }
    }
}