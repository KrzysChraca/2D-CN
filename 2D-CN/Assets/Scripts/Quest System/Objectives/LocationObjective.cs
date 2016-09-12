using UnityEngine;
using System.Collections;
using System;

namespace QuestSystem
{
    public class LocationObjective : IQuestObjective
    {
        private string title;
        private string description;
        private bool isComplete;
        private bool isBonus;
        private string verb;
        private Location targetLocation; //zone, 2d cord

        public string Title
        {
            get
            {
                return title;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
        }

        public bool IsComplete
        {
            get
            {
                return isComplete;
            }
        }

        public bool IsBonus
        {
            get
            {
                return isBonus;
            }
        }

        public void CheckProgress()
        {
            if (PlayerController.GetLocation.Compare(targetLocation))
                isComplete = true;
            else
                isComplete = false;
        }

        public void UpdateProgress()
        {
            throw new NotImplementedException();
        }
    }
}