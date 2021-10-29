using System;

namespace Scripts.Game
{
    public struct HpUnitC
    {
        public int AmountHp { get; set; }

        public bool HaveHp => AmountHp > 0;
        


        public void AddHp(int adding = 1)
        {
            if (adding < 0) throw new Exception("Need a positive number");
            else if (adding == 0) throw new Exception("You're adding zero");
            AmountHp += adding;
        }
        public void TakeHp(int taking = 1)
        {
            if (AmountHp > 0)
            {
                if (taking < 0) throw new Exception("Need a positive number");
                else if (taking == 0) throw new Exception("You're taking zero");
                AmountHp -= taking;

                if (AmountHp < 0) AmountHp = 0;
            }
            else throw new Exception("Hp <= 0");
        }

        public int DefHp() => AmountHp = 0;
        public bool Is(int amountHp) => AmountHp == amountHp;
        public int StandMaxHpUnit(UnitTypes unitType) => UnitValues.StandMaxHpUnit(unitType);
        //public bool HaveStandMaxHp(UnitTypes unitType) => AmountHp >= StandMaxHpUnit(unitType);
        public void AddStandartHp(UnitEffectsC unitEffC, UnitTypes unitType)
        {
            if (unitEffC.Have(StatTypes.Health))
            {
                AmountHp += (int)(CurMaxHpUnit(unitEffC, unitType) * UnitValues.PercentForAddingHp(unitType));
            }
            else AddHp((int)(StandMaxHpUnit(unitType) * UnitValues.PercentForAddingHp(unitType)));
        }
        public void SetStandMaxHp(UnitTypes unitType) => AmountHp = StandMaxHpUnit(unitType);

        public void AddBonusHp(UnitTypes unitType) => AmountHp += (int)(StandMaxHpUnit(unitType) * UnitValues.PercentForBonusHp(unitType));
        public int CurMaxHpUnit(UnitEffectsC effectsC, UnitTypes unitType)
        {
            if (effectsC.Have(StatTypes.Health))
            {
                var v = StandMaxHpUnit(unitType) * UnitValues.PercentForBonusHp(unitType);
                return (int)(StandMaxHpUnit(unitType) + v);
            }
            else return StandMaxHpUnit(unitType);
        }
        public bool HaveCurMaxHpUnit(UnitEffectsC effectsC, UnitTypes unitType) => AmountHp >= CurMaxHpUnit(effectsC, unitType); 
    }
}