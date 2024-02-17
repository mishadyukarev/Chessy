using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.Entity;
using Chessy.View.UI.Entity;
using Chessy.View.UI.System;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.View.System
{
    public sealed class SystemsView : SystemAbstract, IUpdate
    {
        readonly List<Action> _updates;
        readonly EntitiesModel _eM;
        readonly EntitiesView _eV;

        readonly SyncUnitVS _syncUnitVS;
        readonly SyncMainToolWeaponUnitVS _syncMainToolWeaponUnitVS;
        readonly SyncExtraToolWeaponUnitVS _syncExtraToolWeaponUnitVS;
        readonly ShiftUnitVS _shiftUnitVS;
        readonly CloudShiftVS _cloudShiftVS;


        public SystemsView(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eM = eM;
            _eV = eV;

            var cellParentGOCs = new GameObjectVC[IndexCellsValues.CELLS];
            var blackVisisionSrCs = new SpriteRenderer[IndexCellsValues.CELLS];
            var redCircularSRCs = new SpriteRenderer[IndexCellsValues.CELLS];
            var needFoodSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var buildingFlagSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var riverEs = new RiverVE[IndexCellsValues.CELLS];
            var fireSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var adultForestSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var environmentEs = new EnvironmentVE[IndexCellsValues.CELLS];
            var needWaterSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var conditionUnitSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var maxStepsUnitOnCellSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var cloudSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var idxXyInfoCellSRCs = new TMPC[IndexCellsValues.CELLS];
            var hpBarUnitSRCs = new SpriteRenderer[IndexCellsValues.CELLS];
            var stunUnitSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var shieldSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var frozenArrawRightSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var frozenArrawUpSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var trailsSRCs = new SpriteRenderer[IndexCellsValues.CELLS, (byte)DirectTypes.End];
            var buildingSRCs = new SpriteRenderer[IndexCellsValues.CELLS][];
            //var circularAttackKingSRCs = new AnimationVC[IndexCellsValues.CELLS];

            for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
            {
                buildingSRCs[(byte)buildingT] = new SpriteRenderer[IndexCellsValues.CELLS];
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                cellParentGOCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).CellParentGOC;
                blackVisisionSrCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).SupportCellEs.NoneSRC.SR;
                redCircularSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).RedCircularSRC.SR;
                needFoodSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.Block(CellBlockTypes.NeedFood);
                buildingFlagSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).BuildingEs.FlagSRC;
                riverEs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).RiverE;
                fireSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).FireVE.SRC;
                adultForestSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).EnvironmentVEs.EnvironmentE(EnvironmentTypes.AdultForest);
                environmentEs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).EnvironmentVEs;
                needWaterSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.Block(CellBlockTypes.NeedWater);
                conditionUnitSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.Block(CellBlockTypes.Condition);
                maxStepsUnitOnCellSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.Block(CellBlockTypes.MaxSteps);
                cloudSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).CloudSRC;
                idxXyInfoCellSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).IdxAndXyInfoTMPC;
                hpBarUnitSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.UnitHpBarSRC.SR;
                stunUnitSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.StunSRC;
                shieldSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.ShieldSRC;
                frozenArrawRightSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.FrozenArraw(true);
                frozenArrawUpSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.FrozenArraw(false);
                //circularAttackKingSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.CircularAttackAnimC;

                buildingSRCs[cellIdxCurrent] = new SpriteRenderer[(byte)BuildingTypes.End];

                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                {
                    trailsSRCs[cellIdxCurrent, (byte)directT] = eV.CellEs(cellIdxCurrent).TrailCellVC(directT).SR;
                }
                for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                {
                    buildingSRCs[cellIdxCurrent][(byte)buildingT] = eV.CellEs(cellIdxCurrent).BuildingEs.Main(buildingT).SR;
                }
            }

            _syncUnitVS = new SyncUnitVS(eV, eM);
            _syncMainToolWeaponUnitVS = new SyncMainToolWeaponUnitVS(eV, eM);
            _syncExtraToolWeaponUnitVS = new SyncExtraToolWeaponUnitVS(eV, eM);

            _updates = new List<Action>()
            {
                new SyncSupportVS(eV, eM).Sync,
                new SyncSoundVS(eV, eM).Sync,
                new SyncCameraVS(eV, eM).Sync,
                new SyncBarsEnvironmentVS(eV, eM).Sync,
                new SyncSunSideVS(eV, eM).Sync,

                new SyncRotationAllCellsVS(cellParentGOCs, eM).Sync,
                new SyncBlackVisionVS(blackVisisionSrCs, eM).Sync,
                new SyncRedCircularVS(redCircularSRCs, eM).Sync,
                new NeedFoodVS(needFoodSRCs, eM).Sync,
                new BuildingFlagVS(buildingFlagSRCs, eM).Sync,
                new SyncRiverVS(riverEs, eM).Sync,
                new SyncFireVS(fireSRCs, eM).Sync,
                new SyncAdultForestOnCellsVS(adultForestSRCs, eM).Sync,
                new SyncElseEnvironmentVS(environmentEs, eM).Sync,
                new SyncNeedWaterBlockVS(needWaterSRCs, eM).Sync,
                new SyncConditionOnCellVS(conditionUnitSRCs, eM).Sync,
                new SyncCanAttackUnitOnCellVS(maxStepsUnitOnCellSRCs, eM).Sync,
                new SyncCloudsOnCellsVS(cloudSRCs, eM).Sync,
                new SyncIdxAndXyInfoVS(idxXyInfoCellSRCs, eM).Sync,
                new SyncHpBarUnitVS(hpBarUnitSRCs, eM).Sync,
                new SyncStunVS(stunUnitSRCs, eM).Sync,
                new SyncShieldEffectSnowyVS(shieldSRCs, eM).Sync,
                new SyncFrozenArrawVS(frozenArrawRightSRCs, frozenArrawUpSRCs, eM).Sync,
                new SyncBuildingVS(buildingSRCs, eM).Sync,
                new SyncTrailVS(trailsSRCs, eM).Sync,

                _syncUnitVS.Sync,
                _syncMainToolWeaponUnitVS.Sync,
                _syncExtraToolWeaponUnitVS.Sync,
            };


            _shiftUnitVS = new ShiftUnitVS(eV, eM);
            _cloudShiftVS = new CloudShiftVS(eV, eM);
        }

        public void Update()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_updateViewUnitCs[cellIdxCurrent].NeedUpdateView)
                {
                    _syncUnitVS.Sync(cellIdxCurrent);
                    _syncMainToolWeaponUnitVS.Sync(cellIdxCurrent);
                    _syncExtraToolWeaponUnitVS.Sync(cellIdxCurrent);
                    _updateViewUnitCs[cellIdxCurrent].NeedUpdateView = false;
                }
            }

            if (updateAllViewC.NeedUpdateView)
            {
                _updates.ForEach((Action action) => action.Invoke());

                updateAllViewC.NeedUpdateView = false;
            }

            _shiftUnitVS.Sync();
            _cloudShiftVS.Sync();
        }
    }
}