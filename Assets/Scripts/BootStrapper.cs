using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DefaultNamespace;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BootStrapper : MonoInstaller
{
    [SerializeField] private SlotViewSettings _viewSettings;
    [SerializeField] private SlotView _leftSlot;
    [SerializeField] private SlotView _middleSlot;
    [SerializeField] private SlotView _rightSlot;

    [SerializeField] private SlotMachineView _slotMachineView;
    
    [SerializeField] private CoroutineRunner _coroutineRunner;
    
    [SerializeField] private PlayerView _playerView;
    
    private Slot _leftSlotModel;
    private Slot _middleSlotModel;
    private Slot _rightSlotModel;

    public override void InstallBindings()
    {
        CreateSlotsModels();
        Container.Bind<SlotViewSettings>().FromInstance(_viewSettings).AsSingle();
        Container.Bind<SlotMachineView>().FromInstance(_slotMachineView).AsSingle();
        
        Container.Bind<PlayerData>().AsSingle().NonLazy();
        
        Container.Bind<PlayerView>().FromInstance(_playerView).AsSingle();
        
        Container.Bind<CoroutineRunner>().FromInstance(_coroutineRunner).AsSingle();
        var slotsMachine = new SlotsMachine(_leftSlotModel, _middleSlotModel, _rightSlotModel, Container.Resolve<PlayerData>(), _coroutineRunner);

        Container.Bind<SlotsMachine>().FromInstance(slotsMachine).AsSingle();
    }

    private void CreateSlotsModels()
    {
        _leftSlotModel = new Slot(_leftSlot, _viewSettings);
        _middleSlotModel = new Slot(_middleSlot, _viewSettings);
        _rightSlotModel = new Slot(_rightSlot, _viewSettings);
    }
}