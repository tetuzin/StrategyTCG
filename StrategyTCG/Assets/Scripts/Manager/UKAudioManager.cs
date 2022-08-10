using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Threading.Tasks;

using Ch120.Manager.Audio;
using Ch120.Const.Audio;

namespace UK.Manager.Audio
{
    public class UKAudioManager : AudioManager<UKAudioManager>
    {
        // SEの初期化読み込み処理
        protected override async Task InitSELoad()
        {
            await SetSEAudioFile(AudioConst.SE_KEY_CLICK_BUTTON, AudioConst.SE_PATH_CLICK_BUTTON);
            await SetSEAudioFile(AudioConst.SE_KEY_HOVER_BUTTON, AudioConst.SE_PATH_HOVER_BUTTON);
        }
        
        // BGMの初期化読み込み処理
        protected override async Task InitBGMLoad()
        {
            
        }
    }
}