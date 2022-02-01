using Photon.Pun;

namespace Game.Game
{
    sealed class AttackMS : SystemAbstract, IEcsRunSystem
    {
        internal AttackMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            Es.MasterEs.Attack.Get(out var idx_from, out var idx_to);

            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                UnitStatEs(idx_from).StepE.SetStepsAfterAttack();
                UnitEs(idx_from).MainE.ResetCondition();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += UnitEs(idx_from).MainE.DamageAttack(CellEs(idx_from), Es.UnitStatUpgradesEs, attack);

                if (UnitEs(idx_from).MainE.UnitTC.IsMelee)
                    Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += UnitEs(idx_to).MainE.DamageOnCell(CellEs(idx_to), Es.UnitStatUpgradesEs);


                var dirAttack = CellEsWorker.GetDirect(idx_from, idx_to);


                if (Es.SunSidesE.SunSideTC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in Es.SunSidesE.SunSideTC.RaysSun)
                    {
                        if (dirAttack == dir) isSunnedUnit = false;
                    }

                    if (isSunnedUnit)
                    {
                        powerDam_from *= 0.9f;
                    }
                }






                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = CellUnitStatHpE.MAX_HP;
                var minDamage = 0;

                if (!UnitEs(idx_to).MainE.UnitTC.IsMelee) powerDam_to /= 2;

                if (powerDam_to > powerDam_from)
                {
                    max_limit = powerDam_to * 2;
                    min_limit = powerDam_to / 3;

                    if (min_limit >= powerDam_from)
                    {
                        minus_from = maxDamage;
                        powerDam_to = minDamage;
                    }
                    else
                    {
                        minus_to = maxDamage * powerDam_from / max_limit;

                        max_limit = powerDam_from * 2;
                        minus_from = maxDamage * powerDam_to / max_limit;
                    }
                }
                else
                {
                    max_limit = powerDam_from * 2;
                    min_limit = powerDam_from / 3;

                    if (min_limit >= powerDam_to)
                    {
                        minus_to = maxDamage;
                        minus_from = minDamage;
                    }
                    else
                    {
                        minus_from = maxDamage * powerDam_to / max_limit;

                        max_limit = powerDam_to * 2f;
                        minus_to = maxDamage * powerDam_from / max_limit;
                    }
                }


                if (UnitEs(idx_from).MainE.UnitTC.IsMelee)
                {
                    if (UnitEs(idx_from).ToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                    {
                        UnitEs(idx_from).ToolWeaponE.BreakShield();
                    }
                    else if (UnitEffectEs(idx_from).ShieldE.HaveShieldEffect)
                    {
                        UnitEffectEs(idx_from).ShieldE.Take();
                    }
                    else if (minus_from > 0)
                    {
                        UnitStatEs(idx_from).Hp.Attack((int)minus_from);
                    }
                }


                if (UnitEs(idx_to).ToolWeaponE.ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                {
                    UnitEs(idx_to).ToolWeaponE.BreakShield();
                }
                else if (minus_to > 0)
                {
                    UnitStatEs(idx_to).Hp.Attack((int)minus_to);
                }



                if (!UnitStatEs(idx_to).Hp.IsAlive)
                {
                    if (UnitEs(idx_to).MainE.UnitTC.IsAnimal)
                    {
                        Es.InventorResourcesEs.Resource(ResourceTypes.Food, UnitEs(idx_from).MainE.OwnerC.Player).Resources.Amount += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }

                    UnitEs(idx_to).MainE.Kill(Es);


                    if (UnitEs(idx_from).MainE.UnitTC.IsMelee)
                    {
                        if (!UnitStatEs(idx_from).Hp.IsAlive)
                        {
                            UnitEs(idx_from).MainE.Kill(Es);
                        }
                        else
                        {
                            UnitEs(idx_from).MainE.Shift(idx_to, Es);
                        }
                    }
                }

                else if (!UnitStatEs(idx_from).Hp.IsAlive)
                {
                    UnitEs(idx_from).MainE.Kill(Es);
                }

                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Disable();
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Disable();
            }
        }
    }
}