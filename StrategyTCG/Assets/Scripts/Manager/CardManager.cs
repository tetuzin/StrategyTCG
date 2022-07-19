using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Singleton;

using UK.Const.Game;
using UK.Manager.UI;
using UK.Model.CardMain;
using UK.Unit.Card;
using UK.Unit.Card3D;
using UK.Unit.Field;

namespace UK.Manager.Card
{
    public class CardManager : SingletonMonoBehaviour<CardManager>
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("2Dカードプレハブ")] private GameObject _2dCardPrefab = default;
        [SerializeField, Tooltip("3Dカードプレハブ")] private GameObject _3dCardPrefab = default;
        [SerializeField, Tooltip("自分の場")] private CardBattleField _playerField = default;
        [SerializeField, Tooltip("相手の場")] private CardBattleField _opponentField = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public CardUnit IsSelectCardUnit
        {
            get { return _isSelectCardUnit; }
            set { _isSelectCardUnit = value; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // 選択中のカードユニット
        private CardUnit _isSelectCardUnit = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 2Dカードオブジェクトを生成して返す
        public CardUnit Instantiate2DCardUnit(CardMainModel cardModel, bool isPlayer)
        {
            // インスタンスオブジェクト生成
            GameObject obj = Instantiate(_2dCardPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.localScale = new Vector3(0.7f, 0.7f, 1.0f);

            // カードユニット初期化
            CardUnit cardUnit = obj.GetComponent<CardUnit>();
            Canvas canvas = UIManager.Instance.GetCanvas();
            cardUnit.Initialize(cardModel, canvas.gameObject.GetComponent<RectTransform>(), isPlayer);
            return cardUnit;
        }

        // 3Dカードオブジェクトを生成して返す
        public Card3DUnit Instantiate3DCardUnit(CardMainModel cardModel, bool isPlayer)
        {
            // インスタンスオブジェクト生成
            GameObject obj = Instantiate(_3dCardPrefab, Vector3.zero, Quaternion.identity);

            // 2Dオブジェクトを生成
            CardUnit cardUnit = Instantiate2DCardUnit(cardModel, isPlayer);
            
            // カードユニット初期化
            Card3DUnit card3DUnit = obj.GetComponent<Card3DUnit>();
            card3DUnit.Initialize(cardUnit, isPlayer);
            return card3DUnit;
        }

        // カードをトラッシュへ送る
        public void TrashCard(bool isPlayer, CardUnit cardUnit)
        {
            // TODO トラッシュにカードを描画
            CardBattleField battleField = GetCardBattleField(isPlayer);

            // 手札カードの削除
            RemoveCard(isPlayer, cardUnit);
        }

        // 手札のカードを削除する
        public void RemoveCard(bool isPlayer, CardUnit cardUnit)
        {
            CardBattleField battleField = GetCardBattleField(isPlayer);
            battleField.GetHandUnit().RemoveHandCard(cardUnit);
        }
        
        // 自分のデッキを設定
        public void SetPlayerDeck(List<CardMainModel> cardModels)
        {
            _playerField.Initialize(cardModels, GameConst.PLAYER);
        }
        
        // 相手のデッキを設定
        public void SetOpponentDeck(List<CardMainModel> cardModels)
        {
            _opponentField.Initialize(cardModels, GameConst.OPPONENT);
        }

        // 場を取得
        public CardBattleField GetCardBattleField(bool isPlayer)
        {
            return isPlayer ? _playerField : _opponentField;
        }

        // 山札をドロー
        public void DeckDraw(bool isPlayer, int num = 1)
        {
            // Num枚引く
            GetCardBattleField(isPlayer).DrawDeck(num);

            // UI再描画
            int deckCount = GetCardBattleField(isPlayer).GetDeckUnit().GetCardNum();
            UIManager.Instance.GetStatusGroup(isPlayer).SetCurDeckNumText(deckCount);
        }

        // 山札からカードを取得
        public void GetDeckCard(bool isPlayer, List<CardMainModel> cardList)
        {
            GetCardBattleField(isPlayer).GetDeckCard(cardList);
            
            // UI再描画
            int deckCount = GetCardBattleField(isPlayer).GetDeckUnit().GetCardNum();
            UIManager.Instance.GetStatusGroup(isPlayer).SetCurDeckNumText(deckCount);
        }

        // TODO 山札からカードを配置
        public void PlaceDeckCard()
        {
            
        }

        // 選択しているカードがあるか
        public bool IsSelect()
        {
            return _isSelectCardUnit != null;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

