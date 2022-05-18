using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ch120.Game
{
    [DefaultExecutionOrder(-1)]
    public class SceneChecker : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------

        private void Awake()
        {
            Initialize();
        }

        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        private void Initialize()
        {
            if (!InitializeGameManager.IsInitialize)
            {
                SceneManager.LoadScene(0);
            }
        }

        // ---------- protected関数 ---------
    }
}

