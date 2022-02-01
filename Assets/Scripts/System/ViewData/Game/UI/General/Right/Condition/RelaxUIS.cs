using UnityEngine;

namespace Game.Game
{
    sealed class RelaxUIS : SystemViewAbstract, IEcsRunSystem
    {
        public RelaxUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var unit_sel = UnitEs(idx_sel).MainE.UnitTC;
            var selOnUnitCom = UnitEs(idx_sel).MainE.OwnerC;


            var activeButt = false;

            if (UnitEs(idx_sel).MainE.HaveUnit(UnitStatEs(idx_sel)))
            {
                if (selOnUnitCom.Is(Es.WhoseMove.CurPlayerI))
                {
                    activeButt = true;

                    if (UnitEs(idx_sel).MainE.ConditionTC.Is(ConditionUnitTypes.Relaxed))
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.green;
                    }
                    else
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.white;
                    }

                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Archer).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);

                    RightRelaxUIE.Button<GameObjectVC>(unit_sel.Unit).SetActive(true);
                }
            }

            RightRelaxUIE.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}