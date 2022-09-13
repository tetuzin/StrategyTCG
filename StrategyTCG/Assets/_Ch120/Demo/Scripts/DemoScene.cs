using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

using Ch120.Singleton;
using Ch120.Manager.Scene;

namespace Ch120.Demo
{
    public class DemoScene : SingletonMonoBehaviour<DemoScene>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------

        void Start()
        {
            List<DemoMainModel> list = ((DemoMainDao)DemoMasterManager.Instance.GetDao("DemoMainDao")).Get();
            Debug.Log(list[0].Text);
            Debug.Log(list[1].Text);
            Debug.Log(list[2].Text);
        }
        
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}