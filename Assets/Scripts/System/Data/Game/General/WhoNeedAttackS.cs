namespace Game.Game
{
    sealed class WhoNeedAttackS : SystemAbstract, IEcsRunSystem
    {
        internal WhoNeedAttackS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                if (E.UnitEs(idx_0).NeelKillE.DamageC.Damage > 0)
                {
                    if (E.UnitTC(idx_0).Is(UnitTypes.Scout) || E.UnitMainE(idx_0).IsHero)
                    {
                        E.UnitInfo(E.UnitPlayerTC(idx_0).Player, E.UnitTC(idx_0).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(E.UnitTC(idx_0).Unit);
                        E.UnitInfo(E.UnitPlayerTC(idx_0).Player, E.UnitTC(idx_0).Unit).HaveInInventor = true;
                    }

                    if (E.UnitTC(idx_0).Is(UnitTypes.King)) E.WinnerC.Player = E.UnitEs(idx_0).NeelKillE.WhoKiller.Player;

                    E.UnitTC(idx_0).Unit = UnitTypes.None;
                    E.UnitEs(idx_0).NeelKillE.DamageC.Damage = 0;
                }
            }
        }
    }
}