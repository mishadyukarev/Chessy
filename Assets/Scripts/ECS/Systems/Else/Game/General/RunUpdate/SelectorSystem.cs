using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Components;
using Assets.Scripts.ECS.Components.Data.Else.Game.General.AvailCells;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;

internal sealed class SelectorSystem : IEcsRunSystem
{
    private EcsFilter<XyCellComponent> _xyCellFilter = default;
    private EcsFilter<CellUnitDataComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter = default;
    private EcsFilter<CellEnvironDataCom> _cellEnvironDataFilter = default;

    private EcsFilter<SelectorComponent> _selectorFilter = default;
    private EcsFilter<InputComponent> _inputFilter = default;
    private EcsFilter<DonerDataUIComponent> _donerUIFilter = default;

    private EcsFilter<AvailCellsForArcherArsonComp> _availCellsForArcherArsonFilter = default;

    public void Run()
    {
        CellUnitDataComponent CellUnitDataCom(byte idxCell) => _cellUnitFilter.Get1(idxCell);
        OwnerComponent OwnerCellUnitCom(byte idxCell) => _cellUnitFilter.Get2(idxCell);
        OwnerBotComponent OwnerBotCellUnitCom(byte idxCell) => _cellUnitFilter.Get3(idxCell);
        CellEnvironDataCom CellEnvironDataCom(byte idxCell) => _cellEnvironDataFilter.Get1(idxCell);


        ref var selectorCom = ref _selectorFilter.Get1(0);


        if (_inputFilter.Get1(0).IsClicked)
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
                selectorCom.DefSelectedUnit();
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (_donerUIFilter.Get1(0).IsDoned(PhotonNetwork.IsMasterClient))
                {
                    if (!selectorCom.IsSelectedCell)
                    {
                        if (selectorCom.IdxPreviousCell != selectorCom.IdxSelectedCell)
                        {
                            selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                        }
                        else
                        {
                            selectorCom.IdxSelectedCell = default;
                        }


                        selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;
                        //selectorCom.DefSelectedCell();
                    }

                    else
                    {
                        if (selectorCom.IdxSelectedCell != selectorCom.IdxCurrentCell)
                            selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;

                        selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                    }
                }

                else if (selectorCom.IsSelectedUnit)
                {
                    RpcGeneralSystem.SetUniToMaster(selectorCom.IdxCurrentCell, selectorCom.SelectedUnitType);
                }

                else if (selectorCom.IsCellClickType(CellClickTypes.PickFire))
                {
                    if(_availCellsForArcherArsonFilter.Get1(0).HaveIdxCell(PhotonNetwork.IsMasterClient, selectorCom.IdxCurrentCell))
                    {
                        RpcGeneralSystem.FireToMaster(selectorCom.IdxSelectedCell, selectorCom.IdxCurrentCell);
                    }

                    selectorCom.CellClickType = default;
                    selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                }

                else if (selectorCom.IsCellClickType(CellClickTypes.GiveTakeToolWeapon))
                {
                    if (CellUnitDataCom(selectorCom.IdxCurrentCell).IsUnitType(new[] { UnitTypes.Pawn, UnitTypes.Rook, UnitTypes.Bishop }))
                    {
                        RpcGeneralSystem.GiveTakeToolWeapon(selectorCom.ToolWeaponTypeForGiveTake, selectorCom.IdxCurrentCell);
                    }
                    else
                    {
                        selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                        selectorCom.DefCellClickType();
                    }
                }

                else if (selectorCom.IsSelectedCell)
                {
                    if (selectorCom.IdxSelectedCell != selectorCom.IdxCurrentCell)
                        selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;

                    selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;

 
                    if (CellUnitDataCom(selectorCom.IdxSelectedCell).HaveUnit)
                    {
                        if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).HaveOwner)
                        {
                            if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).IsMine)
                            {
                                if (CellUnitDataCom(selectorCom.IdxSelectedCell).IsMelee)
                                {
                                    //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                }
                            }

                            else
                            {
                                RpcGeneralSystem.AttackUnitToMaster(selectorCom.IdxPreviousCell, selectorCom.IdxSelectedCell);
                            }
                        }

                        else if (OwnerBotCellUnitCom(selectorCom.IdxSelectedCell).IsBot)
                        {

                        }
                    }
                    else
                    {
                        RpcGeneralSystem.ShiftUnitToMaster(selectorCom.IdxPreviousCell, selectorCom.IdxSelectedCell);
                    }
                }

                else
                {
                    if (selectorCom.IdxPreviousCell != selectorCom.IdxSelectedCell || selectorCom.IdxPreviousCell == 0)
                    {
                        selectorCom.IdxSelectedCell = selectorCom.IdxCurrentCell;
                    }
                    else
                    {
                        selectorCom.DefSelectedCell();
                    }

                    if (CellUnitDataCom(selectorCom.IdxSelectedCell).HaveUnit)
                    {
                        if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).HaveOwner)
                        {
                            if (OwnerCellUnitCom(selectorCom.IdxSelectedCell).IsMine)
                            {
                                if (CellUnitDataCom(selectorCom.IdxSelectedCell).IsMelee)
                                {
                                    //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickMelee);
                                }
                                else
                                {
                                    //SoundGameGeneralViewWorker.PlaySoundEffect(SoundEffectTypes.PickArcher);
                                }
                            }
                        }
                    }

                    selectorCom.IdxPreviousCell = selectorCom.IdxSelectedCell;
                }

            }

            else
            {
                selectorCom.IdxSelectedCell = 0;
                selectorCom.DefSelectedUnit();

                selectorCom.DefSelectedCell();
            }
        }

        else
        {
            if (selectorCom.RaycastGettedType == RaycastGettedTypes.UI)
            {
            }

            else if (selectorCom.RaycastGettedType == RaycastGettedTypes.Cell)
            {
                if (selectorCom.IsSelectedUnit)
                {
                    if (!CellUnitDataCom(selectorCom.IdxCurrentCell).HaveUnit || !CellUnitDataCom(selectorCom.IdxCurrentCell).IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        if (selectorCom.IsStartDirectToCell)
                        {
                            selectorCom.IdxPreviousVisionCell = selectorCom.IdxCurrentCell;
                            selectorCom.IdxCurrentCell = default;
                        }
                        else
                        {
                            selectorCom.IdxPreviousVisionCell = selectorCom.IdxCurrentCell;
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