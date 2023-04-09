using System.Collections;
using TD.Combat;
using TD.Core;
using TD.Shops;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TD.Control
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] LayerMask canCreateTowerOnLayer;
        Tower towerToCreate;
        Bank bank;
        TowerShop shop;

        private void Awake() 
        {
            shop = FindObjectOfType<TowerShop>();
            bank = GetComponent<Bank>();    
        }
        
        public void InteractWithShop(Tower tower)
        {
            towerToCreate = tower;
            if(bank.GetGold() < towerToCreate.GetPrice()) 
            {
                print("Not enough resources"); 
                return;
            }
            else
            {
                StartCoroutine(ConstructTower());
            }
        }
        
        private IEnumerator ConstructTower()
        {
            var towerInstance = Instantiate(towerToCreate, transform);
            while(true)
            {
                RaycastHit raycastHit;
                if(Physics.Raycast(GetMouseRay(),out raycastHit, 1000, canCreateTowerOnLayer))
                {
                    towerInstance.transform.position = raycastHit.point;
                    towerInstance.GetComponent<Tower>().enabled = false;
                    towerInstance.GetComponent<TowerAttack>().enabled = false;
                    
                    if(Input.GetMouseButtonDown(1))
                    {
                        Destroy(towerInstance.gameObject);
                        yield break;
                    }

                    if(Input.GetMouseButtonDown(0))
                    {
                        bank.SpendResources(towerInstance.GetPrice(), towerInstance.GetWoodPrice(), towerInstance.GetStonePrice());
                        towerInstance.GetComponent<Tower>().enabled = true;
                        towerInstance.GetComponent<TowerAttack>().enabled = true;
                        yield break;
                    }
                }
                yield return null;
            }
        }

        private IEnumerator ConstructTowerMobile()
        {
            var towerInstance = Instantiate(towerToCreate, transform);
            while(true)
            {
                if(UnityEngine.InputSystem.Touchscreen.current.press.IsPressed())
                {

                
                    Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
                    Vector3 touchWorldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
                    towerInstance.transform.position = touchWorldPosition;
                    if(!UnityEngine.InputSystem.Touchscreen.current.press.IsPressed())
                    {
                        bank.SpendResources(towerInstance.GetPrice(), towerInstance.GetWoodPrice(), towerInstance.GetStonePrice());
                        towerInstance.GetComponent<Tower>().enabled = true;
                        towerInstance.GetComponent<TowerAttack>().enabled = true;
                        yield break; 
                    }
                }
            }
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    }
}
