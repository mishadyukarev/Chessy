using Assets.Scripts.ECS.Component.View.Else.Game.General.Cell;
using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

namespace Assets.Scripts.ECS.Game.General.Systems
{
    internal class FliperAndRotatorUnitSystem : IEcsRunSystem
    {
        private EcsFilter<SelectorComponent> _selComFilter = default;

        private EcsFilter<CellUnitMainViewComp, CellUnitExtraViewComp> _cellUnitViewFilter = default;
        private EcsFilter<CellUnitDataComponent, OwnerOnlineComp, OwnerBotComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var selCom = ref _selComFilter.Get1(0);

            foreach (byte idxCurCell in _cellUnitFilter)
            {
                ref var curUnitDatCom = ref _cellUnitFilter.Get1(idxCurCell);
                ref var curOwnUnitCom = ref _cellUnitFilter.Get2(idxCurCell);

                ref var curMainUnitViewCom = ref _cellUnitViewFilter.Get1(idxCurCell);
                ref var curExtraUnitViewCom = ref _cellUnitViewFilter.Get2(idxCurCell);


                if (selCom.IdxSelCell == idxCurCell)
                {
                    if (curUnitDatCom.HaveUnit)

                        if (curOwnUnitCom.HaveOwner)

                            if (curOwnUnitCom.IsMine)
                            {
                                if (curUnitDatCom.IsUnitType(UnitTypes.Rook))
                                {
                                    if (PhotonNetwork.IsMasterClient) curMainUnitViewCom.Set_LocRotEuler(new Vector3(0, 0, -90));
                                    else curMainUnitViewCom.Set_LocRotEuler(new Vector3(0, 0, 90));
                                }
                                else
                                {
                                    curMainUnitViewCom.SetFlipX(true);
                                    curExtraUnitViewCom.SetFlipX(false);
                                }
                            }
                }
                else
                {
                    curMainUnitViewCom.SetFlipX(false);
                    curExtraUnitViewCom.SetFlipX(true);

                    if(PhotonNetwork.IsMasterClient) curMainUnitViewCom.Set_LocRotEuler(new Vector3(0, 0, 0));
                    else curMainUnitViewCom.Set_LocRotEuler(new Vector3(0, 0, 180));
                }
            }
        }
    }
}
