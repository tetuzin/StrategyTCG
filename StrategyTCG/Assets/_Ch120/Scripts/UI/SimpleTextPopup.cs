using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ch120.Popup.Simple
{
    public class SimpleTextPopup : BasePopup
    {
        // ---------- 定数宣言 ----------

        private const float DELAY_TIME = 3.0f;

        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("メインテキスト")] private TextMeshProUGUI _mainText;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private float _curTime = default;
        private bool _isShow = default;

        // ---------- Unity組込関数 ----------

        void FixedUpdate()
        {
            if (_isShow)
            {
                _curTime += Time.deltaTime;

                if (_curTime >= DELAY_TIME)
                {
                    Hide();
                }
            }
        }

        // ---------- Public関数 ----------

        public void Show()
        {
            _isShow = true;
            _curTime = 0.0f;
            Open();
        }

        public void Hide()
        {
            _isShow = false;
            _curTime = 0.0f;
            Close();
        }

        // ---------- Private関数 ----------

        // メイン文言の設定
        private void SetMainText(string text)
        {
            if (_mainText == default) return;

            _mainText.text = text;
        }

        // ---------- protected関数 ---------

        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {

        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            _curTime = 0.0f;
            _isShow = false;

            // データの初期化
            var paramter = new {
                mainText = "Main Text."
            };
            paramter = param;

            // 各種取得データの設定
            SetMainText(paramter.mainText);
        }
    }
}

