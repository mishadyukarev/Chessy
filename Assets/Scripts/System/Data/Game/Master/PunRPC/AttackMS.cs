using Photon.Pun;

namespace Game.Game
{
    sealed class AttackMS : SystemCellAbstract, IEcsRunSystem
    {
        public AttackMS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            var cellEs = Es.CellEs;
            var unitEs = cellEs.UnitEs;
            var buildEs = cellEs.BuildEs;
            var envEs = cellEs.EnvironmentEs;


            Es.MasterEs.Attack.Get(out var idx_from, out var idx_to);

            ref var unit_from = ref UnitEs.Main(idx_from).UnitC;
            ref var ownerUnit_from = ref UnitEs.Main(idx_from).OwnerC;
            ref var hpUnit_from = ref UnitEs.StatEs.Hp(idx_from).Health;
            ref var condUnit_from = ref UnitEs.Main(idx_from).ConditionC;

            ref var tw_from = ref UnitEs.ToolWeapon(idx_from).ToolWeapon;


            ref var unit_to = ref UnitEs.Main(idx_to).UnitC;
            ref var hpUnit_to = ref UnitEs.StatEs.Hp(idx_to).Health;

            ref var tw_to = ref UnitEs.ToolWeapon(idx_to).ToolWeapon;



            var playerSender = Es.WhoseMove.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, playerSender, out var attack))
            {
                UnitEs.StatEs.Step(idx_from).Steps.Reset();
                condUnit_from.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += unitEs.DamageAttack(idx_from, Es.UnitStatUpgradesEs, attack);

                if (unit_from.IsMelee)
                    Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else Es.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += unitEs.DamageOnCell(idx_to, CellEs, Es.UnitStatUpgradesEs);


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

                if (!unit_to.IsMelee) powerDam_to /= 2;

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


                if (unit_from.IsMelee)
                {
                    if (tw_from.Is(ToolWeaponTypes.Shield))
                    {
                        UnitEs.ToolWeapon(idx_from).BreakShield();
                    }
                    else if (minus_from > 0)
                    {
                        UnitEs.StatEs.Hp(idx_from).TakeAttack((int)minus_from);
                    }
                }


                if (tw_to.Is(ToolWeaponTypes.Shield))
                {
                    UnitEs.ToolWeapon(idx_to).BreakShield();
                }
                else if (minus_to > 0)
                {
                    UnitEs.StatEs.Hp(idx_to).TakeAttack((int)minus_to);
                }



                if (!hpUnit_to.Have)
                {
                    if (UnitEs.Main(idx_to).UnitC.IsAnimal)
                    {
                        Es.InventorResourcesEs.Resource(ResourceTypes.Food, ownerUnit_from.Player).Resources += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }

                    UnitEs.Kill(idx_to, Es);


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnit_from.Have)
                        {
                            UnitEs.Kill(idx_from, Es);
                        }
                        else
                        {
                            UnitEs.Shift(idx_from, idx_to, Es);
                        }
                    }
                }

                else if (!hpUnit_from.Have)
                {
                    UnitEs.Kill(idx_from, Es);
                }

                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Disable();
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Disable();
            }
        }
    }
}