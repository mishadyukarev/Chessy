using Assets.Scripts.ECS.Component.Data.Else.Game.General.Cell;
using Assets.Scripts.ECS.Component.Data.Else.Game.Master;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Systems.Else.Game.Master.PunRPC
{
    internal sealed class GiveExtraPawnWeaponMasSys : IEcsRunSystem
    {
        private EcsFilter<ForGiveExtraPawnWeaponComp> _forGiveFilter = default;

        private EcsFilter<CellUnitDataComponent, CellPawnDataComp> _cellPawnFilter = default;

        public void Run()
        {
            //var idxCell = _forGiveFilter.Get1(0).IdxForGiveExtraPawnWeapon;


            //_cellPawnFilter.Get1(idxCell).
        }
    }
}
