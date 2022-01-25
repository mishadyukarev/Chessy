using Photon.Pun;

namespace Game.Game
{
    struct AttackMS : IEcsRunSystem
    {
        public void Run()
        {
            EntityMPool.Attack.Get(out var idx_from, out var idx_to);

            ref var unit_from = ref CellUnitEntities.Else(idx_from).UnitC;
            ref var ownerUnit_from = ref CellUnitEntities.Else(idx_from).OwnerC;
            ref var hpUnit_from = ref CellUnitEntities.Hp(idx_from).AmountC;
            ref var condUnit_from = ref CellUnitEntities.Else(idx_from).ConditionC;

            ref var tw_from = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_from);


            ref var unit_to = ref CellUnitEntities.Else(idx_to).UnitC;
            ref var hpUnit_to = ref CellUnitEntities.Hp(idx_to).AmountC;

            ref var tw_to = ref CellUnitTWE.UnitTW<ToolWeaponC>(idx_to);



            var playerSender = WhoseMoveE.WhoseMove.Player;


            if (CellsForAttackUnitsEs.CanAttack(idx_from, idx_to, playerSender, out var attack))
            {
                CellUnitEntities.Step(idx_from).AmountC.Reset();
                condUnit_from.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += CellUnitEntities.DamageAttack(idx_from, attack);

                if (unit_from.IsMelee)
                    EntityPool.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else EntityPool.Rpc.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);

                powerDam_to += CellUnitEntities.DamageOnCell(idx_to);


                var dirAttack = CellSpaceSupport.GetDirect(idx_from, idx_to);


                if (SunSidesE.SunSideTC.IsAcitveSun)
                {
                    var isSunnedUnit = true;

                    foreach (var dir in SunSidesE.SunSideTC.RaysSun)
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

                var maxDamage = UnitHpValues.MAX_HP;
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
                        CellUnitTWE.Take(idx_from);
                    }
                    else if (minus_from > 0)
                    {
                        CellUnitEntities.Hp(idx_from).TakeAttack((int)minus_from);
                    }
                }


                if (tw_to.Is(ToolWeaponTypes.Shield))
                {
                    CellUnitTWE.Take(idx_to);
                }
                else if (minus_to > 0)
                {
                    CellUnitEntities.Hp(idx_to).TakeAttack((int)minus_to);
                }



                if (!hpUnit_to.Have)
                {
                    if (CellUnitEntities.Else(idx_to).UnitC.IsAnimal)
                    {
                        InventorResourcesE.Resource(ResourceTypes.Food, ownerUnit_from.Player) += EconomyValues.AMOUNT_FOOD_AFTER_KILL_CAMEL;
                    }

                    CellUnitEntities.Kill(idx_to);


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnit_from.Have)
                        {
                            CellUnitEntities.Kill(idx_from);
                        }
                        else
                        {

                            CellUnitEntities.Shift(idx_from, idx_to, true);
                        }
                    }
                }

                else if (!hpUnit_from.Have)
                {
                    CellUnitEntities.Kill(idx_from);
                }

                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_from).Disable();
                //foreach (var item in CellUnitEffectsEs.Keys) CellUnitEffectsEs.HaveEffect<HaveEffectC>(item, idx_to).Disable();
            }
        }
    }
}