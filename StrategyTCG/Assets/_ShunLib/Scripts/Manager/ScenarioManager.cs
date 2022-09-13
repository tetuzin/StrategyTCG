using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.ScenarioUI;
using ShunLib.Scenario.TextWindow;
using ShunLib.Scenario.CharacterView;

namespace ShunLib.Manager.Scenario
{
    public class ScenarioManager : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("UIマネージャー")] protected ScenarioUIManager _uiManager = default;
        [SerializeField, Tooltip("テキストウィンドウ")] protected ScenarioTextWindow _textWindow = default;
        [SerializeField, Tooltip("キャラクタービュー")] protected ScenarioCharacterView _characterView = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        private void Start()
        {
            _uiManager.Initialize();
        }
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

