using Chessy.Model;
using Chessy.Model.Entity;
using Chessy.Model.System;
using Chessy.Model.Values;
using Chessy.View.Component;
using Chessy.View.Entity;
using Chessy.View.UI.Entity;
using Chessy.View.UI.System;
using Photon.Pun;
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


        public SystemsView(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eM = eM;
            _eV = eV;

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
            //var circularAttackKingSRCs = new AnimationVC[IndexCellsValues.CELLS];


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
                //circularAttackKingSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).UnitEs.CircularAttackAnimC;

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

            var t = Time.deltaTime * 7f;
            if (t > 1) t = 1;

            for (byte cellIdxCurrent = 0; cellIdxCurrent < IndexCellsValues.CELLS; cellIdxCurrent++)
            {
                var whereSkinIdxCell = _e.SkinInfoUnitC(cellIdxCurrent).SkinIdxCell;

                if ( _e.UnitMainC(whereSkinIdxCell).Possition.magnitude > 0)
                {
                    _eV.CellEs(whereSkinIdxCell).UnitEs.ParentTC.Transform.position = Vector3.Lerp(_eV.CellEs(whereSkinIdxCell).UnitEs.ParentTC.Transform.position, _e.UnitMainC(whereSkinIdxCell).Possition, t);
                }
            }



            //var centerCloudIdxCell = _e.CenterCloudCellIdx;

            //var whereSkinsIdxCells = new List<byte>();
            //var whereDataIdxCells = new List<byte>();
            //var whereNeedShiftIdxCells = new List<byte>();


            //whereSkinsIdxCells.Add(_e.CloudWhereSkinDataOnCell(centerCloudIdxCell).SkinIdxCell);
            //whereDataIdxCells.Add(centerCloudIdxCell);
            //whereNeedShiftIdxCells.Add(_e.CloudC.WhereNeedShiftCloudIdxCell);

            //foreach (var currentCellIdx in _e.AroundCellsE(centerCloudIdxCell).CellsAround)
            //{
            //    whereSkinsIdxCells.Add(_e.CloudWhereSkinDataOnCell(currentCellIdx).SkinIdxCell);
            //    whereDataIdxCells.Add(currentCellIdx);
            //}

            //foreach (var currentCellIdx in _e.AroundCellsE(_e.CloudC.WhereNeedShiftCloudIdxCell).CellsAround)
            //{
            //    whereNeedShiftIdxCells.Add(currentCellIdx);
            //}

            //for (var i = 0; i < whereSkinsIdxCells.Count; i++)
            //{

            //}

            //var pos_0 = _e.CellE(centerCloudIdxCell).StartPositionC.Possition;
            //var pos_1 = _e.CellE(_e.CloudC.WhereNeedShiftCloudIdxCell).StartPositionC.Possition;

            //var nextPos = _e.CellE(_e.CloudC.WhereNeedShiftCloudIdxCell).StartPositionC.Possition;

            //_eV.CellEs(_e.CloudWhereSkinDataOnCell(centerCloudIdxCell).SkinIdxCell).CloudSRC.Transform.position = nextPos;

            //if (nextPos.magnitude > 0)
            //{
            //    _eV.CellEs(_e.CloudWhereSkinDataOnCell(centerCloudIdxCell).SkinIdxCell).CloudSRC.Transform.position = _e.CellE(_e.CloudC.WhereNeedShiftCloudIdxCell).StartPositionC.Possition;//Vector3.Lerp(_eV.CellEs(_e.CloudWhereSkinDataOnCell(centerCloudIdxCell).SkinIdxCell).CloudSRC.Transform.position, nextPos, t);
            //}
        }
    }
}