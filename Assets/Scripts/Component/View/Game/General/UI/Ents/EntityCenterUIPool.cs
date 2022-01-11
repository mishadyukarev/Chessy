using ECS;
using Game.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityCenterUIPool
    {
        readonly static Dictionary<CenterEntTypes, Entity> _ents;

        public static ref T EndGame<T>() where T : struct => ref _ents[CenterEntTypes.EndGame].Get<T>();
        public static ref T Motion<T>() where T : struct => ref _ents[CenterEntTypes.Motion].Get<T>();
        public static ref T JoinDiscord<T>() where T : struct => ref _ents[CenterEntTypes.JoinDiscord].Get<T>();
        public static ref T Ready<T>() where T : struct => ref _ents[CenterEntTypes.Ready].Get<T>();


        static EntityCenterUIPool()
        {
            _ents = new Dictionary<CenterEntTypes, Entity>();

            for (var type = CenterEntTypes.Start; type <= CenterEntTypes.End; type++)
            {
                _ents.Add(type, default);
            }
        }
        public EntityCenterUIPool(in WorldEcs curGameW, in Transform centerZone)
        {
            _ents[CenterEntTypes.EndGame] = curGameW.NewEntity()
                .Add(new EndGameUIEC())
                .Add(new TextUIC(centerZone.Find("TheEndGameZone").transform.Find("TheEndGame_TextMP").GetComponent<TextMeshProUGUI>()));

            _ents[CenterEntTypes.Motion] = curGameW.NewEntity()
                .Add(new MotionsUIEC())
                .Add(new TextUIC(centerZone.Find("MotionZone").Find("MotionText").GetComponent<TextMeshProUGUI>()));


            var readyZone = centerZone.Find("ReadyZone");

            _ents[CenterEntTypes.JoinDiscord] = curGameW.NewEntity()
                .Add(new ButtonVC(readyZone.Find("JoinDiscordButton").GetComponent<Button>()));

            _ents[CenterEntTypes.Ready] = curGameW.NewEntity()
                .Add(new ButtonVC(readyZone.Find("ReadyButton").GetComponent<Button>()));


            new MistakeUIC(centerZone.gameObject);
        }

        enum CenterEntTypes
        {
            None,
            Start = None,

            EndGame,
            Motion,
            JoinDiscord,
            Ready,

            End
        }
    }
}