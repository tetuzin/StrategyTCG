using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Singleton;

using UK.Const.Game;
using UK.Model.CardMain;
using UK.Unit.Field;

namespace UK.Manager.Card
{
    public class CardManager : SingletonMonoBehaviour<CardManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("自分の場")] private CardBattleField _playerField = default;
        [SerializeField, Tooltip("相手の場")] private CardBattleField _opponentField = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // 自分のデッキを設定
        public void SetPlayerDeck(List<CardMainModel> cardModels)
        {
            _playerField.Initialize(cardModels);
        }
        
        // 相手のデッキを設定
        public void SetOpponentDeck(List<CardMainModel> cardModels)
        {
            _opponentField.Initialize(cardModels);
        }

        // 手札を取得する(自分)
        public void InitializePlayerHand()
        {
            _playerField.DrawDeck(GameConst.START_HAND_CARD);
        }

         // 手札を取得する(相手)
        public void InitializeOpponentHand()
        {
            _opponentField.DrawDeck(GameConst.START_HAND_CARD);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

