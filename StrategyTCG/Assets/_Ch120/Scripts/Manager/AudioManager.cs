using System.Collections;
using System.Collections.Generic;
using Ch120.Singleton;
using UnityEngine;
using UnityEngine.Audio;
using Ch120.Const.Audio;

namespace Ch120.Manager.Audio
{
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField] private AudioSource _audioSourceSE = default;
        [SerializeField] private AudioSource _audioSourceBGM = default;
        [SerializeField] private AudioMixer _audioMixer = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        
        public float VolumeSE
        {
            get { return _volumeSE; }
            set { _volumeSE = value; }
        }
        
        public float VolumeBGM
        {
            get { return _volumeBGM; }
            set
            {
                _volumeBGM = value;
                // _audioSourceBGM.volume = _volumeBGM;
            }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        private Dictionary<string, AudioClip> _seClips = default;
        private Dictionary<string, AudioClip> _bgmClips = default;
        private float _volumeSE = 1.0f;
        private float _volumeBGM = 1.0f;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        public void Initialize()
        {
            _seClips = new Dictionary<string, AudioClip>();
            _bgmClips = new Dictionary<string, AudioClip>();
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
        public void SetSEAudioFile(string key, string file)
        {
            AudioClip audioClip = LoadAudioFile(AudioConst.SE_FILE_PATH + file);
            Debug.Log(audioClip);
            SetSEAudioClip(key, audioClip);
        }

        // BGMファイルの読み込み
        public void SetBGMAudioFile(string key, string file)
        {
            AudioClip audioClip = LoadAudioFile(AudioConst.BGM_FILE_PATH + file);
            SetBGMAudioClip(key, audioClip);
        }
        
        // Master音量の設定
        public void SetMasterVolume(float volume)
        {
            _audioMixer.SetFloat("Master", volume);
        }
        
        // SE音量の設定
        public void SetSEVolume(float volume)
        {
            _audioMixer.SetFloat("SE", volume);
        }
        
        // BGM音量の設定
        public void SetBGMVolume(float volume)
        {
            _audioMixer.SetFloat("BGM", volume);
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
        }
        
        // ---------- protected関数 ---------
    }
}

