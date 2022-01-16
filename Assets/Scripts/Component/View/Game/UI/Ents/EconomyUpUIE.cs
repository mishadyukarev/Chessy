﻿using ECS;
using Game.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EconomyUpUIE
    {
        static Dictionary<UpEntTypes, Entity> _ents;
        static Dictionary<ResTypes, Entity> _economy;

        public static ref T Alpha<T>() where T : struct => ref _ents[UpEntTypes.Alpha].Get<T>();
        public static ref T Leave<T>() where T : struct => ref _ents[UpEntTypes.Leave].Get<T>();
        public static ref T DirectWind<T>() where T : struct => ref _ents[UpEntTypes.DirectWind].Get<T>();

        public static ref T Economy<T>(in ResTypes res) where T : struct => ref _economy[res].Get<T>();

        public EconomyUpUIE(in EcsWorld curGameW)
        {
            _ents = new Dictionary<UpEntTypes, Entity>();
            _economy = new Dictionary<ResTypes, Entity>();


            var upZone_GO = CanvasC.FindUnderCurZone("UpZone").transform;

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                _economy.Add(res, curGameW.NewEntity()
                    .Add(new TextMPUGUIC(upZone_GO.Find("ResourcesZone").Find(res.ToString()).Find(res.ToString() + "_TMP").GetComponent<TextMeshProUGUI>())));
            }


            _ents.Add(UpEntTypes.Leave,curGameW.NewEntity()
                .Add(new ButtonUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave"))));

            _ents.Add(UpEntTypes.DirectWind, curGameW.NewEntity()
                .Add(new TransformVC(upZone_GO))
                .Add(new ImageUIC(upZone_GO.Find("WindZone").Find("Direct_Image").GetComponent<Image>())));


            _ents.Add(UpEntTypes.Alpha, curGameW.NewEntity()
                .Add(new ButtonUIC(upZone_GO.Find("Alpha_Button").GetComponent<Button>())));
        }

        enum UpEntTypes
        {
            None,
            Start = None,

            Leave,
            DirectWind,
            Alpha,

            End
        }
    }
}