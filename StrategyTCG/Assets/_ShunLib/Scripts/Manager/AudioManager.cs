using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Threading.Tasks;

using ShunLib.Const.Audio;
using ShunLib.Dict.Audio;

namespace ShunLib.Manager.Audio
{
    public class AudioManager : MonoBehaviour
    {
        // ---------- 定数宣言 ----------

        private const string AUDIO_MIXER_MASTER = "Master";
        private const string AUDIO_MIXER_SE = "SE";
        private const string AUDIO_MIXER_BGM = "BGM";
        
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField] private AudioSource _audioSourceSE = default;
        [SerializeField] private AudioSource _audioSourceBGM = default;
        [SerializeField] private AudioMixer _audioMixer = default;
        [SerializeField] private AudioDictionary _seAudioDictionary = default;
        [SerializeField] private AudioDictionary _bgmAudioDictionary = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        // 未使用
        // [SerializeField] private List<AudioClip> _seClipList = default;
        // [SerializeField] private List<AudioClip> _bgmClipList = default;
        // private Dictionary<string, AudioClip> _seClips = new Dictionary<string, AudioClip>();
        // private Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        public async Task Initialize()
        {
            await InitSELoad();
            await InitBGMLoad();
        }
        
        // AudioEnumでSEを再生
        public void PlaySE(AudioEnum key)
        {
            if (_seAudioDictionary.IsValue(key))
            {
                _audioSourceSE.Stop();
                _audioSourceSE.PlayOneShot(_seAudioDictionary.GetValue(key));
            }
            else
            {
                Debug.LogWarning("<color=red>SEAudioClip\"" + key + "\"は設定されていません</color>");
            }
        }
        
        // 未使用 indexでSEを再生
        // public void PlaySE(int index)
        // {
        //     if (index >= 0 && index < _seClipList.Count)
        //     {
        //         _audioSourceSE.Stop();
        //         _audioSourceSE.PlayOneShot(_seClipList[index]);
        //     }
        //     else
        //     {
        //         Debug.LogWarning("<color=red>SEAudioClip\"" + index + "\"番は設定されていません</color>");
        //     }
        // }
        
        // 未使用 KeyでSEを再生
        // public void PlaySE(string key)
        // {
        //     if (_seClips.ContainsKey(key))
        //     {
        //         _audioSourceSE.Stop();
        //         _audioSourceSE.PlayOneShot(_seClips[key]);
        //     }
        //     else
        //     {
        //         Debug.LogWarning("<color=red>key\"" + key + "\"は設定されていません</color>");
        //     }
        // }
        
        // AudioEnumでBGMを再生
        public void PlayBGM(AudioEnum key)
        {
            if (_bgmAudioDictionary.IsValue(key))
            {
                _audioSourceBGM.loop = true;
                _audioSourceBGM.clip = _bgmAudioDictionary.GetValue(key);
                _audioSourceBGM.Play();
            }
            else
            {
                Debug.LogWarning("<color=red>BGMAudioClip\"" + key + "\"番は設定されていません</color>");
            }
        }
        
        // 未使用 indexでBGMを再生
        // public void PlayBGM(int index)
        // {
        //     if (index >= 0 && index < _bgmClipList.Count)
        //     {
        //         _audioSourceBGM.loop = true;
        //         _audioSourceBGM.clip = _bgmClipList[index];
        //         _audioSourceBGM.Play();
        //     }
        //     else
        //     {
        //         Debug.LogWarning("<color=red>BGMAudioClip\"" + index + "\"番は設定されていません</color>");
        //     }
        // }

        // 未使用 KeyでBGMを再生
        // public void PlayBGM(string key)
        // {
        //     if (_bgmClips.ContainsKey(key))
        //     {
        //         _audioSourceBGM.loop = true;
        //         _audioSourceBGM.clip = _bgmClips[key];
        //         _audioSourceBGM.Play();
        //     }
        //     else
        //     {
        //         Debug.LogWarning("<color=red>key\"" + key + "\"は設定されていません</color>");
        //     }
        // }

        // 未使用 SEを設定する
        // public void SetSEAudioClip(string key, AudioClip clip)
        // {
        //     _seClips.Add(key, clip);
        // }
        //
        // 未使用 BGMを設定する
        // public void SetBGMAudioClip(string key, AudioClip clip)
        // {
        //     _bgmClips.Add(key, clip);
        // }

        // 未使用 SEファイルの読み込み
        // public Task SetSEAudioFile(string key, string file)
        // {
        //     AudioClip audioClip = LoadAudioFile(AudioConst.SE_FILE_PATH + file);
        //     SetSEAudioClip(key, audioClip);
        //     return Task.CompletedTask;
        // }

        // 未使用 BGMファイルの読み込み
        // public Task SetBGMAudioFile(string key, string file)
        // {
        //     AudioClip audioClip = LoadAudioFile(AudioConst.BGM_FILE_PATH + file);
        //     SetBGMAudioClip(key, audioClip);
        //     return Task.CompletedTask;
        // }
        
        // Master音量の設定
        public void SetMasterVolume(float volume)
        {
            _audioMixer.SetFloat(AUDIO_MIXER_MASTER, volume);
        }
        
        // SE音量の設定
        public void SetSEVolume(float volume)
        {
            _audioMixer.SetFloat(AUDIO_MIXER_SE, volume);
        }
        
        // BGM音量の設定
        public void SetBGMVolume(float volume)
        {
            _audioMixer.SetFloat(AUDIO_MIXER_BGM, volume);
        }

        // ---------- Private関数 ----------
        
        // 未使用 オーディオファイルの読み込み
        // private AudioClip LoadAudioFile(string filePath)
        // {
        //     if (System.IO.File.Exists(@"Assets/Resources/" + filePath + ".mp3"))
        //     {
        //         AudioClip audioClip = Resources.Load<AudioClip>(filePath);
        //         Debug.Log(audioClip);
        //         return audioClip;
        //     }
        //     else
        //     {
        //         Debug.LogWarning(filePath + "は存在しません。");
        //         return null;
        //     }
        // }
        
        // ---------- protected関数 ---------
        
        // SEの初期化読み込み処理
        protected virtual async Task InitSELoad() { }
        
        // BGMの初期化読み込み処理
        protected virtual async Task InitBGMLoad() { }
    }
}

