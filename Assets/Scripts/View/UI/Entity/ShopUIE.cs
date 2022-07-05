using Chessy.View.Component;
using Chessy.View.UI.Component;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct ShopUIE
    {
        public readonly GameObjectVC ShopZoneGOC;
        public readonly ButtonUIC BuyButtonC;
        public readonly ButtonUIC ExitButtonC;

        public ShopUIE(in Transform shopZone)
        {
            ShopZoneGOC = new GameObjectVC(shopZone.gameObject);
            BuyButtonC = new ButtonUIC(shopZone.Find("Buy_Button").GetComponent<Button>());
            ExitButtonC = new ButtonUIC(shopZone.Find("Exit_Button").GetComponent<Button>());
        }
    }
}