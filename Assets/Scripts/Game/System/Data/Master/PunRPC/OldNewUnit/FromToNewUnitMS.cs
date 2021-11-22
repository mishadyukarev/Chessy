using Game.Common;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Game
{
    public sealed class FromToNewUnitMS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyFilt = default;
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
                        var xy_from = _xyFilt.Get1(idx_from).Xy;

                        var list_around = CellSpace.GetXyAround(xy_from);

                        

                        foreach (var xy_1 in list_around)
                        {
                           


                            var idx_1 = _xyFilt.GetIdxCell(xy_1);

                            if (idx_1 == idx_to)
                            {
                                RpcSys.SoundToGeneral(sender, ClipTypes.GetHero);
                                //RpcSys.SoundToGeneral(sender, UniqAbilTypes.GrowAdultForest);

                                unit_from.Remove(unit_from.Unit, levUnit_from.Level, ownUnit_from.Owner);
                                unit_to.Remove(unit_to.Unit, levUnit_to.Level, ownUnit_to.Owner);
                                

                                unit_to.SetNew(unit, levUnit_to.Level, ownUnit_to.Owner);


                                InvUnitsC.TakeUnit(ownUnit_to.Owner, unit_to.Unit, levUnit_to.Level);

                                break;
                            }
                        }

                       
                    }
                }
            }

        }
    }
}