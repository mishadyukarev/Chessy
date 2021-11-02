using System;

namespace Scripts.Game
{
    public struct HpUnitC
    {
        public int AmountHp { get; set; }

        public bool HaveHp => AmountHp > 0;
        public bool IsMinusHp => AmountHp < 0;
        public bool IsZeroHp => AmountHp == 0;
        public bool IsHpDeathAfterAttack => AmountHp <= UnitValues.HP_FOR_DEATH_AFTER_ATTACK;


        public void AddHp(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            AmountHp += adding;
        }
        public void TakeHp(int taking = 1)
        {
            if (HaveHp)
            {
                if (taking < 0) throw new Exception("Need a positive number");
                else if (taking == 0) throw new Exception("You're taking zero");
                AmountHp -= taking;

                if (IsMinusHp) AmountHp = 0;
            }
            else throw new Exception("Hp <= 0");
        }

        public int ZeroHp() => AmountHp = 0;
        public bool Is(int amountHp) => AmountHp == amountHp;

        public int MaxHpUnit(UnitTypes unitType, bool haveBonus, float upgPercent)
        {
            if (unitType == default) throw new Exception();

            var maxHp = 0;
            var standHp = UnitValues.StandMaxHpUnit(unitType);

            maxHp = standHp;

            if (haveBonus) maxHp += (int)(standHp * 0.5f);
            maxHp += (int)(standHp * upgPercent);

            return maxHp;
        }
        public bool HaveMaxHpUnit(UnitTypes unitType, bool haveBonus, float upgPercent) => AmountHp >= MaxHpUnit(unitType, haveBonus,  upgPercent);
        public void AddHealHp(UnitTypes unitType, bool haveBonus, float upgPercent) => AmountHp += (int)(MaxHpUnit(unitType, haveBonus,  upgPercent) * UnitValues.PercentForAddingHp(unitType));
        public void SetMaxHp(UnitTypes unitType, bool haveBonus, float upgPercent) => AmountHp = MaxHpUnit(unitType, haveBonus,  upgPercent);

        public bool TryAddBonusHp(UnitTypes unitType, bool haveBonus, float upgPercent)
        {
            if (haveBonus)
            {
                if (!HaveMaxHpUnit(unitType, haveBonus, 0))
                {
                    AmountHp += MaxHpUnit(unitType, haveBonus, 0) - MaxHpUnit(unitType, false, 0);
                    if (AmountHp > MaxHpUnit(unitType, haveBonus, 0)) AmountHp = MaxHpUnit(unitType, false, 0);
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public bool TryTakeBonusHp(UnitTypes unitType, bool haveBonus)
        {
            if (haveBonus)
            {
                if (HaveHp)
                {
                    AmountHp -= MaxHpUnit(unitType, haveBonus, 0) - MaxHpUnit(unitType, false, 0);
                    if (AmountHp <= 0) AmountHp = 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public void TakeHpThirsty(UnitTypes unitType, bool haveBonus, float upgPercent)
        {
            float percent = 0;
            switch (unitType)
            {
                case UnitTypes.None: throw new Exception();
                case UnitTypes.King: percent = 0.3f; break;
                case UnitTypes.Pawn: percent = 0.5f; break;
                case UnitTypes.Rook: percent = 0.5f; break;
                case UnitTypes.Bishop: percent = 0.5f; break;
                case UnitTypes.Scout: percent = 1; break;
                default: throw new Exception();
            }

            TakeHp((int)(MaxHpUnit(unitType, haveBonus, upgPercent) * percent));
        }
    }
}