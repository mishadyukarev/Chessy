using ECS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityCenterFriendUIPool
    {
        static Entity _entity;

        public static ref C Friend<C>() where C : struct => ref _entity.Get<C>();

        static EntityCenterFriendUIPool()
        {

        }
        public EntityCenterFriendUIPool(in EcsWorld gameW, in Transform centerZone)
        {
            var friendZone = centerZone.Find("FriendZone");

            _entity = gameW.NewEntity()
                .Add(new TextUIC(friendZone.Find("WhoseMotion_TextMP").GetComponent<TextMeshProUGUI>()))
                .Add(new ButtonUIC(friendZone.Find("Ready_Button").GetComponent<Button>()));
        }
    }
}