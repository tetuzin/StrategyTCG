using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Ch120.Popup;
using Ch120.Utils.Resource;
using Ch120.ScrollView;
using UK.Utils.Card;
using UK.Model.CardMain;

namespace UK.Popup.CardDetail
{
    public class CardDetailPopup : BasePopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        
        [SerializeField, Tooltip("キャンセルボタン")] private Button _cancelButton = default;
        [SerializeField, Tooltip("カード画像")] private Image _cardImage;
        [SerializeField, Tooltip("タイトルテキスト")] private TextMeshProUGUI _titleText;
        [SerializeField, Tooltip("カード名テキスト")] private TextMeshProUGUI _cardNameText;
        [SerializeField, Tooltip("カードタイプ名テキスト")] private TextMeshProUGUI _cardTypeText;
        // [SerializeField, Tooltip("レアリティテキスト")] private TextMeshProUGUI _rarityText;
        [SerializeField, Tooltip("国名テキスト")] private TextMeshProUGUI _countryText;
        [SerializeField, Tooltip("HPテキスト")] private TextMeshProUGUI _hpText;
        [SerializeField, Tooltip("攻撃力テキスト")] private TextMeshProUGUI _atkText;
        [SerializeField, Tooltip("コストテキスト")] private TextMeshProUGUI _costText;
        [SerializeField, Tooltip("年月紹介テキスト")] private TextMeshProUGUI _yearText;
        [SerializeField, Tooltip("史実紹介テキスト")] private TextMeshProUGUI _mainText;
        [SerializeField, Tooltip("効果名テキスト")] private TextMeshProUGUI _effectText;
        [SerializeField, Tooltip("スクロールビュー")] private CommonScrollView _scrollView;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private CardMainModel _model = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        
        // スクロールビューの初期化
        private void InitializeScrollView()
        {
            _scrollView.Initialize();
            _scrollView.SetVerticalScroll(true);
            _scrollView.SetHorizontalScroll(false);
        }
        
        // ---------- protected関数 ---------
        
        // データの設定
        protected override void SetData(dynamic param)
        {
            var parameter = new
            {
                cardMainModel = new CardMainModel()
            };
            parameter = param;
            _model = parameter.cardMainModel;
            
            string path = ResourceUtils.GetTexturePath("CardImage/" + _model.Image);
            Sprite sprite = ResourceUtils.GetSprite(path);
            _cardImage.sprite = sprite;
            _cardImage.color = new Color32(255,255,255,255);

            _titleText.text = _model.CardName + "のカード詳細";

            _cardNameText.text = _model.CardName;

            _cardTypeText.text = CardUtils.GetCardTypeName(_model.CardType);

            // _rarityText.text = _model.Rarity.ToString();

            _countryText.text = CardUtils.GetCardCountryName(_model.CountryId);

            _hpText.text = _model.Hp.ToString();

            _atkText.text = _model.Attack.ToString();

            _costText.text = _model.Cost.ToString();

            _yearText.text = _model.Year;

            _mainText.text = _model.Text;

            _effectText.text = CardUtils.GetEffectMainModel(_model.EffectId).EffectText;

            InitializeScrollView();
        }
        
        // ボタンイベントの設定
        protected override void SetButtonEvents()
        {
            _cancelButton.onClick.RemoveAllListeners();
            _cancelButton.onClick.AddListener(() =>
            {
                Close();
            });
        }
    }
}

