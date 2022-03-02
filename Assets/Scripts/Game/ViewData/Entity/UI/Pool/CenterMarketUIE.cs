using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct CenterMarketUIE
    {
        public readonly ButtonUIC FoodToWood;
        public readonly ButtonUIC WoodToFood;
        public readonly ButtonUIC GoldToFood;
        public readonly ButtonUIC GoldToWood;

        public readonly GameObjectVC Zone;

        internal CenterMarketUIE(in Transform leftZone)
        {
            var marketZone = leftZone.Find("Market+");


            Zone = new GameObjectVC(marketZone.gameObject);

            FoodToWood = new ButtonUIC(marketZone.Find("FoodToWood+").Find("Button+").GetComponent<Button>());
            WoodToFood = new ButtonUIC(marketZone.Find("WoodToFood+").Find("Button+").GetComponent<Button>());
            GoldToFood = new ButtonUIC(marketZone.Find("GoldToFood+").Find("Button+").GetComponent<Button>());
            GoldToWood = new ButtonUIC(marketZone.Find("GoldToWood+").Find("Button+").GetComponent<Button>());
        }
    }
}