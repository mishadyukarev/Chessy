using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    public struct UnitAttackUnitS
    {
        public UnitAttackUnitS(in float damage, in PlayerTypes whoKiller, in byte idx_to, in EntitiesModel e, in byte idx_from = 0)
        {
            if (damage <= 0) throw new Exception();

            e.UnitHpC(idx_to).Health -= damage;
            if (e.UnitHpC(idx_to).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                e.UnitHpC(idx_to).Health = 0;

            if (!e.UnitHpC(idx_to).IsAlive)
            {
                if (e.UnitTC(idx_to).Is(UnitTypes.King)) e.WinnerC.Player = whoKiller;
                else if (e.IsHero(e.UnitTC(idx_to).Unit))
                {
                    var cooldown = 0f;

                    switch (e.UnitTC(idx_to).Unit)
                    {
                        case UnitTypes.Elfemale:
                            cooldown = HeroCooldown_VALUES.Elfemale;
                            break;

                        case UnitTypes.Snowy:
                            cooldown = HeroCooldown_VALUES.Snowy;
                            break;

                        case UnitTypes.Undead:
                            cooldown = HeroCooldown_VALUES.Undead;
                            break;

                        case UnitTypes.Hell:
                            cooldown = HeroCooldown_VALUES.Hell;
                            break;

                        default: throw new Exception();
                    }

                    e.PlayerInfoE(e.UnitPlayerTC(idx_to).Player).HeroCooldownC.Cooldown = cooldown;
                    e.PlayerInfoE(e.UnitPlayerTC(idx_to).Player).HaveHeroInInventor = true;
                }


                e.LastDiedE(idx_to).Set(e.UnitMainE(idx_to));
                e.UnitInfo(e.UnitMainE(idx_to)).Take(e.UnitTC(idx_to).Unit, 1);

                if (idx_from != 0)
                {
                    if (e.UnitTC(idx_from).HaveUnit)
                    {
                        if (e.IsMelee(idx_from))
                        {
                            new UnitShiftS(idx_from, idx_to, e);
                        }
                    }

                    if (e.UnitTC(idx_to).Is(UnitTypes.Camel))
                    {
                        e.ResourcesC(e.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }
                }



                e.UnitTC(idx_to).Unit = UnitTypes.None;
            }
        }
    }
}