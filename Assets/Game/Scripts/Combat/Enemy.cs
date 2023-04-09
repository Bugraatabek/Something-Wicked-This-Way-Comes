using System.Collections;
using System.Collections.Generic;
using TD.Attributes;
using TD.Core;
using UnityEngine;
using UnityEngine.Events;
using TD.Control;

namespace TD.Combat
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] float maxHealth = 200f;
        [SerializeField] float damageToPlayer = 1;
        [SerializeField] EArmorType armorType;
        [SerializeField] ArmorChart armorChart;
        [SerializeField] float destroyObjectDelay = 5f;
        [SerializeField] int goldPrize = 40;
        float health;

        public UnityEvent onDie;
        public UnityEvent onTakeDamage;

        private void Awake() 
        {
            health = maxHealth;
        }
        
        public void TakeDamage(float damage, EDamageType damageType)
        {
            health -= armorChart.GetDamage(armorType, damageType, damage);
            
            if(onTakeDamage != null)
            {
                onTakeDamage.Invoke();
            }
            
            if(health <= 0)
            {
                print("I am dead");
                if(onDie != null)
                {
                    onDie.Invoke();
                }
                
                GameObject.FindWithTag("Player").GetComponent<Bank>().GainResources(goldPrize);
                Destroy(gameObject, destroyObjectDelay);
            }
        }

        public float GetEnemyHealth()
        {
            return health;
        }

        public bool IsDead()
        {
            return health <= 0;
        }

        public float GetDamageToPlayer()
        {
            return damageToPlayer;
        }

        public EArmorType GetArmorType()
        {
            return armorType;
        }
    }

}