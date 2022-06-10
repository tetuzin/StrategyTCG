using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Ch120.Utils.Popup;

namespace Ch120.Popup
{
    public class BasePopup : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("ポップアップのオブジェクト")] protected GameObject popupObject = default;
        [SerializeField, Tooltip("モーダルのオブジェクト")] protected GameObject modalObject = default;
        [SerializeField, Tooltip("キャンバス")] private Canvas _canvas = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsOpen
        {
            get { return _isOpen; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Dictionary<string, UnityAction> _actions = default;
        private bool _isOpen = default;
        private GameObject _modal = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void InitPopup(
            Dictionary<string, UnityAction> actions,
            dynamic param = null
            )
        {
            gameObject.name = gameObject.name.Replace( "(Clone)", "" );
            SetActions(actions);
            SetData(param);
            SetButtonEvents();
        }

        // ポップアップを開く
        public void Open()
        {
            if (!_isOpen)
            {
                SetModal();
                _isOpen = true;
                modalObject.SetActive(true);
                popupObject.SetActive(true);
                PopupUtils.AddPopupName(this.gameObject);
            }
        }

        // ポップアップを閉じる
        public virtual void Close()
        {
            if (_isOpen)
            {
                modalObject.SetActive(false);
                popupObject.SetActive(false);
                Destroy(_modal);
                PopupUtils.RemovePopupName(this.gameObject);
                _isOpen = false;
            }
        }

        // ポップアップが開いているか確認
        public bool CheckOpen()
        {
            return _isOpen;
        }

        // ---------- Private関数 ----------

        // コールバックを設定
        void SetActions(Dictionary<string, UnityAction> actions)
        {
            _actions = actions;
        }

        // モーダルの設定
        void SetModal()
        {
            // モーダルが無ければ何もしない
            if (modalObject == null) { return; }

            // モーダル生成
            _modal = PopupUtils.CreateModal(_canvas.gameObject);

            // モーダルにポップアップ閉じる処理を設定
            Button button = _modal.GetComponent<Button>();
            button.onClick.AddListener(Close);

            _modal.transform.SetParent(modalObject.transform);
        }

        // ---------- protected関数 ---------

        // ボタンの処理を設定
        protected virtual void SetButtonEvents(){}

        // データの設定
        protected virtual void SetData(dynamic param) {}

        // コールバックの取得
        protected UnityAction GetAction(string key)
        {
            if (_actions.ContainsKey(key)) { return _actions[key]; }
            
            return () => {};
        }
    }
}