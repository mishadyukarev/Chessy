using UnityEngine;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct RelaxUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
            ref var selOnUnitCom = ref Unit<OwnerC>(SelIdx<IdxC>().Idx);

            ref var selCondUnitC = ref Unit<ConditionC>(SelIdx<IdxC>().Idx);


            var activeButt = false;

            if (unit_sel.Have)
            {
                if (selOnUnitCom.Is(WhoseMoveC.CurPlayerI))
                {
                    activeButt = true;

                    if (selCondUnitC.Is(ConditionUnitTypes.Relaxed))
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