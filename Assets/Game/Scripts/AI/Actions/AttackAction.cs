using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.AI
{
    public class AttackAction : MonoBehaviour, IAction
    {
        [SerializeField] float radarRange = 8;
        [SerializeField] float range = 2;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float damage = 30;
        float timeSinceLastAttack = Mathf.Infinity;
        
        bool isActiveAction = false;

        private Hero target;

        private void OnDrawGizmos() 
        {
            Gizmos.DrawWireSphere(transform.position, radarRange);
        }

        private IEnumerator AttackBehaviour()
        {
            while(true)
            {
                if(isActiveAction == false) { print("breaking attack routine"); yield break;}
                timeSinceLastAttack += Time.deltaTime;
                if(!TargetInRange())
                {
                    MoveToTarget();
                    yield return null;
                }
                else
                {
                    StopMoving();
                    Attack();
                    yield return null;
                }
                yield return null;
            }
            
        }

        private void Attack()
        {
            if(target.IsDead())
            {
                target = null;
                timeSinceLastAttack = Mathf.Infinity;
                return;
            }

            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                target.TakeDamage(damage);
                timeSinceLastAttack = 0;
            }
        }

        private void MoveToTarget()
        {
            GetComponent<MoveAction>().MoveTo(target.transform.position);
        }

        private void StopMoving()
        {
            GetComponent<MoveAction>().Stop();
        }

        private bool TargetInRange()
        {
            if(Vector3.Distance(target.transform.position, transform.position) < range) return true;
            return false;
        }

        public bool InCombat()
        {
            if(!TargetFound()) return false;
            return true;
        }
        
        private bool TargetFound()
        {
            if(this.target != null) return true;
            
            foreach (Hero target in FindObjectsOfType<Hero>())
            {
                if(target.InCombat()) continue;
                if(Vector3.Distance(target.transform.position, transform.position) < radarRange)
                {
                    this.target = target;
                    target.StartCombat();
                    print("Found Target");
                    return true;
                }
            }
            return false;
        }
        
        public void Cancel()
        {
            isActiveAction = false;
        }

        public void StartAction()
        {
            isActiveAction = true;
            StartCoroutine(AttackBehaviour());
        }

        private List<Hero> GetTargets()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, radarRange, Vector3.up, 0);
            List<Hero> targets = new List<Hero>();
            foreach (var target in hits)
            {
                if(target.transform.gameObject.GetComponent<Hero>() != null)
                {
                    targets.Add(target.transform.gameObject.GetComponent<Hero>());
                }
            }
            return targets;
        }
    }
}
