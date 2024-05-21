using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class Slot
{
    private SlotItem _mainItem;
    private SlotItem _previousItem;
    private SlotItem _newItem;
    private SlotItem _nextItem;

    private SlotViewSettings _viewSettings;

    private SlotView _view;

    public bool _isRolling = false;

    public Slot(SlotView view, SlotViewSettings viewSettings)
    {
        _view = view;
        _viewSettings = viewSettings;
        
        _mainItem = SlotItem.Seven;
        _previousItem = SlotItem.Seven;
        _newItem = SlotItem.Seven;
        _nextItem = SlotItem.Seven;
    }

    public void SetNewItem(SlotItem item)
    {
        _newItem = item;
    }

    private void SetupSlotWithNewItem()
    {
        _previousItem = _mainItem;
        _mainItem = _nextItem;
        _nextItem = _newItem;
    }

    public IEnumerator Roll()
    {
        int rollCount = Random.Range(_viewSettings.MinRollingCycle, _viewSettings.MaxRollingCycle);
        
        if (!_isRolling)
        {
            _isRolling = true;
            for (int i = 0; i < rollCount; i++)
            {
                SlotItem newItem = (SlotItem)Random.Range(0, _viewSettings.ItemSettings.Count-1);
                SetNewItem(newItem);
                
                SetupSlotWithNewItem();
                yield return _view.UpdateSlotWithNewItem(_newItem);
            }
        }

        _isRolling = false;
    }
}