using System;

namespace Game.Game
{
    sealed class UnitAttackUnitS : SystemAbstract, IEcsRunSystem
    {
        internal UnitAttackUnitS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_to = 0; idx_to < Start_Values.ALL_CELLS_AMOUNT; idx_to++)
            {
                var damage = E.UnitMainE(idx_to).AttackDamageC.Damage;

                if(damage > 0)
                {
                    E.UnitHpC(idx_to).Health -= damage;
                    if (E.UnitHpC(idx_to).Health <= CellUnitStatHp_Values.HP_FOR_DEATH_AFTER_ATTACK)
                        E.UnitHpC(idx_to).Health = 0;

                    if (!E.UnitHpC(idx_to).IsAlive)
                    {
                        if (E.UnitTC(idx_to).Is(UnitTypes.Scout) || E.UnitMainE(idx_to).IsHero)
                        {
                            E.UnitInfo(E.UnitPlayerTC(idx_to), E.UnitLevelTC(idx_to), E.UnitTC(idx_to)).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(E.UnitTC(idx_to).Unit);
                            E.UnitInfo(E.UnitPlayerTC(idx_to), E.UnitLevelTC(idx_to), E.UnitTC(idx_to)).HaveInInventor = true;
                        }




                        if (E.UnitTC(idx_to).Is(UnitTypes.King)) E.WinnerC.Player = E.UnitMainE(idx_to).WhoKillerC.Player;
                        E.LastDiedE(idx_to).Set(E.UnitMainE(idx_to));
                        E.UnitInfo(E.UnitMainE(idx_to)).UnitsInGame--;



                        var idx_from = E.UnitMainE(idx_to).FromIdx.Idx;

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
                                E.ResourcesC(E.UnitPlayerTC(idx_from).Player, ResourceTypes.Food).Resources += ResourcesEconomy_Values.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                            }
                        }



                        E.UnitTC(idx_to).Unit = UnitTypes.None;
                    }


                    E.UnitMainE(idx_to).AttackDamageC.Damage = 0;
                }
            }
        }
    }
}