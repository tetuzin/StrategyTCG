using System.Threading.Tasks;
using Ch120.Manager.Audio;

namespace Ch120.Demo
{
    public class DemoAudioManager : AudioManager<DemoAudioManager>
    {
        // SEの初期化読み込み処理
        protected override async Task InitSELoad()
        {
            // base.InitSELoad();
            // await SetSEAudioFile("", "");
        }
        
        // BGMの初期化読み込み処理
        protected override async Task InitBGMLoad()
        {
            // base.InitBGMLoad();
            // await SetBGMAudioFile("", "");
        }
    }
}