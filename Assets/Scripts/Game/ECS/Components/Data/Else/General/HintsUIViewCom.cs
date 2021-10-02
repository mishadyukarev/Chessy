//using Assets.Scripts.Abstractions.Data;
//using Assets.Scripts.Abstractions.Enums;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Video;

//namespace Assets.Scripts.ECS.Components.Data.Else.Game.General
//{
//    internal struct HintsUIViewCom
//    {
//        //private RectTransform _parent_RectTrans;
//        //private VideoPlayer _videoPlayer;
//        //private Dictionary<HintUITypes, VideoClip> _hits_CLs;

//        //internal HintsUIViewCom(Transform centerZone_Trans, VideoClipsData videoClipsData)
//        //{
//        //    _parent_RectTrans = centerZone_Trans.Find("RawImage").GetComponent<RectTransform>();

//        //    _videoPlayer = _parent_RectTrans.Find("Image").GetComponent<VideoPlayer>();

//        //    _hits_CLs = new Dictionary<HintUITypes, VideoClip>();
//        //    _hits_CLs.Add(HintUITypes.Start, videoClipsData.Start_VC);
//        //    _hits_CLs.Add(HintUITypes.Building, videoClipsData.Building_VC);
//        //    _hits_CLs.Add(HintUITypes.NeedOtherPlace, videoClipsData.Building_VC);

//        //    Active(HintUITypes.Start);
//        //}

//        //internal void Active(HintUITypes hintUIType)
//        //{
//        //    _parent_RectTrans.gameObject.SetActive(true);
//        //    _videoPlayer.clip = _hits_CLs[hintUIType];
//        //    _parent_RectTrans.anchoredPosition = new Vector3(Random.Range(-500f, 500f), Random.Range(-300f, 300f));
//        //}
//    }
//}
