using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace
{
    public class SlotMachineView : MonoBehaviour
    {
        [SerializeField] private Button _spinButton;
        [SerializeField] private Image _spinButtonImage;
        [SerializeField] private Sprite _spinButtonNonActiveSprite;
        [SerializeField] private Sprite _spinButtonActiveSprite;

        [SerializeField] private List<BetButton> _betButtons;
        
        [SerializeField] private Sprite ActiveBetSprite;
        [SerializeField] private Sprite NonActiveBetSprite;
        
        public event Action OnSpinButtonClicked;

        [Inject]
        private void Construct(Player.PlayerData playerData, SlotsMachine slotsMachine)
        {
            _spinButton.onClick.AddListener(playerData.Spin);

            foreach (var betButton in _betButtons)
                betButton.Button.onClick.AddListener(() =>
                {
                    playerData.SetCurrentBet(betButton.Bet);
                    ClickBetButton(betButton);
                });
            
            playerData.OnSpinResponse += SetSpinButtonActive;
            
            _betButtons.First().Button.onClick.Invoke();

            slotsMachine.onSpinEnded += () => SetSpinButtonActive(false);
        }
        
        public void SetSpinButtonActive(bool isActive)
        {
            if (isActive)
                _spinButtonImage.sprite = _spinButtonActiveSprite;
            else 
                _spinButtonImage.sprite = _spinButtonNonActiveSprite;
            
            SetActiveBetsButtons(!isActive);
            
            _spinButton.interactable = !isActive;
            _spinButtonImage.SetNativeSize();
        }

        public void SetActiveBetSprite(Button button, Image image, bool isActive)
        {
            button.interactable = !isActive;
            image.sprite = isActive ? ActiveBetSprite : NonActiveBetSprite;
            //image.SetNativeSize();
        }

        private void ClickBetButton(BetButton button)
        {
            foreach (var betButton in _betButtons)
                SetActiveBetSprite(betButton.Button, betButton.Button.image, betButton.Bet == button.Bet);
        }

        private void SetActiveBetsButtons(bool isActive)
        {
            foreach (var betButton in _betButtons)
                betButton.Button.interactable = isActive;
        }
    }

    [Serializable]
    public class BetButton
    {
        public Button Button;
        public int Bet;
    }
}