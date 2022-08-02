using System.Collections.Generic;
using UnityEngine;

using UK.Const.Game;
using UK.Const.Card.Type;
using UK.Model.CardMain;
using UK.Unit.Card;
using UK.Unit.Hand;
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
        [SerializeField, Tooltip("トラッシュプレハブ")] private DeckUnit _trashUnit = default;
        [SerializeField, Tooltip("手札オブジェクト")] private HandUnit _handUnit = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // プレイヤーフラグ
        private bool _isPlayer = default;
        // 配置した人物カード配列
        private CardUnit[] _personUnits = default;
        // 配置した建造物カード配列
        private CardUnit[] _buildingUnits = default;
        // 配置した人物カードの数
        private int _personNum = default;
        // 配置した建造物カードの数
        private int _buildingNum = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(List<CardMainModel> cardModels, bool isPlayer)
        {
            _isPlayer = isPlayer;

            // 手札の初期化
            _handUnit.Initialize(_isPlayer);

            // 山札の初期化
            _deckUnit.Initialize(cardModels, _isPlayer);

            // 人物カード配置箇所の初期化
            for (int i = 0; i < _personPlaces.Length; i++)
            {
                _personPlaces[i].Initialize(_isPlayer);
                _personPlaces[i].FieldType = CardType.PERSON;
            }

            // 建造物カード配置箇所の初期化
            for (int i = 0; i < _buildingPlaces.Length; i++)
            {
                _buildingPlaces[i].Initialize(_isPlayer);
                _buildingPlaces[i].FieldType = CardType.BUILDING;
            }

            // 各種値の初期化
            _personUnits = new CardUnit[GameConst.MAX_PERSON_CARD];
            _buildingUnits = new CardUnit[GameConst.MAX_BUILDING_CARD];
            _personNum = 0;
            _buildingNum = 0;
        }

        // TODO ターン開始時の場の更新処理
        public void UpdateFieldByStartTurn()
        {
            // カード配置後の経過ターンを加算する処理
            foreach (CardPlacement place in _personPlaces)
            {
                if (place.GetCard3DUnit() == null) { continue; }

                if (place.GetCard3DUnit().GetCardUnit() == null) { continue; }

                CardUnit unit = place.GetCard3DUnit().GetCardUnit();
                unit.UpdateTurn();
            }
            foreach (CardPlacement place in _buildingPlaces)
            {
                if (place.GetCard3DUnit() == null) { continue; }

                if (place.GetCard3DUnit().GetCardUnit() == null) { continue; }

                CardUnit unit = place.GetCard3DUnit().GetCardUnit();
                unit.UpdateTurn();
            }
        }

        // TODO ターン終了時の場の更新処理
        public void UpdateFieldByEndTurn()
        {
            
        }

        // 上からカードを引いて手札に加える
        public List<CardMainModel> DrawDeck(int num = 1)
        {
            List<CardMainModel> cardList = _deckUnit.Draw(num);
            _handUnit.AddHandCard(cardList);
            return cardList;
        }

        // 選択したカードを山札から取得し手札に加える
        public List<CardMainModel> GetDeckCard(List<CardMainModel> getCardList)
        {
            List<CardMainModel> cardList = _deckUnit.SelectDraw(getCardList);
            _handUnit.AddHandCard(cardList);
            return cardList;
        }

        // 人物カード配列を返す
        public CardPlacement[] GetPersonPlaces()
        {
            return _personPlaces;
        }

        // 建造物カード配列を返す
        public CardPlacement[] GetBuildingPlaces()
        {
            return _buildingPlaces;
        }

        // 山札を返す
        public DeckUnit GetDeckUnit()
        {
            return _deckUnit;
        }

        // 手札を返す
        public HandUnit GetHandUnit()
        {
            return _handUnit;
        }
        
        // 手札を山札に戻す
        public void BackHandCard(CardUnit cardUnit)
        {
            _handUnit.RemoveHandCard(cardUnit);
            _deckUnit.AddCard(cardUnit.CardModel);
        }
        
        // 手札を全て山札に戻す
        public void BackAllHandCard()
        {
            List<CardUnit> handUnit = new List<CardUnit>(_handUnit.HandCard);
            foreach (CardUnit cardUnit in handUnit)
            {
                BackHandCard(cardUnit);
            }
        }

        // 人物カードを追加配置する
        public void AddPersonUnit(CardUnit cardUnit)
        {
            _personUnits[_personNum] = cardUnit;
            _personNum++;
        }
        
        // 場に出ている人物カードのリストを取得
        public List<CardUnit> GetPlacePersonCardList()
        {
            List<CardUnit> placeCardList = new List<CardUnit>();
            foreach (CardPlacement personPlace in _personPlaces)
            {
                if (personPlace.IsPlacement())
                {
                    placeCardList.Add(personPlace.GetCardUnit());
                }
            }
            return placeCardList;
        }
        
        // 場に出ている建造物カードのリストを取得
        public List<CardUnit> GetPlaceBuildingCardList()
        {
            List<CardUnit> placeCardList = new List<CardUnit>();
            foreach (CardPlacement buildingPlace in _buildingPlaces)
            {
                if (buildingPlace.IsPlacement())
                {
                    placeCardList.Add(buildingPlace.GetCardUnit());
                }
            }
            return placeCardList;
        }
        
        // 場に出ているカードのリストを取得
        public List<CardUnit> GetPlaceCardList()
        {
            List<CardUnit> placeCardList = new List<CardUnit>();
            placeCardList.AddRange(GetPlacePersonCardList());
            placeCardList.AddRange(GetPlaceBuildingCardList());
            return placeCardList;
        }
        
        // 場に出ているカードのグレーアウト
        public void SetGrayOutPlaceCard(List<CardUnit> placeCardList)
        {
            foreach (CardUnit cardUnit in placeCardList)
            {
                cardUnit.SetBlinkFrame(false);
                cardUnit.SetGrayOut(true);
            }
        }
        
        // 場に出ているカードの点滅
        public void SetBlinkPlaceCard(List<CardUnit> placeCardList)
        {
            foreach (CardUnit cardUnit in placeCardList)
            {
                cardUnit.SetGrayOut(false, isButtonEvent:true);
                cardUnit.SetBlinkFrame(true);
            }
        }
        
        // 場に出ているカードの描画を初期化
        public void ResetPlaceCard()
        {
            foreach (CardPlacement personPlace in _personPlaces)
            {
                if (personPlace.IsPlacement())
                {
                    CardUnit cardUnit = personPlace.GetCardUnit();
                    cardUnit.SetGrayOut(false);
                    cardUnit.SetBlinkFrame(false);
                }
            }
            
            foreach (CardPlacement buildingPlace in _buildingPlaces)
            {
                if (buildingPlace.IsPlacement())
                {
                    CardUnit cardUnit = buildingPlace.GetCardUnit();
                    cardUnit.SetGrayOut(false);
                    cardUnit.SetBlinkFrame(false);
                }
            }
        }

        // 未配置の人物カードフィールドを返す
        public CardPlacement GetPersonPlacement()
        {
            foreach (CardPlacement personPlace in _personPlaces)
            {
                if (!personPlace.IsPlacement())
                {
                    return personPlace;
                }
            }
            return null;
        }
        
        // 未配置の建造物カードフィールドを返す
        public CardPlacement GetBuildingPlacement()
        {
            foreach (CardPlacement buildingPlace in _buildingPlaces)
            {
                if (!buildingPlace.IsPlacement())
                {
                    return buildingPlace;
                }
            }
            return null;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


