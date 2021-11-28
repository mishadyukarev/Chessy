using Game.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class FromToNewUnitMS : IEcsRunSystem
    {
        private EcsFilter<UnitC, LevelC, OwnerC> _unitMainFilt = default;

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            UnitDoingMC.Get(out var unit);
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            var whoseMove = WhoseMoveC.WhoseMove;

            ref var unit_from = ref _unitMainFilt.Get1(idx_from);
            ref var levUnit_from = ref _unitMainFilt.Get2(idx_from);
            ref var ownUnit_from = ref _unitMainFilt.Get3(idx_from);

            ref var unit_to = ref _unitMainFilt.Get1(idx_to);
            ref var levUnit_to = ref _unitMainFilt.Get2(idx_to);
            ref var ownUnit_to = ref _unitMainFilt.Get3(idx_to);


            if (unit_from.Is(UnitTypes.Archer))
            {
                if (unit_to.Is(UnitTypes.Archer))
                {
                    if (ownUnit_from.Is(whoseMove) && ownUnit_to.Is(whoseMove))
                    {
                        var xy_from = EntityPool.CellC<XyC>(idx_from).Xy;

                        var list_around = CellSpaceC.XyAround(xy_from);

                        
                        foreach (var xy_1 in list_around)
                        {
                            var idx_1 = EntityPool.IdxCell(xy_1);

                            if (idx_1 == idx_to)
                            {
                                RpcSys.SoundToGeneral(sender, ClipTypes.GetHero);

                                unit_to.SetFromToUnit(unit, idx_from);

                                break;
                            }
                        }
                    }
                }
            }

        }
    }
}