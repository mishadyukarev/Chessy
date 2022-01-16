using static Game.Game.CellUnitEs;
using static Game.Game.EntityPool;

namespace Game.Game
{
    struct UniqButSyncUISys : IEcsRunSystem
    {
        public void Run()
        {
            ref var abil1 = ref UnitUniqueButton<UniqueAbilityC>(ButtonTypes.First, SelIdx<IdxC>().Idx);
            ref var abil2 = ref UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Second, SelIdx<IdxC>().Idx);
            ref var abil3 = ref UnitUniqueButton<UniqueAbilityC>(ButtonTypes.Third, SelIdx<IdxC>().Idx);

            //UniqButtonsUIC.SetActive(ButtonTypes.First, abil1.Ability);
            //UniqButtonsUIC.SetActive(ButtonTypes.Second, abil2.Ability);
            //UniqButtonsUIC.SetActive(ButtonTypes.Third, abil3.Ability);


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
                UIEntRightUnique.Buttons<TextMPUGUIC>(ButtonTypes.First).SetActiveParent(Unit<CooldownC>(abil1.Ability, SelIdx<IdxC>().Idx).HaveCooldown);

                UIEntRightUnique.Buttons<TextMPUGUIC>(ButtonTypes.First).Text = Unit<CooldownC>(abil1.Ability, SelIdx<IdxC>().Idx).Cooldown.ToString();
            }
            if (abil2.Ability != default)
            {
                UIEntRightUnique.Buttons<TextMPUGUIC>(ButtonTypes.Second).SetActiveParent(Unit<CooldownC>(abil2.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UIEntRightUnique.Buttons<TextMPUGUIC>(ButtonTypes.Second).Text = Unit<CooldownC>(abil2.Ability, SelIdx<IdxC>().Idx).ToString();
            }
            if (abil3.Ability != default)
            {
                UIEntRightUnique.Buttons<TextMPUGUIC>(ButtonTypes.Third).SetActiveParent(Unit<CooldownC>(abil3.Ability, SelIdx<IdxC>().Idx).HaveCooldown);
                UIEntRightUnique.Buttons<TextMPUGUIC>(ButtonTypes.Third).Text = Unit<CooldownC>(abil3.Ability, SelIdx<IdxC>().Idx).Cooldown.ToString();
            }
        }
    }
}