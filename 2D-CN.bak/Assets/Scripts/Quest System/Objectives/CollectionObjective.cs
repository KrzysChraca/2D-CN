using UnityEngine;
using System.Collections;
using System;

namespace QuestSystem
{
    public class CollectionObjective : IQuestObjective
    {

        private string title;
        private string description;
        private bool isComplete;
        private bool isBonus;
        private string verb;
        private int collectionAmount; //total amount of what is needed
        private int currentAmount; //start at 0
        private GameObject itemToCollect;


        

        /// <summary>
        /// This constructor builds a collection objective for a quest
        /// </summary>
        /// <param name="titleverb">Describes the type of collection</param>
        /// <param name="totalAmount">amount required to complete objective</param>
        /// <param name="item">Item to be collected</param>
        /// <param name="descrip">Describes objective</param>
        /// <param name="bonus">if this objective has a bonus reward</param>
        public CollectionObjective(string titleverb, int totalAmount, GameObject item, string descrip, bool bonus)
        {

            title = titleverb + " " + totalAmount + " " + item.name;
            verb = titleverb;
            description = descrip;
            itemToCollect = item;
            collectionAmount = totalAmount;
            currentAmount = 0;
            isBonus = bonus;
            CheckProgress();
            
        }

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

        public int CollectionAmount
        {
            get
            {
                return collectionAmount;
            }
        }

        public int CurrentAmount
        {
            get
            {
                return currentAmount;
            }
        }

        public GameObject ItemToCollect
        {
            get
            {
                return ItemToCollect;
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
            throw new NotImplementedException();
        }

        public void UpdateProgress()
        {
            if (currentAmount >= collectionAmount)
                isComplete = true;
            else
                isComplete = false;
        }

        public override string ToString()
        {
            return currentAmount + "/" + collectionAmount + " " + itemToCollect.name + " " + verb + "ed!";
        }

    }
}
