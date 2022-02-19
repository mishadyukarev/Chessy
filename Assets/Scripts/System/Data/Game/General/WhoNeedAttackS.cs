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
                if (Es.UnitEs(idx_0).NeelKillE.NeedKill)
                {
                    if (Es.UnitTC(idx_0).Is(UnitTypes.Scout) || Es.UnitEs(idx_0).IsHero)
                    {
                        Es.UnitInfo(Es.UnitPlayerTC(idx_0).Player, Es.UnitTC(idx_0).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(Es.UnitTC(idx_0).Unit);
                        Es.UnitInfo(Es.UnitPlayerTC(idx_0).Player, Es.UnitTC(idx_0).Unit).HaveInInventor = true;
                    }

                    if (Es.UnitTC(idx_0).Is(UnitTypes.King)) Es.WinnerC.Player = Es.UnitEs(idx_0).NeelKillE.WhoKiller.Player;

                    Es.UnitTC(idx_0).Unit = UnitTypes.None;
                    Es.UnitEs(idx_0).NeelKillE.NeedKill = false;
                }
            }
        }
    }
}