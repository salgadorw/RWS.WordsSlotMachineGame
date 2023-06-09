﻿using System.Collections.Generic;

namespace RWS.WordsSlotMachine.Domain
{
    public interface IGameDataRepository
    {
        List<string> Words { get; }
        List<string> GetReels();
        Dictionary<char, int> GetScores();
        WordsTree GetWords();
    }
}