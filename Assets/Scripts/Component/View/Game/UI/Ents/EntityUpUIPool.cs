using ECS;
using Game.Common;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityUpUIPool
    {
        readonly static Dictionary<UpEntTypes, Entity> _ents;
        readonly static Dictionary<ResTypes, Entity> _economy;

        public static ref T Alpha<T>() where T : struct => ref _ents[UpEntTypes.Alpha].Get<T>();
        public static ref T Leave<T>() where T : struct => ref _ents[UpEntTypes.Leave].Get<T>();
        public static ref T DirectWind<T>() where T : struct => ref _ents[UpEntTypes.DirectWind].Get<T>();

        public static ref T Economy<T>(in ResTypes res) where T : struct => ref _economy[res].Get<T>();

        static EntityUpUIPool()
        {
            _ents = new Dictionary<UpEntTypes, Entity>();
            _economy = new Dictionary<ResTypes, Entity>();

            for (var up = UpEntTypes.Start; up <= UpEntTypes.End; up++) _ents.Add(up, default);
            for (var res = ResTypes.Start; res <= ResTypes.End; res++) _economy.Add(res, default);
        }
        public EntityUpUIPool(in EcsWorld curGameW)
        {
            var upZone_GO = CanvasC.FindUnderCurZone("UpZone");

            for (var res = ResTypes.First; res < ResTypes.End; res++)
            {
                _economy[res] = curGameW.NewEntity()
                    .Add(new EconomyUpUIC(res))
                    .Add(new TextMPUGUIC(upZone_GO.transform.Find("ResourcesZone").Find(res.ToString()).Find(res.ToString() + "_TMP").GetComponent<TextMeshProUGUI>()));
            }


            _ents[UpEntTypes.Leave] = curGameW.NewEntity()
                .Add(new ButtonUIC(CanvasC.FindUnderCurZone<Button>("ButtonLeave")));

            _ents[UpEntTypes.DirectWind] = curGameW.NewEntity()
                .Add(new DirWindUIC())
                .Add(new ImageUIC(upZone_GO.transform.Find("WindZone").Find("Direct_Image").GetComponent<Image>()));

            _ents[UpEntTypes.Alpha] = curGameW.NewEntity()
                .Add(new ButtonUIC(upZone_GO.transform.Find("Alpha_Button").GetComponent<Button>()));
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