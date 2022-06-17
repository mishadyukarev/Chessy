using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System.Master;
using Chessy.Game.Values;

namespace Chessy.Game.Model.System
{
    sealed partial class UnitSystems
    {
        readonly EntitiesModelGame _eMG;

        readonly SetMainToolWeaponUnitS[] _setMainTWSs = new SetMainToolWeaponUnitS[StartValues.CELLS];
        readonly ClearUnitS[] _clearUnitSs = new ClearUnitS[StartValues.CELLS];
        readonly SetMainUnitS[] _setMainSs = new SetMainUnitS[StartValues.CELLS];
        readonly SetExtraToolWeaponForUnitS_M[] _setExtraTWSs = new SetExtraToolWeaponForUnitS_M[StartValues.CELLS];
        readonly SetEffectsUnitS[] _setEffectsSs = new SetEffectsUnitS[StartValues.CELLS];
        readonly SetStatsUnitS[] _setStatsSs = new SetStatsUnitS[StartValues.CELLS];

        internal readonly SetLastDiedUnitOnCellS SetLastDiedUnitOnCellS;
        internal readonly AttackShieldUnitOnCellS AttackShieldS;
        internal readonly CopyUnitFromToS_M CopyUnitFromToS;
        internal readonly AttackUnitS AttackUnitS;
        internal readonly KillUnitS_M KillUnitS;


        #region Abilities

        internal readonly IncreaseWindSnowyS_M IncreaseWindSnowyS_M;
        internal readonly CurcularAttackKingS_M CurcularAttackKingS_M;
        internal readonly FirePawnS_M FirePawnS_M;
        internal readonly PutOutFirePawnS_M PutOutFirePawnS_M;
        internal readonly ChangeCornerArcherS_M ChangeCornerArcherS_M;
        internal readonly StunElfemaleS_M StunElfemaleS_M;
        internal readonly FireArcherS_M FireArcherS_M;
        internal readonly GrowAdultForestS_M GrowAdultForestS_M;
        internal readonly TryChangeDirectionWindWithSnowyS_M ChangeDirectionWindS_M;

        #endregion


        internal void SetMain(in byte cell, in UnitTypes unitT, in LevelTypes levelT, in PlayerTypes playerT, in ConditionUnitTypes conditionT, in bool isRight) => _setMainSs[cell].Set(unitT, levelT, playerT, conditionT, isRight);
        internal void SetMainToolWeapon(in byte cell, in ToolWeaponTypes twT, in LevelTypes levelT) => _setMainTWSs[cell].Set(twT, levelT);
        internal void SetExtraToolWeapon(in byte cell, in ToolWeaponTypes twT, in LevelTypes levelT, in float protection) => _setExtraTWSs[cell].Set(twT, levelT, protection);
        internal void SetEffects(in byte cell, in float stun, in float protection, in int shoots, in bool haveKingEffect) => _setEffectsSs[cell].Set(stun, protection, shoots, haveKingEffect);
        internal void SetStats(in byte cell, in double hp, in double steps, in double water) => _setStatsSs[cell].Set(hp, steps, water);
        internal void ClearUnit(in byte cell) => _clearUnitSs[cell].Clear();


        internal UnitSystems(in SystemsModelGame sMG, in EntitiesModelGame eMG)
        {
            _eMG = eMG;

            SetLastDiedUnitOnCellS = new SetLastDiedUnitOnCellS(sMG, eMG);
            CopyUnitFromToS = new CopyUnitFromToS_M(sMG, eMG);
            AttackShieldS = new AttackShieldUnitOnCellS(sMG, eMG);
            KillUnitS = new KillUnitS_M(sMG, eMG);
            AttackUnitS = new AttackUnitS(sMG, eMG);


            #region Abilities

            IncreaseWindSnowyS_M = new IncreaseWindSnowyS_M(sMG, eMG);
            CurcularAttackKingS_M = new CurcularAttackKingS_M(sMG, eMG);
            FirePawnS_M = new FirePawnS_M(sMG, eMG);
            PutOutFirePawnS_M = new PutOutFirePawnS_M(sMG, eMG);
            ChangeCornerArcherS_M = new ChangeCornerArcherS_M(sMG, eMG);
            StunElfemaleS_M = new StunElfemaleS_M(sMG, eMG);
            FireArcherS_M = new FireArcherS_M(sMG, eMG);
            GrowAdultForestS_M = new GrowAdultForestS_M(sMG, eMG);
            ChangeDirectionWindS_M = new TryChangeDirectionWindWithSnowyS_M(sMG, eMG);

            #endregion

            for (byte startCell = 0; startCell < StartValues.CELLS; startCell++)
            {
                _setMainTWSs[startCell] = new SetMainToolWeaponUnitS(eMG.MainToolWeaponE(startCell));
                _clearUnitSs[startCell] = new ClearUnitS(eMG.UnitEs(startCell));
                _setMainSs[startCell] = new SetMainUnitS(eMG.UnitMainE(startCell));
                _setExtraTWSs[startCell] = new SetExtraToolWeaponForUnitS_M(eMG.UnitExtraTWE(startCell));
                _setEffectsSs[startCell] = new SetEffectsUnitS(eMG.UnitEffectsE(startCell));
                _setStatsSs[startCell] = new SetStatsUnitS(eMG.StatsUnitE(startCell));
            }
        }


        internal void SetMain(in byte cell_from, in byte cell_to)
        {
            _eMG.UnitTC(cell_to) = _eMG.UnitTC(cell_from);
            _eMG.UnitLevelTC(cell_to) = _eMG.UnitLevelTC(cell_from);
            _eMG.UnitPlayerTC(cell_to) = _eMG.UnitPlayerTC(cell_from);
            _eMG.UnitConditionTC(cell_to) = _eMG.UnitConditionTC(cell_from);
            _eMG.UnitIsRightArcherC(cell_to) = _eMG.UnitIsRightArcherC(cell_from);
        }
        internal void CopyExtraTW(in byte cell_from, in byte cell_to)
        {
            _eMG.ExtraToolWeaponTC(cell_to) = _eMG.ExtraToolWeaponTC(cell_from);
            _eMG.ExtraTWLevelTC(cell_to) = _eMG.ExtraTWLevelTC(cell_from);
            _eMG.ExtraTWProtectionC(cell_to) = _eMG.ExtraTWProtectionC(cell_from);
        }
        internal void CopyMainTW(in byte cell_from, in byte cell_to)
        {
            _eMG.MainToolWeaponTC(cell_to) = _eMG.MainToolWeaponTC(cell_from);
            _eMG.MainTWLevelTC(cell_to) = _eMG.MainTWLevelTC(cell_from);
        }
        internal void CopyEffects(in byte cell_from, in byte cell_to)
        {
            _eMG.StunUnitC(cell_to) = _eMG.StunUnitC(cell_from);
            _eMG.ShieldUnitEffectC(cell_to) = _eMG.ShieldUnitEffectC(cell_from);
            _eMG.FrozenArrawEffectC(cell_to) = _eMG.FrozenArrawEffectC(cell_from);
            _eMG.HaveKingEffect(cell_to) = _eMG.HaveKingEffect(cell_from);
        }
        internal void Set(in byte cell_from, in byte cell_to)
        {
            _eMG.HpUnitC(cell_to) = _eMG.HpUnitC(cell_from);
            _eMG.StepUnitC(cell_to) = _eMG.StepUnitC(cell_from);
            _eMG.WaterUnitC(cell_to) = _eMG.WaterUnitC(cell_from);
        }
    }
}