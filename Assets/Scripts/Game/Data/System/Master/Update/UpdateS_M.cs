namespace Chessy.Game.System.Model.Master
{
    public static class UpdateS_M
    {
        public static void Update(in EntitiesModel e)
        {

            new UpdatorMS(e).Run();

            new FireUpdateMS(e).Run();
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
            new UnitEatFoodUpdateS_M(e).Run();
            new ThirstyUnitsUpdateMS(e).Run();
            new PawnExtractHillUpdateMS(e).Run();

            new UpdTryFireAroundHellMS(e).Run();
            new UpdAttackFromWaterHellMS(e).Run();

            new UpdGiveWaterCloudScowyMS(e).Run();

            new CamelShiftUpdateMS(e).Run();
            new CamelSpawnUpdateMS(e).Run();

            #endregion




            for (byte idx_0 = 0; idx_0 < StartValues.CELLS; idx_0++)
            {
                //ElfemaleSeedS_M.TrySeed(idx_0, e);
            }





            TryInvokeTruceUpdateMS.Run(e);
        }
    }
}