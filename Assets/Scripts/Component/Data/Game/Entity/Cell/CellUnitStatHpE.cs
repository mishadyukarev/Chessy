﻿using ECS;

namespace Game.Game
{
    public sealed class CellUnitStatHpE : CellEntityAbstract
    {
        ref AmountC HealthRef => ref Ent.Get<AmountC>();
        public AmountC HealthC => Ent.Get<AmountC>();

        public int Health
        {
            get => HealthRef.Amount;
            internal set => HealthRef.Amount = value;
        }

        public const int MAX_HP = CellUnitStatHpValues.MAX_HP;

        public bool IsHpDeathAfterAttack => HealthC.Amount <= CellUnitMainDamageValues.HP_FOR_DEATH_AFTER_ATTACK;
        public bool HaveMax => HealthC.Amount >= MAX_HP;
        public bool IsAlive => HealthC.Amount > 0;


        internal CellUnitStatHpE(in byte idx, in EcsWorld gameW) : base(idx, gameW) { }

        void Add(in int adding = 1)
        {
            HealthRef.Amount += adding;
            if (HealthC.Amount > MAX_HP) HealthRef.Amount = MAX_HP;
        }
        void Take(in Entities ents, in int taking = 1)
        {
            HealthRef.Amount -= taking;
            if (IsHpDeathAfterAttack) HealthRef.Amount = 0;
            if (!IsAlive) ents.UnitEs(Idx).TypeE.Kill(ents);
        }
        internal void Set(in int amountHelth) => HealthRef.Amount = amountHelth;

        internal void Shift(in CellUnitStatHpE hpE_from)
        {
            HealthRef = hpE_from.HealthC;
            hpE_from.HealthRef.Amount = 0;
        }

        public void Attack(in AbilityTypes ability, in Entities Es)
        {
            HealthRef.Amount -= CellUnitStatHpValues.Damage(ability);

            if (IsHpDeathAfterAttack || !IsAlive)
            {
                Es.CellEs(Idx).UnitEs.TypeE.Kill(Es);
            }
        }
        public void Attack(in int damage)
        {
            HealthRef.Amount -= damage;
            if (IsHpDeathAfterAttack) HealthRef.Amount = 0;
        }
        public void TakeHpHellWithNearWater(in Entities ents)
        {
            Take(ents, 15);
        }
        public void TakeHpHellWithCloud(in Entities ents)
        {
            Take(ents, 15);
        }
        public void TakeHpHellWithIceWall(in Entities ents)
        {
            Take(ents, 15);
        }

        public void Thirsty(in Entities es)
        {
            float percent = CellUnitStatHpValues.ThirstyPercent(es.CellEs(Idx).UnitEs.TypeE.UnitTC.Unit);

            HealthRef.Amount -= (int)(CellUnitStatWaterE.MAX_WATER_WITHOUT_EFFECTS * percent);

            if (!es.CellEs(Idx).UnitEs.StatEs.Hp.IsAlive)
            {
                if (es.CellEs(Idx).BuildEs.BuildingE.BuildTC.Is(BuildingTypes.Camp))
                {
                    es.CellEs(Idx).BuildEs.BuildingE.Destroy(es);
                }

                es.CellEs(Idx).UnitEs.TypeE.Kill(es);
            }
        }

        public void Fire(in Entities es)
        {
            if (es.UnitEs(Idx).TypeE.UnitTC.Is(UnitTypes.Hell))
            {
                SetMax();
            }
            else
            {
                HealthRef.Amount -= CellUnitStatHpValues.FIRE_DAMAGE;
                if (!IsAlive) es.CellEs(Idx).UnitEs.TypeE.Kill(es);
            }
        }

        public void SetMax()
        {
            HealthRef.Amount = CellUnitStatHpValues.MAX_HP;
        }
    }
}