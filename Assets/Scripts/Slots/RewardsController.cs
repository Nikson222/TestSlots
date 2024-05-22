using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class RewardsController
    {
        private RewardsSettings _rewardsSettings;
        
        public event Action<int> OnCoinsRewardGiven;
        public event Action<int> OnFreeSpinRewardGiven;
        
        public RewardsController(RewardsSettings rewardsSettings)
        {
            _rewardsSettings = rewardsSettings;
        }
        
        public void TakeReward(Combination combination)
        {
            var rewardSetting = _rewardsSettings.GetRewardSetiings(combination);

            Debug.Log($"{rewardSetting.combination._item1} {rewardSetting.combination._item2} {rewardSetting.combination._item3}"
                      + " " + rewardSetting._type + " " + rewardSetting.count);
            
            switch (rewardSetting._type)
            {
                case RewardType.Multiplier:
                    GiveCoinsReward(rewardSetting.count);
                    break;
                case RewardType.FreeSpin:
                    GiveFreeSpinReward(rewardSetting.count);
                    break;
                case RewardType.None:
                    break;
            }
        }

        private void GiveCoinsReward(int Multiplier)
        {
            OnCoinsRewardGiven?.Invoke(Multiplier);
        }

        private void GiveFreeSpinReward(int count)
        {
            OnFreeSpinRewardGiven?.Invoke(count);
        }
    }
}