using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace
{
    public class SlotView : MonoBehaviour
    {
        private const string ANIMATION_PARAMETR = "IsRolling";

        [SerializeField] private Image _mainItemImage;
        [SerializeField] private Image _previousItemImage;
        [SerializeField] private Image _newItemImage;
        [SerializeField] private Image _nextItemImage;

        [SerializeField] private Animator _animator;

        private SlotViewSettings _slotViewSettings;

        [Inject]
        public void Construct(SlotViewSettings settings)
        {
            _slotViewSettings = settings;

            _mainItemImage.sprite = settings.GetItemSprite(SlotItem.Seven);
            _previousItemImage.sprite = settings.GetItemSprite(SlotItem.Seven);
            _newItemImage.sprite = settings.GetItemSprite(SlotItem.Seven);
            _nextItemImage.sprite = settings.GetItemSprite(SlotItem.Seven);
        }

        public async Task UpdateSlotWithNewItem(SlotItem _newItem)
        {
            await WaitForAnimationToEnd(_newItem);
        }


        private async Task WaitForAnimationToEnd(SlotItem _newItem)
        {
            _animator.SetBool(ANIMATION_PARAMETR, true);

            
            await Task.Yield();
            await Task.Yield();

            
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            while (stateInfo.IsName("SlotRolling") && stateInfo.normalizedTime < 1.0f)
            {
                await Task.Yield();
                stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            }

            
            _animator.SetBool(ANIMATION_PARAMETR, false);

            await Task.Yield(); 
            
            _previousItemImage.sprite = _mainItemImage.sprite;
            _mainItemImage.sprite = _nextItemImage.sprite;
            _nextItemImage.sprite = _newItemImage.sprite;
            _newItemImage.sprite = _slotViewSettings.GetItemSprite(_newItem);
        }
    }
}