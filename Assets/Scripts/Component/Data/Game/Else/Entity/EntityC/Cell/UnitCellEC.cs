using System;
using static Game.Game.CellEs;
using static Game.Game.CellUnitEs;
using static Game.Game.CellTrailEs;
using static Game.Game.CellBuildE;
using static Game.Game.CellEnvironmentEs;
using static Game.Game.CellFireEs;
using static Game.Game.EntityCellRiverPool;

namespace Game.Game
{
    public struct UnitCellEC : IUnitCellE
    {
        readonly byte _idx;
        readonly DamageUnitValues _damageValues;
        readonly StepUnitValues _stepValues;

        UnitTypes Unit
        {
            get => Unit<UnitTC>(_idx).Unit;
            set => Unit<UnitTC>(_idx).Unit = value;
        }
        LevelTypes Level
        {
            get => Unit<LevelTC>(_idx).Level;
            set => Unit<LevelTC>(_idx).Level = value;
        }
        public int DamageAttack(AttackTypes attack)
        {
            var tw = CellUnitTWE.UnitTW<ToolWeaponC>(_idx).ToolWeapon;
            var haveEff = Unit<HaveEffectC>(UnitStatTypes.Damage, _idx).Have;
            //var upgPerc = UnitUpgC.UpgDamagePercent(Unit, Level, Owner);


            var standDamage = _damageValues.StandDamage(Unit, Level);

            float powerDamege = standDamage;

            powerDamege += standDamage * _damageValues.PercentTW(tw);
            if (attack == AttackTypes.Unique) powerDamege += standDamage * DamageUnitValues.UNIQUE_PERCENT_DAMAGE;

            if (haveEff) powerDamege += standDamage * 0.2f;

            powerDamege += standDamage /** upgPerc*/;

            return (int)powerDamege;
        }
        public int DamageOnCell
        {
            get
            {
                var condition = Unit<ConditionUnitC>(_idx).Condition;

                var build = Build<BuildingTC>(_idx).Build;

                float powerDamege = DamageAttack(AttackTypes.Simple);

                var standDamage = _damageValues.StandDamage(Unit, Level);

                powerDamege += standDamage * _damageValues.ProtRelaxPercent(condition);
                powerDamege += standDamage * _damageValues.ProtectionPercent(build);
                foreach (var item in Enviroments) if (Environment<HaveEnvironmentC>(item, _idx).Have) powerDamege += standDamage * _damageValues.ProtectionPercent(item);
                return (int)powerDamege;
            }
        }



        internal UnitCellEC(in byte idx)
        {
            _idx = idx;
            _damageValues = new DamageUnitValues();
        }


        public void SetNew(in (UnitTypes, LevelTypes, PlayerTypes) unit)
        {
            if (unit.Item1 == UnitTypes.None) throw new Exception();
            if (Unit<UnitTC>(_idx).Have) throw new Exception("It's got unit");

            Unit<UnitTC>(_idx).Unit = unit.Item1;
            Unit<LevelTC>(_idx).Level = unit.Item2;
            Unit<PlayerTC>(_idx).Player = unit.Item3;
            
            CellUnitHpEs.SetMaxHp(_idx);
            CellUnitWaterEs.SetMaxWater(_idx);
            CellUnitStepEs.SetMaxSteps(_idx);

            foreach (var item in KeysStat) Unit<HaveEffectC>(item, _idx).Disable();
            Unit<ConditionUnitC>(_idx).Reset();
            foreach (var item in KeysCondition) AmountStepsInCondition<AmountC>(item, _idx).Reset();

            CellUnitTWE.UnitTW<UnitTWCellEC>(_idx).Reset();

            EntWhereUnits.HaveUnit<HaveUnitC>(unit, _idx).Have = true;
        }
        
        public void AddToInventor()
        {
            var level = Unit<LevelTC>(_idx).Level;
            var owner = Unit<PlayerTC>(_idx).Player;

            EntInventorUnits.Units<AmountC>(Unit<UnitTC>(_idx).Unit, level, owner).Amount += 1;

            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(_idx).Unit, level, owner, _idx).Have = false;
            Unit<UnitTC>(_idx).Reset();
        }
        public void SetScout()
        {
            ref var ownUnitC = ref Unit<PlayerTC>(_idx);

            ref var twUnitC = ref CellUnitTWE.UnitTW<UnitTWCellEC>(_idx);
            ref var twC = ref CellUnitTWE.UnitTW<ToolWeaponC>(_idx);
            ref var levTWC = ref CellUnitTWE.UnitTW<LevelTC>(_idx);


            EntWhereUnits.HaveUnit<HaveUnitC>(Unit<UnitTC>(_idx).Unit, Unit<LevelTC>(_idx).Level, ownUnitC.Player, _idx).Have = false;

            Unit<UnitTC>(_idx).Unit = UnitTypes.Scout;
            Unit<LevelTC>(_idx).Level = LevelTypes.First;
            if (twC.HaveTW)
            {
                InventorToolWeaponE.ToolWeapons<AmountC>(twC.ToolWeapon, levTWC.Level, ownUnitC.Player).Amount += 1;
                twUnitC.Reset();
            }

            EntWhereUnits.HaveUnit<HaveUnitC>(UnitTypes.Scout, LevelTypes.First, Unit<PlayerTC>(_idx).Player, _idx).Have = true;
        }

        public void Sync(in UnitTypes unit, in LevelTypes lev, in PlayerTypes owner, in int hp, in int steps, in int water)
        {
            Unit<UnitTC>(_idx).Unit = unit;
            Unit<LevelTC>(_idx).Level = lev;
            Unit<PlayerTC>(_idx).Player = owner;
            CellUnitHpEs.Hp<AmountC>(_idx).Amount = hp;
            CellUnitStepEs.Steps<AmountC>(_idx).Amount = steps;
            CellUnitWaterEs.Water<AmountC>(_idx).Amount = water;
        }
    }
}