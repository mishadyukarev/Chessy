using UnityEngine;

namespace Game.Game
{
    sealed class ProtectUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ProtectUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var isEnableButt = false;

            if (Es.UnitEs(idx_sel).UnitE.HaveUnit)
            {
                if (Es.UnitE(idx_sel).Is(Es.WhoseMoveE.CurPlayerI))
                {
                    isEnableButt = true;

                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);

                    RightProtectUIE.Button<GameObjectVC>(Es.UnitE(idx_sel).Unit).SetActive(true);

                    if (Es.UnitE(idx_sel).Is(ConditionUnitTypes.Protected))
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
