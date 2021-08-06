using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Leopotam.Ecs;
using System;

internal sealed class SetterUnitMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentGameWorld;
    private EcsFilter<InfoMasCom> _infoFilter;
    private EcsFilter<ForSettingUnitMasCom, XyCellForDoingMasCom> _setterFilter;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;
    private EcsFilter<InventorUnitsComponent> _unitInventorFilter;

    public void Init()
    {
        _currentGameWorld.NewEntity()
            .Replace(new ForSettingUnitMasCom())
            .Replace(new XyCellForDoingMasCom(new int[2]));
    }

    public void Run()
    {
        var sender = _infoFilter.Get1(0).FromInfo.Sender;
        var unitTypeForSetting = _setterFilter.Get1(0).UnitTypeForSetting;
        var xyForSetting = _setterFilter.Get2(0).XyCellForDoing;

        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var inventorUnitsCom = ref _unitInventorFilter.Get1(0);



        if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xyForSetting) && !CellUnitsDataSystem.HaveAnyUnit(xyForSetting)
            && MainGameSystem.XyStartCellsCom.IsStartedCell(sender.IsMasterClient, xyForSetting))
        {
            switch (unitTypeForSetting)
            {
                case UnitTypes.None:
                    throw new Exception();


                case UnitTypes.King:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    xyUnitsCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, xyForSetting);
                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;


                case UnitTypes.Pawn:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    xyUnitsCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, xyForSetting);
                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;


                case UnitTypes.PawnSword:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    xyUnitsCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, xyForSetting);
                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;


                case UnitTypes.Rook:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    xyUnitsCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, xyForSetting);
                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;


                case UnitTypes.RookCrossbow:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;


                case UnitTypes.Bishop:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    xyUnitsCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, xyForSetting);
                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;


                case UnitTypes.BishopCrossbow:
                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, unitTypeForSetting, sender.IsMasterClient, xyForSetting);

                    xyUnitsCom.AddAmountUnitInGame(unitTypeForSetting, sender.IsMasterClient, xyForSetting);
                    CellUnitsDataSystem.SetNewPlayerUnit(unitTypeForSetting, sender, xyForSetting);
                    inventorUnitsCom.TakeUnitsInInventor(unitTypeForSetting, sender.IsMasterClient);
                    break;

                default:
                    throw new Exception();
            }

            RPCGameSystem.SetUnitToGeneral(sender, true);
            RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.ClickToTable);
        }

        else
        {
            RPCGameSystem.SetUnitToGeneral(sender, false);
        }
    }
}
