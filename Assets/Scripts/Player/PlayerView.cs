using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Text _coinsText;

        [Inject]
        public void Construct(PlayerData playerData)
        {
            playerData.OnCoinsChanged += SetCoins;    
            SetCoins(playerData.Coins);
        }
        
        public void SetCoins(int coins)
        {
            _coinsText.text = coins.ToString() + " \ncoins";
        }
    }
}