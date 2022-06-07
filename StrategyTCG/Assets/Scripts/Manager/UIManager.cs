using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Ch120.Singleton;

using UK.Unit.Card;

namespace UK.Manager.UI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("キャンバス")] private Canvas _canvas = default;
        [SerializeField, Tooltip("ターン終了ボタン")] private Button _turnEndButton = default;
        [SerializeField, Tooltip("手札ボタン")] private Button _handButton = default;
        [SerializeField, Tooltip("ターンテキスト")] private Text _turnText = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // UIの初期化
        public void Initialize()
        {
            _handButton.gameObject.SetActive(false);
            SwitchTurnText(true);
            SetActiveActionUI(false);
        }

        // Canvasを取得
        public Canvas GetCanvas()
        {
            return _canvas;
        }

        // プレイヤーが操作するUIの活性化・非活性化
        public void SetActiveActionUI(bool b)
        {
            _turnEndButton.gameObject.SetActive(b);
        }

        // ターン終了ボタンにイベントを設定する
        public void SetTurnEndAction(UnityAction action)
        {
            _turnEndButton.onClick.RemoveAllListeners();
            _turnEndButton.onClick.AddListener(action);
        }

        // ターンテキストを切り替える
        public void SwitchTurnText(bool b)
        {
            if (b)
            {
                _turnText.text = "あなたのターン";
                _turnText.color = new Color(0 / 255f, 55 / 255f, 200 / 255f);
            }
            else
            {
                _turnText.text = "あいてのターン";
                _turnText.color = new Color(200 / 255f, 20 / 255f, 0 / 255f);
            }
        }

        // 手札ボタンの設定
        public void SetHandButtonAction(UnityAction callback)
        {
            _handButton.onClick.RemoveAllListeners();
            _handButton.onClick.AddListener(() => {
                _handButton.gameObject.SetActive(false);
                callback();
            });
        }

        // 手札ボタンの活性化・非活性化
        public void SetHandButtonActive(bool b)
        {
            _handButton.gameObject.SetActive(b);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

