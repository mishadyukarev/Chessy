﻿using Photon.Pun;
using static Game.Game.EntityCellPool;
using static Game.Game.EntCellUnit;

namespace Game.Game
{
    struct AttackMS : IEcsRunSystem
    {
        public void Run()
        {
            FromToDoingMC.Get(out var idx_from, out var idx_to);

            ref var unit_from = ref Unit<UnitC>(idx_from);
            ref var hpUnitCell_from = ref Unit<UnitCellEC>(idx_from);
            ref var hpUnit_from = ref Unit<HpC>(idx_from);
            ref var stepUnit_from = ref Unit<UnitCellEC>(idx_from);
            ref var condUnit_from = ref Unit<ConditionC>(idx_from);

            ref var tw_from = ref UnitTW<ToolWeaponC>(idx_from);


            ref var unitE_to = ref Unit<UnitCellEC>(idx_to);
            ref var unit_to = ref Unit<UnitC>(idx_to);
            ref var hpUnitCell_to = ref Unit<UnitCellEC>(idx_to);
            ref var hpUnit_to = ref Unit<HpC>(idx_to);

            ref var tw_to = ref UnitTW<ToolWeaponC>(idx_to);



            var playerSender = InfoC.Sender(MGOTypes.Master).GetPlayer();


            if (Unit<UnitCellEC>(idx_from).CanAttack(playerSender, idx_to, out var attack))
            {
                stepUnit_from.Reset();
                condUnit_from.Reset();


                float powerDam_from = 0;
                float powerDam_to = 0;


                powerDam_from += Unit<UnitCellEC>(idx_from).DamageAttack(attack);

                if (unit_from.IsMelee)
                    EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.AttackMelee);
                else EntityPool.Rpc<RpcC>().SoundToGeneral(RpcTarget.All, ClipTypes.AttackArcher);



                powerDam_to += Unit<UnitCellEC>(idx_to).DamageOnCell;


                float min_limit = 0;
                float max_limit = 0;
                float minus_to = 0;
                float minus_from = 0;

                var maxDamage = UnitCellEC.MAX_HP;
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
                        UnitTW<ShieldEC>(idx_from).Take();
                    }
                    else if (minus_from > 0)
                    {
                        hpUnitCell_from.TakeAttack((int)minus_from);
                    }
                }


                if (tw_to.Is(TWTypes.Shield))
                {
                    UnitTW<ShieldEC>(idx_to).Take();
                }
                else if (minus_to > 0)
                {
                    hpUnitCell_to.TakeAttack((int)minus_to);
                }



                if (!hpUnit_to.Have)
                {
                    unitE_to.Kill();


                    if (unit_from.IsMelee)
                    {
                        if (!hpUnit_from.Have)
                        {
                            Unit<UnitCellEC>(idx_from).Kill();
                        }
                        else
                        {

                            Unit<UnitCellEC>(idx_from).Shift(idx_to);
                        }
                    }
                }

                else if (!hpUnit_from.Have)
                {
                    Unit<UnitCellEC>(idx_from).Kill();
                }

                foreach (var item in Stats) Unit<HaveEffectC>(item, idx_from).Disable();
                foreach (var item in Stats) Unit<HaveEffectC>(item, idx_to).Disable();
            }
        }
    }
}