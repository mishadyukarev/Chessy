using Assets.Scripts.Abstractions.Enums.WeaponsAndTools;
using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataComponent, CellUnitMainViewComp, CellUnitExtraViewComp> _cellUnitFilter = default;
        private EcsFilter<SelectorComponent> _selectorFilter = default;

        public void Run()
        {
            ref var selectorCom = ref _selectorFilter.Get1(0);

            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curMainUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);

                curMainUnitViewCom.Disable_SR();
                curExtraUnitViewCom.Disable_SR();


                if (curUnitDataCom.HaveUnit)
                {
                    if (curUnitDataCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        curMainUnitViewCom.Enable_SR();

                        if (curUnitDataCom.IsUnitType(UnitTypes.King))
                        {
                            curMainUnitViewCom.SetKing_Sprite();
                        }

                        else if (curUnitDataCom.IsUnitType(UnitTypes.Pawn))
                        {
                            curMainUnitViewCom.SetPawn_Spriter();


                            if (curUnitDataCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDataCom.ExtraToolWeaponPawnType);
                            }
                        }

                        else if (curUnitDataCom.IsUnitType(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                            if (curUnitDataCom.ArcherWeapon)
                            {
                                curMainUnitViewCom.SetArcher_Sprite(curUnitDataCom.UnitType, curUnitDataCom.ArcherWeaponType);
                            }
                            else
                            {
                                throw new Exception();
                            }

                            if (curUnitDataCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDataCom.ExtraToolWeaponPawnType);
                            }
                        }

                        else
                        {
                            throw new Exception();
                        }
                    }
                }

                else
                {

                    if (selectorCom.IsSelectedUnit)
                    {
                        if (selectorCom.IdxCurrentCell == idxCurCell)
                        {
                            _cellUnitFilter.Get2(idxCurCell).Enable_SR();

                            if (selectorCom.SelectedUnitType == UnitTypes.King)
                            {
                                _cellUnitFilter.Get2(idxCurCell).SetKing_Sprite();
                            }
                            else if (selectorCom.SelectedUnitType == UnitTypes.Pawn)
                            {
                                _cellUnitFilter.Get2(idxCurCell).SetPawn_Spriter();
                            }
                            else if (selectorCom.SelectedUnitType == UnitTypes.Rook || selectorCom.SelectedUnitType == UnitTypes.Bishop)
                            {
                                _cellUnitFilter.Get2(idxCurCell).SetArcher_Sprite(selectorCom.SelectedUnitType, ToolWeaponTypes.Bow);
                            }
                        }
                    }
                }
            }
        }
    }
}
