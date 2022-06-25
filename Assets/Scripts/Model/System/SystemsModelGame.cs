using Chessy.Common;
using Chessy.Common.Enum;
using Chessy.Model;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chessy.Model
{
    public sealed partial class SystemsModel : IUpdate
    {
        readonly EntitiesModel _e;
        readonly List<Action> _runs;

        internal readonly UnitSystems UnitSs;
        internal readonly GetDataCellsAfterAnyDoingS_M GetDataCellsS;
        internal readonly ExecuteAIBotLogicAfterUpdateS_M AIBotS;

        public readonly SystemsModelGameForUI ForUISystems;

        const byte MAX_PLAYERS = 2;

        public SystemsModel(in EntitiesModel eMG)
        {
            _e = eMG;

            _runs = new List<Action>()
            {
                new InputS(this, eMG).Update,
                new CheatsS(this, eMG).Update,
                new RayS(this, eMG).Update,
                new SelectorS(this, eMG).Update,

                new MistakeS(this, eMG).Update,
            };

            ForUISystems = new SystemsModelGameForUI(this, eMG);
            UnitSs = new UnitSystems(this, eMG);
            GetDataCellsS = new GetDataCellsAfterAnyDoingS_M(this, eMG);
            AIBotS = new ExecuteAIBotLogicAfterUpdateS_M(this, eMG);

            Application.runInBackground = true;

            var nowTime = DateTime.Now;
            _e.AdC = new AdC(nowTime);

            _e.OpenedNowPageBookT = PageBookTypes.None;

            _e.SceneT = SceneTypes.Menu;
        }

        public void Update()
        {
            _runs.ForEach((Action action) => action());

            _e.ForUpdateViewTimer += Time.deltaTime;

            if (_e.ForUpdateViewTimer >= 0.5f)
            {
                _e.NeedUpdateView = true;
                _e.ForUpdateViewTimer = 0;
            }
        }

        public void ToggleScene(in SceneTypes newSceneT)
        {
            _e.SceneT = newSceneT;

            switch (newSceneT)
            {
                case SceneTypes.None:
                    throw new Exception();

                case SceneTypes.Menu:
                    {
                        break;
                    }

                case SceneTypes.Game:
                    {
                        //_eMC.IsOpenedBook = true;
                        //_eMC.PageBookTC.PageBookT = PageBookTypes.Main;
                        break;
                    }
                default: throw new Exception();
            }
        }

        public void ComeToTrainingAfterDownloadingGame()
        {
            PhotonNetwork.OfflineMode = true;
            _e.GameModeT = GameModeTypes.TrainingOffline;
            PhotonNetwork.CreateRoom(default);
        }

        public void OnLeftRoom()
        {
            _e.SceneT = SceneTypes.Menu;
            _e.NeedUpdateView = true;
        }

        public void OnPlayerLeftRoom(in Player otherPlayer)
        {
            if (PhotonNetwork.InRoom) PhotonNetwork.LeaveRoom();
        }

        public void BuyPremiumProduct()
        {
            if (_e.ShopC.IsInitialized) //если покупка инициализирована 
            {
                var product = _e.ShopC.StoreController.products.WithID(ShopC.PREMIUM_NAME); //находим продукт покупки 

                if (product == default) throw new Exception();

                if (product.availableToPurchase) //если продукт найдет и готов для продажи
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                    _e.ShopC.StoreController.InitiatePurchase(product); //покупаем
                }
                else
                {
                    Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                }
            }
            else
            {
                Debug.Log("BuyProductID FAIL. Not initialized.");
            }
        }

        public void CreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();

            _e.GameModeT = GameModeTypes.PublicOnline;

            //roomOptions.CustomRoomPropertiesForLobby = new string[] { nameof(StepModeTypes) };
            //roomOptions.CustomRoomProperties = new Hashtable() { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };

            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;
            roomOptions.EmptyRoomTtl = 3000;
            var roomName = UnityEngine.Random.Range(1, 9999999).ToString();

            PhotonNetwork.CreateRoom(roomName, roomOptions, default, default);// CreateRoom(roomName, roomOptions);
        }
        public void CreateFriendRoom(in string roomNameFromViewBar)
        {
            _e.GameModeT = GameModeTypes.WithFriendOnline;

            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = MAX_PLAYERS;
            roomOptions.IsVisible = false;
            roomOptions.IsOpen = true;

            PhotonNetwork.CreateRoom(roomNameFromViewBar, roomOptions, default);
        }
        public void JoinRandomRoom()
        {
            _e.GameModeT = GameModeTypes.PublicOnline;
            //Hashtable expectedCustomRoomProperties = new Hashtable { { nameof(StepModeTypes), _rightZoneFilter.Get2(0).StepModValue } };
            PhotonNetwork.JoinRandomRoom(/*expectedCustomRoomProperties, MAX_PLAYERS*/);
        }
        public void JoinFriendRoom(in string nameRoomFromViewBar)
        {
            _e.GameModeT = GameModeTypes.WithFriendOnline;
            PhotonNetwork.JoinRoom(nameRoomFromViewBar);
        }

        public void CreateOffGame(in GameModeTypes offGameMode)
        {
            _e.GameModeT = offGameMode;
            PhotonNetwork.CreateRoom(default);
        }




        internal void ExecuteSoundAction(in ClipTypes clipT) => _e.SoundAction(clipT).Invoke();
        internal void ExecuteSoundAction(in AbilityTypes abilityT) => _e.SoundAction(abilityT).Invoke();

        internal void ExecuteAnimationClip(in byte cellIdx, in AnimationCellTypes animationCellT) => _e.DataFromViewC.AnimationCell(cellIdx, animationCellT).Invoke();

        internal void ActiveMotion() => _e.MotionTimer = 4;

        internal void ExecuteMistake(in MistakeTypes mistakeT, in float[] needRes)
        {
            Mistake(mistakeT);
            _e.SoundAction(ClipTypes.WritePensil).Invoke();

            if (mistakeT == MistakeTypes.Economy)
            {
                _e.MistakeEconomy(ResourceTypes.Food).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Wood).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Ore).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Iron).Resources = 0;
                _e.MistakeEconomy(ResourceTypes.Gold).Resources = 0;

                _e.MistakeEconomy(ResourceTypes.Food).Resources = needRes[0];
                _e.MistakeEconomy(ResourceTypes.Wood).Resources = needRes[1];
                _e.MistakeEconomy(ResourceTypes.Ore).Resources = needRes[2];
                _e.MistakeEconomy(ResourceTypes.Iron).Resources = needRes[3];
                _e.MistakeEconomy(ResourceTypes.Gold).Resources = needRes[4];
            }
        }




        internal void ExecuteSoundActionToGeneral(in RpcTarget rpcTargetT, in ClipTypes clipT) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTargetT, new object[] { nameof(ExecuteSoundAction), clipT });
        internal void ExecuteSoundActionToGeneral(in Player playerTo, ClipTypes clipT) => _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(ExecuteSoundAction), clipT });
        public void SoundToGeneral(RpcTarget rpcTarget, AbilityTypes uniq) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(ExecuteSoundAction), uniq });
        public void SoundToGeneral(Player playerTo, AbilityTypes uniq) => _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(ExecuteSoundAction), uniq });

        public void ActiveMotionZoneToGeneneral(in Player player) => _e.RpcC.Action1(_e.RpcC.PunRPCName, player, new object[] { nameof(ActiveMotion) });
        public void ActiveMotionZoneToGeneneral(in RpcTarget rpcTarget) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(ActiveMotion) });

        public void AnimationCellToGeneral(in byte cellIdx, in AnimationCellTypes animationCellT, in RpcTarget rpcTarget) => _e.RpcC.Action0(_e.RpcC.PunRPCName, rpcTarget, new object[] { nameof(ExecuteAnimationClip), cellIdx, animationCellT });

        public void SimpleMistakeToGeneral(MistakeTypes mistakeType, Player playerTo) => _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(ExecuteMistake), mistakeType });
        internal void MistakeEconomyToGeneral(Player playerTo, Dictionary<ResourceTypes, float> needRes)
        {
            var needRes2 = new float[(int)ResourceTypes.End];
            needRes2[0] = needRes[ResourceTypes.Food];
            needRes2[1] = needRes[ResourceTypes.Wood];
            needRes2[2] = needRes[ResourceTypes.Ore];
            needRes2[3] = needRes[ResourceTypes.Iron];
            needRes2[4] = needRes[ResourceTypes.Gold];

            _e.RpcC.Action1(_e.RpcC.PunRPCName, playerTo, new object[] { nameof(ExecuteMistake), MistakeTypes.Economy, needRes2 });
        }
    }
}