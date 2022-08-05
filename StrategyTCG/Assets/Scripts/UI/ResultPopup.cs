using UnityEngine;
using UnityEngine.UI;

using Ch120.Popup;
using Ch120.Utils.Popup;

namespace UK.Popup.Result
{
    public class ResultPopup : BasePopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("再戦ボタン")] private Button _rematchButton = default;
        [SerializeField, Tooltip("終了ボタン")] private Button _endButton = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        
        // 再戦ボタンの表示・非表示
        private void SetActiveRematchButton(bool isRematch)
        {
            _rematchButton.gameObject.SetActive(isRematch);
        }
        
        // ---------- protected関数 ---------
        
        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {
            _rematchButton.onClick.RemoveAllListeners();
            _rematchButton.onClick.AddListener(() =>
            {
                // TODO 再戦
            });
            
            _endButton.onClick.RemoveAllListeners();
            _endButton.onClick.AddListener(() =>
            {
                // TODO タイトルor対戦画面へ戻る
            });
        }
        
        // データの設定
        protected override void SetData(dynamic param)
        {
            var parameter = new
            {
                isRematch = false
            };
            parameter = param;
            SetActiveRematchButton(parameter.isRematch);
        }
    }
}