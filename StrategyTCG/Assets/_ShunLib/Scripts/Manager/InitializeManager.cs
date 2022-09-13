using System.Threading.Tasks;
using UnityEngine;

using ShunLib.Singleton;
using ShunLib.Manager.Scene;
using ShunLib.Manager.Game;
using ShunLib.Manager.Master;
using ShunLib.Manager.Audio;
using ShunLib.Manager.User;

namespace ShunLib.Manager.Initialize
{
    [DefaultExecutionOrder(-1)]
    public class InitializeManager : SingletonMonoBehaviour<InitializeManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [Header("マネージャー")] 
        [SerializeField] protected MasterManager _masterManager = default;
        [SerializeField] protected UserManager _userManager = default;
        [SerializeField] protected AudioManager _audioManager = default;
        [SerializeField] protected GameManager _gameManager = default;
        
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
        
        // MasterManagerの初期化
        private async Task InitMasterManager()
        {
            if (_masterManager != default)
            {
                await _masterManager.Initialize();
                _gameManager.SetDaoCallback(_masterManager.GetDao);
            }
        }
        
        // UserManagerの初期化
        private async Task InitUserManager()
        {
            if (_userManager != default) await _userManager.Initialize();
        }
        
        // AudioManagerの初期化
        private async Task InitAudioManager()
        {
            if (_audioManager != default)
            {
                await _audioManager.Initialize();
                _gameManager.SetPlaySECallback(_audioManager.PlaySE);
                _gameManager.SetPlayBGMCallback(_audioManager.PlayBGM);
            }
        }

        // 最初に遷移するシーン名を取得
        private string GetSceneName()
        {
            return "TitleScene";
        }
        
        // 最初のシーンへ遷移
        private void Transition()
        {
            string sceneName = GetSceneName();
            SceneLoadManager.Instance.TransitionScene(sceneName);
        }
        
        // ---------- protected関数 ---------
    }
}

