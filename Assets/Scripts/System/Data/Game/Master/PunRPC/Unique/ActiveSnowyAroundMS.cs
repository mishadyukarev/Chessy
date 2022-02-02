namespace Game.Game
{
    sealed class ActiveSnowyAroundMS : SystemAbstract, IEcsRunSystem
    {
        internal ActiveSnowyAroundMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var whoseMove = Es.WhoseMove.WhoseMove.Player;
            var ability = Es.MasterEs.AbilityC.Ability;
            var idx_0 = Es.MasterEs.ActiveSnowyAroundME.Where.Idx;

            if (UnitStatEs(idx_0).WaterE.Have(ability) || RiverEs(idx_0).River.HaveRiver)
            {
                if(!RiverEs(idx_0).River.HaveRiver) UnitStatEs(idx_0).WaterE.Take(ability);

                if (UnitStatEs(idx_0).StepE.Have(ability))
                {
                    UnitStatEs(idx_0).StepE.Take(ability);
                    UnitEs(idx_0).CooldownAbility(ability).SetAfterAbility();

                    foreach (var idx_1 in CellWorker.GetIdxsAround(idx_0))
                    {
                        if (UnitEs(idx_1).MainE.HaveUnit(UnitStatEs(idx_1)))
                        {
                            if (UnitEs(idx_1).MainE.OwnerC.Is(whoseMove))
                            {
                                if (UnitEs(idx_1).MainE.UnitTC.IsMelee && !UnitEs(idx_1).MainE.UnitTC.Is(UnitTypes.Camel, UnitTypes.Scout))
                                {
                                    UnitStatEs(idx_1).WaterE.SetMax(UnitEs(idx_1).MainE, Es.UnitStatUpgradesEs);
                                    UnitStatEs(idx_1).Hp.SetMax();
                                    UnitEffectEs(idx_1).ShieldE.Set(ability);
                                }
                                if (UnitEs(idx_1).MainE.UnitTC.Is(UnitTypes.Archer))
                                {
                                    UnitEffectEs(idx_1).FrozenArrowE.Enable();
                                }
                            }
                            else
                            {
                                UnitEffectEs(idx_1).StunE.Set(ability);
                            }
                        }

                        EffectEs(idx_1).FireE.Disable();
                    }
                }
            }
        }
    }
}