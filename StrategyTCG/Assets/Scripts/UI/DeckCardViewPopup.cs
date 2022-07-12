using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Ch120.Popup;
using Ch120.Popup.Common;
using Ch120.ScrollView;

using UK.Manager.Card;
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

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 山札一覧の設定
        public void SetDeckView(DeckUnit deckUnit)
        {
            // リストの初期化
            _deckCardList = new List<CardMainModel>(deckUnit.GetDeckCardList());
            _isSelectDeckCardList = new bool[_deckCardList.Count];

            // 表示するカードの生成
            for (int i = 0; i < _deckCardList.Count; i++)
            {
                CardUnit cardUnit = CardManager.Instance.Instantiate2DCardUnit(_deckCardList[i], true);
                cardUnit.Index = i;
                cardUnit.SetActiveBackImage(false);

                // カード選択用のボタンイベント
                cardUnit.SetSelectButtonEvent(() => {
                    SetCardSelectButton(cardUnit);
                });

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

        // ---------- protected関数 ---------

        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {

        }

        // データの設定
        protected override void SetData(dynamic param)
        {
            InitializeScrollView();
        }
    }
}