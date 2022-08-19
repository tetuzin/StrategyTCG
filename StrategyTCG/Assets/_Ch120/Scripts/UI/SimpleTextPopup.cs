using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Ch120.Popup.Simple
{
    public class SimpleTextPopup : BasePopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("メインテキスト")] protected TextMeshProUGUI _mainText = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        [SerializeField, Tooltip("表示する時間")] protected float delayTime = 1.5f;
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        protected float _curTime = default;
        protected bool _isShow = default;

        // ---------- Unity組込関数 ----------

        void FixedUpdate()
        {
            if (_isShow)
            {
                _curTime += Time.deltaTime;

                if (_curTime >= delayTime)
                {
                    Hide();
                }
            }
        }

        // ---------- Public関数 ----------
        
        // ポップアップを開く
        public override void Open(bool isModal = true)
        {
            base.Open(isModal);
            Show();
        }

        public virtual void Show()
        {
            _isShow = true;
            _curTime = 0.0f;
        }

        public virtual void Hide()
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
                mainText = param.mainText
            };

            // 各種取得データの設定
            SetMainText(paramter.mainText);
        }
    }
}

