using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.Core
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] string displayName = "Root Tower";
        [SerializeField] Sprite icon = null;
        [SerializeField] float health = 100f;
        [SerializeField] float mana = 0f;
        [SerializeField] int price = 10;
        [SerializeField] int woodPrice = 0;
        [SerializeField] int stonePrice = 0;
        
        public bool CreateTower(Tower tower, Vector3 position)
        {
            Bank bank = FindObjectOfType<Bank>();
            if (bank == null)
            {
                return false;
            }
                
            if (bank.GetGold() >= price)
            {
                bank.SpendResources(price, woodPrice,stonePrice);
                Instantiate(tower.gameObject,position,Quaternion.identity);
                return true;
            }
            return false;
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
        }

        public void Repair(float value)
        {
            health += value;
        }

        public void useMana(float value)
        {
            mana -= value;
        }

        public int GetPrice()
        {
            return price;
        }

        public int GetWoodPrice()
        {
            return woodPrice;
        }

        public int GetStonePrice()
        {
            return stonePrice;
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetDisplayName()
        {
            return displayName;
        }
    }
}
