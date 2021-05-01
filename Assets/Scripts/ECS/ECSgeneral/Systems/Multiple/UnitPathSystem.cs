using Leopotam.Ecs;
using Photon.Realtime;
using System.Collections.Generic;
using static MainGame;


public struct UnitPathComponent
{
    private CellManager _cellManager;
    private SystemsGeneralManager _systemsGeneralManager;

    private UnitPathTypes _unitPathTypeIN;
    private int[] _xyStartCellIN;
    private Player _playerIN;

    private List<int[]> _xyAvailableCellsOUT;

    public UnitPathComponent(SystemsGeneralManager systemsGeneralManager, StartValuesGameConfig nameValueManager, CellManager cellManager)
    {
        _systemsGeneralManager = systemsGeneralManager;
        _cellManager = cellManager;

        _unitPathTypeIN = default;

        _xyStartCellIN = new int[nameValueManager.XY_FOR_ARRAY];
        _playerIN = default;

        _xyAvailableCellsOUT = new List<int[]>();
    }



    internal List<int[]> GetAvailableCells(UnitPathTypes unitPathType, in int[] xyStartCellIN, in Player playerIN)
    {
        _xyAvailableCellsOUT.Clear();

        _unitPathTypeIN = unitPathType;
        _cellManager.CopyXYinTo(xyStartCellIN, _xyStartCellIN);
        _playerIN = playerIN;

        _systemsGeneralManager.InvokeRunSystem(SystemGeneralTypes.Multiple, nameof(UnitPathSystem));

        return _cellManager.CopyListXY(_xyAvailableCellsOUT);
    }

    internal void Unpack(out UnitPathTypes unitPathTypeIN, out int[] xyStartCellIN, out Player playerIN)
    {
        unitPathTypeIN = _unitPathTypeIN;
        xyStartCellIN = _xyStartCellIN;
        playerIN = _playerIN;
    }

    internal void Pack(in List<int[]> xyAvailableCellsOUT)
    {
        _xyAvailableCellsOUT = xyAvailableCellsOUT;
    }
}



public partial class UnitPathSystem : CellReduction, IEcsRunSystem
{
    private EcsComponentRef<UnitPathComponent> _unitPathComponentRef;

    internal UnitPathSystem(ECSmanager eCSmanager) : base(eCSmanager)
    {
        _unitPathComponentRef = eCSmanager.EntitiesGeneralManager.UnitPathComponentRef;
    }


    public void Run()
    {
        _unitPathComponentRef.Unref().Unpack(out UnitPathTypes unitPathTypeIN, out int[] xyStartCellIN, out Player playerIN);


        var listAvailable = InstanceGame.SupportGameManager.CellFinderWay.TryGetXYAround(xyStartCellIN);

        switch (unitPathTypeIN)
        {
            case UnitPathTypes.Shift:

                var xyAvailableCellsForShift = new List<int[]>();
   
                foreach (var xy in listAvailable)
                {
                    if (!CellEnvironmentComponent(xy).HaveMountain)
                    {
                        if (CellUnitComponent(xyStartCellIN).AmountSteps >= CellUnitComponent(xy).NeedAmountSteps(CellEnvironmentComponent(xy).ListEnvironmentTypes)
                            || CellUnitComponent(xyStartCellIN).HaveMaxSteps)
                        {
                            xyAvailableCellsForShift.Add(xy);
                        }
                    }
                }
                _unitPathComponentRef.Unref().Pack(xyAvailableCellsForShift);

                break;

            case UnitPathTypes.Attack:

                var xyAvailableCellsForAttack = new List<int[]>();

                foreach (var xy in listAvailable)
                {
                    if (!CellEnvironmentComponent(xy).HaveMountain)
                    {
                        if (CellUnitComponent(xy).HaveUnit)
                        {
                            if (CellUnitComponent(xyStartCellIN).AmountSteps >= CellUnitComponent(xyStartCellIN).NeedAmountSteps(CellEnvironmentComponent(xyStartCellIN).ListEnvironmentTypes)
                                || CellUnitComponent(xyStartCellIN).HaveMaxSteps)
                            {
                                if (playerIN.ActorNumber != CellUnitComponent(xy).ActorNumber)
                                {
                                    xyAvailableCellsForAttack.Add(xy);
                                }
                            }
                        }
                    }

                    _unitPathComponentRef.Unref().Pack(xyAvailableCellsForAttack);
                }

                break;
        }
    }
}
