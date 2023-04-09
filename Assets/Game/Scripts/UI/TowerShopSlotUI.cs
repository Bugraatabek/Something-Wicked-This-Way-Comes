using TD.Control;
using TD.Core;
using TD.Shops;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TD.UI
{
    public class TowerShopSlotUI : MonoBehaviour 
    {
        //Config
        [SerializeField] Image icon;
        [SerializeField] Button buyButton;
        [SerializeField] TextMeshProUGUI displayName;
        
        //State
        Tower tower;
        TowerShop towerShop;

        public void Setup(Tower tower)
        {
            this.tower = tower;
            this.displayName.text = tower.GetDisplayName();
            this.icon.sprite = tower.GetIcon();

            towerShop = FindObjectOfType<TowerShop>();
            buyButton.onClick.AddListener(ChooseTower);
        }

        private void ChooseTower()
        { 
            if(towerShop != null)
            {
                towerShop.Choose(tower);
            }  
        }
    }
}