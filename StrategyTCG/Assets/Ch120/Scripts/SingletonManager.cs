using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Ch120.Singleton.SingletonManager
{
    public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (m_sync_obj)
                    {
                        if (_instance == null)
                        {
                            // _instance = new T();
                            Type t = typeof(T);
                            _instance = (T)FindObjectOfType (t);
                        }
                    }
                }
                return _instance;
            }
        }
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // シングルトンパターン用変数
        private static T _instance = default;
        // デッドロック回避用インスタンス
        private static object m_sync_obj = new object (); 

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // コンストラクタ
        protected SingletonManager() {}

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

