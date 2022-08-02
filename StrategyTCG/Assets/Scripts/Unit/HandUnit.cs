using System.Collections;
using System.Collections.Generic;
using UK.Const.Card.Type;
using UnityEngine;

using UK.Const.Card.UseType;
using UK.Manager.Card;
using UK.Unit.Card;
using UK.Model.CardMain;
using UK.Utils.Card;

namespace UK.Unit.Hand
{
    public class HandUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード配置パネル")] private GameObject _panel = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        public List<CardUnit> HandCard
        {
            get { return _handCardUnit; }
            set { _handCardUnit = value; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        
        // ユニットリスト（手札）
        private List<CardUnit> _handCardUnit = default;
        // プレイヤーフラグ
        private bool _isPlayer = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(bool isPlayer)
        {
            _isPlayer = isPlayer;
            _handCardUnit = new List<CardUnit>();
        }

        // カードをパネルに配置（手札表示
        public void SetCardPanel(CardUnit card)
        {
            card.gameObject.transform.SetParent(_panel.transform);
            card.gameObject.transform.localPosition = Vector3.zero;
            card.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            card.SetActiveBackImage(!_isPlayer);
        }

        // カードリストを手札に追加
        public void AddHandCard(List<CardMainModel> addCardList)
        {
            foreach (CardMainModel cardModel in addCardList)
            {
                CardUnit cardUnit = CardManager.Instance.Instantiate2DCardUnit(cardModel, _isPlayer);
                SetCardPanel(cardUnit);
                _handCardUnit.Add(cardUnit);
            }
        }

        // 手札を削除
        public void RemoveHandCard(CardUnit cardUnit)
        {
            _handCardUnit.Remove(cardUnit);
            Destroy(cardUnit.gameObject);// TODO Destroy
        }
        
        // 手札を全て削除
        public void RemoveAllHandCard()
        {
            foreach (CardUnit cardUnit in _handCardUnit)
            {
                RemoveHandCard(cardUnit);
            }
        }
        
        // 配置使用のカードを取得
        public List<CardUnit> GetPlacementCardList()
        {
            List<CardUnit> cardList = new List<CardUnit>();
            foreach (CardUnit cardUnit in _handCardUnit)
            {
                if (CardUseType.PLACEMENT == CardUtils.GetCardUseType(cardUnit.CardModel))
                {
                    cardList.Add(cardUnit);
                }
            }
            return cardList;
        }
        
        // 人物カードを取得
        public List<CardUnit> GetPersonCardList()
        {
            List<CardUnit> cardList = new List<CardUnit>();
            foreach (CardUnit cardUnit in _handCardUnit)
            {
                if (CardType.PERSON == CardUtils.GetCardType(cardUnit.CardModel))
                {
                    cardList.Add(cardUnit);
                }
            }
            return cardList;
        }
        
        // 建造物カードを取得
        public List<CardUnit> GetBuildingCardList()
        {
            List<CardUnit> cardList = new List<CardUnit>();
            foreach (CardUnit cardUnit in _handCardUnit)
            {
                if (CardType.BUILDING == CardUtils.GetCardType(cardUnit.CardModel))
                {
                    cardList.Add(cardUnit);
                }
            }
            return cardList;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


