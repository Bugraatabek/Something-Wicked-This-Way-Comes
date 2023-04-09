using UnityEngine;
using UnityEngine.UI;
using TD.Control;
using System.Collections.Generic;
using TD.Core;
using TD.Shops;

namespace TD.UI
{
    public class TowerShopUI : MonoBehaviour 
    {
        [SerializeField] Transform content;
        [SerializeField] TowerShopSlotUI slotUIPrefab;
        PlayerController playerController;
        TowerShop towerShop;
        
        
        private void Start() 
        {
            BuildShop();  
        }

        private void BuildShop()
        {
            towerShop = FindObjectOfType<TowerShop>();
            if(towerShop.GetShopList() == null) return;
            foreach (Tower tower in towerShop.GetShopList())
            {
                var slotUIInstance = Instantiate(slotUIPrefab.gameObject, content);
                slotUIInstance.GetComponent<TowerShopSlotUI>().Setup(tower);
            }
        }
    }
}