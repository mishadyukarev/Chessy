using Game.Common;
using UnityEngine;
using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct ProtectUIS : IEcsRunSystem
    {
        public void Run()
        {
            ref var unit_sel = ref Unit(SelectedIdxE.IdxC.Idx);
            ref var ownUnit_sel = ref CellUnitElseEs.Owner(SelectedIdxE.IdxC.Idx);
            ref var cond_sel = ref CellUnitElseEs.Condition(SelectedIdxE.IdxC.Idx);


            var isEnableButt = false;

            if (unit_sel.Have)
            {
                if (ownUnit_sel.Is(WhoseMoveE.CurPlayerI))
                {
                    isEnableButt = true;

                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Archer).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);

                    RightProtectUIE.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);

                    if (cond_sel.Is(ConditionUnitTypes.Protected))
                    {
                        RightProtectUIE.Button<ImageUIC>().Color = Color.yellow;
                    }

                    else
                    {
                        RightProtectUIE.Button<ImageUIC>().Color = Color.white;
                    }
                }
            }

            RightProtectUIE.Button<ImageUIC>().SetActiveParent(isEnableButt);
        }
    }
}
