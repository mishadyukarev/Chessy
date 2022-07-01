using Chessy.Model;
using Chessy.View.Component;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public readonly struct UpUIEs
    {
        public readonly UpEconomyUIE EconomyE;
        public readonly SunsUIE SunsE;

        public readonly ButtonUIC LeaveButtonC;
        public readonly ButtonUIC SettingsButtonC;

        public readonly GameObjectVC ParentWindGOC;
        public readonly ButtonUIC WindButtonC;
        public readonly TransformVC WindTrC;
        public readonly ImageUIC WindC;
        public readonly TextUIC WindTextC;
        public readonly TextUIC MotionsTextC;

        internal readonly ButtonUIC DiscordButtonC;

        public UpUIEs(in Button leaveButton, in Transform upZone)
        {
            EconomyE = new UpEconomyUIE(upZone);
            SunsE = new SunsUIE(upZone);


            LeaveButtonC = new ButtonUIC(leaveButton);
            SettingsButtonC = new ButtonUIC(upZone.Find("Settings+").Find("Button+").GetComponent<Button>());

            var windZone = upZone.Find("Wind+");

            ParentWindGOC = new GameObjectVC(windZone.gameObject);

            WindButtonC = new ButtonUIC(windZone.Find("Button+").GetComponent<Button>());

            var image = windZone.Find("Direct_Image").GetComponent<Image>();

            WindTrC = new TransformVC(image.transform);
            WindC = new ImageUIC(image);
            WindTextC = new TextUIC(windZone.Find("Text_TMP+").GetComponent<TextMeshProUGUI>());


            MotionsTextC = new TextUIC(upZone.Find("Motions_TMP+").GetComponent<TextMeshProUGUI>());
            DiscordButtonC = new ButtonUIC(upZone.Find("Discord_Button+").GetComponent<Button>());
        }
    }
}