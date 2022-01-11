using UnityEngine;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ProtectUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
            ref var ownUnit_sel = ref Unit<OwnerC>(SelIdx<IdxC>().Idx);
            ref var cond_sel = ref Unit<ConditionC>(SelIdx<IdxC>().Idx);


            var isEnableButt = false;

            if (unit_sel.Have)
            {
                if (ownUnit_sel.Is(WhoseMoveC.CurPlayerI))
                {
                    isEnableButt = true;
                    ProtectUIC.SetZone(unit_sel.Unit);

                    if (cond_sel.Is(ConditionUnitTypes.Protected))
                    {
                        ProtectUIC.SetColor(Color.yellow);
                    }

                    else
                    {
                        ProtectUIC.SetColor(Color.white);
                    }
                }
            }

            ProtectUIC.SetActiveButton(isEnableButt);
        }
    }
}
