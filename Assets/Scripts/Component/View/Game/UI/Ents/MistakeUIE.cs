using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Game
{
    public struct MistakeUIE
    {
        static Entity _background;
        static Dictionary<MistakeTypes, Entity> _zones;
        static Dictionary<ResourceTypes, Entity> _needAmountRes;

        public static ref C Background<C>() where C : struct => ref _background.Get<C>();
        public static ref C Zones<C>(in MistakeTypes mistake) where C : struct => ref _zones[mistake].Get<C>();
        public static ref C NeedAmountResources<C>(in ResourceTypes res) where C : struct => ref _needAmountRes[res].Get<C>();


        public static HashSet<MistakeTypes> KeysMistake
        {
            get
            {
                var keys = new HashSet<MistakeTypes>();
                foreach (var item in _zones) keys.Add(item.Key);
                return keys;
            }
        }
        public static HashSet<ResourceTypes> KeysResource
        {
            get
            {
                var keys = new HashSet<ResourceTypes>();
                foreach (var item in _needAmountRes) keys.Add(item.Key);
                return keys;
            }
        }


        public MistakeUIE(in EcsWorld gameW, in Transform centerZone)
        {
            _zones = new Dictionary<MistakeTypes, Entity>();
            _needAmountRes = new Dictionary<ResourceTypes, Entity>();


            var mistakeZone = centerZone.Find("Mistake");


            _background = gameW.NewEntity()
                .Add(new GameObjectVC(mistakeZone.Find("BackgroudZone").gameObject))
                .Add(new TextMPUGUIC(mistakeZone.Find("Text (TMP)").GetComponent<TextMeshProUGUI>()));



            for (var mistake = MistakeTypes.Economy; mistake < MistakeTypes.End; mistake++)
            {
                _zones.Add(mistake, gameW.NewEntity()
                    .Add(new GameObjectVC(mistakeZone.Find(mistake.ToString()).gameObject)));
            }

            for (var res = ResourceTypes.Food; res < ResourceTypes.End; res++)
            {
                var economy = mistakeZone.transform.Find(MistakeTypes.Economy.ToString());

                _needAmountRes.Add(res, gameW.NewEntity()
                    .Add(new TextMPUGUIC(economy.Find(res.ToString()).Find("TMP").GetComponent<TextMeshProUGUI>())));
            }
        }
    }
}
