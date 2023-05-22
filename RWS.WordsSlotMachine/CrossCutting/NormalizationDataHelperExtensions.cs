namespace RWS.WordsSlotMachine.CrossCutting
{
    public static class NormalizationDataHelperExtensions
    {        
        private static readonly string APOSTROPHE = "'";
        private static readonly string A_TILDE = "Ã";
        private static readonly string U_DIAERESIS = "Ü";
        private readonly static string E_ACUTE = "É";
        private readonly static string E_BACKTICK = "È";
        public static string NormalizeWord(this string word)
        {
            

            return  word.ToUpper().Replace(APOSTROPHE, string.Empty)
                .Replace(A_TILDE,"A")
                .Replace(U_DIAERESIS, "U")
                .Replace(E_ACUTE, "E")
                .Replace(E_BACKTICK,"E");
        }
    }
}
