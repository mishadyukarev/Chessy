using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
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
    }
}