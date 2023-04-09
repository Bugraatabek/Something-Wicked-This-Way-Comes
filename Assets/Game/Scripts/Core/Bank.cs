using System;
using UnityEngine;


namespace TD.Core
{
    public class Bank : MonoBehaviour
    {
        [SerializeField] int initialGold = 20;
        [SerializeField] int initialWood = 20;
        [SerializeField] int initialStone = 20;
        int gold;
        int wood;
        int stone;

        public event Action balanceUpdated;

        private void Awake() 
        {       
            gold = initialGold;
            wood = initialWood;
            stone = initialStone;    
        }
        
        public void SpendResources(int price, int woodPrice, int stonePrice)
        {
            gold -= price;
            wood -= woodPrice;
            stone -= stonePrice;
            balanceUpdated.Invoke();
        }

        public void GainResources(int value)
        {
            gold += value;
            balanceUpdated.Invoke();
        }

        public float GetGold()
        {
            return gold;
        }

        public float GetWood()
        {
            return wood;
        }

        public float GetStone()
        {
            return stone;
        }
    }
}
