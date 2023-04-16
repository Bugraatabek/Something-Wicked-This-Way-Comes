using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.AI
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction = null;

        public void StartAction(IAction action)
        {
            if(action == currentAction) return;
            if(currentAction != null)
            {
                currentAction.Cancel();
            }
            currentAction = action;
            currentAction.StartAction();
        }
    }
}
