namespace Game.Game
{
    sealed class UnitAttackUnitS : SystemAbstract
    {
        internal UnitAttackUnitS(in EntitiesModel ents) : base(ents)
        {
            ents.UnitAttackE = new UnitAttackUnitE(Attack);
        }

        void Attack(float damage, PlayerTypes whoKiller, byte idx_0)
        {
            E.UnitHpC(idx_0).Health -= damage;
            if (E.UnitHpC(idx_0).Health <= CellUnitStatHp_Values.HP_FOR_DEATH_AFTER_ATTACK)
                E.UnitHpC(idx_0).Health = 0;

            if (!E.UnitHpC(idx_0).IsAlive)
            {
                if (E.UnitTC(idx_0).Is(UnitTypes.Scout) || E.UnitMainE(idx_0).IsHero)
                {
                    E.UnitInfo(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level, E.UnitTC(idx_0).Unit).ScoutHeroCooldownC.Cooldown = ScoutHeroCooldownValues.AfterKill(E.UnitTC(idx_0).Unit);
                    E.UnitInfo(E.UnitPlayerTC(idx_0).Player, E.UnitLevelTC(idx_0).Level, E.UnitTC(idx_0).Unit).HaveInInventor = true;
                }

                if (E.UnitTC(idx_0).Is(UnitTypes.King)) E.WinnerC.Player = whoKiller;


                E.LastDiedE(idx_0).Set(E.UnitTC(idx_0), E.UnitLevelTC(idx_0), E.UnitPlayerTC(idx_0));

                E.UnitTC(idx_0).Unit = UnitTypes.None;
            }
        }
    }
}