using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using Ch120.Singleton;

using UK.Const.Game;
using UK.Unit.Card;
using UK.Unit.Player;
using UK.UI.StatusGroup;

namespace UK.Manager.UI
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("キャンバス")]
        [SerializeField, Tooltip("キャンバス")] private Canvas _canvas = default;

        [Header("キャンバスグループ")]
        [SerializeField, Tooltip("プレイヤーアクション実行用UI")] private CanvasGroup _playerActionGroup = default;
        [SerializeField, Tooltip("手札表示・手札使用UI")] private CanvasGroup _handCardGroup = default;

        [Header("ボタン")]
        [SerializeField, Tooltip("ターン終了ボタン")] private Button _turnEndButton = default;
        [SerializeField, Tooltip("手札表示・非表示ボタン")] private Button _handViewButton = default;
        [SerializeField, Tooltip("手札ボタン")] private Button _handButton = default;

        [Header("テキスト")]
        [SerializeField, Tooltip("ターンテキスト")] private TextMeshProUGUI _turnText = default;

        [Header("ステータスグループ")]
        [SerializeField, Tooltip("自分のステータスグループ")] private PlayerStatusGroup _playerStatus = default;
        [SerializeField, Tooltip("相手のステータスグループ")] private PlayerStatusGroup _opponentStatus = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private bool _isShowHandView = false;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // UIの初期化
        virtual public void Initialize()
        {
            Debug.Log("UIManager.Initialize");
            InitializeButton();
            _handButton.gameObject.SetActive(false);
            SwitchTurnText(true);
            SetActiveActionUI(false);
            SetHandViewButton(() => {
                _isShowHandView = !_isShowHandView;
                _handCardGroup.alpha = _isShowHandView ? 1 : 0;
            });
            SetActiveHandGroup(false);
        }

        // ボタン関数の初期化
        public void InitializeButton()
        {
            _turnEndButton.onClick.RemoveAllListeners();
            _handButton.onClick.RemoveAllListeners();
            _handViewButton.onClick.RemoveAllListeners();
        }

        // Canvasを取得
        public Canvas GetCanvas()
        {
            return _canvas;
        }

        // プレイヤーが操作するUIの表示・非表示
        public void SetActiveActionUI(bool b)
        {
            _playerActionGroup.alpha = b ? 1 : 0;
        }

        // プレイヤーが操作するUIの活性化・非活性化
        public void SetActiveHandGroup(bool b)
        {
            _handCardGroup.interactable = b;
        }

        // ターン終了ボタンにイベントを設定する
        public void SetTurnEndAction(UnityAction action)
        {
            _turnEndButton.onClick.RemoveAllListeners();
            _turnEndButton.onClick.AddListener(action);
        }

        // ターン終了ボタンにイベントを設定する
        public void SetHandViewButton(UnityAction action)
        {
            _handViewButton.onClick.RemoveAllListeners();
            _handViewButton.onClick.AddListener(action);
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

        // 自分のステータス詳細を設定
        public void InitializePlayerStatusGroup(PlayerUnit statusGroup)
        {
            _playerStatus.Initialize(statusGroup, GameConst.PLAYER);
        }

        // 相手のステータス詳細を設定
        public void InitializeOpponentStatusGroup(PlayerUnit statusGroup)
        {
            _opponentStatus.Initialize(statusGroup, GameConst.OPPONENT);
        }

        // ステータス詳細を取得
        public PlayerStatusGroup GetStatusGroup(bool isPlayer)
        {
            if (isPlayer)
            {
                return _playerStatus;
            }
            else
            {
                return _opponentStatus;
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

