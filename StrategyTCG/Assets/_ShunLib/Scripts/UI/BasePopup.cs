using System;
using System.Collections.Generic;
using ShunLib.Manager.Audio;
using ShunLib.Manager.Game;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using ShunLib.Utils.Popup;

namespace ShunLib.Popup
{
    public class BasePopup : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("親オブジェクト")] protected GameObject popupObject = default;
        [SerializeField, Tooltip("ポップアップのオブジェクト")] protected GameObject baseObject = default;
        [SerializeField, Tooltip("モーダルのオブジェクト")] protected GameObject modalObject = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        
        [SerializeField, Tooltip("アニメーションフラグ")] protected bool isAnimation = true;

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
            Initialize();
            SetActions(actions);
            SetData(param);
            SetButtonEvents();
        }

        // ポップアップを開く
        public void Open(bool isModal = true)
        {
            if (!_isOpen)
            {
                ShowOpenAnimation(() =>
                {
                    ShowPopup(isModal);
                }, () => { });
            }
        }

        // ポップアップを閉じる
        public void Close()
        {
            if (_isOpen)
            {
                ShowCloseAnimation(() => { }, () =>
                {
                    HidePopup();
                });
            }
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
        
        // 初期化
        protected virtual void Initialize() { }
        
        // ポップアップを開くときの処理
        protected virtual void ShowPopup(bool isModal)
        {
            SetModal(isModal);
            _isOpen = true;
            modalObject.SetActive(true);
            popupObject.SetActive(true);
            PopupUtils.AddPopupName(this.gameObject);
            PlayOpenSE();
        }
        
        // ポップアップを閉じるときの処理
        protected virtual void HidePopup()
        {
            modalObject.SetActive(false);
            popupObject.SetActive(false);
            Destroy(_modal);
            PopupUtils.RemovePopupName(this.gameObject);
            _isOpen = false;
            PlayCloseSE();
        }

        // ボタンの処理を設定
        protected virtual void SetButtonEvents() { }

        // データの設定
        protected virtual void SetData(dynamic param) { }

        // コールバックの取得
        protected Action GetAction(string key)
        {
            if (_actions.ContainsKey(key)) { return _actions[key]; }
            
            return () => {};
        }
        
        // ポップアップを開いたときのSE
        protected virtual void PlayOpenSE()
        {
            
        }
        
        // ポップアップを開いたときのSE
        protected virtual void PlayCloseSE()
        {
            
        }
        
        // ポップアップを開いたときのアニメ―ション
        protected virtual void ShowOpenAnimation(Action startCallback, Action endCallback)
        {
            if (isAnimation)
            {
                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(() =>
                {
                    baseObject.transform.localScale = Vector3.zero;
                    startCallback?.Invoke();
                }).Append(baseObject.transform.DOScale(Vector3.one, 0.2f)).AppendCallback(() =>
                {
                    endCallback?.Invoke();
                });
            }
            else
            {
                startCallback?.Invoke();
                endCallback?.Invoke();
            }
        }
        
        // ポップアップを閉じたときのアニメ―ション
        protected virtual void ShowCloseAnimation(Action startCallback, Action endCallback)
        {
            if (isAnimation)
            {
                Sequence seq = DOTween.Sequence();
                seq.AppendCallback(() =>
                {
                    baseObject.transform.localScale = Vector3.one;
                    startCallback?.Invoke();
                }).Append(baseObject.transform.DOScale(Vector3.zero, 0.2f)).AppendCallback(() =>
                {
                    endCallback?.Invoke();
                });
            }
            else
            {
                startCallback?.Invoke();
                endCallback?.Invoke();
            }
        }
    }
}