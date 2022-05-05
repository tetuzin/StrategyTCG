using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ch120.BaseRoot
{
    public class BaseRoot : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [Header("BaseRootの変数")]
        [SerializeField, Tooltip("Cameraオブジェクト")] private Camera mainCamera;
        [SerializeField, Tooltip("Canvasオブジェクト")] private GameObject canvas;
        [SerializeField, Tooltip("AudioSourceオブジェクト")] private AudioSource audioSource;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public virtual void InitRoot()
        {
            SetData();
            SetButtonEvents();
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------

        // ボタンイベントを設定する（継承先でOverrideする）
        protected virtual void SetButtonEvents(){}

        // データを設定する（継承先でOverrideする）
        protected virtual void SetData() {}
    }
}