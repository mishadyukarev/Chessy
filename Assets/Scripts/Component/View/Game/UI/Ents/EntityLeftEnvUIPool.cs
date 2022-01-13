using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityLeftEnvUIPool
    {
        static readonly Dictionary<LeftEnvEntType, Entity> _ents;
        static readonly Dictionary<ResTypes, Entity> _envs;

        public static ref C Info<C>() where C : struct, ILeftEnvInfoButtonUIE => ref _ents[LeftEnvEntType.Info].Get<C>();
        public static ref C Resources<C>(in ResTypes res) where C : struct, ILeftEnvResTextUIE => ref _envs[res].Get<C>();

        static EntityLeftEnvUIPool()
        {
            _ents = new Dictionary<LeftEnvEntType, Entity>();
            _envs = new Dictionary<ResTypes, Entity>();

            for (var ent = LeftEnvEntType.Start; ent <= LeftEnvEntType.End; ent++) _ents.Add(ent, default);
            for (var res = ResTypes.Start; res <= ResTypes.End; res++) _envs.Add(res, default);
        }
        public EntityLeftEnvUIPool(in EcsWorld gameW, in Transform leftZone)
        {
            var envZone = leftZone.Find("EnvironmentZone");

            _ents[LeftEnvEntType.Info] = gameW.NewEntity().Add(new ButtonUIC(envZone.Find("EnvironmentInfoButton").GetComponent<Button>()));

            _envs[ResTypes.Food] = gameW.NewEntity().Add(new TextMPUGUIC(envZone.Find("FertilizerResources_TextMP").GetComponent<TextMeshProUGUI>()));
            _envs[ResTypes.Wood] = gameW.NewEntity().Add(new TextMPUGUIC(envZone.Find("ForestResources_TextMP").GetComponent<TextMeshProUGUI>()));
            _envs[ResTypes.Ore] = gameW.NewEntity().Add(new TextMPUGUIC(envZone.Find("OreResources_TextMP").GetComponent<TextMeshProUGUI>()));
        }

        enum LeftEnvEntType
        {
            None,
            Start = None,

            Info,

            End,
        }
        public interface ILeftEnvInfoButtonUIE { }
        public interface ILeftEnvResTextUIE { }
    }
}