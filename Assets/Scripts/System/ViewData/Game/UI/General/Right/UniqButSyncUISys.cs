using static Game.Game.EntCellUnit;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct UniqButSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var abil1 = ref Unit<UniqueAbilityC>(UniqueButtonTypes.First, SelIdx<IdxC>().Idx);
            ref var abil2 = ref Unit<UniqueAbilityC>(UniqueButtonTypes.Second, SelIdx<IdxC>().Idx);
            ref var abil3 = ref Unit<UniqueAbilityC>(UniqueButtonTypes.Third, SelIdx<IdxC>().Idx);

            //UniqButtonsUIC.SetActive(UniqueButtonTypes.First, abil1.Ability);
            //UniqButtonsUIC.SetActive(UniqueButtonTypes.Second, abil2.Ability);
            //UniqButtonsUIC.SetActive(UniqueButtonTypes.Third, abil3.Ability);


            //if (ability == default)
            //{
            //    _buttons[uniqBut].gameObject.SetActive(false);
            //}
            //else
            //{
            //    _buttons[uniqBut].gameObject.SetActive(true);

            //    _zones[uniqBut][ability].SetActive(true);
            //    foreach (var item_0 in _zones)
            //    {
            //        if (item_0.Key == uniqBut)
            //        {
            //            foreach (var item_1 in item_0.Value)
            //            {
            //                if (item_1.Key != ability)
            //                {
            //                    _zones[item_0.Key][item_1.Key].SetActive(false);
            //                }
            //            }
            //        }
            //    }
            //}


            if (abil1.Ability != default)
            {
                UIEntRightUnique.Buttons<TextUIC>(UniqueButtonTypes.First).SetActiveParent(Unit<CooldownC>(abil1.Ability, SelIdx<IdxC>().Idx).HaveCooldown);

                UIEntRightUnique.Buttons<TextUIC>(UniqueButtonTypes.First).Text = Unit<CooldownC>(abil1.Ability, SelIdx<IdxC>().Idx).Cooldown.ToString();
            }
            if (abil2.Ability != default)
            {
                UIEntRightUnique.Buttons<TextUIC>(UniqueButtonTypes.Second).SetActiveParent(Unit<CooldownC>(abil2.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UIEntRightUnique.Buttons<TextUIC>(UniqueButtonTypes.Second).Text = Unit<CooldownC>(abil2.Ability, SelIdx<IdxC>().Idx).ToString();
            }
            if (abil3.Ability != default)
            {
                UIEntRightUnique.Buttons<TextUIC>(UniqueButtonTypes.Third).SetActiveParent(Unit<CooldownC>(abil3.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UIEntRightUnique.Buttons<TextUIC>(UniqueButtonTypes.Third).Text = Unit<CooldownC>(abil3.Ability, SelIdx<IdxC>().Idx).Cooldown.ToString();
            }
        }
    }
}