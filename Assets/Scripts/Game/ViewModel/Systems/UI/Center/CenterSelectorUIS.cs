namespace Chessy.Game
{
    sealed class CenterSelectorUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterSelectorUIS( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(entsUI, ents)
        {
        }

        public void Run()
        {
            //var click_cur = E.CellClickTC.Click;
            //var uniq = E.SelectedE.AbilityTC.Ability;


            //UIE.CenterEs.SelectorE.SelectorUI(click_cur).SetActiveParent(false);

            //foreach (var click in UIE.CenterEs.SelectorE.KeysClick)
            //{
            //    UIE.CenterEs.SelectorE.SelectorUI(click).SetActive(false);
            //}

            //foreach (var unique in UIE.CenterEs.SelectorE.KeysUnique)
            //{
            //    UIE.CenterEs.SelectorE.SelectorUI(unique).SetActive(false);
            //}

            //if (click_cur != CellClickTypes.None
            //    && click_cur != CellClickTypes.SimpleClick
            //    && click_cur != CellClickTypes.SetUnit)
            //{
            //    UIE.CenterEs.SelectorE.SelectorUI(click_cur).SetActiveParent(true);

            //    if (click_cur == CellClickTypes.UniqueAbility)
            //    {
            //        if (UIE.CenterEs.SelectorE.KeysUnique.Contains(uniq))
            //        {
            //            UIE.CenterEs.SelectorE.SelectorUI(click_cur).SetActive(true);
            //            UIE.CenterEs.SelectorE.SelectorUI(uniq).SetActive(true);
            //        }
            //    }
            //    else
            //    {
            //        UIE.CenterEs.SelectorE.SelectorUI(click_cur).SetActive(true);
            //    }
            //}
        }
    }
}
