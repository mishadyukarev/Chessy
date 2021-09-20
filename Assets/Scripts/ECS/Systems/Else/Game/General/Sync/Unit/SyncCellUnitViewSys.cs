using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Components.Data.Else.Game.General;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataCom, CellUnitMainViewComp, CellUnitExtraViewComp> _cellUnitFilter = default;

        public void Run()
        {
            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curMainUnitViewCom = ref _cellUnitFilter.Get2(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitFilter.Get3(idxCurCell);

                curMainUnitViewCom.Disable_SR();
                curExtraUnitViewCom.Disable_SR();


                if (curUnitDatCom.HaveUnit)
                {
                    if (curUnitDatCom.IsVisibleUnit(WhoseMoveCom.CurPlayer))
                    {
                        curMainUnitViewCom.Enable_SR();

                        if (curUnitDatCom.IsUnit(UnitTypes.King))
                        {
                            curMainUnitViewCom.SetKing_Sprite();
                        }

                        else if (curUnitDatCom.IsUnit(UnitTypes.Pawn))
                        {
                            curMainUnitViewCom.SetPawn_Spriter();


                            if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.ExtraTWPawnType);
                            }
                        }

                        else if (curUnitDatCom.Is(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                            if (curUnitDatCom.HaveArcherWeapon)
                            {
                                curMainUnitViewCom.SetArcher_Sprite(curUnitDatCom.UnitType, curUnitDatCom.ArcherWeapType);
                            }
                            else
                            {
                                throw new Exception();
                            }

                            if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.ExtraTWPawnType);
                            }
                        }

                        else
                        {
                            throw new Exception();
                        }
                    }
                }
            }
        }
    }
}
