using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitPoolEs
    {
        readonly AbilityTC[] _uniqueButtons;
        readonly CooldownC[] _abilities;
        readonly CellUnitForPlayerE[] _visibles;
        readonly StepsC[] _needStepsForShift;
        readonly IdxsC[] _forAttack;


        public UnitTC UnitTC;
        public PlayerTC PlayerTC;
        public LevelTC LevelTC;

        public ConditionUnitTC ConditionTC;
        public IsRightArcherC IsRightArcherC;

        public StunC StunC;
        public ProtectionC ShieldEffectC;
        public FrozenArrawC FrozenArrawC;

        public HealthC HealthC;
        public StepsC StepC;
        public WaterC WaterC;

        public bool IsAnimal;
        public bool IsMelee;
        public bool IsHero;

        public ToolWeaponTC MainToolWeaponTC;
        public LevelTC MainLevelTC;

        public ToolWeaponTC ExtraToolWeaponTC;
        public LevelTC ExtraTWLevelTC;
        public ProtectionC ExtraTWShieldC;

        public DamageC DamageAttackC;
        public DamageC DamageOnCell;

        public IdxsC ForArson;
        public IdxsC ForShift;

        public CellUnitExtractPawnE ExtractPawnE;
        public CellUnitNeedKillE NeelKillE;


        public IdxsC ForAttack(in AttackTypes attack) => _forAttack[(byte)attack - 1];
        public ref StepsC NeedSteps(in byte idx_cell) => ref _needStepsForShift[idx_cell];

        public ref AbilityTC Ability(in ButtonTypes button) => ref _uniqueButtons[(byte)button - 1];
        public ref CooldownC CoolDownC(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public ref CellUnitForPlayerE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player - 1];


        internal CellUnitPoolEs(in bool def) : this()
        {
            _uniqueButtons = new AbilityTC[(byte)ButtonTypes.End - 1];

            _abilities = new CooldownC[(byte)AbilityTypes.End - 1];

            _visibles = new CellUnitForPlayerE[(byte)PlayerTypes.End - 1];
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _visibles[(byte)player - 1] = new CellUnitForPlayerE();
            }

            ForArson = new IdxsC(new HashSet<byte>());
            ForShift = new IdxsC(new HashSet<byte>());

            _needStepsForShift = new StepsC[StartValues.ALL_CELLS_AMOUNT];
            _forAttack = new IdxsC[(byte)AttackTypes.End - 1];

            _forAttack[(byte)AttackTypes.Simple - 1] = new IdxsC(new HashSet<byte>());
            _forAttack[(byte)AttackTypes.Unique - 1] = new IdxsC(new HashSet<byte>());
        }

        public void Set(in CellUnitPoolEs unitE)
        {
            UnitTC.Unit = unitE.UnitTC.Unit;
            LevelTC.Level = unitE.LevelTC.Level;
            PlayerTC.Player = unitE.PlayerTC.Player;
            ConditionTC.Condition = unitE.ConditionTC.Condition;
            IsRightArcherC.IsRight = unitE.IsRightArcherC.IsRight;

            StunC.Stun = unitE.StunC.Stun;
            ShieldEffectC.Protection = unitE.ShieldEffectC.Protection;
            FrozenArrawC.Shoots = unitE.FrozenArrawC.Shoots;

            HealthC.Health = unitE.HealthC.Health;
            StepC.Steps = unitE.StepC.Steps;
            WaterC.Water = unitE.WaterC.Water;

            MainToolWeaponTC.ToolWeapon = unitE.MainToolWeaponTC.ToolWeapon;
            MainLevelTC.Level = unitE.MainLevelTC.Level;

            ExtraToolWeaponTC.ToolWeapon = unitE.ExtraToolWeaponTC.ToolWeapon;
            ExtraTWLevelTC.Level = unitE.ExtraTWLevelTC.Level;
            ExtraTWShieldC.Protection = unitE.ExtraTWShieldC.Protection;

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                Ability(buttonT).Ability = unitE.Ability(buttonT).Ability;
            }
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                CoolDownC(abilityT).Cooldown = unitE.CoolDownC(abilityT).Cooldown;
            }
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                ForPlayer(playerT).IsVisibleC = unitE.ForPlayer(playerT).IsVisibleC;
            }
        }
    }
}