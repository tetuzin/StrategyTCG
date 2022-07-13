using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using Ch120.Popup;
using Ch120.Popup.Common;
using Ch120.ScrollView;

using UK.Const.Card.Type;
using UK.Manager.Card;
using UK.Manager.Popup;
using UK.Model.CardMain;
using UK.Unit.Card;
using UK.Unit.Deck;

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
        public void SetDeckView(DeckUnit deckUnit, List<CardType> activeCardList = default, int selectCardNum = default)
        {
            activeCardList = new List<CardType>();
            activeCardList.Add(CardType.PERSON);

            _selectCardNum = selectCardNum;


            // リストの初期化
            _deckCardList = new List<CardMainModel>(deckUnit.GetDeckCardList());
            _isSelectDeckCardList = new bool[_deckCardList.Count];

            // 表示するカードの生成
            for (int i = 0; i < _deckCardList.Count; i++)
            {
                CardUnit cardUnit = CardManager.Instance.Instantiate2DCardUnit(_deckCardList[i], true);
                cardUnit.Index = i;
                cardUnit.SetActiveBackImage(false);

                if (_selectCardNum != default)
                {
                    if (IsCardType(cardUnit, activeCardList))
                    {
                        // カード選択用のボタンイベント
                        cardUnit.SetSelectButtonEvent(() => {
                            SetCardSelectButton(cardUnit);
                        });
                    }
                    else
                    {
                        cardUnit.SetGrayCard();
                    }
                }

                _scrollView.AddContent(cardUnit.gameObject);
            }
        }

        // ---------- Private関数 ----------

        // スクロールビューの初期化
        private void InitializeScrollView()
        {
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
        private List<CardMainModel> GetSelectCardUnit()
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
                        // TODO 選択したカードを取得
                    }
                    else
                    {
                        // ちゃんと選択するように注意ポップアップを表示する
                        string text = "";
                        if (_selectCardNum > num)
                        {
                            text = "合計" + num + "枚になるように選択してください。";
                        }
                        else
                        {
                            text = "あと" +  (num - _selectCardNum) + "枚選択してください。";
                        }
                        Dictionary<string, UnityAction> actions = new Dictionary<string, UnityAction>();
                        PopupManager.Instance.SetSimpleTextPopup(text, actions);
                        PopupManager.Instance.ShowSimpleTextPopup();
                    }
                }
            });

            _cancelButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.AddListener(() => {
                // TODO 選択を本当にやめるか確認するポップアップを表示する
                // 強制発動なら閉じるボタンは非表示にする
            });
        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            InitializeScrollView();
        }
    }
}