using System;
using System.Collections;
using System.Collections.Generic;
using TD.Core;
using UnityEngine;
using TD.Attributes;


namespace TD.Combat
{
    [RequireComponent(typeof(Tower))]
    public class TowerAttack : MonoBehaviour
    {
        [SerializeField] EDamageType damageType;
        [SerializeField] GameObject projectile = null;
        [SerializeField] float damage = 20;
        [SerializeField] float attackSpeed = 1;
        [SerializeField] float range = 5f;


        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float timeBetweenAttacks = 3f;
        Target target = null;

        
        
        private void Update()
        {
            AttackBehaviour();
            timeSinceLastAttack += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            if (target == null)
            {
                target = FindTarget();
                return;
            }
            else
            {
                if(!TargetisInRange()) 
                {
                    target = null;
                    return;
                }
                Shoot();
            }
        }

        private Target FindTarget()
        {
            
            foreach (Target target in FindObjectsOfType<Target>())
            {
                if(target.GetComponent<Enemy>().IsDead()) continue;
                if(Vector3.Distance(target.transform.position, transform.position) < range)
                {
                    print(target);
                    return target;
                }
            }
             return null;
        }

        public Target GetTarget()
        {
            return target;
        }

        private bool TargetisInRange()
        {
            
            if(Vector3.Distance(target.transform.position, transform.position) > range || target.GetComponent<Enemy>().IsDead())
            {
                print("target is out of range");
                return false;
            }
            return true;
        }

        private void Shoot()
        {
            if(target != null)
            {
                if(timeSinceLastAttack > timeBetweenAttacks)
                {
                    var projectileInstance = Instantiate(projectile,transform);
                    projectileInstance.GetComponent<Projectile>().Setup(damageType, target, damage);
                    timeSinceLastAttack = 0;
                }
            }
        }

        private void OnDrawGizmos() 
        {
            Gizmos.DrawWireSphere(transform.position, range);    
        }

        public float GetDamage()
        {
            return damage;
        }

    }
}
