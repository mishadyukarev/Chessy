using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.System.Model
{
    sealed class UnitAttackUnitS : CellSystem, IEcsRunSystem
    {
        internal UnitAttackUnitS(in byte idx, in EntitiesModel eM) : base(idx, eM)
        {
        }

        public void Run()
        {
            var idx_to = Idx;

            var damage = E.DamageAttackUnitC(idx_to).Damage;

            if (damage > 0)
            {
                E.UnitHpC(idx_to).Health -= damage;
                if (E.UnitHpC(idx_to).Health <= HpValues.HP_FOR_DEATH_AFTER_ATTACK)
                    E.UnitHpC(idx_to).Health = 0;

                if (!E.UnitHpC(idx_to).IsAlive)
                {
                    if (E.UnitTC(idx_to).Is(UnitTypes.King)) E.WinnerC.Player = E.AttackUnitKillerTC(idx_to).Player;
                    else if (E.IsHero(E.UnitTC(idx_to).Unit))
                    {
                        var cooldown = 0f;

                        switch (E.UnitTC(idx_to).Unit)
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

                        E.PlayerInfoE(E.UnitPlayerTC(idx_to).Player).HeroCooldownC.Cooldown = cooldown;
                        E.PlayerInfoE(E.UnitPlayerTC(idx_to).Player).HaveHeroInInventor = true;
                    }


                    E.LastDiedE(idx_to).Set(E.UnitMainE(idx_to));
                    E.UnitInfo(E.UnitMainE(idx_to)).Take(E.UnitTC(idx_to).Unit, 1);



                    var idx_from = E.AttackUnitFromIdxC(idx_to).Idx;

                    if (idx_from != 0)
                    {
                        if (E.UnitTC(idx_from).HaveUnit)
                        {
                            if (E.UnitMainE(idx_from).IsMelee)
                            {
                                E.UnitMainE(idx_from).ShiftTo.Idx = idx_to;
                            }
                        }

                        if (E.UnitTC(idx_to).Is(UnitTypes.Camel))
                        {
                            E.ResourcesC(E.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                        }
                    }



                    E.UnitTC(idx_to).Unit = UnitTypes.None;
                }


                E.DamageAttackUnitC(idx_to).Damage = 0;
            }
        }
    }
}