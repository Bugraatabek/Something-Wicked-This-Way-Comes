using System.Collections;
using System.Collections.Generic;
using TD.Attributes;
using TD.Combat;
using UnityEngine;

namespace TD.Core
{
    public class FinishPoint : MonoBehaviour
    {
        PlayerHealth playerHealth;

        private void Awake() 
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();    
        }
        
        private void OnTriggerEnter(Collider other) 
        {
            if(other.gameObject.tag == "Enemy")
            {
                playerHealth.TakeDamage(other.gameObject.GetComponent<Enemy>().GetDamageToPlayer());
                Destroy(other.gameObject, 0.5f);
            }    
        }
    }
}
