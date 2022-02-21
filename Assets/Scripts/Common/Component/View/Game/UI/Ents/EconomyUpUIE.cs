using ECS;
using Game.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EconomyUpUIE
    {
        static Dictionary<UpEntTypes, Entity> _ents;
        static Dictionary<ResourceTypes, Entity> _economy;
        static Dictionary<ResourceTypes, Entity> _economyExtract;

        public static ref T Alpha<T>() where T : struct => ref _ents[UpEntTypes.Alpha].Get<T>();
        public static ref T Leave<T>() where T : struct => ref _ents[UpEntTypes.Leave].Get<T>();
        public static ref T DirectWind<T>() where T : struct => ref _ents[UpEntTypes.DirectWind].Get<T>();

        public static ref T Economy<T>(in ResourceTypes res) where T : struct => ref _economy[res].Get<T>();
        public static ref T EconomyExtract<T>(in ResourceTypes res) where T : struct => ref _economyExtract[res].Get<T>();

        public EconomyUpUIE(in EcsWorld curGameW, in Transform upZone)
        {
            _ents = new Dictionary<UpEntTypes, Entity>();
            _economy = new Dictionary<ResourceTypes, Entity>();
            _economyExtract = new Dictionary<ResourceTypes, Entity>();


            for (var res = ResourceTypes.None + 1; res < ResourceTypes.End; res++)
            {
                var resZone = upZone.Find("ResourcesZone").Find(res.ToString());

                _economy.Add(res, curGameW.NewEntity()
                    .Add(new TextUIC(resZone.Find(res.ToString() + "_TMP").GetComponent<TextMeshProUGUI>())));


                if (res != ResourceTypes.Gold && res != ResourceTypes.Iron)
                {
                    _economyExtract.Add(res, curGameW.NewEntity()
                        .Add(new TextUIC(resZone.Find(res.ToString() + "Adding_TMP").GetComponent<TextMeshProUGUI>())));
                }  
            }


            _ents.Add(UpEntTypes.Leave,curGameW.NewEntity()
                .Add(new ButtonUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave"))));


            var image = upZone.Find("WindZone").Find("Direct_Image").GetComponent<Image>();

            _ents.Add(UpEntTypes.DirectWind, curGameW.NewEntity()
                .Add(new TransformVC(image.transform))
                .Add(new ImageUIC(image)));


            _ents.Add(UpEntTypes.Alpha, curGameW.NewEntity()
                .Add(new ButtonUIC(upZone.Find("Alpha_Button").GetComponent<Button>())));
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