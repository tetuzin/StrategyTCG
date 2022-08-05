using System;
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

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsOpen
        {
            get { return _isOpen; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        protected Dictionary<string, Action> _actions = default;
        protected bool _isOpen = default;
        protected GameObject _modal = default;
        protected Button _modalBtn = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void InitPopup(
            Dictionary<string, Action> actions = null,
            dynamic param = null,
            Canvas canvas = null
            )
        {
            gameObject.name = gameObject.name.Replace( "(Clone)", "" );
            SetActions(actions);
            SetData(param);
            SetButtonEvents();
        }

        // ポップアップを開く
        public virtual void Open(bool isModal = true)
        {
            if (!_isOpen)
            {
                SetModal(isModal);
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
        
        // モーダルボタンの設定
        public void SetModalEvent(Action action)
        {
            if (_modalBtn == default) return;
            
            _modalBtn.onClick.RemoveAllListeners();
            _modalBtn.onClick.AddListener(() =>
            {
                action();
            });
        }

        // ---------- Private関数 ----------

        // コールバックを設定
        void SetActions(Dictionary<string, Action> actions)
        {
            if (actions == null)
            {
                _actions = new Dictionary<string, Action>();
            }
            else
            {
                _actions = actions;
            }
        }

        // モーダルの設定
        void SetModal(bool isModal = true)
        {
            // モーダルが無ければ何もしない
            if (modalObject == null) { return; }

            // モーダル生成
            _modal = PopupUtils.CreateModal(modalObject);

            // モーダルにポップアップ閉じる処理を設定
            Button button = _modal.GetComponent<Button>();
            _modalBtn = button;
            if (isModal)
            {
                SetModalEvent(Close);
            }
        }

        // ---------- protected関数 ---------

        // ボタンの処理を設定
        protected virtual void SetButtonEvents(){}

        // データの設定
        protected virtual void SetData(dynamic param) {}

        // コールバックの取得
        protected Action GetAction(string key)
        {
            if (_actions.ContainsKey(key)) { return _actions[key]; }
            
            return () => {};
        }
    }
}