using ECS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct UIEntDownDoner
    {
        static Entity _doner;
        static Entity _wait;

        public static ref C Doner<C>() where C : struct => ref _doner.Get<C>();
        public static ref C Wait<C>() where C : struct => ref _wait.Get<C>();

        public UIEntDownDoner(in EcsWorld gameW, in Transform downZone)
        {
            _doner = gameW.NewEntity()
                .Add(new ButtonUIC(downZone.Find("DonerButton").GetComponent<Button>()));       

            _wait = gameW.NewEntity()
                .Add(new GameObjectVC(downZone.Find("WaitZone").gameObject));
        }
    }
}
