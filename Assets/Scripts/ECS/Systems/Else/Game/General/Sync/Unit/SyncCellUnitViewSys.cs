using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System;

namespace Assets.Scripts.ECS.Game.General.Systems.SupportVision
{
    internal sealed class SyncCellUnitViewSys : IEcsRunSystem
    {
        private EcsFilter<CellUnitDataComponent, CellUnitMainViewComp, CellUnitExtraViewComp> _cellUnitFilter = default;

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
                    if (curUnitDatCom.IsVisibleUnit(PhotonNetwork.IsMasterClient))
                    {
                        curMainUnitViewCom.Enable_SR();

                        if (curUnitDatCom.IsUnitType(UnitTypes.King))
                        {
                            curMainUnitViewCom.SetKing_Sprite();
                        }

                        else if (curUnitDatCom.IsUnitType(UnitTypes.Pawn))
                        {
                            curMainUnitViewCom.SetPawn_Spriter();


                            if (curUnitDatCom.HaveExtraToolWeaponPawn)
                            {
                                curExtraUnitViewCom.Enable_SR();
                                curExtraUnitViewCom.SetToolWeapon_Sprite(curUnitDatCom.ExtraTWPawnType);
                            }
                        }

                        else if (curUnitDatCom.IsUnitType(new[] { UnitTypes.Rook, UnitTypes.Bishop }))
                        {
                            if (curUnitDatCom.HaveArcherWeapon)
                            {
                                curMainUnitViewCom.SetArcher_Sprite(curUnitDatCom.UnitType, curUnitDatCom.ArcherWeaponType);
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
