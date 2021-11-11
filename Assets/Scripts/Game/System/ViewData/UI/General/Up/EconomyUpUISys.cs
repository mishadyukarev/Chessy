using Leopotam.Ecs;
using Photon.Pun;

namespace Chessy.Game
{
    public sealed class EconomyUpUISys : IEcsRunSystem
    {
        private readonly EcsFilter<EnvC> _cellEnvFilt = default;
        private readonly EcsFilter<HpC, ConditionUnitC> _cellUnitFilt = default;

        public void Run()
        {
            var curPlayer = WhoseMoveC.CurPlayerI;

            var amountUnitsInGame = WhereUnitsC.AmountUnitsExcept(curPlayer, UnitTypes.King);
            var haveUpgFarms = BuildsUpgC.HaveUpgrade(curPlayer, BuildTypes.Farm);
            var amountFarms = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Farm);
            var amountAddFood = ExtractC.GetAddFood(haveUpgFarms, amountFarms, amountUnitsInGame);

            if (amountAddFood < 0) EconomyViewUIC.SetAddText(ResTypes.Food, amountAddFood.ToString());
            else EconomyViewUIC.SetAddText(ResTypes.Food, "+ " + amountAddFood.ToString());



            var amountWoodcutter = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Woodcutter);
            var extOneWoodcut = ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(curPlayer, BuildTypes.Woodcutter));
            var amountAddWood = 0;
            foreach (var idx_0 in WhereUnitsC.IdxsUnits(curPlayer, UnitTypes.Pawn, LevelUnitTypes.First))
            {
                if (_cellEnvFilt.Get1(idx_0).Have(EnvTypes.AdultForest)) 
                    if (_cellUnitFilt.Get2(idx_0).Is(CondUnitTypes.Relaxed))
                            amountAddWood += 1;
            }
            foreach (var idx_0 in WhereUnitsC.IdxsUnits(curPlayer, UnitTypes.Pawn, LevelUnitTypes.Second))
            {
                if (_cellEnvFilt.Get1(idx_0).Have(EnvTypes.AdultForest))
                    if(_cellUnitFilt.Get2(idx_0).Is(CondUnitTypes.Relaxed)) 
                        amountAddWood += 2;
            }
            amountAddWood += amountWoodcutter * extOneWoodcut;
            EconomyViewUIC.SetAddText(ResTypes.Wood, "+ " + amountAddWood);


            var amountAddOre = WhereBuildsC.AmountBuilds(curPlayer, BuildTypes.Mine)
                * ExtractC.GetExtractOneBuild(BuildsUpgC.HaveUpgrade(curPlayer, BuildTypes.Mine));
            EconomyViewUIC.SetAddText(ResTypes.Ore, "+ " + amountAddOre);




            EconomyViewUIC.SetMainText(ResTypes.Food, InventResC.AmountRes(curPlayer, ResTypes.Food).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Wood, InventResC.AmountRes(curPlayer, ResTypes.Wood).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Ore, InventResC.AmountRes(curPlayer, ResTypes.Ore).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Iron, InventResC.AmountRes(curPlayer, ResTypes.Iron).ToString());
            EconomyViewUIC.SetMainText(ResTypes.Gold, InventResC.AmountRes(curPlayer, ResTypes.Gold).ToString());

        }
    }
}