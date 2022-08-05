using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

using Ch120.Singleton;
using Ch120.Manager.Scene;

using UK.Manager.TitleUI;

namespace UK.Manager.Title
{
    public class TitleManager : SingletonMonoBehaviour<TitleManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("Cameraオブジェクト")] protected Camera _mainCamera = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------

        void Start()
        {
            Initialized();
        }

        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        private void Initialized()
        {
            if (_mainCamera != default)
            {
                _mainCamera.gameObject.transform.DORotate(Vector3.up * 10.0f, 2.0f)
                    .SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            }
            
            TitleUIManager.Instance.Initialize();
            
            TitleUIManager.Instance.SetButtonEvent(TitleButtonType.CPU_BATTLE, () =>
            {
                SceneLoadManager.Instance.TransitionScene("IngameScene");
            });
        }
        
        // ---------- protected関数 ---------
    }
}