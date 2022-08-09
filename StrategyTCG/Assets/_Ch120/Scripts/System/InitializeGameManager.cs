using System.Collections;
using System.Collections.Generic;
using Ch120.Manager.Audio;
using UnityEngine;

using Ch120.Singleton;
using Ch120.Manager.Scene;
using Ch120.Manager.Master;
using Ch120.Manager.User;

namespace Ch120.Game
{
    [DefaultExecutionOrder(-1)]
    public class InitializeGameManager : SingletonMonoBehaviour<InitializeGameManager>
    {
        // ---------- 定数宣言 ----------
        
        // 最初に開くシーン
        private const string SCENE_NAME = "TitleScene";
        
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("マネージャー")] 
        [SerializeField] protected MasterManager _masterManager = default;
        [SerializeField] protected UserManager _userManager = default;
        [SerializeField] protected AudioManager _audioManager = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public static bool IsInitialize
        {
            get { return _isInitialize; }
            set { _isInitialize = value; }
        }
        
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private static bool _isInitialize = false;

        // ---------- Unity組込関数 ----------
        
        void Start()
        {
            Initialize();
        }

        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // 起動時の初期設定
        private void Initialize()
        {
            if (_masterManager != default) _masterManager.Initialize();
            if (_userManager != default) _userManager.Initialize();
            if (_audioManager != default) _audioManager.Initialize();
            _isInitialize = true;
            SceneLoadManager.Instance.TransitionScene(SCENE_NAME);
        }

        // ---------- protected関数 ---------
    }
}

