using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Effect;
using Photon.Pun;

namespace Chessy.Game.System.Model.Master
{
    public static class UpdateS_M
    {
        public static void UpdateMove(in SystemsModel sMM, in EntitiesModel e)
        {
            new UpdatorMS(e).Run();

            FireUpdateMS.Run(sMM, e);
            new RiverFertilizeAroundUpdateMS(e).Run();
            new WorldDryFertilizerMS(e).Run();
            new CitiesAddPeopleUpdateMS(e).Run();
            new WorldMeltIceWallUpdateMS(e).Run();

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
            UnitEatFoodUpdateS_M.Run(sMM, e);
            ThirstyUnitsUpdateMS.Run(sMM, e);
            new PawnExtractHillUpdateMS(e).Run();

            new UpdTryFireAroundHellMS(e).Run();
            new UpdAttackFromWaterHellMS(e).Run();

            new UpdGiveWaterCloudScowyMS(e).Run();

            new CamelShiftUpdateMS(e).Run();
            e.SpawnCamelUpdate();

            #endregion



            if (e.MotionsC.Motions % 3 == 0)
            {
                for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
                {
                    //e.RpcPoolEs.SoundToGeneral(RpcTarget.All, ClipTypes.AfterBuildTown);

                    if (e.UnitTC(idx_0).Is(UnitTypes.Snowy))
                    {
                        foreach (var idx_1 in e.CellEs(idx_0).IdxsAround)
                        {
                            if (e.UnitTC(idx_1).HaveUnit)
                            {
                                if (e.UnitPlayerTC(idx_1).Player == e.UnitPlayerTC(idx_0).Player)
                                {
                                    if (e.UnitTC(idx_1).Is(UnitTypes.Pawn))
                                    {
                                        if (e.UnitMainTWTC(idx_1).Is(ToolWeaponTypes.BowCrossbow))
                                        {
                                            e.UnitEffectFrozenArrawC(idx_1).Shoots++;
                                        }
                                        else
                                        {
                                            e.UnitEffectShield(idx_1).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                        }
                                    }
                                    else
                                    {
                                        e.UnitEffectShield(idx_1).Protection = ShieldValues.AFTER_DIRECT_WAVE;
                                    }
                                }
                            }
                        }
                    }
                    else if (e.UnitTC(idx_0).Is(UnitTypes.Elfemale))
                    {
                        foreach (var idx_1 in e.CellEs(idx_0).IdxsAround)
                        {
                            if (!e.UnitTC(idx_1).HaveUnit)
                            {
                                if (e.AdultForestC(idx_1).HaveAnyResources)
                                {
                                    e.UnitEs(idx_1).SetNewUnitHere(UnitTypes.Pawn, e.UnitPlayerTC(idx_0).Player, e.PlayerInfoE(e.UnitPlayerTC(idx_0).Player));
                                }
                            }
                        }
                    }
                }
            }
            

            e.Truce();
        }
    }
}