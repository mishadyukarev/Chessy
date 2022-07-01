using Chessy.Model;
using Chessy.View.UI.Component;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chessy.View.UI.Entity
{
    public struct OnlineZoneUIE
    {
        public readonly ButtonUIC JoinButtonC;

        public readonly ImageUIC FronImageC;

        public readonly ButtonUIC CreatePublicRoomButtonC;
        public readonly ButtonUIC JoinRandomPublicRoomButtonC;

        public readonly ButtonUIC CreateFriendRoomButtonC;
        public readonly TMP_InputFieldUIC CreateFriendRoomInputFieldC;

        public readonly ButtonUIC JoinFriendRoomButtonC;
        public readonly TMP_InputFieldUIC JoinFriendRoomInputFieldC;



        public OnlineZoneUIE(in RectTransform rightZone)
        {
            JoinButtonC = new ButtonUIC(rightZone.Find("ConnectOnline_Button").GetComponent<Button>());



            FronImageC = new ImageUIC(rightZone.Find("RightZone_Image").GetComponent<Image>());

            var randomZone = rightZone.Find("Random+");

            CreatePublicRoomButtonC = new ButtonUIC(randomZone.transform.Find("CreateRoomButton").GetComponent<Button>());
            JoinRandomPublicRoomButtonC = new ButtonUIC(randomZone.transform.Find("JoinRandom_Button").GetComponent<Button>());


            var friendZone = rightZone.Find("Friend+");

            CreateFriendRoomButtonC = new ButtonUIC(friendZone.transform.Find("CreateFriendRoom_Button").GetComponent<Button>());
            CreateFriendRoomInputFieldC = new TMP_InputFieldUIC(friendZone.transform.Find("CreateFriendRoom_InputField").GetComponent<TMP_InputField>());

            JoinFriendRoomButtonC = new ButtonUIC(friendZone.transform.Find("JoinFriendRoom_Button").GetComponent<Button>());
            JoinFriendRoomInputFieldC = new TMP_InputFieldUIC(friendZone.transform.Find("JoinFriendRoom_InputField").GetComponent<TMP_InputField>());


        }
    }
}
