using System.Collections;
using System.Collections.Generic;
using TD.Attributes;
using TD.Core;
using TMPro;
using UnityEngine;

namespace TD.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthText = null;
        [SerializeField] TextMeshProUGUI goldText = null;
        [SerializeField] TextMeshProUGUI woodText = null;
        [SerializeField] TextMeshProUGUI stoneText = null;
        [SerializeField] Bank bank = null;
        [SerializeField] PlayerHealth playerHealth = null;

        private void Start() 
        {
            healthText.text = $"Health: {playerHealth.GetCurrentHealth()} points";
            goldText.text = $"{bank.GetGold()} prayer";
            woodText.text = $"{bank.GetWood()} wood";
            stoneText.text = $"{bank.GetStone()} stone";
        }

        private void OnEnable() 
        {
            bank.balanceUpdated += RefreshBalance;
            playerHealth.playerHealthUpdated += RefreshHealth;
        }

        private void OnDisable() 
        {
            bank.balanceUpdated -= RefreshBalance;
            playerHealth.playerHealthUpdated -= RefreshHealth;
        }

        private void RefreshBalance()
        {
            goldText.text = $"{bank.GetGold()} gold";
            woodText.text = $"{bank.GetWood()} wood";
            stoneText.text = $"{bank.GetStone()} stone";
        }

        private void RefreshHealth()
        {
            healthText.text = $"Health: {playerHealth.GetCurrentHealth()} points";
        }
    }
}
