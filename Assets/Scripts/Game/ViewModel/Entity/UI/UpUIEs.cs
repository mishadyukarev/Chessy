using Chessy.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Game
{
    public readonly struct UpUIEs
    {
        public readonly UpEconomyUIE EconomyE;
        public readonly UpSunsUIEs SunsE;

        public readonly ButtonUIC LeaveC;
        public readonly ButtonUIC AlphaC;

        public readonly ButtonUIC WindButtonC;
        public readonly TransformVC WindTrC;
        public readonly ImageUIC WindC;
        public readonly TextUIC WindTextC;
        public readonly TextUIC MotionsTextC;

        public UpUIEs(in Button leaveButton, in Transform upZone)
        {
            EconomyE = new UpEconomyUIE(upZone);
            SunsE = new UpSunsUIEs(upZone);


            LeaveC = new ButtonUIC(leaveButton);

            var windZone = upZone.Find("Wind+");


            WindButtonC = new ButtonUIC(windZone.Find("Button+").GetComponent<Button>());

            var image = windZone.Find("Direct_Image").GetComponent<Image>();

            WindTrC = new TransformVC(image.transform);
            WindC = new ImageUIC(image);
            WindTextC = new TextUIC(windZone.Find("Text_TMP+").GetComponent<TextMeshProUGUI>());


            AlphaC = new ButtonUIC(upZone.Find("Alpha_Button").GetComponent<Button>());

            MotionsTextC = new TextUIC(upZone.Find("Motions_TMP+").GetComponent<TextMeshProUGUI>());
        }
    }
}