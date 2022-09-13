

using System.Collections;
using System.Collections.Generic;
using ShunLib.Const.Game;
using UnityEngine;

using ShunLib.Singleton;

namespace ShunLib.Manager.Particle
{
    public class ParticleManager : SingletonMonoBehaviour<ParticleManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [Header("パーティクル")]
        [SerializeField] protected List<ParticleSystem> _particleList = new List<ParticleSystem>();
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        
        void Start() { Initialise(); }
        
        // ---------- Public関数 ----------
        
        // パーティクルの生成
        public ParticleSystem CreateParticle(int index, Vector3 pos, Quaternion rot)
        {
            if (index < _particleList.Count && index >= 0)
            {
                GameObject particleObj = Instantiate(_particleList[index].gameObject, pos, rot);
                ParticleSystem particle = particleObj.GetComponent<ParticleSystem>();
                return particle;
            }
            Debug.LogWarning("そのIndexのパーティクルは存在しません。");
            return null;
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        
        // 初期化
        protected void Initialise()
        {
            
        }
    }
}

