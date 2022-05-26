using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Ch120.Utils.Resource;

using UK.Model.CardMain;

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

        private CardMainModel _model = default;
        private int _curHp = default;
        private int _curAtk = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(CardMainModel model)
        {
            _model = model;
            SetCardImage(_model.Image);
            SetCardNameText(_model.CardName);
            SetCardTypeIcon(_model.CardType);
            SetAtkText(_model.Attack);
            SetHpText(_model.Hp);
            SetCostText(_model.Cost);
        }

        // ---------- Private関数 ----------

        // カード画像の設定
        private void SetCardImage(string imageFile)
        {
            // TODO
            string path = ResourceUtils.GetTexturePath("CardImage/" + imageFile);
            Sprite sprite = ResourceUtils.GetSprite(path);
            _image.sprite = sprite;
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
        private void SetAtkText(int attack)
        {
            _atkText.text = attack.ToString();
        }

        // HPテキストの設定
        private void SetHpText(int hp)
        {
            _hpText.text = hp.ToString();
        }

        // Costテキストの設定
        private void SetCostText(int cost)
        {
            _costText.text = cost.ToString();
        }

        // ---------- protected関数 ---------
    }
}

