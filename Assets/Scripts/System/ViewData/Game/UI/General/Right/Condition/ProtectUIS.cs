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

            var unit_sel = UnitEs(idx_sel).TypeE.UnitTC;
            var ownUnit_sel = UnitEs(idx_sel).OwnerE.OwnerC;


            var isEnableButt = false;

            if (UnitEs(idx_sel).TypeE.HaveUnit)
            {
                if (ownUnit_sel.Is(Es.WhoseMoveE.CurPlayerI))
                {
                    isEnableButt = true;

                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);
                    RightProtectUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);

                    RightProtectUIE.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);

                    if (UnitEs(idx_sel).ConditionE.ConditionTC.Is(ConditionUnitTypes.Protected))
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
