using System.Collections;
using TD.Combat;
using TD.Core;
using TD.Shops;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

namespace TD.Control
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] LayerMask canCreateTowerOnLayer;
        [SerializeField] Button cancelButton;
        Tower towerToCreate;
        Bank bank;
        TowerShop shop;
        UnityEngine.InputSystem.EnhancedTouch.Touch enhancedTouch;

        private void Awake() 
        {
            EnhancedTouchSupport.Enable();
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
                StartCoroutine(ConstructTowerMobile());
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
            RaycastHit raycastHit;
            while(true)
            {
                foreach (var terrain in GameObject.FindGameObjectsWithTag("Constructable"))
                {
                    terrain.GetComponent<MeshRenderer>().material.color = Color.green;
                }

                yield return new WaitForEndOfFrame();
                if(UnityEngine.InputSystem.Touchscreen.current.press.IsPressed())
                {
                    if(Physics.Raycast(GetTouchRay(), out raycastHit, 1000, canCreateTowerOnLayer))
                    {
                        foreach (var terrain in GameObject.FindGameObjectsWithTag("Constructable"))
                        {
                            terrain.GetComponent<MeshRenderer>().material.color = Color.white;
                        }

                        var towerInstance = Instantiate(towerToCreate,raycastHit.point, Quaternion.identity);
                        bank.SpendResources(towerInstance.GetPrice(), towerInstance.GetWoodPrice(), towerInstance.GetStonePrice());
                        yield break;
                    }
                }
            }
        }

        private void TouchPractice()
        {
            //enhancedTouch
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        
        public static Ray GetTouchRay()
        {
            return Camera.main.ScreenPointToRay(Touchscreen.current.primaryTouch.position.ReadValue());
        }

    }
}
