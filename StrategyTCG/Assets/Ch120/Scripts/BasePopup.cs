using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Ch120.Popup
{
    public class BasePopup : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("ポップアップのオブジェクト")] public GameObject popupObject;
        [SerializeField, Tooltip("モーダルのオブジェクト")] private GameObject modalObject;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        public static bool IsOpen
        {
            get { return _isOpen; }
        }
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Dictionary<string, UnityAction> _actions;
        private static bool _isOpen;
        
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
            }
        }

        // ポップアップを閉じる
        public virtual void Close()
        {
            modalObject.SetActive(false);
            popupObject.SetActive(false);
            Destroy(modalObject);
            Destroy(popupObject);
            _isOpen = false;
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