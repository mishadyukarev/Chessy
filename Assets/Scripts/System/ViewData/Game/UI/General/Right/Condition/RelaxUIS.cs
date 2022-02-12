using UnityEngine;

namespace Game.Game
{
    sealed class RelaxUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RelaxUIS(in Entities ents, in EntitiesUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxE.IdxC.Idx;

            var activeButt = false;

            if (Es.UnitEs(idx_sel).UnitE.HaveUnit)
            {
                if (Es.UnitE(idx_sel).Is(Es.WhoseMoveE.CurPlayerI))
                {
                    activeButt = true;

                    if (Es.UnitE(idx_sel).Is(ConditionUnitTypes.Relaxed))
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.green;
                    }
                    else
                    {
                        RightRelaxUIE.Button<ImageUIC>().Color = Color.white;
                    }

                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);
                    RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);

                    RightRelaxUIE.Button<GameObjectVC>(Es.UnitE(idx_sel).Unit).SetActive(true);
                }
            }

            RightRelaxUIE.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}