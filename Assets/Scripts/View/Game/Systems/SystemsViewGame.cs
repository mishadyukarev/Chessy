using Chessy.Common;
using Chessy.Common.Entity;
using Chessy.Common.Entity.View;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.View.System;
using UnityEngine;

namespace Chessy.Game.System.View
{
    public sealed class SystemsViewGame : IEcsRunSystem
    {
        readonly SyncNoneVisionS SyncNoneVisionS;
        readonly NeedFoodS SyncNeedFoodS;
        readonly BuildingFlagVS SyncBuildingFlagS;
        readonly SyncTrailVS SyncTrailS;
        readonly SyncBarsEnvironmentVS SyncBarsEnvironmentS;
        readonly SyncRiverVS SyncRiverS;
        readonly SyncFireVS SyncFireS;
        readonly SyncEnvironmentVS SyncEnvironmentS;
        readonly SyncStatsVS SyncStatsS;
        readonly SyncUnitVS SyncUnitVS;
        readonly SyncPawnVS SyncPawnS;

        readonly EntitiesViewGame _eVGame;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesViewCommon _eVCommon;
        readonly EntitiesModelCommon _eMCommon;



        public SystemsViewGame(in EntitiesViewGame eVGame, in EntitiesModelGame eMGame, in EntitiesViewCommon eVCommon, in EntitiesModelCommon eMCommon)
        {
            _eVGame = eVGame;
            _eMGame = eMGame;
            _eVCommon = eVCommon;
            _eMCommon = eMCommon;


            SyncUnitVS = new SyncUnitVS(eVGame, eMGame);

        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMGame.UnitNeedUpdateViewC(cell_0).NeedUpdateView)
                {
                    SyncUnitVS.Sync(cell_0);
                    _eMGame.UnitNeedUpdateViewC(cell_0).NeedUpdateView = false;
                }
            }

            if (_eMGame.NeedUpdateView)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    if (_eMGame.CellE(cell_0).IsActiveParentSelf)
                    {
                        SyncUnitVS.Sync(cell_0);

                        SyncBuildingVS.Sync(cell_0, _eVGame, _eMGame);
                        SyncStatsS.Sync(cell_0, _eVGame, _eMGame);
                        SyncEnvironmentS.Run(cell_0, _eVGame, _eMGame);
                        SyncFireS.Sync(cell_0, _eVGame, _eMGame);
                        SyncRiverS.Sync(cell_0, _eVGame, _eMGame);
                        SyncBarsEnvironmentS.Sync(cell_0, _eVGame, _eMGame);
                        SyncTrailS.Sync(cell_0, _eVGame, _eMGame);
                        SyncNoneVisionS.Sync(cell_0, _eVGame.CellEs(cell_0).SupportCellEs.NoneSRC, _eMGame);
                        SyncNeedFoodS.Sync(cell_0, _eVGame.CellEs(cell_0).UnitVEs.NeedFoodSRC, _eMGame);
                        SyncBuildingFlagS.Sync(_eVGame.BuildingEs(cell_0).FlagSRC, cell_0, _eMGame);


                        _eVGame.CellEs(cell_0).UnitVEs.EffectVEs.SyncVision(_eMGame.UnitEs(cell_0), cell_0 == _eMGame.CellsC.Selected, _eMGame);
                        SyncStunVS.Sync(cell_0, _eVGame, _eMGame);
                        ShieldVS.Run(cell_0, _eVGame, _eMGame);
                    }
                }

                SoundVS.Sync(_eVGame);
                SupportVS.Sync(_eMGame, _eVGame);
                CloudVS.Run(_eVGame, _eMGame);
                RotateAllVS.Rotate(_eVGame, _eMGame, _eVCommon);
            }
        }
    }
}