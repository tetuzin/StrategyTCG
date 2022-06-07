using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Ch120.Utils.Resource;

using UK.Manager.Card;
using UK.Manager.UI;
using UK.Model.CardMain;

namespace UK.Unit.Card
{
    public class CardUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード画像")] private Image _image = default;
        [SerializeField, Tooltip("カード名")] private TextMeshProUGUI _cardName = default;
        [SerializeField, Tooltip("攻撃力テキスト")] private TextMeshProUGUI _atkText = default;
        [SerializeField, Tooltip("HPテキスト")] private TextMeshProUGUI _hpText = default;
        [SerializeField, Tooltip("カードコスト")] private TextMeshProUGUI _costText = default;
        [SerializeField, Tooltip("カード種別アイコン")] private GameObject _cardTypeIcon = default;
        [SerializeField, Tooltip("カード種別アイコン配列")] private GameObject[] _cardTypeIcons = default;
        [SerializeField, Tooltip("カード背面画像")] private GameObject _cardBackImage = default;
        [SerializeField, Tooltip("カード選択用ボタン")] private Button _cardButton = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        public CardMainModel Model
        {
            get { return _model; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // CanvasのRectTransform
        private RectTransform _canvasRect = default;
        // カードモデル
        private CardMainModel _model = default;
        // 現在のHP
        private int _curHp = default;
        // 現在の攻撃力
        private int _curAtk = default;
        // 選択フラグ
        private bool _isSelect = default;
        // 手札の初期位置
        private Vector3 _defaultPosition = default;

        // ---------- Unity組込関数 ----------

        void FixedUpdate()
        {
            // 選択時にマウスカーソルに追従させる
            if (_isSelect)
            {
                Vector3 mousePos = Input.mousePosition;
                var magnification = _canvasRect.sizeDelta.x / Screen.width;
                mousePos.x = mousePos.x * magnification - _canvasRect.sizeDelta.x / 2;
                mousePos.y = mousePos.y * magnification - _canvasRect.sizeDelta.y / 2;
                mousePos.z = transform.localPosition.z;
                mousePos.y += 400;

                transform.localPosition = mousePos;
            }
        }

        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(CardMainModel model, RectTransform canvasRect)
        {
            _model = model;
            _canvasRect = canvasRect;
            SetCardImage(_model.Image);
            SetCardNameText(_model.CardName);
            SetCardTypeIcon(_model.CardType);
            SetAtkText(_model.Attack);
            SetHpText(_model.Hp);
            SetCostText(_model.Cost);

            // カードボタン処理
            _cardButton.onClick.RemoveAllListeners();
            _cardButton.onClick.AddListener(() => {

                // 選択中のカードがあるか
                if (!CardManager.Instance.IsSelect())
                {
                    _defaultPosition = this.gameObject.transform.localPosition;
                    CardManager.Instance.IsSelectCardUnit = this;

                    // 手札ボタンの設定
                    UIManager.Instance.SetHandButtonAction(() => {
                        _isSelect = false;
                        CardManager.Instance.IsSelectCardUnit = null;
                        this.gameObject.transform.localPosition = _defaultPosition;
                        _cardButton.gameObject.SetActive(true);
                    });
                    UIManager.Instance.SetHandButtonActive(true);
                    
                    _cardButton.gameObject.SetActive(false);
                    _isSelect = true;
                }
            });
        }

        // カードを非活性化
        public void SetCardActive(bool b)
        {
            _cardButton.gameObject.SetActive(b);
            SetActiveBackImage(b);
        }

        // ---------- Private関数 ----------

        // カード画像の設定
        private void SetCardImage(string imageFile)
        {
            // TODO
            string path = ResourceUtils.GetTexturePath("CardImage/" + imageFile);
            Sprite sprite = ResourceUtils.GetSprite(path);
            _image.sprite = sprite;
            _image.color = new Color32(255,255,255,255);
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
                GameObject obj = Instantiate(_cardTypeIcons[cardType], Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(_cardTypeIcon.transform, false);
            }
            else
            {
                GameObject obj = Instantiate(_cardTypeIcons[0], Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(_cardTypeIcon.transform, false);
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

        // カード背面画像の表示を設定
        public void SetActiveBackImage(bool b)
        {
            _cardBackImage.SetActive(b);
        }

        // ---------- protected関数 ---------
    }
}

