using ECS;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct LeftMarketUIEs
    {
        public readonly LeftMarketButtonUIE FoodToWood;
        public readonly LeftMarketButtonUIE WoodToFood;
        public readonly LeftMarketButtonUIE GoldToFood;
        public readonly LeftMarketButtonUIE GoldToWood;

        public readonly GameObjectVC Zone;

        internal LeftMarketUIEs(in Transform leftZone, in EcsWorld gameW)
        {
            var marketZone = leftZone.Find("Market+");


            Zone = new GameObjectVC(marketZone.gameObject);

            FoodToWood = new LeftMarketButtonUIE(marketZone.Find("FoodToWood+").Find("Button+").GetComponent<Button>(), gameW);
            WoodToFood = new LeftMarketButtonUIE(marketZone.Find("WoodToFood+").Find("Button+").GetComponent<Button>(), gameW);
            GoldToFood = new LeftMarketButtonUIE(marketZone.Find("GoldToFood+").Find("Button+").GetComponent<Button>(), gameW);
            GoldToWood = new LeftMarketButtonUIE(marketZone.Find("GoldToWood+").Find("Button+").GetComponent<Button>(), gameW);
        }
    }
}