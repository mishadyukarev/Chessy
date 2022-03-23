using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Photon.Pun;

namespace Chessy.Game.System.Model.Master
{
    public readonly struct UpdateS_M
    {
        readonly SystemsModelGame _sMGame;
        readonly EntitiesModelGame _eMGame;

        public UpdateS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame)
        {
            _sMGame = sMGame;
            _eMGame = eMGame;
        }


        public void Run(in GameModeTC gameModeTC)
        {
            new UpdatorMS().Run(gameModeTC, _eMGame);

            FireUpdateMS.Run(_sMGame, _eMGame);
            new RiverFertilizeAroundUpdateMS(_eMGame).Run();
            new WorldDryFertilizerMS(_eMGame).Run();
            new CitiesAddPeopleUpdateMS(_eMGame).Run();
            _sMGame.WorldMeltIceWallUpdateS_M.Run(_eMGame);

            new CloudUpdMS(_eMGame).Run();
            new CloudFertilizeUpdMS(_eMGame).Run();


            #region Building

            new WoodcutterExtractAdultForestUpdateMS(_eMGame).Run();
            new FarmExtractFertilizeUpdateMS(_eMGame).Run();
            new MineExtractUpdateMS(_eMGame).Run();
            new IceWallGiveWaterUnitsUpdMS(_eMGame).Run();
            new IceWallFertilizeAroundUpdateMS(_eMGame).Run();

            #endregion


            #region Environment



            #endregion


            #region Unit

            new PawnExtractAdultForestMS(_eMGame).Run();
            new ResumeUnitUpdMS(_eMGame).Run();
            new UpdateHealingUnitMS(_eMGame).Run();
            _sMGame.UnitEatFoodUpdateS_M.Run(_sMGame, _eMGame);
            ThirstyUnitsUpdateMS.Run(gameModeTC, _sMGame, _eMGame);
            new PawnExtractHillUpdateMS(_eMGame).Run();

            new UpdTryFireAroundHellMS(_eMGame).Run();
            new UpdAttackFromWaterHellMS(_eMGame).Run();

            new UpdGiveWaterCloudScowyMS(_eMGame).Run();

            new CamelShiftUpdateMS(_eMGame).Run();

            new CamelSpawnUpdateS_M().SpawnCamelUpdate(_eMGame);

            #endregion



            if (_eMGame.MotionsC.Motions % 5 == 0)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    _eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                    if (_eMGame.CellEs(idx_0).IsActiveParentSelf)
                    {
                        if (_eMGame.UnitTC(idx_0).HaveUnit)
                        {
                            if (_eMGame.PlayerInfoE(_eMGame.UnitPlayerTC(idx_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                if (_eMGame.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                {
                                    if (_eMGame.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                    {
                                        _eMGame.UnitEffectFrozenArrawC(idx_0).Shoots++;
                                    }
                                    else
                                    {
                                        _eMGame.UnitEffectShield(idx_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                                else
                                {
                                    _eMGame.UnitEffectShield(idx_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                }
                            }
                        }
                        else
                        {
                            if (_eMGame.AdultForestC(idx_0).HaveAnyResources)
                            {
                                if (!_eMGame.HaveTreeUnit)
                                {
                                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                    {
                                        if (_eMGame.PlayerInfoE(playerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {

                                            _eMGame.UnitEs(idx_0).SetNewUnitHere(UnitTypes.Tree, playerT, _eMGame.PlayerInfoE(playerT), _eMGame);

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            TryInvokeTruceUpdateMS.Truce(gameModeTC, _eMGame);
        }
    }
}