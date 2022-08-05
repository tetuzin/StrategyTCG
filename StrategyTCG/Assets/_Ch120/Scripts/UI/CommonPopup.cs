using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using Ch120.Popup;

namespace Ch120.Popup.Common
{
    public class CommonPopup : BasePopup
    {
        // ---------- 定数宣言 ----------

        public const string DECISION_BUTTON_EVENT = "decisionButtonEvent";
        public const string CANCEL_BUTTON_EVENT = "cancelButtonEvent";

        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("決定ボタン")] private Button _decisionButton = default;
        [SerializeField, Tooltip("キャンセルボタン")] private Button _cancelButton = default;
        [SerializeField, Tooltip("タイトルテキスト")] private TextMeshProUGUI _titleText;
        [SerializeField, Tooltip("メインテキスト")] private TextMeshProUGUI _mainText;
        [SerializeField, Tooltip("決定ボタンテキスト")] private TextMeshProUGUI _decisionText;
        [SerializeField, Tooltip("キャンセルボタンテキスト")] private TextMeshProUGUI _cancelText;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // タイトル文言の設定
        private void SetTitleText(string text)
        {
            if (_titleText == default) return;

            _titleText.text = text;
        }

        // メイン文言の設定
        private void SetMainText(string text)
        {
            if (_mainText == default) return;

            _mainText.text = text;
        }

        // 決定ボタン文言の設定
        private void SetDecisionText(string text)
        {
            if (_decisionText == default) return;

            _decisionText.text = text;
        }

        // キャンセルボタン文言の設定
        private void SetCancelText(string text)
        {
            if (_cancelText == default) return;
            
            _cancelText.text = text;
        }

        // ---------- protected関数 ---------

        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {
            if (_decisionButton != default)
            {
                _decisionButton.onClick.RemoveAllListeners();
                _decisionButton.onClick.AddListener(() => {
                    Action action = GetAction(DECISION_BUTTON_EVENT);
                    action();
                    Close();
                });
            }
            
            if (_cancelButton != default)
            {
                _cancelButton.onClick.RemoveAllListeners();
                _cancelButton.onClick.AddListener(() => {
                    Action action = GetAction(CANCEL_BUTTON_EVENT);
                    action();
                    Close();
                });
            }
        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            // データの初期化
            var paramter = new {
                titleText = "title",
                mainText = "Please select...",
                decisionText = "OK",
                cancelText = "Cancel"
            };
            paramter = param;

            // 各種取得データの設定
            SetTitleText(paramter.titleText);
            SetMainText(paramter.mainText);
            SetDecisionText(paramter.decisionText);
            SetCancelText(paramter.cancelText);
        }
    }
}


