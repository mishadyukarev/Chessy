﻿using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        readonly EntitiesModelGame _eMG;
        readonly List<Action> _runs;

        internal readonly BuildingSystems BuildingSs;
        internal readonly UnitSystems UnitSs;
        internal readonly GetDataCellsAfterAnyDoingS_M GetDataCellsS;
        internal readonly ExecuteAIBotLogicAfterUpdateS_M AIBotS;
        internal readonly ExecuteUpdateEverythingMS ExecuteUpdateEverythingMS;
        internal readonly TruceS TruceS;
        public readonly SystemsModelCommon CommonSs;
        public readonly SystemsModelGameForUI ForUISystems;

        public SystemsModelGame(in SystemsModelCommon sMC, in EntitiesModelGame eMG)
        {
            CommonSs = sMC;

            _eMG = eMG;

            _runs = new List<Action>()
            {
                new InputS(this, eMG).Update,
                new CheatsS(this, eMG).Update,
                new RayS(this, eMG).Update,
                new SelectorS(this, eMG).Update,

                new MistakeS(this, eMG).Update,
            };

            ForUISystems = new SystemsModelGameForUI(this, eMG);
            BuildingSs = new BuildingSystems(this, eMG);
            UnitSs = new UnitSystems(this, eMG);
            GetDataCellsS = new GetDataCellsAfterAnyDoingS_M(this, eMG);
            AIBotS = new ExecuteAIBotLogicAfterUpdateS_M(this, eMG);
            ExecuteUpdateEverythingMS = new ExecuteUpdateEverythingMS(this, eMG);
            TruceS = new TruceS(this, eMG);
        }

        public void Update()
        {
            _runs.ForEach((Action action) => action());

            _eMG.ForUpdateViewTimer += Time.deltaTime;

            if (_eMG.ForUpdateViewTimer >= 0.5f)
            {
                _eMG.NeedUpdateView = true;
                _eMG.ForUpdateViewTimer = 0;
            }
        }

        internal void ExecuteSoundAction(in ClipTypes clipT) => _eMG.SoundAction(clipT).Invoke();
        internal void ExecuteSoundAction(in AbilityTypes abilityT) => _eMG.SoundAction(abilityT).Invoke();

        internal void ExecuteAnimationClip(in byte cellIdx, in AnimationCellTypes animationCellT) => _eMG.DataFromViewC.AnimationCell(cellIdx, animationCellT).Invoke();

        internal void ActiveMotion() => _eMG.MotionTimer = 4;

        internal void ExecuteMistake(in MistakeTypes mistakeT, in float[] needRes)
        {
            Mistake(mistakeT);
            _eMG.SoundAction(ClipTypes.WritePensil).Invoke();

            if (mistakeT == MistakeTypes.Economy)
            {
                _eMG.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                _eMG.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                _eMG.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                _eMG.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                _eMG.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                _eMG.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                _eMG.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                _eMG.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                _eMG.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                _eMG.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
            }
        }




        internal void ExecuteSoundActionToGeneral(in RpcTarget rpcTargetT, in ClipTypes clipT) => _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, rpcTargetT, new object[] { nameof(ExecuteSoundAction), clipT });
        internal void ExecuteSoundActionToGeneral(in Player playerTo, ClipTypes clipT) => _eMG.RpcPoolEs.Action1(_eMG.RpcPoolEs.MasterRPCName, playerTo, new object[] { nameof(ExecuteSoundAction), clipT });

        public void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes uniq) => _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, rpcTarget, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });
        public void SoundToGeneral(Player playerTo, AbilityTypes uniq) => _eMG.RpcPoolEs.Action1(_eMG.RpcPoolEs.MasterRPCName, playerTo, new object[] { RpcGeneralTypes.SoundUniqueAbility, uniq });

        public void SimpleMistake_ToGeneral(MistakeTypes mistakeType, Player playerTo) => _eMG.RpcPoolEs.Action1(_eMG.RpcPoolEs.MasterRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, mistakeType });

        public void ActiveMotionZone_ToGeneneral(Player player) => _eMG.RpcPoolEs.Action1(_eMG.RpcPoolEs.MasterRPCName, player, new object[] { nameof(ActiveMotion) });
        public void ActiveMotionZone_ToGeneneral(in RpcTarget rpcTarget) => _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, rpcTarget, new object[] { RpcGeneralTypes.ActiveMotion });

        public void AnimationCell_ToGeneral(in byte cellIdx, in AnimationCellTypes animationCellT, in RpcTarget rpcTarget) => _eMG.RpcPoolEs.Action0(_eMG.RpcPoolEs.MasterRPCName, rpcTarget, new object[] { RpcGeneralTypes.AnimationCell, cellIdx, animationCellT });


        internal void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResourceTypes, float> needRes)
        {
            var needRes2 = new float[(int)ResourceTypes.End];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            _eMG.RpcPoolEs.Action1(_eMG.RpcPoolEs.MasterRPCName, playerTo, new object[] { RpcGeneralTypes.Mistake, MistakeTypes.Economy, needRes2 });
        }
    }
}