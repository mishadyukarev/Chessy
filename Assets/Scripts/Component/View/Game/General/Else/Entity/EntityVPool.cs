using ECS;
using Game.Common;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Game
{
    public readonly struct EntityVPool
    {
        static readonly Dictionary<string, Entity> _ents;

        public static ref C Background<C>() where C : struct => ref _ents[nameof(Background)].Get<C>();
        public static ref C GeneralZone<C>() where C : struct => ref _ents[nameof(GeneralZone)].Get<C>();


        static EntityVPool()
        {
            _ents = new Dictionary<string, Entity>();
        }
        public EntityVPool(in WorldEcs curGameW)
        {
            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);
            _ents[nameof(GeneralZone)] = curGameW.NewEntity()
                .Add(new GeneralZoneVEC())
                .Add(new GameObjectC(genZone));


            new VideoClipsResC(true);
            new SpritesResC(true);

            SoundC.SavedVolume = SoundC.Volume;


            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), MainGoVC.Rot);
            GeneralZone<GeneralZoneVEC>().Attach(backGroundGO.transform);
            backGroundGO.transform.rotation = PhotonNetwork.IsMasterClient ? new Quaternion(0, 0, 0, 0) : new Quaternion(0, 0, 180, 0);

            _ents[nameof(Background)] = curGameW.NewEntity()
                .Add(new GameObjectC(backGroundGO));


            var aSParent = new GameObject("AudioSource");
            GeneralZone<GeneralZoneVEC>().Attach(aSParent.transform);


            new SoundEffectVC(aSParent);


        }
    }
}