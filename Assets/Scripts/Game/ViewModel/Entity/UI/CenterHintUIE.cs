using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Chessy.Game
{
    public readonly struct CenterHintUIE
    {
        //static Dictionary<VideoClipTypes, Entity> _urls;

        //public static ref C Hint<C>() where C : struct => ref _entity.Get<C>();

        public CenterHintUIE(in Transform centerZone)
        {
            var rect = centerZone.Find("HintZone").GetComponent<RectTransform>();
            rect.gameObject.SetActive(Common.HintC.IsOnHint);

            var button = rect.Find("Hint_Button").GetComponent<Button>();
            var videoPlayer = button.GetComponent<VideoPlayer>();

            //_entity = gameW.NewEntity()
            //    .Add(new ButtonUIC(button))
            //    .Add(new VideoPlayerVC(videoPlayer));


            Debug.Log(Application.streamingAssetsPath);

            //_urls = new Dictionary<VideoClipTypes, string>();
            //_urls.Add(VideoClipTypes.Start, Path.Combine(Application.streamingAssetsPath, "Start.mp4"));
            //_urls.Add(VideoClipTypes.Start2, Path.Combine(Application.streamingAssetsPath, "Start2.mp4"));
            //_urls.Add(VideoClipTypes.Start3, Path.Combine(Application.streamingAssetsPath, "Start3.mp4"));
            //_urls.Add(VideoClipTypes.Start4, Path.Combine(Application.streamingAssetsPath, "Start4.mp4"));
            //_urls.Add(VideoClipTypes.Start5, Path.Combine(Application.streamingAssetsPath, "SpeciesAttack.mp4"));
            //_urls.Add(VideoClipTypes.BuldFarms, Path.Combine(Application.streamingAssetsPath, "Farm.mp4"));
            //_urls.Add(VideoClipTypes.SeedFire, Path.Combine(Application.streamingAssetsPath, "FireSeed.mp4"));
            //_urls.Add(VideoClipTypes.Woodcutter, Path.Combine(Application.streamingAssetsPath, "Woodcutter.mp4"));
            //_urls.Add(VideoClipTypes.BuildMine, Path.Combine(Application.streamingAssetsPath, "Mine.mp4"));
            //_urls.Add(VideoClipTypes.Vision, Path.Combine(Application.streamingAssetsPath, "Vision.mp4"));
            //_urls.Add(VideoClipTypes.ProtRelax, Path.Combine(Application.streamingAssetsPath, "ProtRelax.mp4"));
            //_urls.Add(VideoClipTypes.CircularAttack, Path.Combine(Application.streamingAssetsPath, "CircularAttack.mp4"));
            //_urls.Add(VideoClipTypes.BonusKing, Path.Combine(Application.streamingAssetsPath, "BonusKing.mp4"));
            //_urls.Add(VideoClipTypes.UpgToolWeapon, Path.Combine(Application.streamingAssetsPath, "UpgToolWeapon.mp4"));
            //_urls.Add(VideoClipTypes.Pick, Path.Combine(Application.streamingAssetsPath, "Pick.mp4"));
            //_urls.Add(VideoClipTypes.CreatingHero, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.CreatingHero.ToString() + ".mp4"));
            //_urls.Add(VideoClipTypes.CreatingScout, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.CreatingScout.ToString() + ".mp4"));
            //_urls.Add(VideoClipTypes.GrowingAdForesElfemale, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.GrowingAdForesElfemale.ToString() + ".mp4"));
            //_urls.Add(VideoClipTypes.StunElfemale, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.StunElfemale.ToString() + ".mp4"));
            //_urls.Add(VideoClipTypes.PutOutElfemale, Path.Combine(Application.streamingAssetsPath, VideoClipTypes.PutOutElfemale.ToString() + ".mp4"));


            //Hint<VideoPlayerVC>().SetVideoClip(default);
        }
    }
}