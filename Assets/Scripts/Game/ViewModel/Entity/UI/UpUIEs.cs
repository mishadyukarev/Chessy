using Chessy.Common;
using TMPro;
using UnityEngine.UI;

namespace Chessy.Game
{
    public struct UpUIEs
    {
        public UpEconomyUIE EconomyE;
        public UpSunsUIEs SunsE;

        public ButtonUIC LeaveC;
        public ButtonUIC AlphaC;

        public TransformVC WindTrC;
        public ImageUIC WindC;
        public TextUIC WindTextC;

        public UpUIEs(in bool def)
        {
            var upZone = CanvasC.FindUnderCurZone("UpZone").transform;
            EconomyE = new UpEconomyUIE(upZone);
            SunsE = new UpSunsUIEs(upZone);


            LeaveC = new ButtonUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave"));

            var windZone = upZone.Find("WindZone");
            var image = windZone.Find("Direct_Image").GetComponent<Image>();

            WindTrC = new TransformVC(image.transform);
            WindC = new ImageUIC(image);
            WindTextC = new TextUIC(windZone.Find("Text_TMP+").GetComponent<TextMeshProUGUI>());


            AlphaC = new ButtonUIC(upZone.Find("Alpha_Button").GetComponent<Button>());
        }
    }
}