using ECS;
using System.Collections.Generic;

namespace Game.Game
{
    public struct CellUnitEs
    {
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

        public PowerDamageC DamageAttackC;
        public PowerDamageC DamageOnCell;

        public IdxsC ForArson;


        readonly Dictionary<ButtonTypes, CellUnitAbilityButtonsE> _uniqueButtons;
        readonly Dictionary<AbilityTypes, CellUnitAbilityE> _abilities;
        readonly ForPlayerCellUnitE[] _visibles;

        public CellUnitAbilityButtonsE AbilityButton(in ButtonTypes button) => _uniqueButtons[button];
        public CellUnitAbilityE Ability(in AbilityTypes ability) => _abilities[ability];
        public ref ForPlayerCellUnitE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player - 1];


        internal CellUnitEs(in CellEs cellEs, in EcsWorld gameW) : this()
        {
            _uniqueButtons = new Dictionary<ButtonTypes, CellUnitAbilityButtonsE>();
            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                _uniqueButtons.Add(buttonT, new CellUnitAbilityButtonsE(gameW));
            }

            _abilities = new Dictionary<AbilityTypes, CellUnitAbilityE>();
            for (var ability = AbilityTypes.None + 1; ability < AbilityTypes.End; ability++)
            {
                _abilities.Add(ability, new CellUnitAbilityE(ability, cellEs, gameW));
            }

            _visibles = new ForPlayerCellUnitE[(byte)PlayerTypes.End - 1];
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _visibles[(byte)player - 1] = new ForPlayerCellUnitE(default);
            }

            ForArson = new IdxsC(new HashSet<byte>());
        }

        public void Set(in CellUnitEs unitE)
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
                AbilityButton(buttonT).AbilityC.Ability = unitE.AbilityButton(buttonT).AbilityC.Ability;
            }
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                Ability(abilityT).CooldownC.Cooldown = unitE.Ability(abilityT).CooldownC.Cooldown;
            }
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                ForPlayer(playerT).IsVisibleC = unitE.ForPlayer(playerT).IsVisibleC;
            }
        }
    }
}