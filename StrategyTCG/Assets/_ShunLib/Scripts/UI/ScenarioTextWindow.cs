using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShunLib.Scenario.TextWindow
{
    public class ScenarioTextWindow : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("テキスト")] protected TextMeshProUGUI _text = default;
        [SerializeField, Tooltip("ページ送りアイコン")] protected GameObject _nextIcon = default;
        [SerializeField, Tooltip("ウィンドウ画像")] protected Image _windowImage = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // テキストの設定
        public void SetText(string text)
        {
            _text.text = text;
        }

        // アイコンの表示・非表示
        public void SetActiveNextIcon(bool b)
        {
            _nextIcon.SetActive(b);
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}