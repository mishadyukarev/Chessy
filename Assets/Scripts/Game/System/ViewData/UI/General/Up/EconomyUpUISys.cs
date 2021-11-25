using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class EconomyUpUISys : IEcsRunSystem
    {
        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;


            var unitsInGame = 0;

            for (var unit = UnitTypes.First; unit < UnitTypes.End; unit++)
            {
                if (unit != UnitTypes.King)
                {
                    for (var lev = LevelTypes.First; lev < LevelTypes.End; lev++)
                    {
                        unitsInGame += 1;
                    }
                }
            }

            var percUpgFarms = BuildsUpgC.PercUpg(BuildTypes.Farm, curPlayer);
            var amountFarms = WhereBuildsC.Amount(BuildTypes.Farm, curPlayer);
            var amountAddFood = Extractor.GetAddFood(percUpgFarms, amountFarms, unitsInGame);

            if (amountAddFood < 0) EconomyUIC.SetAddText(ResTypes.Food, amountAddFood.ToString());
            else EconomyUIC.SetAddText(ResTypes.Food, "+ " + amountAddFood.ToString());



            var amountWoodcutter = WhereBuildsC.Amount(BuildTypes.Woodcutter, curPlayer);
            var extOneWoodcut = Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Woodcutter, curPlayer));
            var amountAddWood = 0;
            foreach (var idx_0 in WhereUnitsC.Idxs(UnitTypes.Pawn, LevelTypes.First, curPlayer))
            {
                if (EntityDataPool.GetEnvCellC<EnvC>(idx_0).Have(EnvTypes.AdultForest))
                    if (EntityDataPool.GetUnitCellC<ConditionUnitC>(idx_0).Is(CondUnitTypes.Relaxed))
                        amountAddWood += 1;
            }
            foreach (var idx_0 in WhereUnitsC.Idxs(UnitTypes.Pawn, LevelTypes.Second, curPlayer))
            {
                if (EntityDataPool.GetEnvCellC<EnvC>(idx_0).Have(EnvTypes.AdultForest))
                    if (EntityDataPool.GetUnitCellC<ConditionUnitC>(idx_0).Is(CondUnitTypes.Relaxed))
                        amountAddWood += 2;
            }
            amountAddWood += amountWoodcutter * extOneWoodcut;
            EconomyUIC.SetAddText(ResTypes.Wood, "+ " + amountAddWood);


            var amountAddOre = WhereBuildsC.Amount(BuildTypes.Mine, curPlayer)
                * Extractor.GetExtractOneBuild(BuildsUpgC.PercUpg(BuildTypes.Mine, curPlayer));
            EconomyUIC.SetAddText(ResTypes.Ore, "+ " + amountAddOre);




            EconomyUIC.SetMainText(ResTypes.Food, InvResC.AmountRes(ResTypes.Food, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Wood, InvResC.AmountRes(ResTypes.Wood, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Ore, InvResC.AmountRes(ResTypes.Ore, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Iron, InvResC.AmountRes(ResTypes.Iron, curPlayer).ToString());
            EconomyUIC.SetMainText(ResTypes.Gold, InvResC.AmountRes(ResTypes.Gold, curPlayer).ToString());

        }
    }
}