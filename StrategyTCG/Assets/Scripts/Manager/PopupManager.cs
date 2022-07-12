using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Ch120.Singleton;
using Ch120.Popup.Common;

using UK.Model.CardMain;
using UK.Popup.DeckCardView;
using UK.Unit.Deck;

namespace UK.Manager.Popup
{
    public class PopupManager : SingletonMonoBehaviour<PopupManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード消費ポップアップ")] private CommonPopup _consumptionPopup = default;
        [SerializeField, Tooltip("カード効果発動確認ポップアップ")] private CommonPopup _checkEffectPopup = default;
        [SerializeField, Tooltip("山札カード一覧ポップアップ")] private DeckCardViewPopup _deckCardViewPopup = default;

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
        public void SetConsumptionPopup(CardMainModel model, Dictionary<string, UnityAction> actions)
        {
            string cardTypeName = UK.Utils.Card.CardUtils.GetCardTypeName(model.CardType);
            string text = cardTypeName + "カード[" + model.CardName + "]を使用しますか？";
            var paramter = new {
                titleText = "カード使用確認",
                mainText = text,
                decisionText = "使う",
                cancelText = "やめる"
            };
            _consumptionPopup.InitPopup(actions, paramter);
        }

        // カード効果発動確認ポップアップ表示
        public void ShowCheckEffectPopup()
        {
            _checkEffectPopup.Open();
        }

        // カード効果発動確認ポップアップ設定
        public void SetCheckEffectPopup(CardMainModel model, Dictionary<string, UnityAction> actions)
        {
            string text = "[" + model.CardName + "]のカード効果を発動しますか？";
            var paramter = new {
                titleText = "カード効果発動確認",
                mainText = text,
                decisionText = "発動",
                cancelText = "やめる"
            };
            _checkEffectPopup.InitPopup(actions, paramter);
        }

        // 山札カード一覧ポップアップ表示
        public void ShowDeckCardViewPopup()
        {
            _deckCardViewPopup.Open();
        }

        // 山札カード一覧ポップアップ設定
        public void SetDeckCardViewPopup(DeckUnit deckUnit, Dictionary<string, UnityAction> actions)
        {
            var paramter = new {
                titleText = "山札カード一覧",
                decisionText = "決定",
                cancelText = "やめる"
            };
            _deckCardViewPopup.InitPopup(actions, paramter);
            _deckCardViewPopup.SetDeckView(deckUnit);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


