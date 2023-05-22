using System.Collections.Generic;

namespace RWS.WordsSlotMachine.Domain
{
    public interface IWordsTreeNode
    {
        char Letter { get; set; }
        IWordsTreeNode NextLetterWord { get; set; }
        List<IWordsTreeNode> NextWordsLetters { get; set; }
        IWordsTreeNode PreviousLetterNode { get; set; }
    }
}