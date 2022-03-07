using System;

namespace Chessy.Game
{
    sealed class UnitAttackUnitS : SystemAbstract, IEcsRunSystem
    {
        internal UnitAttackUnitS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_to = 0; idx_to < Start_VALUES.ALL_CELLS_AMOUNT; idx_to++)
            {
                var damage = E.DamageAttackUnitC(idx_to).Damage;

                if(damage > 0)
                {
                    E.UnitHpC(idx_to).Health -= damage;
                    if (E.UnitHpC(idx_to).Health <= CellUnitStatHp_VALUES.HP_FOR_DEATH_AFTER_ATTACK)
                        E.UnitHpC(idx_to).Health = 0;

                    if (!E.UnitHpC(idx_to).IsAlive)
                    {
                        if (E.UnitTC(idx_to).Is(UnitTypes.King)) E.WinnerC.Player = E.AttackUnitKillerTC(idx_to).Player;
                        E.LastDiedE(idx_to).Set(E.UnitMainE(idx_to));
                        E.UnitInfo(E.UnitMainE(idx_to)).UnitsInGame--;



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
                                E.ResourcesC(E.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += ECONOMY_VALUES.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                            }
                        }



                        E.UnitTC(idx_to).Unit = UnitTypes.None;
                    }


                    E.DamageAttackUnitC(idx_to).Damage = 0;
                }
            }
        }
    }
}