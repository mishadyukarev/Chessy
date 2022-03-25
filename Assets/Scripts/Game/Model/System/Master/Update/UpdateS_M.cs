using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Photon.Pun;

namespace Chessy.Game.System.Model.Master
{
    public sealed class UpdateS_M : SystemModelGameAbs
    {
        readonly SystemsModelGame _sMGame;

        public UpdateS_M(in SystemsModelGame sMGame, in EntitiesModelGame eMGame) : base(eMGame)
        {
            _sMGame = sMGame;
        }

        public void Run(in GameModeTC gameModeTC)
        {
            new UpdatorMS().Run(gameModeTC, eMGame);

            FireUpdateMS.Run(_sMGame, eMGame);
            new RiverFertilizeAroundUpdateMS(eMGame).Run();
            new WorldDryFertilizerMS(eMGame).Run();
            new CitiesAddPeopleUpdateMS(eMGame).Run();
            _sMGame.MasterSystems.WorldMeltIceWallUpdateS_M.Run();

            new CloudUpdMS(eMGame).Run();
            new CloudFertilizeUpdMS(eMGame).Run();


            #region Building

            new WoodcutterExtractAdultForestUpdateMS(_sMGame.TakeAdultForestResourcesS, eMGame).Run();
            new FarmExtractFertilizeUpdateMS(eMGame).Run();
            new MineExtractUpdateMS(eMGame).Run();
            new IceWallGiveWaterUnitsUpdMS(eMGame).Run();
            new IceWallFertilizeAroundUpdateMS(eMGame).Run();

            #endregion


            #region Environment



            #endregion


            #region Unit

            new PawnExtractAdultForestMS(_sMGame.TakeAdultForestResourcesS, _sMGame.BuildS, eMGame).Run();
            new ResumeUnitUpdMS(eMGame).Run();
            new UpdateHealingUnitMS(eMGame).Run();
            _sMGame.MasterSystems.UnitEatFoodUpdateS_M.Run();
            ThirstyUnitsUpdateMS.Run(gameModeTC, _sMGame, eMGame);
            new PawnExtractHillUpdateMS(eMGame).Run();

            new UpdTryFireAroundHellMS(eMGame).Run();
            new UpdAttackFromWaterHellMS(eMGame).Run();

            new UpdGiveWaterCloudScowyMS(eMGame).Run();

            new CamelShiftUpdateMS(eMGame).Run();

            new CamelSpawnUpdateS_M().SpawnCamelUpdate(eMGame);

            #endregion



            if (eMGame.MotionsC.Motions % 5 == 0)
            {
                for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    eMGame.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                    if (eMGame.CellEs(cell_0).IsActiveParentSelf)
                    {
                        if (eMGame.UnitTC(cell_0).HaveUnit)
                        {
                            if (eMGame.PlayerInfoE(eMGame.UnitPlayerTC(cell_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                if (eMGame.UnitTC(cell_0).Is(UnitTypes.Pawn))
                                {
                                    if (eMGame.UnitMainTWTC(cell_0).Is(ToolWeaponTypes.BowCrossbow))
                                    {
                                        eMGame.UnitEffectFrozenArrawC(cell_0).Shoots++;
                                    }
                                    else
                                    {
                                        eMGame.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                                else
                                {
                                    eMGame.UnitEffectShield(cell_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                }
                            }
                        }
                        else
                        {
                            if (eMGame.AdultForestC(cell_0).HaveAnyResources)
                            {
                                if (!eMGame.HaveTreeUnit)
                                {
                                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                    {
                                        if (eMGame.PlayerInfoE(playerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {
                                            _sMGame.UnitSystems.SetNewUnitS.Set(UnitTypes.Tree, playerT, cell_0);

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            TryInvokeTruceUpdateMS.Truce(gameModeTC, eMGame);
        }
    }
}