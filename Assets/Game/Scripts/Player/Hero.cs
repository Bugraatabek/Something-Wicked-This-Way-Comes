using UnityEngine;

public class Hero : MonoBehaviour 
{
    [SerializeField] float health = 100f;
    private bool inCombat = false;
    


    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            gameObject.SetActive(false);
        }
        print(health);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void StartCombat()
    {
        inCombat = true;
    }

    public bool InCombat()
    {
        return inCombat;
    }
}