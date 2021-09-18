using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SelectorSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataCom, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvironDataFilter = default;

    private EcsFilter<SelectorCom> _selectorFilter = default;
    private EcsFilter<InputComponent> _inputFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;
    private EcsFilter<SoundEffectsComp> _soundFilter = default;

    private EcsFilter<CellsArsonArcherComp> _availCellsForArcherArsonFilter = default;
    private EcsFilter<AvailCellsForAttackComp> _availCellsForAttackFilter = default;
    private EcsFilter<AvailCellsForShiftComp> _availCellsForShiftFilter = default;

    private EcsFilter<WhoseMoveCom> _whoseMoveFilter = default;

    public void Run()
    {
        CellUnitDataCom CellUnitDataCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
        OwnerOnlineComp OwnerCellUnitCom(byte idxCell) => _cellUnitFilter.Get2(idxCell);
        OwnerBotComponent OwnerBotCellUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
        CellEnvironDataCom CellEnvironDataCom(byte idxCell) => _cellEnvironDataFilter.Get1(idxCell);




        ref var selCom = ref _selectorFilter.Get1(0);
        ref var cellsAttackCom = ref _availCellsForAttackFilter.Get1(0);
        ref var cellsShiftCom = ref _availCellsForShiftFilter.Get1(0);
        ref var soundEffectCom = ref _soundFilter.Get1(0);


        if (_inputFilter.Get1(0).IsClicked)
        {
            if (selCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
                selCom.DefSelectedUnit();
            }

            else if (selCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
                {
                    if (!selCom.IsSelectedCell)
                    {
                        if (selCom.IdxPreCell != selCom.IdxSelCell)
                        {
                            selCom.IdxSelCell = selCom.IdxCurCell;
                        }
                        else
                        {
                            selCom.IdxSelCell = default;
                        }


                        selCom.IdxPreCell = selCom.IdxSelCell;
                    }

                    else
                    {
                        if (selCom.IdxSelCell != selCom.IdxCurCell)
                            selCom.IdxPreCell = selCom.IdxSelCell;

                        selCom.IdxSelCell = selCom.IdxCurCell;
                    }
                }

                else if (selCom.IsSelectedUnit)
                {
                    RpcSys.SetUniToMaster(selCom.IdxCurCell, selCom.SelUnitType);
                    selCom.SelUnitType = default;
                }

                else if (selCom.IsCellClickType(CellClickTypes.PickFire))
                {

                    RpcSys.FireToMaster(selCom.IdxSelCell, selCom.IdxCurCell);

                    selCom.CellClickType = default;
                    selCom.IdxSelCell = selCom.IdxCurCell;
                }

                else if (selCom.IsCellClickType(CellClickTypes.GiveTakeTW))
                {
                    if (CellUnitDataCom(selCom.IdxCurCell).Is(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop }))
                    {
                        RpcSys.GiveTakeToolWeapon(selCom.ToolWeaponTypeForGiveTake, selCom.IdxCurCell);
                    }
                    else
                    {
                        selCom.IdxSelCell = selCom.IdxCurCell;
                        selCom.DefCellClickType();
                    }
                }

                else if (selCom.IsSelectedCell)
                {
                    if (selCom.IdxSelCell != selCom.IdxCurCell)
                        selCom.IdxPreCell = selCom.IdxSelCell;

                    selCom.IdxSelCell = selCom.IdxCurCell;


                    var isMainMove = false;

                    if (PhotonNetwork.OfflineMode)
                    {
                        isMainMove = WhoseMoveCom.IsMainMove;
                    }
                    else
                    {
                        isMainMove = PhotonNetwork.IsMasterClient;
                    }

                    var b1 = cellsAttackCom.FindByIdx(AttackTypes.Simple, isMainMove, selCom.IdxPreCell, selCom.IdxSelCell);
                    var b2 = cellsAttackCom.FindByIdx(AttackTypes.Unique, isMainMove, selCom.IdxPreCell, selCom.IdxSelCell);

                    if (b1 || b2)
                    {
                        RpcSys.AttackUnitToMaster(selCom.IdxPreCell, selCom.IdxSelCell);
                    }

                    if (cellsShiftCom.HaveIdxCell(isMainMove, selCom.IdxPreCell, selCom.IdxSelCell))
                    {
                        RpcSys.ShiftUnitToMaster(selCom.IdxPreCell, selCom.IdxSelCell);
                    }

                    else if (CellUnitDataCom(selCom.IdxSelCell).HaveUnit)
                    {
                        if (OwnerCellUnitCom(selCom.IdxSelCell).HaveOwner)
                        {
                            if (OwnerCellUnitCom(selCom.IdxSelCell).IsMine)
                            {
                                if (CellUnitDataCom(selCom.IdxSelCell).IsMelee)
                                {
                                    soundEffectCom.Play(SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    soundEffectCom.Play(SoundEffectTypes.PickArcher);
                                }
                            }
                        }
                    }
                }

                else
                {
                    if (selCom.IdxPreCell != selCom.IdxSelCell || selCom.IdxPreCell == 0)
                    {
                        selCom.IdxSelCell = selCom.IdxCurCell;
                    }
                    else
                    {
                        selCom.DefSelectedCell();
                    }

                    if (CellUnitDataCom(selCom.IdxSelCell).HaveUnit)
                    {
                        if (OwnerCellUnitCom(selCom.IdxSelCell).HaveOwner)
                        {
                            if (OwnerCellUnitCom(selCom.IdxSelCell).IsMine)
                            {
                                if (CellUnitDataCom(selCom.IdxSelCell).IsMelee)
                                {
                                    soundEffectCom.Play(SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    soundEffectCom.Play(SoundEffectTypes.PickArcher);
                                }
                            }
                        }
                    }

                    selCom.IdxPreCell = selCom.IdxSelCell;
                }

            }

            else
            {
                selCom.IdxSelCell = 0;
                selCom.DefSelectedUnit();

                selCom.DefSelectedCell();
            }
        }

        else
        {
            if (selCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
            }

            else if (selCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (selCom.IsSelectedUnit)
                {
                    if (!CellUnitDataCom(selCom.IdxCurCell).HaveUnit || !CellUnitDataCom(selCom.IdxCurCell).IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        if (selCom.IsStartDirectToCell)
                        {
                            selCom.IdxPreviousVisionCell = selCom.IdxCurCell;
                            selCom.IdxCurCell = default;
                        }
                        else
                        {
                            selCom.IdxPreviousVisionCell = selCom.IdxCurCell;
                        }
                    }
                }
            }

            else
            {

            }
        }
    }
}