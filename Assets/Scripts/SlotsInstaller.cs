using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SlotsInstaller : MonoInstaller
{
    [SerializeField] private SlotViewSettings _viewSettings;
    [SerializeField] private SlotView _leftSlot;
    [SerializeField] private SlotView _middleSlot;
    [SerializeField] private SlotView _rightSlot;

    [SerializeField] private Button _spinButton;

    private Slot _leftSlotModel;
    private Slot _middleSlotModel;
    private Slot _rightSlotModel;

    public override void InstallBindings()
    {
        Container.Bind<SlotViewSettings>().FromInstance(_viewSettings).AsSingle();

        _leftSlotModel = new Slot(_leftSlot, _viewSettings);
        _middleSlotModel = new Slot(_middleSlot, _viewSettings);
        _rightSlotModel = new Slot(_rightSlot, _viewSettings);

        _spinButton.onClick.AddListener(Spin);
    }

    public void Spin()
    {
        if (!_leftSlotModel._isRolling && !_middleSlotModel._isRolling && !_rightSlotModel._isRolling)
        {
            print("spin");
            _leftSlotModel.Roll();
            _middleSlotModel.Roll();
            _rightSlotModel.Roll();
        }
    }
}