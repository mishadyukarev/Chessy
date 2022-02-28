using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct RightUniqueUIE
    {
        public GameObjectVC Paren;
        public ButtonUIC Button;
        public TextUIC TextUIC;
        public ImageUIC ImageC;

        public RightUniqueUIE(in Transform button)
        {
            Paren = new GameObjectVC(button.gameObject);
            Button = new ButtonUIC(button.Find("Button").GetComponent<Button>());
            TextUIC = new TextUIC(button.Find("Cooldown").Find("Text (TMP)").GetComponent<TextMeshProUGUI>());
            ImageC = new ImageUIC(button.Find("Ability_Image").GetComponent<Image>());
        }
    }
}