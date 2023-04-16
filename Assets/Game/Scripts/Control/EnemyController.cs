using System.Collections;
using System.Collections.Generic;
using TD.AI;
using UnityEngine;
using UnityEngine.AI;

namespace TD.Control
{
    public class EnemyController : MonoBehaviour
    {
        Vector3 startingPosition;
        
        ActionScheduler actionScheduler;
        MoveAction moveAction;
        AttackAction attackAction;
        CastAction castAction;

        private void Awake() 
        {
            actionScheduler = GetComponent<ActionScheduler>();
            
            moveAction = GetComponent<MoveAction>();
            
            if(GetComponent<AttackAction>() != null)
            {
                attackAction = GetComponent<AttackAction>();
            }

            if(GetComponent<CastAction>() != null)
            {
                castAction = GetComponent<CastAction>();  
            }
        }

        private void Update()
        {
            CheckActions();
        }

        private void CheckActions()
        {
            if (attackAction != null)
            {
                if (attackAction.InCombat())
                {
                    actionScheduler.StartAction(attackAction);
                    return;
                }
            }
            
            if(castAction != null)
            {
                if(castAction.IsEngaged())
                {
                    actionScheduler.StartAction(castAction);
                    return;
                }
            }

            actionScheduler.StartAction(moveAction);
        }

        private void OnEnable()
        {
            startingPosition = transform.position;
        }

        public void OnDeath()
        {
            transform.position = startingPosition;
            GetComponent<EnemyController>().enabled = false;
            gameObject.SetActive(false);
        }
    }
}
