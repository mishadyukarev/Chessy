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

        internal readonly MistakeSs MistakeSs;
        internal readonly MasterSystems MasterSs;

        public readonly SystemsModelCommon CommonSs;
        public readonly SystemsModelGameForUI ForUISystems;
        public readonly OnJoinedRoomS OnJoinedRoomS;



        internal UnitSystems UnitSs => MasterSs.UnitSs;
        internal BuildingSystems BuildingSs => MasterSs.BuildingSs;



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

                new Chessy.Game.MistakeS(this, eMG).Update,
            };

            MistakeSs = new MistakeSs(this, eMG);
            MasterSs = new MasterSystems(this, eMG);
            ForUISystems = new SystemsModelGameForUI(this, eMG);
            OnJoinedRoomS = new OnJoinedRoomS(this, eMG);
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