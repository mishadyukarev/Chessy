namespace Chessy.Game
{
    sealed class CenterSelectorUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal CenterSelectorUIS(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var click_cur = E.CellClickTC.Click;
            var uniq = E.SelectedAbilityTC.Ability;


            UIEs.CenterEs.SelectorE.SelectorUI(click_cur).SetActiveParent(false);

            foreach (var click in UIEs.CenterEs.SelectorE.KeysClick)
            {
                UIEs.CenterEs.SelectorE.SelectorUI(click).SetActive(false);
            }

            foreach (var unique in UIEs.CenterEs.SelectorE.KeysUnique)
            {
                UIEs.CenterEs.SelectorE.SelectorUI(unique).SetActive(false);
            }

            if (click_cur != CellClickTypes.None
                && click_cur != CellClickTypes.SimpleClick
                && click_cur != CellClickTypes.SetUnit)
            {
                UIEs.CenterEs.SelectorE.SelectorUI(click_cur).SetActiveParent(true);

                if (click_cur == CellClickTypes.UniqueAbility)
                {
                    if (UIEs.CenterEs.SelectorE.KeysUnique.Contains(uniq))
                    {
                        UIEs.CenterEs.SelectorE.SelectorUI(click_cur).SetActive(true);
                        UIEs.CenterEs.SelectorE.SelectorUI(uniq).SetActive(true);
                    }
                }
                else
                {
                    UIEs.CenterEs.SelectorE.SelectorUI(click_cur).SetActive(true);
                }
            }
        }
    }
}
