using System.Collections;
using System.Collections.Generic;
using TD.Combat;
using TMPro;
using UnityEngine;

namespace TD.UI
{
    public class EnemyInfo : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthText = null;
        [SerializeField] TextMeshProUGUI armorTypeText = null;

        [SerializeField] Enemy enemy = null;

        private void Start() 
        {
            armorTypeText.text = enemy.GetArmorType().ToString();
            healthText.text = enemy.GetEnemyHealth().ToString();
        }
        public void RefreshUI()
        {
            healthText.text = enemy.GetEnemyHealth().ToString();
        }
    }
}
