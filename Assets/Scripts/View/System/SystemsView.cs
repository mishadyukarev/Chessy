using Chessy.Model;
using Chessy.Model.Values;
using System;
using System.Collections.Generic;

namespace Chessy.Model.System.View
{
    public sealed class SystemsView : SystemAbstract, IUpdate
    {
        readonly List<Action> _updates;

        readonly NeedFoodVS[] _syncNeedFoodSs = new NeedFoodVS[StartValues.CELLS];
        readonly BuildingFlagVS[] _syncBuildingFlagSs = new BuildingFlagVS[StartValues.CELLS];
        readonly Dictionary<DirectTypes, SyncTrailVS[]> _syncTrailSs = new Dictionary<DirectTypes, SyncTrailVS[]>();
        readonly SyncBarsEnvironmentVS[] _syncBarsEnvironmentSs = new SyncBarsEnvironmentVS[StartValues.CELLS];
        readonly SyncRiverVS[] _syncRiverSs = new SyncRiverVS[StartValues.CELLS];
        readonly SyncFireVS[] _syncFireSs = new SyncFireVS[StartValues.CELLS];
        readonly SyncEnvironmentVS[] _syncEnvironmentSs = new SyncEnvironmentVS[StartValues.CELLS];
        readonly SyncBlocksVS[] _syncStatsSs = new SyncBlocksVS[StartValues.CELLS];
        readonly SyncBuildingVS[] _syncBuildingSs = new SyncBuildingVS[StartValues.CELLS];
        readonly SyncUnitBarHpVS[] _syncUnitBarHpSs = new SyncUnitBarHpVS[StartValues.CELLS];
        readonly SyncFrozenArrawVS[] _syncFrozenArrawSs = new SyncFrozenArrawVS[StartValues.CELLS];
        readonly SyncStunVS[] _syncStunSs = new SyncStunVS[StartValues.CELLS];
        readonly SyncShieldVS[] _syncShieldSs = new SyncShieldVS[StartValues.CELLS];
        readonly SyncCloudVS[] _syncCloudSs = new SyncCloudVS[StartValues.CELLS];
        readonly SyncRotationVS[] _syncRotationSs = new SyncRotationVS[StartValues.CELLS];
        readonly SyncIdxAndXyInfoVS[] _syncIdxAndXyInfoSs = new SyncIdxAndXyInfoVS[StartValues.CELLS];

        readonly SyncSupportVS _syncSupportS;
        readonly SyncSoundVS _syncSoundS;
        readonly SyncCameraVS _syncCameraS;

        readonly EntitiesModel _eM;

        readonly SyncUnitVS _syncUnitVS;


        public SystemsView(in EntitiesView eV, in EntitiesModel eM) : base(eM)
        {
            _eM = eM;

            for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
            {
                _syncTrailSs.Add(dirT, new SyncTrailVS[StartValues.CELLS]);
            }

            var blackVisisionSrCs = new SpriteRendererVC[StartValues.CELLS];
            var redCircularSRCs = new SpriteRendererVC[StartValues.CELLS];
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
            {
                blackVisisionSrCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).SupportCellEs.NoneSRC;
                redCircularSRCs[cellIdxCurrent] = eV.CellEs(cellIdxCurrent).RedCircularSRC;
            }


            _syncUnitVS = new SyncUnitVS(eV, eM);

            _updates = new List<Action>()
            {
                new SyncBlackVisionVS(blackVisisionSrCs, eM).Sync,
                new SyncRedCircularVS(redCircularSRCs, eM).Sync,
                _syncUnitVS.Sync,
            };

            for (byte startCell = 0; startCell < StartValues.CELLS; startCell++)
            {
                //_updates.Add(new KingPassiveVS(_eV.CellEs(startCell).UnitEs.EffectE.KingPassiveGOC, startCell, _eM).Sync);

                _syncBuildingSs[startCell] = new SyncBuildingVS(eV.CellEs(startCell).BuildingEs, startCell, eM);
                _syncStatsSs[startCell] = new SyncBlocksVS(eV, startCell, eM);
                _syncUnitBarHpSs[startCell] = new SyncUnitBarHpVS(eV.CellEs(startCell).UnitEs.UnitHpBarSRC, startCell, eM);
                _syncFrozenArrawSs[startCell] = new SyncFrozenArrawVS(eV.CellEs(startCell).UnitEs.EffectE, startCell, eM);
                _syncEnvironmentSs[startCell] = new SyncEnvironmentVS(eV.CellEs(startCell).EnvironmentVEs, startCell, eM);
                _syncFireSs[startCell] = new SyncFireVS(eV.CellEs(startCell).FireVE.SRC, startCell, eM);
                _syncRiverSs[startCell] = new SyncRiverVS(eV.CellEs(startCell).RiverE, startCell, eM);
                _syncBarsEnvironmentSs[startCell] = new SyncBarsEnvironmentVS(eV, startCell, eM);

                _syncNeedFoodSs[startCell] = new NeedFoodVS(eV.CellEs(startCell).UnitEs.Block(CellBlockTypes.NeedFood), startCell, _eM);
                _syncBuildingFlagSs[startCell] = new BuildingFlagVS(eV.CellEs(startCell).BuildingEs.FlagSRC, startCell, _eM);
                _syncStunSs[startCell] = new SyncStunVS(eV.CellEs(startCell).UnitEs.EffectE.StunSRC, startCell, _eM);
                _syncShieldSs[startCell] = new SyncShieldVS(eV.CellEs(startCell).UnitEs.EffectE.ShieldSRC, startCell, _eM);

                _syncCloudSs[startCell] = new SyncCloudVS(eV.CellEs(startCell).CloudSRC, startCell, _eM);
                _syncRotationSs[startCell] = new SyncRotationVS(eV.CellEs(startCell), startCell, _eM);

                _syncIdxAndXyInfoSs[startCell] = new SyncIdxAndXyInfoVS(eV.CellEs(startCell).IdxAndXyInfoTMPC, startCell, _eM);



                for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++)
                {
                    _syncTrailSs[dirT][startCell] = new SyncTrailVS(dirT, eV.CellEs(startCell).TrailCellVC(dirT), startCell, eM);
                }
            }

            _updates.Add(new SyncSunSideVS(eV, _eM).Sync);


            _syncSupportS = new SyncSupportVS(eV, _eM);
            _syncSoundS = new SyncSoundVS(eV, _eM);
            _syncCameraS = new SyncCameraVS(eV, _eM);
        }

        public void Update()
        {
            for (byte cellIdxCurrent = 0; cellIdxCurrent < StartValues.CELLS; cellIdxCurrent++)
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

                for (byte currentCellIdx = 0; currentCellIdx < StartValues.CELLS; currentCellIdx++)
                {
                    if (!_eM.IsBorder(currentCellIdx))
                    {
                        _syncBuildingSs[currentCellIdx].Sync();
                        _syncStatsSs[currentCellIdx].Sync();
                        _syncUnitBarHpSs[currentCellIdx].Sync();
                        _syncFrozenArrawSs[currentCellIdx].Sync();
                        _syncEnvironmentSs[currentCellIdx].Sync();
                        _syncFireSs[currentCellIdx].Sync();
                        _syncRiverSs[currentCellIdx].Sync();
                        _syncBarsEnvironmentSs[currentCellIdx].Sync();
                        _syncNeedFoodSs[currentCellIdx].Sync();
                        _syncBuildingFlagSs[currentCellIdx].Sync();
                        _syncStunSs[currentCellIdx].Sync();
                        _syncStunSs[currentCellIdx].Sync();
                        _syncShieldSs[currentCellIdx].Sync();
                        _syncCloudSs[currentCellIdx].Sync();
                        _syncRotationSs[currentCellIdx].Sync();
                        _syncIdxAndXyInfoSs[currentCellIdx].Sync();

                        for (var dirT = (DirectTypes)1; dirT < DirectTypes.End; dirT++) _syncTrailSs[dirT][currentCellIdx].Sync();
                    }
                }

                _syncSupportS.Sync();
                _syncSoundS.Sync();
                _syncCameraS.Sync();


                _eM.NeedUpdateView = false;
            }
        }
    }
}