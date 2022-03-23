using Chessy.Common;
using Chessy.Game.Entity.Model;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Photon.Pun;

namespace Chessy.Game.System.Model.Master
{
    public struct UpdateS_M
    {
        public void UpdateMove(in GameModeTC gameModeTC, in SystemsModelGame sMM, in EntitiesModelGame e)
        {
            new UpdatorMS().Run(gameModeTC, e);

            FireUpdateMS.Run(sMM, e);
            new RiverFertilizeAroundUpdateMS(e).Run();
            new WorldDryFertilizerMS(e).Run();
            new CitiesAddPeopleUpdateMS(e).Run();
            sMM.WorldMeltIceWallUpdateS_M.Run(e);

            new CloudUpdMS(e).Run();
            new CloudFertilizeUpdMS(e).Run();


            #region Building

            new WoodcutterExtractAdultForestUpdateMS(e).Run();
            new FarmExtractFertilizeUpdateMS(e).Run();
            new MineExtractUpdateMS(e).Run();
            new IceWallGiveWaterUnitsUpdMS(e).Run();
            new IceWallFertilizeAroundUpdateMS(e).Run();

            #endregion


            #region Environment



            #endregion


            #region Unit

            new PawnExtractAdultForestMS(e).Run();
            new ResumeUnitUpdMS(e).Run();
            new UpdateHealingUnitMS(e).Run();
            sMM.UnitEatFoodUpdateS_M.Run(sMM, e);
            ThirstyUnitsUpdateMS.Run(gameModeTC, sMM, e);
            new PawnExtractHillUpdateMS(e).Run();

            new UpdTryFireAroundHellMS(e).Run();
            new UpdAttackFromWaterHellMS(e).Run();

            new UpdGiveWaterCloudScowyMS(e).Run();

            new CamelShiftUpdateMS(e).Run();

            new CamelSpawnUpdateS_M().SpawnCamelUpdate(e);

            #endregion



            if (e.MotionsC.Motions % 5 == 0)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    e.RpcPoolEs.SoundToGeneral(RpcTarget.All, AbilityTypes.GrowAdultForest);


                    if (e.CellEs(idx_0).IsActiveParentSelf)
                    {
                        if (e.UnitTC(idx_0).HaveUnit)
                        {
                            if (e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player).MyHeroTC.Is(UnitTypes.Snowy))
                            {
                                if (e.UnitTC(idx_0).Is(UnitTypes.Pawn))
                                {
                                    if (e.UnitMainTWTC(idx_0).Is(ToolWeaponTypes.BowCrossbow))
                                    {
                                        e.UnitEffectFrozenArrawC(idx_0).Shoots++;
                                    }
                                    else
                                    {
                                        e.UnitEffectShield(idx_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                                else
                                {
                                    e.UnitEffectShield(idx_0).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                }
                            }
                        }
                        else
                        {
                            if (e.AdultForestC(idx_0).HaveAnyResources)
                            {
                                if (!e.HaveTreeUnit)
                                {
                                    for (var playerT = PlayerTypes.None + 1; playerT < PlayerTypes.End; playerT++)
                                    {
                                        if (e.PlayerInfoE(playerT).MyHeroTC.Is(UnitTypes.Elfemale))
                                        {

                                            e.UnitEs(idx_0).SetNewUnitHere(UnitTypes.Tree, playerT, e.PlayerInfoE(playerT), e);

                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            TryInvokeTruceUpdateMS.Truce(gameModeTC, e);
        }
    }
}