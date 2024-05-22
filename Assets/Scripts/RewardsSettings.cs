using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardsSettings", menuName = "Settings/RewardsSettings")]
public class RewardsSettings : ScriptableObject
{
    public List<RewardSetting> _rewardSettings;

    public RewardSetting GetRewardSetiings(Combination combination)
    {
        foreach (var rewardSetting in _rewardSettings)
        {
            if(rewardSetting.combination._item1 != combination._item1)
                continue;
            if (rewardSetting.combination._item2 != combination._item2)
                continue;
            if (rewardSetting.combination._item3 != combination._item3)
                continue;
            
            return rewardSetting;
        }

        return new RewardSetting(new Combination(combination._item1, combination._item2, combination._item3), RewardType.None, 0);
    }
}

[Serializable]
public class RewardSetting
{
    public RewardSetting(Combination combination, RewardType type, int count)
    {
        this.combination = combination;
        this._type = type;
        this.count = count;
    }
    
    public Combination combination;
    public RewardType _type;
    public int count;
}

[Serializable]
public class Combination
{
    public SlotItem _item1;
    public SlotItem _item2;
    public SlotItem _item3;

    public Combination(SlotItem item1, SlotItem item2, SlotItem item3)
    {
        _item1 = item1;
        _item2 = item2;
        _item3 = item3;
    }
}

public enum RewardType
{
    Multiplier,
    FreeSpin,
    None
}
