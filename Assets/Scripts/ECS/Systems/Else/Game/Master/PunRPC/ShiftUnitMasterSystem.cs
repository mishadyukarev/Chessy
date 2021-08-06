using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game.Master;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static Assets.Scripts.Workers.CellBaseOperat;

internal sealed class ShiftUnitMasterSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _currentGameWorld;
    private EcsFilter<InfoMasCom> _infoFilter;
    private EcsFilter<ShiftMasCom, XyFromToComponent> _shiftFilter;
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    private Player Sender => _infoFilter.Get1(0).FromInfo.Sender;
    private int[] FromXy => _shiftFilter.Get2(0).FromXy;
    private int[] ToXy => _shiftFilter.Get2(0).ToXy;

    public void Init()
    {
        _currentGameWorld.NewEntity()
            .Replace(new ShiftMasCom())
            .Replace(new XyFromToComponent(new int[2], new int[2]));
    }

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        List<int[]> xyAvailableCellsForShift = CellUnitsDataSystem.GetCellsForShift(FromXy);

        if (CellUnitsDataSystem.IsHim(Sender, FromXy) && CellUnitsDataSystem.HaveMinAmountSteps(FromXy))
        {
            if (xyAvailableCellsForShift.TryFindCell(ToXy))
            {
                RPCGameSystem.SoundToGeneral(Sender, SoundEffectTypes.ClickToTable);


                var fromUnitType = CellUnitsDataSystem.UnitType(FromXy);
                var fromIsMasterClient = CellUnitsDataSystem.IsMasterClient(FromXy);

                var fromCondition = CellUnitsDataSystem.ConditionType(FromXy);


                MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(fromCondition, fromUnitType, fromIsMasterClient, FromXy);

                xyUnitsCom.RemoveAmountUnitsInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), FromXy);
                xyUnitsCom.AddAmountUnitInGame(CellUnitsDataSystem.UnitType(FromXy), CellUnitsDataSystem.IsMasterClient(FromXy), ToXy);
                CellUnitsDataSystem.ShiftPlayerUnitToBaseCell(FromXy, ToXy);

                MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(ConditionUnitTypes.None, fromUnitType, fromIsMasterClient, ToXy);


                CellUnitsDataSystem.TakeAmountSteps(ToXy, CellEnvrDataSystem.NeedAmountSteps(ToXy));
                if (CellUnitsDataSystem.AmountSteps(ToXy) < 0) CellUnitsDataSystem.ResetAmountSteps(ToXy);

                CellUnitsDataSystem.ResetConditionType(ToXy);
            }
        }
    }
}
