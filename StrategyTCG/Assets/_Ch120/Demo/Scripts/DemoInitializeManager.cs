using UnityEngine;
using System.Threading.Tasks;
using Ch120.Manager.Initialize;

namespace Ch120.Demo
{
    public class DemoInitializeManager : InitializeGameManager<DemoInitializeManager>
    {
        [Header("マネージャー")] 
        [SerializeField] protected DemoMasterManager _masterManager = default;
        [SerializeField] protected DemoUserManager _userManager = default;
        [SerializeField] protected DemoAudioManager _audioManager = default;
        
        // MasterManagerの初期化
        protected override async Task InitMasterManager()
        {
            if (_masterManager != default) await _masterManager.Initialize();
        }
        
        // UserManagerの初期化
        protected override async Task InitUserManager()
        {
            if (_userManager != default) await _userManager.Initialize();
        }
        
        // AudioManagerの初期化
        protected override async Task InitAudioManager()
        {
            if (_audioManager != default) await _audioManager.Initialize();
        }

        // 最初に遷移するシーン名を取得
        protected override string GetSceneName()
        {
            return "DemoScene";
        }
    }
}