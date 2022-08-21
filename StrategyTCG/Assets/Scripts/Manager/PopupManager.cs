using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Ch120.Singleton;
using Ch120.Popup.Common;
using Ch120.Popup.Simple;

using UK.Model.CardMain;
using UK.Popup.CardEffectActivate;
using UK.Unit.Deck;
using UK.Popup.DeckCardView;
using UK.Popup.Result;

namespace UK.Manager.Popup
{
    public class PopupManager : SingletonMonoBehaviour<PopupManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード消費ポップアップ")] private CommonPopup _consumptionPopup = default;
        [SerializeField, Tooltip("カード効果発動確認ポップアップ")] private CommonPopup _checkEffectPopup = default;
        [SerializeField, Tooltip("山札カード一覧ポップアップ")] private DeckCardViewPopup _deckCardViewPopup = default;
        [SerializeField, Tooltip("シンプルテキストポップアップ")] private SimpleTextPopup _simpleTextPopup = default;
        [SerializeField, Tooltip("勝利ポップアップ")] private ResultPopup _resultWinPopup = default;
        [SerializeField, Tooltip("敗北ポップアップ")] private ResultPopup _resultLosePopup = default;
        [SerializeField, Tooltip("カード効果発動ポップアップ")] private CardEffectActivatePopup _cardEffectActivatePopup = default;
        [SerializeField, Tooltip("Player攻撃ポップアップ")] private SimpleTextPopup _playerAttackPopup = default;
        [SerializeField, Tooltip("Opponent攻撃ポップアップ")] private SimpleTextPopup _opponentAttackPopup = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // カード消費ポップアップ表示
        public void ShowConsumptionPopup()
        {
            _consumptionPopup.Open();
        }

        // カード消費ポップアップ設定
        public void SetConsumptionPopup(CardMainModel model, Dictionary<string, Action> actions)
        {
            string cardTypeName = UK.Utils.Card.CardUtils.GetCardTypeName(model.CardType);
            string text = cardTypeName + "カード[" + model.CardName + "]を使用しますか？";
            var parameter = new {
                titleText = "カード使用確認",
                mainText = text,
                decisionText = "使う",
                cancelText = "やめる"
            };
            _consumptionPopup.InitPopup(actions, parameter);
        }

        // カード効果発動確認ポップアップ表示
        public void ShowCheckEffectPopup()
        {
            _checkEffectPopup.Open();
        }

        // カード効果発動確認ポップアップ設定
        public void SetCheckEffectPopup(CardMainModel model, Dictionary<string, Action> actions)
        {
            string text = "[" + model.CardName + "]のカード効果を発動しますか？";
            var parameter = new {
                titleText = "カード効果発動確認",
                mainText = text,
                decisionText = "発動",
                cancelText = "やめる"
            };
            _checkEffectPopup.InitPopup(actions, parameter);
        }

        // 山札カード一覧ポップアップ表示
        public void ShowDeckCardViewPopup()
        {
            _deckCardViewPopup.Open(isModal:false);
        }

        // 山札カード一覧ポップアップ設定
        public void SetDeckCardViewPopup(DeckUnit deckUnit, Dictionary<string, Action> actions, dynamic popupParam)
        {
            var parameter = new {
                titleText = "山札カード一覧",
                decisionText = "決定",
                cancelText = "やめる"
            };
            _deckCardViewPopup.InitPopup(actions, parameter);
            _deckCardViewPopup.SetDeckView(deckUnit, popupParam);
        }

        // シンプルテキストポップアップ表示
        public void ShowSimpleTextPopup()
        {
            _simpleTextPopup.Show();
        }

        // シンプルテキストポップアップ設定
        public void SetSimpleTextPopup(string text, Dictionary<string, Action> actions = null)
        {
            var parameter = new {
                mainText = text
            };
            _simpleTextPopup.InitPopup(actions, parameter);
        }
        
        // 効果発動キャンセルポップアップ表示
        public void ShowEffectCancelPopup()
        {
            _consumptionPopup.Open();
        }
        
        // 効果発動キャンセルポップアップ設定
        public void SetEffectCancelPopup(Dictionary<string, Action> actions)
        {
            var parameter = new {
                titleText = "効果発動確認",
                mainText = "効果の発動をやめますか？",
                decisionText = "やめる",
                cancelText = "発動続行"
            };
            _consumptionPopup.InitPopup(actions, parameter);
        }
        
        // 勝利ポップアップ表示
        public void ShowResultWinPopup()
        {
            _resultWinPopup.Open(isModal:false);
        }
        
        // 勝利ポップアップ設定
        public void SetResultWinPopup(bool isRematchBool, Dictionary<string, Action> actions)
        {
            var parameter = new {
                isRematch = isRematchBool
            };
            _resultWinPopup.InitPopup(actions, parameter);
        }
        
        // 敗北ポップアップ表示
        public void ShowResultLosePopup()
        {
            _resultLosePopup.Open(isModal:false);
        }
        
        // 敗北ポップアップ設定
        public void SetResultLosePopup(bool isRematchBool, Dictionary<string, Action> actions)
        {
            var parameter = new {
                isRematch = isRematchBool
            };
            _resultLosePopup.InitPopup(actions, parameter);
        }
        
        // カード効果発動ポップアップ表示
        public void ShowCardEffectActivatePopup()
        {
            _cardEffectActivatePopup.Open(isModal:false);
        }
        
        // カード効果発動ポップアップ設定
        public void SetCardEffectActivatePopup(dynamic param, Dictionary<string, Action> actions)
        {
            _cardEffectActivatePopup.InitPopup(actions, param);
        }
        
        // シンプルテキストポップアップ表示
        public void ShowPlayerAttackPopup()
        {
            _playerAttackPopup.Open(isModal:false);
        }

        // シンプルテキストポップアップ設定
        public void SetPlayerAttackPopup(string text, Dictionary<string, Action> actions = null)
        {
            var parameter = new {
                mainText = text
            };
            _playerAttackPopup.InitPopup(actions, parameter);
        }
        
        // シンプルテキストポップアップ表示
        public void ShowOpponentAttackPopup()
        {
            _opponentAttackPopup.Open(isModal:false);
        }

        // シンプルテキストポップアップ設定
        public void SetOpponentAttackPopup(string text, Dictionary<string, Action> actions = null)
        {
            var parameter = new {
                mainText = text
            };
            _opponentAttackPopup.InitPopup(actions, parameter);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


