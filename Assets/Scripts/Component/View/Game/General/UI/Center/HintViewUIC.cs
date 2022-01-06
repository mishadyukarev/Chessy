using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Game.Game
{
    public struct HintViewUIC
    {
        private static RectTransform _hintZone_Rect;
        private static Button _hint_But;
        private static VideoPlayer _videoPlayer;
        private static Dictionary<VideoClipTypes, string> _urls;


        public HintViewUIC(Transform centZone_Trans, bool isActive)
        {
            _hintZone_Rect = centZone_Trans.Find("HintZone").GetComponent<RectTransform>();

            _hint_But = _hintZone_Rect.transform.Find("Hint_Button").GetComponent<Button>();
            _videoPlayer = _hint_But.GetComponent<VideoPlayer>();

            _urls = new Dictionary<VideoClipTypes, string>();
            _urls.Add(VideoClipTypes.Start, Path.Combine(Application.streamingAssetsPath, "Start.mp4"));
            _urls.Add(VideoClipTypes.Start2, Path.Combine(Application.streamingAssetsPath, "Start2.mp4"));
            _urls.Add(VideoClipTypes.Start3, Path.Combine(Application.streamingAssetsPath, "Start3.mp4"));
            _urls.Add(VideoClipTypes.Start4, Path.Combine(Application.streamingAssetsPath, "Start4.mp4"));
            _urls.Add(VideoClipTypes.Start5, Path.Combine(Application.streamingAssetsPath, "SpeciesAttack.mp4"));
            _urls.Add(VideoClipTypes.BuldFarms, Path.Combine(Application.streamingAssetsPath, "Farm.mp4"));
            _urls.Add(VideoClipTypes.SeedFire, Path.Combine(Application.streamingAssetsPath, "FireSeed.mp4"));
            _urls.Add(VideoClipTypes.Woodcutter, Path.Combine(Application.streamingAssetsPath, "Woodcutter.mp4"));
            _urls.Add(VideoClipTypes.BuildMine, Path.Combine(Application.streamingAssetsPath, "Mine.mp4"));
            _urls.Add(VideoClipTypes.Vision, Path.Combine(Application.streamingAssetsPath, "Vision.mp4"));
            _urls.Add(VideoClipTypes.ProtRelax, Path.Combine(Application.streamingAssetsPath, "ProtRelax.mp4"));
            _urls.Add(VideoClipTypes.CircularAttack, Path.Combine(Application.streamingAssetsPath, "CircularAttack.mp4"));
            _urls.Add(VideoClipTypes.BonusKing, Path.Combine(Application.streamingAssetsPath, "BonusKing.mp4"));
            _urls.Add(VideoClipTypes.UpgToolWeapon, Path.Combine(Application.streamingAssetsPath, "UpgToolWeapon.mp4"));
            _urls.Add(VideoClipTypes.Pick, Path.Combine(Application.streamingAssetsPath, "Pick.mp4"));
            _urls.Add(VideoClipTypes.CreatingHero, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.CreatingHero.ToString() + ".mp4"));
            _urls.Add(VideoClipTypes.CreatingScout, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.CreatingScout.ToString() + ".mp4"));
            _urls.Add(VideoClipTypes.GrowingAdForesElfemale, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.GrowingAdForesElfemale.ToString() + ".mp4"));
            _urls.Add(VideoClipTypes.StunElfemale, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.StunElfemale.ToString() + ".mp4"));
            _urls.Add(VideoClipTypes.PutOutElfemale, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.PutOutElfemale.ToString() + ".mp4"));

            SetVideoClip(VideoClipTypes.Start);

            _hintZone_Rect.gameObject.SetActive(isActive);
        }

        public static void SetActiveHintZone(bool isActive) => _hintZone_Rect.gameObject.SetActive(isActive);
        public static void SetPos(Vector3 pos) => _hintZone_Rect.anchoredPosition = pos;
        public static void AddListHint_But(UnityAction unityAction) => _hint_But.onClick.AddListener(unityAction);
        public static void SetVideoClip(VideoClipTypes videoClipType)
        {
            _videoPlayer.url = _urls[videoClipType];
        }
    }
}