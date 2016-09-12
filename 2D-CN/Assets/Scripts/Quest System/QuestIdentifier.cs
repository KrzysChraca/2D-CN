using UnityEngine;
using System;

namespace QuestSystem
{
    public class QuestIdentifier : IQuestIdentifier
    {
        private int iD;
        private int chainQuestID;
        private int sourceID;

        public int ID
        {
            get
            {
                return iD;
            }
        }

        public int ChainQuestID
        {
            get
            {
                return chainQuestID;
            }
        }

        public int SourceID
        {
            get
            {
                return sourceID;
            }
        }
    }
}
