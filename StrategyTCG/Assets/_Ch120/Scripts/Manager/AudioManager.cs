using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System.Threading.Tasks;

using Ch120.Const.Audio;
using Ch120.Singleton;

namespace Ch120.Manager.Audio
{
    public class AudioManager<T> : SingletonMonoBehaviour<T> where T : MonoBehaviour
    {
        // ---------- 定数宣言 ----------

        private const string AUDIO_MIXER_MASTER = "Master";
        private const string AUDIO_MIXER_SE = "SE";
        private const string AUDIO_MIXER_BGM = "BGM";
        
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField] private AudioSource _audioSourceSE = default;
        [SerializeField] private AudioSource _audioSourceBGM = default;
        [SerializeField] private AudioMixer _audioMixer = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        private Dictionary<string, AudioClip> _seClips = new Dictionary<string, AudioClip>();
        private Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        public async Task Initialize()
        {
            await InitSELoad();
            await InitBGMLoad();
        }
        
        // SEを再生
        public void PlaySE(string key)
        {
            if (_seClips.ContainsKey(key))
            {
                _audioSourceSE.PlayOneShot(_seClips[key]);
            }
            else
            {
                Debug.LogWarning("<color=red>key\"" + key + "\"は設定されていません</color>");
            }
        }

        // BGMを再生
        public void PlayBGM(string key)
        {
            if (_bgmClips.ContainsKey(key))
            {
                _audioSourceBGM.loop = true;
                _audioSourceBGM.clip = _bgmClips[key];
                _audioSourceBGM.Play();
            }
            else
            {
                Debug.LogWarning("<color=red>key\"" + key + "\"は設定されていません</color>");
            }
        }

        // SEを設定する
        public void SetSEAudioClip(string key, AudioClip clip)
        {
            _seClips.Add(key, clip);
        }

        // BGMを設定する
        public void SetBGMAudioClip(string key, AudioClip clip)
        {
            _bgmClips.Add(key, clip);
        }

        // SEファイルの読み込み
        public Task SetSEAudioFile(string key, string file)
        {
            AudioClip audioClip = LoadAudioFile(AudioConst.SE_FILE_PATH + file);
            SetSEAudioClip(key, audioClip);
            return Task.CompletedTask;
        }

        // BGMファイルの読み込み
        public Task SetBGMAudioFile(string key, string file)
        {
            AudioClip audioClip = LoadAudioFile(AudioConst.BGM_FILE_PATH + file);
            SetBGMAudioClip(key, audioClip);
            return Task.CompletedTask;
        }
        
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
        
        // オーディオファイルの読み込み
        private AudioClip LoadAudioFile(string filePath)
        {
            if (System.IO.File.Exists(@"Assets/Resources/" + filePath + ".mp3"))
            {
                AudioClip audioClip = Resources.Load<AudioClip>(filePath);
                Debug.Log(audioClip);
                return audioClip;
            }
            else
            {
                Debug.LogWarning(filePath + "は存在しません。");
                return null;
            }
            // if (System.IO.File.Exists(filePath))
            // {
            //     AudioClip audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(filePath);
            //     Debug.Log(audioClip);
            //     return audioClip;
            // }
            // else
            // {
            //     Debug.LogWarning(filePath + "は存在しません。");
            //     return null;
            // }
        }
        
        // ---------- protected関数 ---------
        
        // SEの初期化読み込み処理
        protected virtual async Task InitSELoad() { }
        
        // BGMの初期化読み込み処理
        protected virtual async Task InitBGMLoad() { }
    }
}

