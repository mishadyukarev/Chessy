using UnityEngine;

namespace Game.Game
{
    sealed class ProtectUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal ProtectUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxC.Idx;

            var isEnableButt = false;

            if (Es.UnitTC(idx_sel).HaveUnit)
            {
                if (Es.UnitPlayerTC(idx_sel).Is(Es.CurPlayerI.Player))
                {
                    isEnableButt = true;

                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);

                    RightProtectUIE.Button<GameObjectVC>(Es.UnitTC(idx_sel).Unit).SetActive(true);

                    if (Es.UnitConditionTC(idx_sel).Is(ConditionUnitTypes.Protected))
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
