using Chessy.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.Menu
{
    public struct OfflineZoneUIE
    {
        public readonly ButtonUIC JoinButtonC;
        public readonly ImageUIC FrontImageC;

        public readonly ButtonUIC TrainingButtonC;
        public readonly ButtonUIC WithFriendButtonC;


        public OfflineZoneUIE(RectTransform leftZoneRectTrans)
        {
            JoinButtonC = new ButtonUIC(leftZoneRectTrans.Find("ConnectOffline_Button").GetComponent<Button>());
            FrontImageC = new ImageUIC(leftZoneRectTrans.Find("LeftZone_Image").GetComponent<Image>());

            TrainingButtonC = new ButtonUIC(leftZoneRectTrans.Find("Training_Button").GetComponent<Button>());
            WithFriendButtonC = new ButtonUIC(leftZoneRectTrans.Find("WithFriend_Button").GetComponent<Button>());


        }
    }
}
