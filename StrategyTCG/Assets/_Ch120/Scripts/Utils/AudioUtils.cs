using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ch120.Utils.AudioUtils
{
    public class AudioUtils
    {
        // 辞書型でkeyとSEを保持
        private static Dictionary<string, AudioClip> _seClips = new Dictionary<string, AudioClip>();
        private static Dictionary<string, AudioClip> _bgmClips = new Dictionary<string, AudioClip>();
        private static AudioSource _audioSource = new AudioSource();
        private static float _volumeSE = 1.0f;
        private static float _volumeBGM = 1.0f;
        
        // SEを再生
        public static void PlaySE(string key, Vector3 pos)
        {
            if (_seClips.ContainsKey(key))
            {
                if (pos == null)
                {
                    AudioSource.PlayClipAtPoint(_seClips[key], Vector3.zero, _volumeSE);
                }
                else
                {
                    AudioSource.PlayClipAtPoint(_seClips[key], pos, _volumeSE);
                }
            }
            else
            {
                Debug.LogWarning("<color=red>key\"" + key + "\"は設定されていません</color>");
            }
        }

        // BGMを再生
        public static void PlayBGM(string key)
        {
            if (_bgmClips.ContainsKey(key))
            {
                _audioSource.loop = true;
                _audioSource.clip = _bgmClips[key];
                _audioSource.Play();
            }
            else
            {
                Debug.LogWarning("<color=red>key\"" + key + "\"は設定されていません</color>");
            }
        }

        // SEを設定する
        public static void SetSEAudioClip(string key, AudioClip clip)
        {
            _seClips.Add(key, clip);
        }

        // BGMを設定する
        public static void SetBGMAudioClip(string key, AudioClip clip)
        {
            _bgmClips.Add(key, clip);
        }

        // SE音量を設定する
        public static void SetSEVolume(float vol)
        {
            _volumeSE = vol;
        }

        // BGM音量を設定する
        public static void SetBGMVolume(float vol)
        {
            _volumeBGM = vol;
            _audioSource.volume = _volumeBGM;
        }

        // SEファイルの読み込み
        public static void SetSEAudioFile(string key, string file)
        {
            IEnumerator ienumerator = LoadAudioFile(file);
            AudioClip audioClip = (AudioClip)ienumerator.Current;
            SetSEAudioClip(key, audioClip);
        }

        // BGMファイルの読み込み
        public static void SetBGMAudioFile(string key, string file)
        {
            IEnumerator ienumerator = LoadAudioFile(file);
            AudioClip audioClip = (AudioClip)ienumerator.Current;
            SetBGMAudioClip(key, audioClip);
        }

        // オーディオファイルの読み込み
        private static IEnumerator LoadAudioFile(string filePath)
        {
            AudioClip audioClip = Resources.Load<AudioClip>(filePath);
            yield return audioClip;
        }
    }
}