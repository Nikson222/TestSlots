using System;
using System.Collections;
using System.Collections.Generic;
using Player;

namespace DefaultNamespace
{
    public class SlotsMachine
    {
        private Slot _leftSlotModel;
        private Slot _middleSlotModel;
        private Slot _rightSlotModel;

        public bool _leftSlotIsRolling = false;
        public bool _middleSlotIsRolling = false;
        public bool _rightSlotIsRolling = false;
        
        private CoroutineRunner _coroutineRunner;
        public event Action onSpinEnded;
            
        public SlotsMachine(Slot leftSlotModel, Slot middleSlotModel, Slot rightSlotModel, PlayerData playerData, CoroutineRunner coroutineRunner)
        {
            playerData.OnSpinResponse += Spin;
            
            _leftSlotModel = leftSlotModel;
            _middleSlotModel = middleSlotModel;
            _rightSlotModel = rightSlotModel;
            
            _leftSlotModel.OnRollEnd += () => _leftSlotIsRolling = false;
            _middleSlotModel.OnRollEnd += () => _middleSlotIsRolling = false;
            _rightSlotModel.OnRollEnd += () => _rightSlotIsRolling = false;
            
            _coroutineRunner = coroutineRunner;
        }
        
        public void Spin(bool success)
        {
            if(!success)
                return;
            
            if (!_leftSlotIsRolling && !_middleSlotIsRolling && !_rightSlotIsRolling)
            {
                _leftSlotIsRolling = true;
                _middleSlotIsRolling = true;
                _rightSlotIsRolling = true;
                
                _coroutineRunner.StartMyCoroutine(_leftSlotModel.Roll());
                _coroutineRunner.StartMyCoroutine(_middleSlotModel.Roll());
                _coroutineRunner.StartMyCoroutine(_rightSlotModel.Roll());
                
                _coroutineRunner.StartMyCoroutine(WaitAvailableSpinButton());
            }
        }
        
        private IEnumerator WaitAvailableSpinButton()
        {
            while (_leftSlotIsRolling || _middleSlotIsRolling || _rightSlotIsRolling)
            {
                yield return null;
            }
            
            onSpinEnded?.Invoke();
        }
    }
}