using ECS;
using System;
using System.Collections.Generic;

namespace Game.Game
{
    public readonly struct CellUnitEs
    {
        static Entity[] _units;
        static Dictionary<PlayerTypes, Entity[]> _unitEnts;
        static Dictionary<UniqueAbilityTypes, Entity[]> _uniqueAbilEnts;
        static Dictionary<UnitStatTypes, Entity[]> _unitStatEnts;
        static Dictionary<ConditionUnitTypes, Entity[]> _conditionUnitEnts;
        static Dictionary<ButtonTypes, Entity[]> _uniqButtonUnitEnts;
        static Dictionary<ButtonTypes, Entity[]> _buildButtonUnits;

        public static ref C Unit<C>(in byte idx) where C : struct, IUnitCellE => ref _units[idx].Get<C>();
        public static ref C Unit<C>(in PlayerTypes player, in byte idx) where C : struct, IUnitPlayerCellE => ref _unitEnts[player][idx].Get<C>();
        public static ref C Unit<C>(in UniqueAbilityTypes uniq, in byte idx) where C : struct, IUnitUniqueCellE
        {
            if (!_uniqueAbilEnts.ContainsKey(uniq)) throw new Exception();
            return ref _uniqueAbilEnts[uniq][idx].Get<C>();
        }
        public static ref C Unit<C>(in UnitStatTypes stat, in byte idx) where C : struct, IUnitStatCellE
        {
            if (!_unitStatEnts.ContainsKey(stat)) throw new Exception();
            return ref _unitStatEnts[stat][idx].Get<C>();
        }
        public static ref C AmountStepsInCondition<C>(in ConditionUnitTypes cond, in byte idx) where C : struct, IUnitConditionCellE
        {
            if (!_conditionUnitEnts.ContainsKey(cond)) throw new Exception();
            return ref _conditionUnitEnts[cond][idx].Get<C>();
        }
        public static ref C UnitUniqueButton<C>(in ButtonTypes button, in byte idx) where C : struct, IUnitUniqueButtonCellE
        {
            if (!_uniqButtonUnitEnts.ContainsKey(button)) throw new Exception();
            return ref _uniqButtonUnitEnts[button][idx].Get<C>();
        }
        public static ref C UnitBuildButton<C>(in ButtonTypes button, in byte idx) where C : struct
        {
            if (!_buildButtonUnits.ContainsKey(button)) throw new Exception();
            return ref _buildButtonUnits[button][idx].Get<C>();
        }

        public static HashSet<UniqueAbilityTypes> KeysUnique
        {
            get
            {
                var hash = new HashSet<UniqueAbilityTypes>();
                foreach (var item in _uniqueAbilEnts) hash.Add(item.Key);
                return hash;
            }
        }
        public static HashSet<UnitStatTypes> KeysStat
        {
            get
            {
                var hash = new HashSet<UnitStatTypes>();
                foreach (var item in _unitStatEnts) hash.Add(item.Key);
                return hash;
            }
        }
        public static HashSet<ConditionUnitTypes> KeysCondition
        {
            get
            {
                var hash = new HashSet<ConditionUnitTypes>();
                foreach (var item in _conditionUnitEnts) hash.Add(item.Key);
                return hash;
            }
        }

        public CellUnitEs(in EcsWorld gameW)
        {
            _units = new Entity[CellValues.ALL_CELLS_AMOUNT];

            _unitEnts = new Dictionary<PlayerTypes, Entity[]>();
            _uniqueAbilEnts = new Dictionary<UniqueAbilityTypes, Entity[]>();
            _unitStatEnts = new Dictionary<UnitStatTypes, Entity[]>();
            _conditionUnitEnts = new Dictionary<ConditionUnitTypes, Entity[]>();
            _uniqButtonUnitEnts = new Dictionary<ButtonTypes, Entity[]>();
            _buildButtonUnits = new Dictionary<ButtonTypes, Entity[]>();

            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                _unitEnts.Add(player, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
            for (var uniqAbil = UniqueAbilityTypes.First; uniqAbil < UniqueAbilityTypes.End; uniqAbil++)
            {
                _uniqueAbilEnts.Add(uniqAbil, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
            for (var unitStat = UnitStatTypes.First; unitStat < UnitStatTypes.End; unitStat++)
            {
                _unitStatEnts.Add(unitStat, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
            for (var cond = ConditionUnitTypes.Start; cond < ConditionUnitTypes.End; cond++)
            {
                _conditionUnitEnts.Add(cond, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }
            for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
            {
                _buildButtonUnits.Add(button, new Entity[CellValues.ALL_CELLS_AMOUNT]);
                _uniqButtonUnitEnts.Add(button, new Entity[CellValues.ALL_CELLS_AMOUNT]);
            }



            for (byte idx = 0; idx < CellValues.ALL_CELLS_AMOUNT; idx++)
            {
                _units[idx] = gameW.NewEntity()
                    .Add(new UnitCellEC(idx))
                    .Add(new UnitTC())
                    .Add(new LevelTC())
                    .Add(new PlayerTC())
                    .Add(new NeedStepsForExitStunC())
                    .Add(new ConditionUnitC())
                    .Add(new IsCornedArcherC())
                    .Add(new HpC())
                    .Add(new StepC())
                    .Add(new WaterC());


                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    _unitEnts[player][idx] = gameW.NewEntity()
                        .Add(new IsVisibledC());
                }

                for (var uniqAbil = UniqueAbilityTypes.First; uniqAbil < UniqueAbilityTypes.End; uniqAbil++)
                {
                    _uniqueAbilEnts[uniqAbil][idx] = gameW.NewEntity()
                        .Add(new CooldownC());
                }

                for (var unitStat = UnitStatTypes.First; unitStat < UnitStatTypes.End; unitStat++)
                {
                    _unitStatEnts[unitStat][idx] = gameW.NewEntity()
                        .Add(new HaveEffectC());
                }

                for (var cond = ConditionUnitTypes.Start; cond < ConditionUnitTypes.End; cond++)
                {
                    _conditionUnitEnts[cond][idx] = gameW.NewEntity()
                        .Add(new StepC());
                }

                for (var button = ButtonTypes.First; button < ButtonTypes.End; button++)
                {
                    _buildButtonUnits[button][idx] = gameW.NewEntity()
                        .Add(new BuildingC());

                    _uniqButtonUnitEnts[button][idx] = gameW.NewEntity()
                        .Add(new UniqueAbilityC());
                }
            }
        }
    }
}