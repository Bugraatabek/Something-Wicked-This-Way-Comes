using System;
using UnityEngine;

namespace TD.Attributes
{
    public class PlayerHealth : MonoBehaviour 
    {
        [SerializeField] float maxHealth = 5f;
        float health;

        public event Action playerHealthUpdated;

        private void Awake() 
        {
            health = maxHealth;    
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            playerHealthUpdated.Invoke();
        }

        public float GetCurrentHealth()
        {
            return health;
        }
    }
}