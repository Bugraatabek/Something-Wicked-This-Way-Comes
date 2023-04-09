using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TD.Core;
using TD.Control;

namespace TD.Shops
{
    public class TowerShop : MonoBehaviour
    {
        [SerializeField] List<Tower> towersAvailable = new List<Tower>();
        PlayerController playerController;
        
        private void Awake() 
        {
            playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();   
        }

        public List<Tower> GetShopList()
        {
            return towersAvailable;
        }

        public void Choose(Tower tower)
        {
            playerController.InteractWithShop(tower);
        }
    }
}
