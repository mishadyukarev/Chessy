namespace Game.Game
{
    sealed class DirectWaveSnowyMS : SystemAbstract, IEcsRunSystem
    {
        internal DirectWaveSnowyMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var sender = InfoC.Sender(MGOTypes.Master);
            var whoseMove = Es.WhoseMove.WhoseMove.Player;
            var ability = Es.MasterEs.AbilityC.Ability;

            Es.MasterEs.DirectWaveSnowyME.ForDirectWave.Get(out var idx_from, out var idx_to);

            if (CellWorker.TryGetDirect(idx_from, idx_to, out var direct_0))
            {
                if (UnitStatEs(idx_from).WaterE.Have(ability) || RiverEs(idx_from).River.HaveRiverNear)
                {
                    if (!RiverEs(idx_from).River.HaveRiverNear) UnitStatEs(idx_from).WaterE.Take(ability);

                    if (UnitStatEs(idx_from).StepE.Have(ability))
                    {
                        UnitStatEs(idx_from).StepE.Take(ability);
                        UnitEs(idx_from).CooldownAbility(ability).SetAfterAbility();

                        var idx_0 = idx_to;

                        for (var i = 0; i < 3; i++)
                        {
                            if (!CellEs(idx_0).ParentE.IsActiveSelf.IsActive) break;

                            if (UnitEs(idx_0).MainE.HaveUnit(UnitStatEs(idx_0)))
                            {
                                if (UnitEs(idx_0).MainE.OwnerC.Is(whoseMove))
                                {
                                    //UnitEffectEs(idx_0).ShieldE.Set(ability);
                                    //UnitStatEs(idx_0).Hp.SetMax();
                                    //UnitStatEs(idx_0).Water.SetMax(UnitEs(idx_0).MainE, Es.UnitStatUpgradesEs);
                                }
                                else
                                {
                                    UnitEffectEs(idx_0).StunE.Set(ability);
                                }
                            }

                            EffectEs(idx_0).FireE.Disable();

                            idx_0 = CellWorker.GetIdxCellByDirect(idx_0, direct_0);
                        }
                    }
                    else
                    {
                        Es.Rpc.SimpleMistakeToGeneral(MistakeTypes.NeedMoreSteps, sender);
                    }
                }
            }
        }
    }
}