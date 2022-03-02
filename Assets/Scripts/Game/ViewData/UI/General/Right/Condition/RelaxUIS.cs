using UnityEngine;

namespace Chessy.Game
{
    sealed class RelaxUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal RelaxUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            //var idx_sel = E.SelectedIdxC.Idx;

            //var activeButt = false;

            //if (E.UnitTC(idx_sel).HaveUnit)
            //{
            //    if (E.UnitPlayerTC(idx_sel).Is(E.CurPlayerITC.Player))
            //    {
            //        activeButt = true;

            //        if (E.UnitConditionTC(idx_sel).Is(ConditionUnitTypes.Relaxed))
            //        {
            //            RightRelaxUIE.Button<ImageUIC>().Color = Color.green;
            //        }
            //        else
            //        {
            //            RightRelaxUIE.Button<ImageUIC>().Color = Color.white;
            //        }

            //        RightRelaxUIE.Button<GameObjectVC>(UnitTypes.King).SetActive(false);
            //        RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Pawn).SetActive(false);
            //        RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Scout).SetActive(false);
            //        RightRelaxUIE.Button<GameObjectVC>(UnitTypes.Elfemale).SetActive(false);

            //        RightRelaxUIE.Button<GameObjectVC>(E.UnitTC(idx_sel).Unit).SetActive(true);
            //    }
            //}

            //RightRelaxUIE.Button<ButtonUIC>().SetActive(activeButt);
        }
    }
}