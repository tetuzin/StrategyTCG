using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Threading.Tasks;

using ShunLib.Manager.Audio;
using ShunLib.Const.Audio;

namespace UK.Manager.Audio
{
    public class UKAudioManager : AudioManager<UKAudioManager>
    {
        // SEの初期化読み込み処理
        protected override async Task InitSELoad()
        {
            base.InitSELoad();
            await SetSEAudioFile(AudioConst.SE_CLICK_BUTTON, AudioConst.SE_CLICK_BUTTON);
            await SetSEAudioFile(AudioConst.SE_HOVER_BUTTON, AudioConst.SE_HOVER_BUTTON);
            await SetSEAudioFile(AudioConst.SE_ATTACK_PHASE, AudioConst.SE_ATTACK_PHASE);
            await SetSEAudioFile(AudioConst.SE_BATTLE_START, AudioConst.SE_BATTLE_START);
            await SetSEAudioFile(AudioConst.SE_BOM, AudioConst.SE_BOM);
            await SetSEAudioFile(AudioConst.SE_CANNON, AudioConst.SE_CANNON);
            await SetSEAudioFile(AudioConst.SE_EFFECT, AudioConst.SE_EFFECT);
            await SetSEAudioFile(AudioConst.SE_PUNCH, AudioConst.SE_PUNCH);
            await SetSEAudioFile(AudioConst.SE_RESULT_LOSE, AudioConst.SE_RESULT_LOSE);
            await SetSEAudioFile(AudioConst.SE_RESULT_WIN, AudioConst.SE_RESULT_WIN);
            await SetSEAudioFile(AudioConst.SE_SLASH, AudioConst.SE_SLASH);
            await SetSEAudioFile(AudioConst.SE_UNIT_HP_HEAL, AudioConst.SE_UNIT_HP_HEAL);
            await SetSEAudioFile(AudioConst.SE_WARNING, AudioConst.SE_WARNING);
            await SetSEAudioFile(AudioConst.SE_CARD_OPEN, AudioConst.SE_CARD_OPEN);
            await SetSEAudioFile(AudioConst.SE_CARD_SHUFFLE, AudioConst.SE_CARD_SHUFFLE);
            await SetSEAudioFile(AudioConst.SE_CARD_PLACEMENT, AudioConst.SE_CARD_PLACEMENT);
        }
        
        // BGMの初期化読み込み処理
        protected override async Task InitBGMLoad()
        {
            base.InitBGMLoad();
            await SetBGMAudioFile(AudioConst.BGM_BATTLE_BACK, AudioConst.BGM_BATTLE_BACK);
        }
    }
}