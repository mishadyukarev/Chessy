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
        public int MaxHpUnit(UnitEffectsC unitEffectsC, UnitTypes unitType) => UnitValues.StandMaxHpUnit(unitEffectsC.Have(StatTypes.Health), unitType);
        public int MaxHpUnit(bool haveBonus, UnitTypes unitType) => UnitValues.StandMaxHpUnit(haveBonus, unitType);
        public bool HaveMaxHpUnit(UnitEffectsC effectsC, UnitTypes unitType) => AmountHp >= MaxHpUnit(effectsC, unitType);
        public bool HaveMaxHpUnit(bool haveBonus, UnitTypes unitType) => AmountHp >= MaxHpUnit(haveBonus, unitType);
        public void AddHealHp(UnitEffectsC unitEffC, UnitTypes unitType) => AmountHp += (int)(MaxHpUnit(unitEffC, unitType) * UnitValues.PercentForAddingHp(unitType));
        public void SetStandMaxHp(UnitEffectsC unitEffC, UnitTypes unitType) => AmountHp = MaxHpUnit(unitEffC, unitType);

        public bool TryAddBonusHp(UnitEffectsC unitEffC, UnitTypes unitType)
        {
            if (unitEffC.Have(StatTypes.Health))
            {
                if (!HaveMaxHpUnit(true, unitType))
                {
                    AmountHp += MaxHpUnit(true, unitType) - MaxHpUnit(false, unitType);
                    if (AmountHp > MaxHpUnit(true, unitType)) AmountHp = MaxHpUnit(true, unitType);
                    return true;
                }
                else return false;
            }
            else return false;
        }
        public bool TryTakeBonusHp(UnitEffectsC unitEffC, UnitTypes unitType)
        {
            if (unitEffC.Have(StatTypes.Health))
            {
                if (HaveHp)
                {
                    AmountHp -= MaxHpUnit(true, unitType) - MaxHpUnit(false, unitType);
                    if (AmountHp <= 0) AmountHp = 1;
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public void TakeHpThirsty(UnitEffectsC unitEffC, UnitTypes unitType)
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

            TakeHp((int)(MaxHpUnit(unitEffC, unitType) * percent));
        }
    }
}