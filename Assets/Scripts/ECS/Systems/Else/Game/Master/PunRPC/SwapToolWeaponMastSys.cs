using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class SwapToolWeaponMastSys : IEcsRunSystem
    {
        private EcsFilter<ForSwapToolWeaponComp> _forSwapToolWeapFilter = default;

        private EcsFilter<CellUnitDataComponent> _cellUnitFilter = default;

        public void Run()
        {
            ref var forSwapToolWeaponComp = ref _forSwapToolWeapFilter.Get1(0);

            ref var cellUnitDataCompForSwap = ref _cellUnitFilter.Get1(forSwapToolWeaponComp.IdxCellForSwap);

            if (cellUnitDataCompForSwap.HaveUnit)
            {
                if (cellUnitDataCompForSwap.HaveExtraToolWeapon)
                {
                    var preExtraToolWeapType = cellUnitDataCompForSwap.ExtraToolWeaponType;

                    cellUnitDataCompForSwap.ExtraToolWeaponType = cellUnitDataCompForSwap.MainToolWeaponType;
                    cellUnitDataCompForSwap.MainToolWeaponType = preExtraToolWeapType;
                }
            }
        }
    }
}
