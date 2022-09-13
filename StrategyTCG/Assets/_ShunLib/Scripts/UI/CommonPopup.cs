using System;
using UnityEngine;
using TMPro;

using ShunLib.UI.CommonBtn;

namespace ShunLib.Popup.Common
{
    public class CommonPopup : BasePopup
    {
        // ---------- 定数宣言 ----------

        public const string DECISION_BUTTON_EVENT = "decisionButtonEvent";
        public const string CANCEL_BUTTON_EVENT = "cancelButtonEvent";

        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("決定ボタン")] protected CommonButton _decisionButton = default;
        [SerializeField, Tooltip("キャンセルボタン")] protected CommonButton _cancelButton = default;
        [SerializeField, Tooltip("タイトルテキスト")] protected TextMeshProUGUI _titleText;
        [SerializeField, Tooltip("メインテキスト")] protected TextMeshProUGUI _mainText;
        [SerializeField, Tooltip("決定ボタンテキスト")] protected TextMeshProUGUI _decisionText;
        [SerializeField, Tooltip("キャンセルボタンテキスト")] protected TextMeshProUGUI _cancelText;
        
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
                _decisionButton.SetOnEvent(() => {
                    Action action = GetAction(DECISION_BUTTON_EVENT);
                    action?.Invoke();
                    Close();
                });
            }
            
            if (_cancelButton != default)
            {
                _cancelButton.SetOnEvent(() => {
                    Action action = GetAction(CANCEL_BUTTON_EVENT);
                    action?.Invoke();
                    Close();
                });
            }
        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            var parameter = new
            {
                titleText = param.titleText,
                mainText = param.mainText,
                decisionText = param.decisionText,
                cancelText = param.cancelText
            };
            
            // 各種取得データの設定
            SetTitleText(parameter.titleText);
            SetMainText(parameter.mainText);
            SetDecisionText(parameter.decisionText);
            SetCancelText(parameter.cancelText);
        }
    }
}


