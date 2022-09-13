using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using ShunLib.Popup;
using ShunLib.ScrollView;
using UK.Const.Ability;
using UK.Const.Game;
using UK.Const.Card.Type;
using UK.Const.Effect;
using UK.Manager.Card;
using UK.Manager.Popup;
using UK.Model.CardMain;
using UK.Unit.Card;
using UK.Unit.Deck;
using UK.Utils.Card;

namespace UK.Popup.DeckCardView
{
    public class DeckCardViewPopup : BasePopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("スクロールビュー")] private CommonScrollView _scrollView;
        [SerializeField, Tooltip("決定ボタン")] private Button _decisionButton = default;
        [SerializeField, Tooltip("キャンセルボタン")] private Button _cancelButton = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private List<CardMainModel> _deckCardList = default;
        private bool[] _isSelectDeckCardList = default;
        private int _selectCardNum = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 山札一覧の設定
        public void SetDeckView(DeckUnit deckUnit, dynamic popupParam)
        {
            // 値の格納
            var param = new
            {
                ability = AbilityType.NONE,
                activeCardList = new List<CardType>(),
                abilityParameter = 0,
                cardId = 0
            };
            param = popupParam;
            _selectCardNum = param.abilityParameter;

            if (_selectCardNum > 0)
            {
                SetModalEvent(() => {});
            }

            // リストの初期化
            _deckCardList = new List<CardMainModel>(deckUnit.GetDeckCardList());
            _isSelectDeckCardList = new bool[_deckCardList.Count];

            // 表示するカードの生成
            for (int i = 0; i < _deckCardList.Count; i++)
            {
                CardUnit cardUnit = CardManager.Instance.Instantiate2DCardUnit(_deckCardList[i], true);
                cardUnit.Index = i;
                cardUnit.SetActiveBackImage(false);

                // カードが選択できる場合
                if (_selectCardNum != default)
                {
                    switch (param.ability)
                    {
                        // カードタイプ別でカード選択
                        case AbilityType.DECK_CARD_GET:
                        case AbilityType.DECK_PEASON_GET:
                        case AbilityType.DECK_PEASON_PLACE:
                        case AbilityType.DECK_BUILDING_GET:
                        case AbilityType.DECK_BUILDING_PLACE:
                        case AbilityType.DECK_GOODS_GET:
                        case AbilityType.DECK_GOODS_PLACE:
                        case AbilityType.DECK_POLICY_GET:
                        case AbilityType.DECK_POLICY_PLACE:
                            if (IsCardType(cardUnit, param.activeCardList))
                            {
                                // カード選択用のボタンイベント
                                cardUnit.SetSelectButtonEvent(() => {
                                    SetCardSelectButton(cardUnit);
                                });
                            }
                            else
                            {
                                cardUnit.SetGrayOut(true);
                            }
                            break;
                        
                        // カードIDでカード選択
                        case AbilityType.DECK_CARD_GET_NAME:
                        case AbilityType.DECK_CARD_PLACE_NAME:
                            if (param.cardId == cardUnit.CardModel.CardId)
                            {
                                // カード選択用のボタンイベント
                                cardUnit.SetSelectButtonEvent(() => {
                                    SetCardSelectButton(cardUnit);
                                });
                            }
                            else
                            {
                                cardUnit.SetGrayOut(true);
                            }
                            break;
                        
                        default:
                            cardUnit.SetGrayOut(true);
                            break;
                    }
                }
                else
                {
                    cardUnit.SetRemoveCardButtonEvent();
                    _decisionButton.gameObject.SetActive(false);
                }

                _scrollView.AddContent(cardUnit.gameObject);
            }
        }

        public void SetActiveCancelButton(TriggerType triggerType)
        {
            if (!CardUtils.CheckSelectEffectTrigger(triggerType))
            {
                _cancelButton.gameObject.SetActive(false);
            }
        }

        // ---------- Private関数 ----------

        // スクロールビューの初期化
        private void InitializeScrollView()
        {
            _scrollView.Initialize();
            _scrollView.SetVerticalScroll(false);
            _scrollView.SetHorizontalScroll(true);
        }

        // カード選択用のボタンイベント設定
        private void SetCardSelectButton(CardUnit cardUnit)
        {
            cardUnit.SetActiveSelectFrame(!_isSelectDeckCardList[cardUnit.Index]);
            _isSelectDeckCardList[cardUnit.Index] = !_isSelectDeckCardList[cardUnit.Index];
        }

        // 選択中カードの取得
        private List<CardMainModel> GetSelectCardModel()
        {
            List<CardMainModel> selectCardList = new List<CardMainModel>();
            for (int i = 0; i < _isSelectDeckCardList.Length; i++)
            {
                if (!_isSelectDeckCardList[i]) continue;

                selectCardList.Add(_deckCardList[i]);
            }
            return selectCardList;
        }

        // 選択中カードの枚数を取得
        private int GetSelectCardNum()
        {
            int count = 0;
            foreach(bool b in _isSelectDeckCardList)
            {
                if (b) count++;
            }
            return count;
        }

        // カード種別がリストに存在するか
        private bool IsCardType(CardUnit cardUnit, List<CardType> activeCardList)
        {
            if (activeCardList == default) return true;

            foreach (CardType type in activeCardList)
            {
                if ((CardType)cardUnit.CardModel.CardType == type) return true;
            }
            return false;
        }

        // TODO カードリストのソート
        private void SortCardList()
        {

        }

        // ---------- protected関数 ---------

        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {
            _decisionButton.onClick.RemoveAllListeners();
            _decisionButton.onClick.AddListener(() => {
                if (_selectCardNum != default)
                {
                    int num = GetSelectCardNum();
                    if (_selectCardNum == num)
                    {
                        List<CardMainModel> getCardList = GetSelectCardModel();

                        if (true)
                        {
                            // 選択したカードを取得(手札)
                            CardManager.Instance.GetDeckCard(GameConst.PLAYER, getCardList);
                            Close();
                        }
                        else
                        {
                            // TODO 選択したカードを取得(フィールド)
                        }
                    }
                    else
                    {
                        // ちゃんと選択するように注意ポップアップを表示する
                        string text = "";
                        if (_selectCardNum < num)
                        {
                            text = "合計" + _selectCardNum + "枚になるように選択してください。";
                        }
                        else
                        {
                            text = "あと" +  (_selectCardNum - num) + "枚選択してください。";
                        }
                        PopupManager.Instance.SetSimpleTextPopup(text, new Dictionary<string, Action>());
                        PopupManager.Instance.ShowSimpleTextPopup();
                    }
                }
            });

            _cancelButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.AddListener(() => {
                if (_selectCardNum != default)
                {
                    // 選択を本当にやめるか確認するポップアップを表示する
                    Dictionary<string, Action> actions = new Dictionary<string, Action>();
                    actions.Add(
                        ShunLib.Popup.Common.CommonPopup.DECISION_BUTTON_EVENT,
                        () =>
                        {
                            Close();
                        });
                    PopupManager.Instance.SetEffectCancelPopup(actions);
                    PopupManager.Instance.ShowEffectCancelPopup();
                }
                else
                {
                    Close();
                }
            });
        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            InitializeScrollView();
        }
    }
}