using static Game.Game.CenterSelectorUIE;

namespace Game.Game
{
    sealed class SelectorUIS : SystemUIAbstract, IEcsRunSystem
    {
        internal SelectorUIS(in Entities ents, in EntitiesViewUI entsUI) : base(ents, entsUI)
        {
        }

        public void Run()
        {
            var click_cur = E.CellClickTC.Click;
            var uniq = E.SelectedAbilityTC.Ability;


            SelectorUI<GameObjectVC>(click_cur).SetActiveParent(false);

            foreach (var click in KeysClick)
            {
                SelectorUI<GameObjectVC>(click).SetActive(false);
            }

            foreach (var unique in KeysUnique)
            {
                SelectorUI<GameObjectVC>(unique).SetActive(false);
            }

            if (click_cur != CellClickTypes.None
                && click_cur != CellClickTypes.SimpleClick
                && click_cur != CellClickTypes.SetUnit)
            {
                SelectorUI<GameObjectVC>(click_cur).SetActiveParent(true);

                if (click_cur == CellClickTypes.UniqueAbility)
                {
                    if (KeysUnique.Contains(uniq))
                    {
                        SelectorUI<GameObjectVC>(click_cur).SetActive(true);
                        SelectorUI<GameObjectVC>(uniq).SetActive(true);
                    }
                }
                else
                {
                    SelectorUI<GameObjectVC>(click_cur).SetActive(true);
                }
            }
        }
    }
}
