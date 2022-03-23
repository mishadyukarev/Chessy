using Chessy.Common.Entity.View;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using UnityEngine;

namespace Chessy.Game.System.View
{
    public struct SystemsViewGame : IEcsRunSystem
    {
        float _timerForUpdate;

        readonly EntitiesViewGame _eVGame;
        readonly EntitiesModelGame _eMGame;
        readonly EntitiesViewCommon _eVCommon;

        public readonly SyncNoneVisionS SyncNoneVisionS;
        public readonly NeedFoodS SyncNeedFoodS;
        public readonly BuildingFlagVS SyncBuildingFlagS;
        public readonly SyncTrailVS SyncTrailS;
        public readonly SyncBarsEnvironmentVS SyncBarsEnvironmentS;
        public readonly SyncRiverVS SyncRiverS;
        public readonly SyncFireVS SyncFireS;
        public readonly SyncEnvironmentVS SyncEnvironmentS;
        public readonly SyncStatsVS SyncStatsS;

        public SystemsViewGame(in EntitiesViewGame eVGame, in EntitiesModelGame eMGame, in EntitiesViewCommon eVCommon) : this()
        {
            _eVGame = eVGame;
            _eMGame = eMGame;
            _eVCommon = eVCommon;
        }

        public void Run()
        {
            _timerForUpdate += Time.deltaTime;

            if (_timerForUpdate >= 0.04f)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    SyncUnitVS.Sync(idx_0, _eVGame, _eMGame);
                    SyncBuildingVS.Sync(idx_0, _eVGame, _eMGame);
                    SyncStatsS.Sync(idx_0, _eVGame, _eMGame);
                    SyncEnvironmentS.Run(idx_0, _eVGame, _eMGame);
                    SyncFireS.Sync(idx_0, _eVGame, _eMGame);
                    SyncRiverS.Sync(idx_0, _eVGame, _eMGame);
                    SyncBarsEnvironmentS.Sync(idx_0, _eVGame, _eMGame);
                    SyncTrailS.Sync(idx_0, _eVGame, _eMGame);
                    SyncNoneVisionS.Sync(idx_0, _eVGame.CellEs(idx_0).SupportCellEs.NoneSRC, _eMGame);
                    SyncNeedFoodS.Sync(idx_0, _eVGame.CellEs(idx_0).UnitVEs.NeedFoodSRC, _eMGame);
                    SyncBuildingFlagS.Sync(_eVGame.BuildingEs(idx_0).FlagSRC, idx_0, _eMGame);


                    _eVGame.CellEs(idx_0).UnitVEs.EffectVEs.SyncVision(_eMGame.UnitEs(idx_0), idx_0 == _eMGame.CellsC.Selected, _eMGame);
                    SyncStunVS.Sync(idx_0, _eVGame, _eMGame);
                    ShieldVS.Run(idx_0, _eVGame, _eMGame);
                }

                SoundVS.Sync(_eVGame);
                SupportVS.Sync(_eMGame, _eVGame);
                CloudVS.Run(_eVGame, _eMGame);
                RotateAllVS.Rotate(_eVGame, _eMGame, _eVCommon);


                _timerForUpdate = 0;
            }
        }

    }
}