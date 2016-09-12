using System.Collections.Generic;

namespace QuestSystem
{
    public class Quest
    {
        //Name
        //Description Summary
        //Quest Hint
        //Quest Dialog
        //sourceID
        //questID
        //chain quest and the next quest is blank
        //chain questID

       /* private IQuestText information;
        public IQuestText Information
        {
            get { return information; }
        }*/

        public Quest()
        {

        }


        //objectives
        private List<IQuestObjective> objectives;

            //collection objective
                //kill for enemies
                //get stuff
            //location Objective
                //go from point A to B
        
        //bonus Objectives
        //Rewards



        //events
            //on completetion
            //on failed
            //on update

        private bool IsComplete()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                if(objectives[i].IsComplete == false && objectives[i].IsBonus == false)
                {
                    return false;
                }
            }

            return true; //get reward
        }


    }
}
