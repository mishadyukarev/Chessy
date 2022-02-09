using static Game.Game.CenterSelectorUIE;

namespace Game.Game
{
    sealed class SelectorUIS : SystemViewAbstract, IEcsRunSystem
    {
        public SelectorUIS(in Entities ents, in EntitiesView entsView) : base(ents, entsView)
        {
        }

        public void Run()
        {
            var click_cur = Es.ClickerObjectE.CellClickCRef.Click;
            var uniq = Es.SelectedAbilityE.AbilityTC.Ability;


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
