using Leopotam.Ecs;
using UnityEngine;
using static Game.Game.EntityCellPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    public sealed class RelaxUISys : IEcsRunSystem
    {
        private EcsFilter<ConditionC> _effUnitF = default;

        public void Run()
        {
            ref var unit_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
            ref var selOnUnitCom = ref Unit<OwnerC>(SelIdx<IdxC>().Idx);

            ref var selCondUnitC = ref _effUnitF.Get1(SelIdx<IdxC>().Idx);  


            var activeButt = false;

            if (unit_sel.Have)
            {
                if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                {
                    activeButt = true;

                    if (selCondUnitC.Is(CondUnitTypes.Relaxed))
                    {
                        RelaxUIC.SetColor(Color.green);
                    }
                    else
                    {
                        RelaxUIC.SetColor(Color.white);
                    }

                    RelaxUIC.SetZone(unit_sel.Unit);
                }
            }

            RelaxUIC.SetActiveButton(activeButt);
        }
    }
}