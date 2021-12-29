using Leopotam.Ecs;
using Photon.Pun;
using static Game.Game.EntityCellPool;

namespace Game.Game
{
    public sealed class AttackMS : IEcsRunSystem
    {
        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            ref var unit_from = ref Unit<UnitC>(idx_from);
            ref var ownUnit_from = ref Unit<OwnerC>(idx_from);
            ref var hpUnitCell_from = ref Unit<HpUnitWC>(idx_from);
            ref var hpUnit_from = ref Unit<HpC>(idx_from);
            ref var stepUnit_from = ref Unit<StepUnitWC>(idx_from);
            ref var condUnit_from = ref Unit<ConditionC>(idx_from);
            ref var effUnit_from = ref Unit<EffectsC>(idx_from);

            ref var tw_from = ref UnitTW<ToolWeaponC>(idx_from);


            ref var unit_to = ref Unit<UnitC>(idx_to);
            ref var hpUnitCell_to = ref Unit<HpUnitWC>(idx_to);
            ref var hpUnit_to = ref Unit<HpC>(idx_to);
            ref var effUnit_to = ref Unit<EffectsC>(idx_to);

            ref var tw_to = ref UnitTW<ToolWeaponC>(idx_to);





            var simpUniqueType = AttackCellsC.WhichAttack(ownUnit_from.Owner, idx_from, idx_to);

            if (simpUniqueType != default)
            {
                stepUnit_from.Reset();
                condUnit_from.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += Unit<DamageUnitC>(idx_from).DamageAttack(simpUniqueType);

                if (unit_from.IsMelee)
                    RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else RpcSys.SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);



                powerDam_to += Unit<DamageUnitC>(idx_to).DamageOnCell;


                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = HpUnitWC.MAX;
                var minDamage = HpC.MIN;

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
                    if (tw_from.Is(TWTypes.Shield))
                    {
                        UnitTW<ShieldC>(idx_from).Take();
                    }
                    else if (minus_from > 0)
                    {
                        hpUnitCell_from.TakeAttack((int)minus_from);
                    }
                }


                if (tw_to.Is(TWTypes.Shield))
                {
                    UnitTW<ShieldC>(idx_to).Take();
                }
                else if (minus_to > 0)
                {
                    hpUnitCell_to.TakeAttack((int)minus_to);
                }



                if (!hpUnit_to.Have)
                {
                    Unit<UnitCellWC>(idx_to).Kill();


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnit_from.Have)
                        {
                            Unit<UnitCellWC>(idx_from).Kill();
                        }
                        else
                        {
                            
                            Unit<UnitCellWC>(idx_to).Shift(idx_to);
                        }
                    }
                }

                else if (!hpUnit_from.Have)
                {
                    Unit<UnitCellWC>(idx_from).Kill();
                }

                effUnit_from.DefAllEffects();
                effUnit_to.DefAllEffects();
            }
        }
    }
}