using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class SlotsMachine
    {
        private const float FREE_SPIN_WAITING_TIME = 0.5f;
        private Slot _leftSlotModel;
        private Slot _middleSlotModel;
        private Slot _rightSlotModel;

        public bool _leftSlotIsRolling = false;
        public bool _middleSlotIsRolling = false;
        public bool _rightSlotIsRolling = false;

        private CoroutineRunner _coroutineRunner;
        public event Action onSpinEnded;

        private int _freeSpinCount = 0;
        private bool _isFreeSpins = false;

        private RewardsController _rewardsController;

        public SlotsMachine(Slot leftSlotModel, Slot middleSlotModel, Slot rightSlotModel, PlayerData playerData,
            CoroutineRunner coroutineRunner, RewardsController rewardsController)
        {
            playerData.OnSpinResponse += (x) => _coroutineRunner.StartMyCoroutine(Spin(x));

            _leftSlotModel = leftSlotModel;
            _middleSlotModel = middleSlotModel;
            _rightSlotModel = rightSlotModel;

            _leftSlotModel.OnRollEnd += () => _leftSlotIsRolling = false;
            _middleSlotModel.OnRollEnd += () => _middleSlotIsRolling = false;
            _rightSlotModel.OnRollEnd += () => _rightSlotIsRolling = false;

            _coroutineRunner = coroutineRunner;

            _rewardsController = rewardsController;
            rewardsController.OnFreeSpinRewardGiven += TakeFreeSpins;
        }

        public IEnumerator Spin(bool success)
        {
            if (!success)
                yield break;

            if (!_leftSlotIsRolling && !_middleSlotIsRolling && !_rightSlotIsRolling)
            {
                _leftSlotIsRolling = true;
                _middleSlotIsRolling = true;
                _rightSlotIsRolling = true;

                _coroutineRunner.StartMyCoroutine(_leftSlotModel.Roll());
                _coroutineRunner.StartMyCoroutine(_middleSlotModel.Roll());
                _coroutineRunner.StartMyCoroutine(_rightSlotModel.Roll());

                yield return _coroutineRunner.StartMyCoroutine(WaitAvailableSpinButton());
            }
        }

        private IEnumerator WaitAvailableSpinButton()
        {
            while (_leftSlotIsRolling || _middleSlotIsRolling || _rightSlotIsRolling)
            {
                yield return null;
            }

            _rewardsController.TakeReward(new Combination(_leftSlotModel.MainItem, _middleSlotModel.MainItem,
                _rightSlotModel.MainItem));

            onSpinEnded?.Invoke();
        }

        private void TakeFreeSpins(int count)
        {
            _freeSpinCount += count;
            FreeSpin();
        }

        private void FreeSpin()
        {
            if (!_isFreeSpins)
            {
                _coroutineRunner.StartMyCoroutine(FreeSpinsCoroutine());
                _isFreeSpins = true;
            }
        }

        private IEnumerator FreeSpinsCoroutine()
        {
            while (_freeSpinCount > 0)
            {
                yield return new WaitForSeconds(FREE_SPIN_WAITING_TIME);
                yield return _coroutineRunner.StartMyCoroutine(Spin(true));
                yield return new WaitForSeconds(FREE_SPIN_WAITING_TIME);
                _freeSpinCount--;
            }

            _isFreeSpins = false;
        }
    }
}