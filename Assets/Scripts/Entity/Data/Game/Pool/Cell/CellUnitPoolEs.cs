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

        public IdxsC ForArson;
        public IdxsC ForShift;

        public CellUnitMainE MainE;
        public CellUnitStatsE StatsE;
        public CellUnitStatsMaxE StatsMaxE;
        public CellUnitEffectsE EffectsE;
        public CellUnitMainToolWeaponE MainToolWeaponE;
        public CellUnitExtraToolWeaponE ExtraToolWeaponE;
        public CellUnitWhoLastDiedHereE WhoLastDiedHereE;
        public CellUnitDamageE DamageE;
        public CellUnitExtractE ExtractE;
        public CellUnitAttackUnitE NeelKillE;



        public IdxsC ForAttack(in AttackTypes attack) => _forAttack[(byte)attack - 1];
        public ref StepsC NeedSteps(in byte idx_cell) => ref _needStepsForShift[idx_cell];

        public ref AbilityTC Ability(in ButtonTypes button) => ref _uniqueButtons[(byte)button - 1];
        public ref CooldownC CoolDownC(in AbilityTypes ability) => ref _abilities[(byte)ability - 1];
        public ref CellUnitForPlayerE ForPlayer(in PlayerTypes player) => ref _visibles[(byte)player];


        internal CellUnitPoolEs(in bool def) : this()
        {
            _uniqueButtons = new AbilityTC[(byte)ButtonTypes.End - 1];

            _abilities = new CooldownC[(byte)AbilityTypes.End - 1];

            _visibles = new CellUnitForPlayerE[(byte)PlayerTypes.End];
            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                _visibles[(byte)player] = new CellUnitForPlayerE();
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
            MainE = unitE.MainE;
            EffectsE = unitE.EffectsE;
            StatsE = unitE.StatsE;
            MainToolWeaponE = unitE.MainToolWeaponE;
            ExtraToolWeaponE = unitE.ExtraToolWeaponE;

            for (var buttonT = ButtonTypes.None + 1; buttonT < ButtonTypes.End; buttonT++)
            {
                Ability(buttonT) = unitE.Ability(buttonT);
            }
            for (var abilityT = AbilityTypes.None + 1; abilityT < AbilityTypes.End; abilityT++)
            {
                CoolDownC(abilityT) = unitE.CoolDownC(abilityT);
            }
            for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
            {
                ForPlayer(playerT) = unitE.ForPlayer(playerT);
            }
        }
    }
}