using Game.Common;
using UnityEngine;
using static Game.Game.EntCellUnit;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ProtectUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit<UnitC>(SelIdx<IdxC>().Idx);
            ref var ownUnit_sel = ref Unit<PlayerC>(SelIdx<IdxC>().Idx);
            ref var cond_sel = ref Unit<ConditionUnitC>(SelIdx<IdxC>().Idx);


            var isEnableButt = false;

            if (unit_sel.Have)
            {
                if (ownUnit_sel.Is(EntWhoseMove.CurPlayerI))
                {
                    isEnableButt = true;

                    UIEntRightProtect.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    UIEntRightProtect.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    UIEntRightProtect.Button<GameObjectVC>(UnitTypes.Archer).SetActive(false);
                    UIEntRightProtect.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);
                    UIEntRightProtect.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);

                    UIEntRightProtect.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);

                    if (cond_sel.Is(ConditionUnitTypes.Protected))
                    {
                        UIEntRightProtect.Button<ImageUIC>().Color = Color.yellow;
                    }

                    else
                    {
                        UIEntRightProtect.Button<ImageUIC>().Color = Color.white;
                    }
                }
            }

            UIEntRightProtect.Button<ImageUIC>().SetActive(isEnableButt);
        }
    }
}
