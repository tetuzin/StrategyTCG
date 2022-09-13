using System;
using ShunLib.Const.Audio;
using ShunLib.Dao;
using ShunLib.Singleton;
using UnityEngine;

namespace ShunLib.Manager.Game
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Func<string, BaseDao> _getDaoCallback = default;
        private Action<AudioEnum> _playSeCallback = default;
        private Action<AudioEnum> _playBgmCallback = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // Dao取得処理を実行
        public BaseDao GetDao(string daoName)
        {
            Debug.Log(_getDaoCallback);
            return _getDaoCallback?.Invoke(daoName);
        }
        
        // Dao取得処理のコールバックを設定
        public void SetDaoCallback(Func<string, BaseDao> callback)
        {
            _getDaoCallback = callback;
        }

        // SE再生
        public void PlaySE(AudioEnum key)
        {
            _playSeCallback?.Invoke(key);
        }
        
        // SE再生のコールバック設定
        public void SetPlaySECallback(Action<AudioEnum> callback)
        {
            _playSeCallback = callback;
        }
        
        // BGM再生
        public void PlayBGM(AudioEnum key)
        {
            _playBgmCallback?.Invoke(key);
        }
        
        // SE再生のコールバック設定
        public void SetPlayBGMCallback(Action<AudioEnum> callback)
        {
            _playBgmCallback = callback;
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}