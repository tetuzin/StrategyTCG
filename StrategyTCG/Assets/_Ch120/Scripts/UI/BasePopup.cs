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
        
        [SerializeField, Tooltip("ポップアップのオブジェクト")] protected GameObject popupObject;
        [SerializeField, Tooltip("モーダルのオブジェクト")] protected GameObject modalObject;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        public bool IsOpen
        {
            get { return _isOpen; }
        }
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Dictionary<string, UnityAction> _actions;
        private bool _isOpen;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void InitPopup(
            Dictionary<string, UnityAction> actions,
            GameObject modal,
            dynamic param
            )
        {
            SetActions(actions);
            SetModal(modal);
            SetData(param);
            SetButtonEvents();
        }

        // ポップアップを開く
        public void Open()
        {
            if (!_isOpen)
            {
                _isOpen = true;
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
                Destroy(modalObject);
                Destroy(popupObject);
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
        void SetModal(GameObject modal)
        {
            if (modal != null)
            {
                Button button = modal.GetComponent<Button>();
                button.onClick.AddListener(Close);
                modalObject = modal;
            }
        }

        // ---------- protected関数 ---------

        // ボタンの処理を設定
        protected virtual void SetButtonEvents(){}

        // データの設定
        protected virtual void SetData(dynamic param) {}

        // コールバックの取得
        protected UnityAction GetAction(string key)
        {
            return _actions[key];
        }
    }
}