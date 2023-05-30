using RWS.WordsSlotMachine.CrossCutting;
using System.Collections.Generic;
using System.Linq;

namespace RWS.WordsSlotMachine.Domain
{
    public class WordsTree : IWordsTree
    {
        protected readonly IWordsTreeNode wordsTreeNodes;

        private const char START = '^';
        private const char END_WORD = '=';

        public IWordsTreeNode GetWordsTreeNodes { get { return wordsTreeNodes; } }

        public WordsTree(WordsTreeNode nodes = null)
        {
            wordsTreeNodes = nodes ?? WordsTreeNode.BuidNewNode(START);
        }
        public virtual bool Search(string s)
        {
            s = s.NormalizeWord();
            return GetLastKnowedNode(s, out _).Letter == END_WORD;
        }

        private IWordsTreeNode GetLastKnowedNode(string s, out int index)
        {
            index = 0;
            var searchCursor = wordsTreeNodes;
            do
            {
                if (searchCursor.NextLetterWord.Letter.Equals(s[index]))
                    searchCursor = searchCursor.NextLetterWord;
                else
                {
                    var i = index;
                    var nextForkNode = searchCursor.NextWordsLetters.GetValueOrDefault(s[i]);

                    if (nextForkNode == null)
                        return searchCursor;
                    searchCursor = nextForkNode;
                }
                index++;

            } while (index < s.Length);

            return searchCursor.NextLetterWord.Letter == END_WORD ? searchCursor.NextLetterWord :
                searchCursor.NextWordsLetters.GetValueOrDefault(END_WORD)
                ?? searchCursor;
        }

        public virtual void Insert(string s)
        {
            s = s.NormalizeWord();
            var insertCursor = GetLastKnowedNode(s, out int index);

            if (insertCursor.Letter != END_WORD)
            {
                WordsTreeNode newNode = WordsTreeNode.BuidNewNode(END_WORD, insertCursor);

                if (s.Length > index)
                    newNode.Letter = s[index];

                insertCursor.NextWordsLetters.Add(newNode.Letter,newNode);
                insertCursor = newNode;

                for (var i = ++index; i < s.Length; i++)
                {
                    insertCursor.NextLetterWord = WordsTreeNode.BuidNewNode(s[i], insertCursor);
                    insertCursor = insertCursor.NextLetterWord;
                }
                if (insertCursor.Letter != END_WORD)
                    insertCursor.NextLetterWord = WordsTreeNode.BuidNewNode(END_WORD, insertCursor);
            }
        }

        public virtual void Delete(string s)
        {
            s = s.NormalizeWord();
            var lastNode = GetLastKnowedNode(s, out _);
            if (lastNode.Letter == END_WORD)
            {
                IWordsTreeNode nextWordLetterNode;
                do
                {
                    var previousNode = lastNode.PreviousLetterNode;
                    nextWordLetterNode = lastNode;
                    lastNode = previousNode;

                } while (lastNode.NextWordsLetters.Count == 0 || (lastNode.NextLetterWord.Letter == 0 && lastNode.PreviousLetterNode != null));

                lastNode.NextWordsLetters.Remove(nextWordLetterNode.Letter);
                if (lastNode.NextLetterWord == nextWordLetterNode)
                {
                    lastNode.NextLetterWord = WordsTreeNode.BuidNewNode();
                }
            }
        }
    }
}
