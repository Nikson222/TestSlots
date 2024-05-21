using System;
using DefaultNamespace;
using UnityEngine;

namespace Player
{
    public class PlayerData
    {
        private int _coins;
        private int _currentBet;

        public event Action<bool> OnSpinResponse;
        public event Action<int> OnCoinsChanged;
        
        public PlayerData()
        {
            _coins = PlayerPrefs.GetInt("coins", 10);
        }

        public int Coins
        {
            get => _coins;
            set
            {
                _coins = value;
                OnCoinsChanged?.Invoke(_coins);
            }
        }
        public void SetCurrentBet(int bet)
        {
            _currentBet = bet;
        }

        private void AddCoins(int coins)
        {
            Coins += coins;
            PlayerPrefs.SetInt("coins", _coins);
        }

        private void RemoveCoins(int value)
        {
            if (_coins >= value)
                Coins -= value;
            
            PlayerPrefs.SetInt("coins", _coins);
        }

        public void Spin()
        {
            if (_coins >= _currentBet)
            {
                RemoveCoins(_currentBet);
                OnSpinResponse?.Invoke(true);
            }
            else
                OnSpinResponse?.Invoke(false);
        }
    }
}