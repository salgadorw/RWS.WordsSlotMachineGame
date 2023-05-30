using System.Collections.Generic;
namespace RWS.WordsSlotMachine.Domain
{
    public class WordsTreeNode : IWordsTreeNode
    {
        private WordsTreeNode() { }
        public char Letter { get; set; }

        public Dictionary<char,IWordsTreeNode> NextWordsLetters { get; set; }

        public IWordsTreeNode NextLetterWord { get; set; }

        public IWordsTreeNode PreviousLetterNode { get; set; }

        public static WordsTreeNode BuidNewNode(char letter= new(), IWordsTreeNode previousLetterNode = null)
        {
            return new WordsTreeNode() { Letter = letter, PreviousLetterNode = previousLetterNode, NextLetterWord = new WordsTreeNode(), NextWordsLetters = new Dictionary<char, IWordsTreeNode>() };
        }
    }
}
