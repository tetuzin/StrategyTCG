using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ch120.Manager.Audio;
using UnityEngine;

using Ch120.Singleton;
using Ch120.Manager.Scene;
using Ch120.Manager.Master;
using Ch120.Manager.User;

namespace Ch120.Manager.Initialize
{
    [DefaultExecutionOrder(-1)]
    public class InitializeGameManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public static bool IsInitialize
        {
            get { return _isInitialize; }
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
        private async void Initialize()
        {
            await InitMasterManager();
            await InitUserManager();
            await InitAudioManager();
            
            _isInitialize = true;
            
            Transition();
        }
        
        // 最初のシーンへ遷移
        private void Transition()
        {
            string sceneName = GetSceneName();
            SceneLoadManager.Instance.TransitionScene(sceneName);
        }
        
        // ---------- protected関数 ---------
        
        // MasterManagerの初期化
        protected virtual async Task InitMasterManager() { }
        
        // UserManagerの初期化
        protected virtual async Task InitUserManager() { }
        
        // AudioManagerの初期化
        protected virtual async Task InitAudioManager() { }

        // 最初に遷移するシーン名を取得
        protected virtual string GetSceneName()
        {
            return "";
        }
    }
}

