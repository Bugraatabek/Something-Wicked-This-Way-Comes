using System;
using System.Collections;
using System.Collections.Generic;
using TD.Combat;
using TD.Core;
using UnityEngine;

namespace TD.AI
{
    public class CastAction : MonoBehaviour, IAction
    {
        Hero hero;
        Tower tower;
        Enemy enemy;
        
        [SerializeField] TargetType targetType;
        [SerializeField] Ability ability;
        
        [SerializeField] float spellRange = 5f;
        [SerializeField] float cooldown = 2f;

        float timeSinceLastCast = Mathf.Infinity;
        private bool isActiveAction = false;
        
        
        private IEnumerator CastBehaviour()
        {
            while(true)
            {
                if(isActiveAction == false) { print("breaking cast routine"); yield break;}
                timeSinceLastCast += Time.deltaTime;

                if(!TargetInRange())
                {
                    MoveToTarget();
                }
                else
                {
                    if(targetType == TargetType.Hero) 
                    {
                        StopMoving();
                        CastToHero();
                        yield return null;
                    }

                    if(targetType == TargetType.Tower) 
                    {
                        StopMoving(); 
                        CastToTower(); 
                        yield return null;
                    }
                    if(targetType == TargetType.Enemy ) 
                    { 
                        StopMoving();
                        CastToEnemy();
                        yield return null;
                    }
                }
                yield return null;
            }
            
        }

        private void CastToTower()
        {
            if(tower.IsDestroyed())
            {
                tower = null;
                timeSinceLastCast = Mathf.Infinity;
                return;
            }

            if(timeSinceLastCast > cooldown)
            {
                ability.CastToTower(tower);
                timeSinceLastCast = 0;
            }
        }

        private void CastToEnemy()
        {
            if(enemy.IsMaxHealth())
            {
                enemy = null;
                timeSinceLastCast = Mathf.Infinity;
                return;
            }

            if(timeSinceLastCast > cooldown)
            {
                ability.CastToEnemy(enemy);
                timeSinceLastCast = 0;
            }
        }

        private void CastToHero()
        {
            if(hero.IsDead())
            {
                hero = null;
                timeSinceLastCast = Mathf.Infinity;
                return;
            }
            
            if(timeSinceLastCast > cooldown)
            {
                ability.CastToHero(hero);
                timeSinceLastCast = 0;
            }
        }

        public bool IsEngaged()
        {
            if(!TargetFound()) return false;
            return true;
        }

        public bool TargetInRange()
        {
            if(targetType == TargetType.Hero)
            {
                if(Vector3.Distance(hero.transform.position, transform.position) < spellRange) return true;
            }

            if(targetType == TargetType.Tower)
            {
                if(Vector3.Distance(tower.transform.position, transform.position) < spellRange) return true;
            }

            if(targetType == TargetType.Enemy)
            {
                if(Vector3.Distance(enemy.transform.position, transform.position) < spellRange) return true;
            }
            return false;
        }
        
        private bool TargetFound()
        {
            if(targetType == TargetType.Hero)
            {
                if(this.hero != null) return true;
                foreach (var target in FindObjectsOfType<Hero>())
                {
                    if(target.InCombat()) continue;
                    if(Vector3.Distance(target.transform.position, transform.position) < spellRange)
                    {
                        this.hero = target;
                        target.StartCombat();
                        print("Found Target");
                        return true;
                    }
                }
                return false;
            }
            if(targetType == TargetType.Tower)
            {
                //if(this.tower != null) return true;
                foreach (var tower in FindObjectsOfType<Tower>())
                {
                    //if(target.InCombat()) continue;
                    if(Vector3.Distance(tower.transform.position, transform.position) < spellRange)
                    {
                        this.tower = tower;
                        //target.StartCombat();
                        print("Found Tower");
                        return true;
                    }
                }
                return false;
            }
            if(targetType == TargetType.Enemy)
            {
                if(this.enemy != null) return true;
                foreach (var enemy in FindObjectsOfType<Enemy>())
                {
                    if(enemy == this.enemy) continue;
                    if(enemy.IsMaxHealth()) continue;
                    if(Vector3.Distance(enemy.transform.position, transform.position) < spellRange)
                    {
                        this.enemy = enemy;
                        //target.StartCombat();
                        print("Found Enemy");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        private void MoveToTarget()
        {
            MoveAction moveAction = GetComponent<MoveAction>();
            if(targetType == TargetType.Hero){ moveAction.MoveTo(hero.transform.position); }
            if(targetType == TargetType.Tower){ moveAction.MoveTo(tower.transform.position); }
            if(targetType == TargetType.Enemy){ moveAction.MoveTo(enemy.transform.position); }
        }

        private void StopMoving()
        {
            GetComponent<MoveAction>().Stop();
        }

        public void Cancel()
        {
            isActiveAction = false;
        }

        public void StartAction()
        {
            isActiveAction = true;
            StartCoroutine(CastBehaviour());
        }

        private void OnDrawGizmos() 
        {
            Gizmos.DrawWireSphere(transform.position, spellRange);
        }

        public enum TargetType
        {
            Hero,
            Tower,
            Enemy,
            Aura
        }

    }
}
