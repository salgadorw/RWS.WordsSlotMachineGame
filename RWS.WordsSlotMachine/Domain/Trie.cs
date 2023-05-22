namespace RWS.WordsSlotMachine.Domain
{
    public class Trie : WordsTree, IWordsTree
    {

        public Trie(WordsTreeNode nodes = null) : base(nodes)
        {

        }
        public override bool Search(string s)
        {
            return base.Search(s);
        }

        public override void Insert(string s)
        {
            base.Insert(s);
        }

        public override void Delete(string s)
        {
            base.Delete(s);
        }
    }
}