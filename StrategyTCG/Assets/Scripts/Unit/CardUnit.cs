using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

using Ch120.Utils.Resource;

using UK.Manager.Card;
using UK.Manager.UI;
using UK.Manager.Popup;

using UK.Model.CardMain;
using UK.Model.EffectMain;
using UK.Model.EffectGroup;
using UK.Model.EffectAbility;

using UK.Utils.Card;
using UK.Const.Card.UseType;

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
        [SerializeField, Tooltip("カード選択時フレーム")] private Image _cardSelectFrame = default;
        [SerializeField, Tooltip("カード選択用ボタン")] private Button _cardButton = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        public CardMainModel CardModel
        {
            get { return _cardModel; }
        }
        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        public int Turn
        {
            get { return _turn; }
        }
        public bool IsDestroy
        {
            get { return _isDestroy; }
        }
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // CanvasのRectTransform
        private RectTransform _canvasRect = default;
        // カードモデル
        private CardMainModel _cardModel = default;
        // カード効果
        private EffectMainModel _effectModel = default;
        // カード効果グループ
        private EffectGroupModel _effectGroup = default;
        // カード効果能力
        private EffectAbilityModel _effectAbility = default;
        // 現在のHP
        private int _curHp = default;
        // 現在の攻撃力
        private int _curAtk = default;
        // 選択フラグ
        private bool _isSelect = default;
        // プレイヤーフラグ
        private bool _isPlayer = default;
        // 出現してからのターン数
        private int _turn = default;
        // 破壊フラグ
        private bool _isDestroy = default;
        // カード効果発動回数
        private int _cntEffect = default;
        // リスト内のカードインデックス
        private int _index = default;
        // 点滅フラグ
        private bool _isBlink = default;
        // 点滅用Tweener
        private Tweener _blinkTweener = default;


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
        public void Initialize(CardMainModel model, RectTransform canvasRect, bool isPlayer)
        {
            _cardModel = model;
            _canvasRect = canvasRect;
            _isPlayer = isPlayer;
            _effectModel = CardUtils.GetEffectMainModel(_cardModel.EffectId);
            _turn = 0;
            _isDestroy = false;
            _cntEffect = 0;

            SetCardImage(_cardModel.Image);
            SetCardNameText(_cardModel.CardName);
            SetCardTypeIcon(_cardModel.CardType);
            SetAtkText(_cardModel.Attack);
            SetHpText(_cardModel.Hp);
            SetCostText(_cardModel.Cost);

            // カードボタン処理
            _cardButton.onClick.RemoveAllListeners();
            _cardButton.onClick.AddListener(() => {

                // 選択中のカードがあるか
                if (!CardManager.Instance.IsSelect())
                {
                    // カード使用タイプを取得
                    CardUseType cardUseType = CardUtils.GetCardUseType(_cardModel);

                    // 配置タイプのカード
                    if (cardUseType == CardUseType.PLACEMENT)
                    {
                        UseCardPlacement();
                    }

                    // 消費タイプのカード
                    if (cardUseType == CardUseType.CONSUMPTION)
                    {
                        UseCardConsumption();
                    }
                }
            });

            _cardSelectFrame.gameObject.SetActive(false);
            InitBlinkFrame();
        }

        // カード使用（配置）
        public void UseCardPlacement()
        {
            Vector3 position = this.gameObject.transform.localPosition;
            CardManager.Instance.IsSelectCardUnit = this;

            // 手札ボタンの設定
            UIManager.Instance.SetHandButtonAction(() => {
                _isSelect = false;
                CardManager.Instance.IsSelectCardUnit = null;
                this.gameObject.transform.localPosition = position;
                _cardButton.gameObject.SetActive(true);
            });
            UIManager.Instance.SetHandButtonActive(true);
            
            _cardButton.gameObject.SetActive(false);
            _isSelect = true;
        }

        // カード使用（消費）
        public void UseCardConsumption()
        {
            Dictionary<string, UnityAction> actions = new Dictionary<string, UnityAction>();
            actions.Add(
                Ch120.Popup.Common.CommonPopup.DECISION_BUTTON_EVENT,
                () => {
                    // カードをトラッシュへ
                    CardManager.Instance.TrashCard(_isPlayer, this);
                    // カード効果発動
                    EffectActivation();
                }
            );
            PopupManager.Instance.SetConsumptionPopup(_cardModel, actions);
            PopupManager.Instance.ShowConsumptionPopup();
        }

        // カード配置時処理
        public void Placement()
        {
            _turn = 1;
            EffectActivation();
        }

        // カード配置後の経過ターンを加算
        public void UpdateTurn()
        {
            if (_turn <= 0) { return; }
            _turn++;
        }

        // TODO カード破壊(HP0)
        public void Destroy()
        {
            if (_curHp <= 0)
            {
                _isDestroy = true;
            }
        }

        // カード効果発動
        public void EffectActivation()
        {
            bool activate = CardUtils.CardEffectActivation(_effectModel, this);
            if (activate) { _cntEffect++; }
        }

        // カード効果を発動させたことがあるか
        public bool CheckEffectActivation()
        {
            return _cntEffect > 0;
        }

        // カードを非活性化
        public void SetCardActive(bool b)
        {
            _cardButton.gameObject.SetActive(b);
            SetActiveBackImage(b);
        }

        // 簡易選択用ボタンイベントの設定
        public void SetSelectButtonEvent(UnityAction action)
        {
            _cardButton.onClick.RemoveAllListeners();
            _cardButton.onClick.AddListener(() => {
                action();
            });
        }

        // 選択フレームの表示・非表示
        public void SetActiveSelectFrame(bool b)
        {
            _cardSelectFrame.gameObject.SetActive(b);
        }

        // カードボタンの活性化・非活性化
        public void SetActiveCardButton(bool b)
        {
            _cardButton.gameObject.SetActive(b);
        }

        public void SetRemoveCardButtonEvent()
        {
            _cardButton.onClick.RemoveAllListeners();
        }

        // カードをグレーアウトにする
        public void SetGrayCard()
        {
            _cardButton.onClick.RemoveAllListeners();
            SetActiveBackImage(true);
            Image image = _cardBackImage.GetComponent<Image>();
            image.color = new Color32(0, 0, 0, 130);
        }
        
        // カード背面画像の表示を設定
        public void SetActiveBackImage(bool b)
        {
            _cardBackImage.SetActive(b);
        }

        // フレーム点滅の表示・非表示
        public void SetBlinkFrame(bool b)
        {
            _isBlink = b;
            _cardSelectFrame.gameObject.SetActive(b);
            if (_isBlink)
            {
                _blinkTweener.Play();
            }
            else
            {
                _blinkTweener.Pause();
            }
        }

        // ---------- Private関数 ----------

        // カード画像の設定
        private void SetCardImage(string imageFile)
        {
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
        
        // フレーム点滅の初期化
        private void InitBlinkFrame()
        {
            _blinkTweener = _cardSelectFrame.DOFade(endValue: 0.0f, duration: 0.5f).SetLoops(-1,LoopType.Yoyo);
            _blinkTweener.Pause();
        }

        // ---------- protected関数 ---------
    }
}

