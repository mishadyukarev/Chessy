using UnityEngine;

namespace Game.Game
{
    sealed class RelaxUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RelaxUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var idx_sel = Es.SelectedIdxC.Idx;

            var activeButt = false;

            if (Es.UnitTC(idx_sel).HaveUnit)
            {
                if (Es.UnitPlayerTC(idx_sel).Is(Es.CurPlayerI.Player))
                {
                    activeButt = true;

                    if (Es.UnitConditionTC(idx_sel).Is(ConditionUnitTypes.Relaxed))
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

                    RightRelaxUIE.Button<GameObjectVC>(Es.UnitTC(idx_sel).Unit).SetActive(true);
                }
            }

            RightRelaxUIE.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}