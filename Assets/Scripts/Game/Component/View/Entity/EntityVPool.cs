using Game.Common;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEditor;
using UnityEngine;

namespace Game.Game
{
    public readonly struct EntityVPool
    {


        public EntityVPool(in EcsWorld curGameW)
        {
            ToggleZoneVC.ReplaceZone(SceneTypes.Game);

            var genZone = new GameObject("GeneralZone");
            ToggleZoneVC.Attach(genZone.transform);


            new VideoClipsResC(true);
            new SpritesResC(true);

            SoundC.SavedVolume = SoundC.Volume;


            new GenerZoneVC(genZone);

            var backGroundGO = GameObject.Instantiate(PrefabResC.BackGroundCollider2D,
                MainGoVC.Pos + new Vector3(7, 5.5f, 2), MainGoVC.Rot);

            GenerZoneVC.Attach(backGroundGO.transform);

            var aSParent = new GameObject("AudioSource");
            GenerZoneVC.Attach(aSParent.transform);


            new SoundEffectVC(aSParent);
            new BackgroundVC(backGroundGO, PhotonNetwork.IsMasterClient);
        }
    }
}