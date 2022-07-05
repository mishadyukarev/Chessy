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

namespace Chessy.View.System
{
    public sealed class SystemsView : SystemAbstract, IUpdate
    {
        readonly List<Action> _updates;
        readonly EntitiesModel _eM;

        readonly SyncUnitVS _syncUnitVS;


        public SystemsView(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eM = eM;

            var cellParentGOCs = new GameObjectVC[IndexCellsValues.CELLS];
            var blackVisisionSrCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var redCircularSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
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
            var hpBarUnitSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var stunUnitSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var shieldSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var frozenArrawRightSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var frozenArrawUpSRCs = new SpriteRendererVC[IndexCellsValues.CELLS];
            var trailsSRCs = new Dictionary<DirectTypes, SpriteRendererVC[]>();
            var buildingSRCs = new Dictionary<BuildingTypes, SpriteRendererVC[]>();


            for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
            {
                trailsSRCs.Add(directT, new SpriteRendererVC[IndexCellsValues.CELLS]);
            }

            for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
            {
                buildingSRCs.Add(buildingT, new SpriteRendererVC[IndexCellsValues.CELLS]);
            }

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                cellParentGOCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).CellParentGOC;
                blackVisisionSrCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).SupportCellEs.NoneSRC;
                redCircularSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).RedCircularSRC;
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
                hpBarUnitSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.UnitHpBarSRC;
                stunUnitSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.StunSRC;
                shieldSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.ShieldSRC;
                frozenArrawRightSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.FrozenArraw(true);
                frozenArrawUpSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.EffectE.FrozenArraw(false);

                for (var directT = (DirectTypes)1; directT < DirectTypes.End; directT++)
                {
                    trailsSRCs[directT][cellIdxCurrent] = eV.CellEs(cellIdxCurrent).TrailCellVC(directT);
                }
                for (var buildingT = (BuildingTypes)1; buildingT < BuildingTypes.End; buildingT++)
                {
                    buildingSRCs[buildingT][cellIdxCurrent] = eV.CellEs(cellIdxCurrent).BuildingEs.Main(buildingT);
                }
            }

            _syncUnitVS = new SyncUnitVS(eV, eM);

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
                new SyncMaxStepsUnitOnCellVS(maxStepsUnitOnCellSRCs, eM).Sync,
                new SyncCloudsOnCellsVS(cloudSRCs, eM).Sync,
                new SyncIdxAndXyInfoVS(idxXyInfoCellSRCs, eM).Sync,
                new SyncHpBarUnitVS(hpBarUnitSRCs, eM).Sync,
                new SyncStunVS(stunUnitSRCs, eM).Sync,
                new SyncShieldVS(shieldSRCs, eM).Sync,
                new SyncFrozenArrawVS(frozenArrawRightSRCs, frozenArrawUpSRCs, eM).Sync,
                new SyncBuildingVS(buildingSRCs, eM).Sync,
                new SyncTrailVS(trailsSRCs, eM).Sync,

                _syncUnitVS.Sync,
            };
        }

        public void Update()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                if (_eM.UnitNeedUpdateViewC(cellIdxCurrent).NeedUpdateView)
                {
                    _syncUnitVS.Sync(cellIdxCurrent);
                    _eM.UnitNeedUpdateViewC(cellIdxCurrent).NeedUpdateView = false;
                }
            }

            if (_eM.NeedUpdateView)
            {
                _updates.ForEach((Action action) => action.Invoke());

                _eM.NeedUpdateView = false;
            }
        }
    }
}