using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UK.Model.Card;

namespace UK.Unit.Card
{
    public class CardUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード画像")] private Image _image = default;
        [SerializeField, Tooltip("カード名")] private Text _cardName = default;
        [SerializeField, Tooltip("攻撃力テキスト")] private Text _atkText = default;
        [SerializeField, Tooltip("HPテキスト")] private Text _hpText = default;
        [SerializeField, Tooltip("カードコスト")] private Text _costText = default;
        [SerializeField, Tooltip("カード種別アイコン")] private GameObject _cardTypeIcon = default;
        [SerializeField, Tooltip("カード種別アイコン配列")] private GameObject[] _cardTypeIcons = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private CardModel _model = default;
        private int _curHp = default;
        private int _curAtk = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(CardModel model)
        {
            _model = model;
            SetCardImage();
            SetCardNameText(_model.CardName);
            SetCardTypeIcon(_model.CardType);
            SetAtkText(_model.Attack);
            SetHpText(_model.Hp);
            SetCostText(_model.Cost);
        }

        // ---------- Private関数 ----------

        // カード画像の設定
        private void SetCardImage()
        {
            // TODO
        }

        // カード名テキストの設定
        private void SetCardNameText(string text)
        {
            _cardName.text = text;
        }

        // カード種別アイコンの設定
        private void SetCardTypeIcon(int cardType)
        {
            if (_cardTypeIcons[cardType])
            {
                _cardTypeIcon = _cardTypeIcons[cardType];
            }
            else
            {
                _cardTypeIcon = _cardTypeIcons[0];
            }
        }

        // ATKテキストの設定
        private void SetAtkText(string text)
        {
            _atkText.text = text;
        }

        // HPテキストの設定
        private void SetHpText(string text)
        {
            _hpText.text = text;
        }

        // Costテキストの設定
        private void SetCostText(string text)
        {
            _costText.text = text;
        }

        // ---------- protected関数 ---------
    }
}

