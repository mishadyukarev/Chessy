﻿using ECS;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Game
{
    public readonly struct EntityLeftEnvUIPool
    {
        static Dictionary<LeftEnvEntType, Entity> _ents;
        static Dictionary<ResourceTypes, Entity> _envs;

        public static ref C Info<C>() where C : struct, ILeftEnvInfoButtonUIE => ref _ents[LeftEnvEntType.Info].Get<C>();
        public static ref C Resources<C>(in ResourceTypes res) where C : struct, ILeftEnvResTextUIE => ref _envs[res].Get<C>();


        public EntityLeftEnvUIPool(in EcsWorld gameW, in Transform leftZone)
        {
            _ents = new Dictionary<LeftEnvEntType, Entity>();
            _envs = new Dictionary<ResourceTypes, Entity>();

            for (var ent = LeftEnvEntType.Start; ent <= LeftEnvEntType.End; ent++) _ents.Add(ent, default);
            for (var res = ResourceTypes.None; res <= ResourceTypes.End; res++) _envs.Add(res, default);


            var envZone = leftZone.Find("EnvironmentZone");

            _ents[LeftEnvEntType.Info] = gameW.NewEntity().Add(new ButtonUIC(envZone.Find("EnvironmentInfoButton").GetComponent<Button>()));

            _envs[ResourceTypes.Food] = gameW.NewEntity().Add(new TextUIC(envZone.Find("FertilizerResources_TextMP").GetComponent<TextMeshProUGUI>()));
            _envs[ResourceTypes.Wood] = gameW.NewEntity().Add(new TextUIC(envZone.Find("ForestResources_TextMP").GetComponent<TextMeshProUGUI>()));
            _envs[ResourceTypes.Ore] = gameW.NewEntity().Add(new TextUIC(envZone.Find("OreResources_TextMP").GetComponent<TextMeshProUGUI>()));
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