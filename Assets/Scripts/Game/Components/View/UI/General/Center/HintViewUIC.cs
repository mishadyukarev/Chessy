using Scripts.Common;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Scripts.Game
{
    internal struct HintViewUIC
    {
        private static RectTransform _hintZone_Rect;
        private static Button _hint_But;
        private static VideoPlayer _videoPlayer;

        internal HintViewUIC(Transform centZone_Trans)
        {
            _hintZone_Rect = centZone_Trans.Find("HintZone").GetComponent<RectTransform>();

            _hint_But = _hintZone_Rect.transform.Find("Hint_Button").GetComponent<Button>();
            _videoPlayer = _hint_But.GetComponent<VideoPlayer>();
        }

        internal static void SetActiveHintZone(bool isActive) => _hintZone_Rect.gameObject.SetActive(isActive);
        internal static void SetPos(Vector3 pos) => _hintZone_Rect.anchoredPosition = pos;
        internal static void AddListHint_But(UnityAction unityAction) => _hint_But.onClick.AddListener(unityAction);
        internal static void SetVideoClip(VideoClipTypes videoClipType) => _videoPlayer.clip = VideoClipsResCom.VideoClip(videoClipType);
    }
}