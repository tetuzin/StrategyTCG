using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Const.Game;
using UK.Model.CardMain;
using UK.Unit.Card;
using UK.Unit.Deck;
using UK.Unit.Place;

namespace UK.Unit.Field
{
    public class CardBattleField : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ---------

        [SerializeField, Tooltip("人物カード配置プレハブ")] private CardPlacement[] _personPlaces = default;
        [SerializeField, Tooltip("建造物カード配置プレハブ")] private CardPlacement[] _buildingPlaces = default;
        [SerializeField, Tooltip("デッキプレハブ")] private DeckUnit _deckUnit = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // 配置した人物カード配列
        private CardUnit[] _personUnits = new CardUnit[GameConst.MAX_PERSON_CARD];
        // 配置した建造物カード配列
        private CardUnit[] _buildingUnits = new CardUnit[GameConst.MAX_BUILDING_CARD];
        // 配置した人物カードの数
        private int _personNum = 0;
        // 配置した建造物カードの数
        private int _buildingNum = 0;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // デッキを設定
        public void SetDeck(List<CardMainModel> cardModels)
        {
            _deckUnit.Initialize(cardModels);
        }

        // 人物カードを追加配置する
        public void AddPersonUnit(CardUnit cardUnit)
        {
            _personUnits[_personNum] = cardUnit;
            _personNum++;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


