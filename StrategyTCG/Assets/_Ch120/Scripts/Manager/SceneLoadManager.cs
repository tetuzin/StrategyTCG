using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Ch120.Singleton;

namespace Ch120.Manager.Scene
{
    public class SceneLoadManager : SingletonMonoBehaviour<SceneLoadManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 指定シーンへ遷移する（同期）
        public void TransitionScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        // 指定シーンへ遷移する（非同期）
        public IEnumerator TransitionAsyncScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

