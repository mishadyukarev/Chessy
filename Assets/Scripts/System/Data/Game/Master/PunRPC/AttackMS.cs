using Photon.Pun;

namespace Game.Game
{
    sealed class AttackMS : SystemAbstract, IEcsRunSystem
    {
        public AttackMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            Es.MasterEs.Attack.Get(out var idx_from, out var idx_to);

            var whoseMove = Es.WhoseMove.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, whoseMove, out var attack))
            {
                UnitEs.StatEs.Step(idx_from).Steps.Amount = 0;
                UnitEs.Main(idx_from).ResetCondition();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += UnitEs.Main(idx_from).DamageAttack(CellEs, Es.UnitStatUpgradesEs, attack);

                if (UnitEs.Main(idx_from).UnitTC.IsMelee)
                    Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += UnitEs.Main(idx_to).DamageOnCell(CellEs, Es.UnitStatUpgradesEs);


                var dirAttack = CellEs.GetDirect(idx_from, idx_to);


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

                var maxDamage = CellUnitHpValues.MAX_HP;
                var minDamage = 0;

                if (!UnitEs.Main(idx_to).UnitTC.IsMelee) powerDam_to /= 2;

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


                if (UnitEs.Main(idx_from).UnitTC.IsMelee)
                {
                    if (UnitEs.ToolWeapon(idx_from).ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                    {
                        UnitEs.ToolWeapon(idx_from).BreakShield();
                    }
                    else if (minus_from > 0)
                    {
                        UnitEs.StatEs.Hp(idx_from).Attack((int)minus_from);
                    }
                }


                if (UnitEs.ToolWeapon(idx_to).ToolWeaponTC.Is(ToolWeaponTypes.Shield))
                {
                    UnitEs.ToolWeapon(idx_to).BreakShield();
                }
                else if (minus_to > 0)
                {
                    UnitEs.StatEs.Hp(idx_to).Attack((int)minus_to);
                }



                if (!UnitEs.StatEs.Hp(idx_to).IsAlive)
                {
                    if (UnitEs.Main(idx_to).UnitTC.IsAnimal)
                    {
                        Es.InventorResourcesEs.Resource(ResourceTypes.Food, UnitEs.Main(idx_from).OwnerC.Player).Resources.Amount += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }

                    UnitEs.Main(idx_to).Kill(Es);


                    if (UnitEs.Main(idx_from).UnitTC.IsMelee)
                    {
                        if (!UnitEs.StatEs.Hp(idx_from).IsAlive)
                        {
                            UnitEs.Main(idx_from).Kill(Es);
                        }
                        else
                        {
                            UnitEs.Shift(idx_from, idx_to, Es);
                        }
                    }
                }

                else if (!UnitEs.StatEs.Hp(idx_from).IsAlive)
                {
                    UnitEs.Main(idx_from).Kill(Es);
                }

                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Disable();
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Disable();
            }
        }
    }
}