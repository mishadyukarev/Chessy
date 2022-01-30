using UnityEngine;

namespace Game.Game
{
    sealed class ProtectUIS : SystemViewAbstract, IEcsRunSystem
    {
        public ProtectUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            ref var unit_sel = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).UnitC;
            ref var ownUnit_sel = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).OwnerC;
            ref var cond_sel = ref Es.CellEs.UnitEs.Main(Es.SelectedIdxE.IdxC.Idx).ConditionC;


            var isEnableButt = false;

            if (unit_sel.Have)
            {
                if (ownUnit_sel.Is(Es.WhoseMove.CurPlayerI))
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
