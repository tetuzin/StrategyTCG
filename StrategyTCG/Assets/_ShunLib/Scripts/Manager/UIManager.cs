using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using ShunLib.Singleton;
using ShunLib.Popup;
using ShunLib.UI.CommonBtn;
using ShunLib.Utils.Popup;

namespace ShunLib.Manager.UI
{
    public class UIManager : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        [Header("キャンバス")]
        [SerializeField] protected List<Canvas> _canvasList = new List<Canvas>();
        
        [Header("テキスト")]
        [SerializeField] protected List<TextMeshProUGUI> _textList = new List<TextMeshProUGUI>();
        
        [Header("ボタン")]
        [SerializeField] protected List<CommonButton> _buttonList = new List<CommonButton>();
        
        [Header("画像")]
        [SerializeField] protected List<Image> _imageList = new List<Image>();
        
        [Header("キャンバスグループ")]
        [SerializeField] protected List<CanvasGroup> _canvasGroupList = new List<CanvasGroup>();
        
        [Header("ポップアップ")]
        [SerializeField, Tooltip("ポップアップ生成時の親オブジェクト")] protected GameObject _popupParent = default;
        [SerializeField] protected List<BasePopup> _PopupList = new List<BasePopup>();
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // 初期化
        public virtual void Initialize()
        {
            InitButtons();
        }

        // テキストの表示・非表示
        public void SetTextActive(int index, bool isActive)
        {
            if (!IsBounds(index, _textList.Count)) return;
            _textList[index].gameObject.SetActive(isActive);
        }
        
        // テキストに文字列の設定
        public void SetText(int index, string text)
        {
            if (!IsBounds(index, _textList.Count)) return;
            _textList[index].text = text;
        }
        
        // テキストの色の設定
        public void SetTextColor(int index, Color color)
        {
            if (!IsBounds(index, _textList.Count)) return;
            _textList[index].color = color;
        }
        
        // ボタンの表示・非表示
        public void SetButtonActive(int index, bool isActive)
        {
            if (!IsBounds(index, _buttonList.Count)) return;
            _buttonList[index].gameObject.SetActive(isActive);
        }
        
        // ボタンのイベント設定
        public void SetButtonEvent(int index, UnityAction action)
        {
            if (!IsBounds(index, _buttonList.Count)) return;
            _buttonList[index].SetOnEvent(() =>
            {
                action();
            });
        }
        
        // 画像の表示・非表示
        public void SetImageActive(int index, bool isActive)
        {
            if (!IsBounds(index, _imageList.Count)) return;
            _imageList[index].gameObject.SetActive(isActive);
        }
        
        // 画像色の変更
        public void SetImageColor(int index, Color color)
        {
            if (!IsBounds(index, _imageList.Count)) return;
            _imageList[index].color = color;
        }
        
        // キャンバスグループの表示・非表示
        public void SetCanvasGroupActive(int index, bool isActive)
        {
            if (!IsBounds(index, _canvasGroupList.Count)) return;
            _canvasGroupList[index].alpha = isActive ? 1 : 0;
            _canvasGroupList[index].interactable = isActive;
            _canvasGroupList[index].blocksRaycasts = isActive;
        }
        
        // キャンバスグループのアルファ値設定
        public void SetCanvasGroupAlpha(int index, float alpha)
        {
            if (!IsBounds(index, _canvasGroupList.Count)) return;
            if (alpha > 1.0f)
            {
                _canvasGroupList[index].alpha = 1.0f;
            }
            else if (alpha < 0)
            {
                _canvasGroupList[index].alpha = 0.0f;
            }
            else
            {
                _canvasGroupList[index].alpha = alpha;
            }
        }

        // ポップアップの表示
        public void ShowPopup(int index, bool isModal = true)
        {
            if (!IsBounds(index, _PopupList.Count)) return;
            _PopupList[index].Open(isModal);
        }
        
        // ポップアップの設定
        public void SetPopup(int index, Dictionary<string, Action> actions,  dynamic param)
        {
            if (!IsBounds(index, _PopupList.Count)) return;
            _PopupList[index].InitPopup(actions, param);
        }
        
        // ポップアップの生成と表示
        public void CreateOpenPopup(int index, Dictionary<string, Action> actions = null,  dynamic param = null)
        {
            if (!IsBounds(index, _PopupList.Count)) return;
            if (_popupParent == default)
            {
                Debug.LogWarning("ポップアップを生成するための親オブジェクトが設定されていません！");
                return;
            }
            PopupUtils.OpenPopup(
                _popupParent,
                _PopupList[index].gameObject,
                actions,
                param
                );
        }
        
        // ---------- Private関数 ----------

        // 全ボタンの初期化
        private void InitButtons()
        {
            foreach (CommonButton button in _buttonList)
            {
                button.RemoveOnEvent();
            }
        }

        // ---------- protected関数 ---------
        
        // indexが範囲内かチェック
        protected bool IsBounds(int index, int count)
        {
            if (index < 0)
            {
                Debug.LogWarning("indexの値がマイナスです！");
                return false;
            }

            if (index >= count)
            {
                Debug.LogWarning("そのindexは要素数を超えています！");
                return false;
            }
            return true;
        }
    }
}