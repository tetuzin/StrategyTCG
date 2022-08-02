using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

using Ch120.Utils.Resource;

using UK.Const.Game;
using UK.Const.Card.UseType;
using UK.Const.Ability;
using UK.Const.Card.Type;

using UK.Manager.Ingame;
using UK.Manager.Card;
using UK.Manager.UI;
using UK.Manager.Popup;

using UK.Model.CardMain;
using UK.Model.EffectMain;

using UK.Unit.Player;
using UK.Unit.Place;
using UK.Unit.Effect;
using UK.Unit.EffectList;

using UK.Utils.Card;

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
        [SerializeField, Tooltip("カードグレーアウト用画像")] private GameObject _cardGrayOutImage = default;

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
        public EffectUnitList EffectList
        {
            get { return _effectList; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // CanvasのRectTransform
        private RectTransform _canvasRect = default;
        // カードモデル
        private CardMainModel _cardModel = default;
        // カード効果
        private EffectMainModel _effectModel = default;
        // 現在のHP
        private int _curHp = default;
        // 現在の最大HP
        private int _curMaxHp = default;
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
        // 受けているカード効果のリスト
        private EffectUnitList _effectList = default;
        // 配置されている場所
        private CardPlacement _placement = default;

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

            _curHp = _cardModel.Hp;
            _curMaxHp = _cardModel.Hp;
            _curAtk = _cardModel.Attack;

            _effectList = new EffectUnitList();
            _effectList.Initialize();

            SetCardImage(_cardModel.Image);
            SetCardNameText(_cardModel.CardName);
            SetCardTypeIcon(_cardModel.CardType);
            SetAtkText(_cardModel.Attack);
            SetHpText(_curHp);
            SetCostText(_curAtk);

            if (CheckOperationPlayer())
            {
                // カードボタン処理初期化
                InitSelectButtonEvent();
            }
            
            _cardSelectFrame.gameObject.SetActive(false);
            _cardGrayOutImage.SetActive(false);
        }

        // カードを使用できるかチェック
        public bool CheckUse()
        {
            int cost = _cardModel.Cost;
            int fund = IngameManager.Instance.GetPlayerUnit(_isPlayer).Fund;
            return fund >= cost;
        }

        // カード配置時処理
        public void Placement(CardPlacement placement)
        {
            _placement = placement;
            _turn = 1;
            UseCard();
            EffectActivation();
        }

        // カード配置後の経過ターンを加算
        public void UpdateTurn()
        {
            if (_turn <= 0) { return; }
            _turn++;
        }

        // TODO カード破壊
        public void DestroyCard()
        {
            _isDestroy = true;
            _curHp = 0;
            if ((CardType)_cardModel.CardType == CardType.PERSON)
            {
                PlayerUnit unit = IngameManager.Instance.GetPlayerUnit(_isPlayer);
                unit.Power -= _curAtk;
                unit.PeopleNum--;
                unit.TurnFund -= GameConst.PLACEMENT_UP_TURN_FUND;
            }
            _placement.DestroyPlaceCard();
            Debug.Log(_cardModel.CardName + "は倒れた！");
        }
        
        // ダメージを受ける
        public void Damage(int value)
        {
            // TODO ダメージの算出(受けている効果やバフなどを考慮する
            int damage = value;
            
            // プレイヤーがもつ発動中効果の考慮
            foreach (EffectUnit effect in IngameManager.Instance.GetPlayerUnit(_isPlayer).EffectList.GetEffectUnitList())
            {
                switch (effect.Ability)
                {
                    // カードユニットが受けるダメージをN減少させる
                    case AbilityType.ALL_CARD_DAMAGE_DOWN:
                        Debug.Log("効果発動：カードユニットの被ダメ-" + effect.Value);
                        damage -= effect.Value;
                        break;
                    
                    default:
                        break;
                }
            }
            
            // カードユニットがもつ発動中効果の考慮
            foreach (EffectUnit effect in _effectList.GetEffectUnitList())
            {
                switch (effect.Ability)
                {
                    // カードユニットが受けるダメージをN減少させる
                    case AbilityType.CARD_DAMAGE_DOWN:
                    case AbilityType.ALL_CARD_DAMAGE_DOWN:
                        Debug.Log("効果発動：カードユニットの被ダメ-" + effect.Value);
                        damage -= effect.Value;
                        break;
                    
                    default:
                        break;
                }
            }
            
            // TODO ダメージを受ける
            _curHp -= damage;
            if (_curHp <= 0)
            {
                _curHp = 0;
            }
            
            // TODO 画面上にダメージを表示

            // 被ダメ後の状態を反映させて再描画
            SetHpText(_curHp);
            Debug.Log(_cardModel.CardName + "は" + damage + "ダメージを受けた！");

            if (_curHp == 0) DestroyCard();
        }
        
        // HP回復
        public void Heal(int value)
        {
            Debug.Log(_cardModel.CardName + "のHPを" + value.ToString() + "回復！");
            _curHp += value;
            if (_curHp > _curMaxHp)
            {
                _curHp = _curMaxHp;
            }
            SetHpText(_curHp);
        }
        
        // HP最大値の更新
        public void UpdateHp(AbilityType abilityType, int value)
        {
            switch (abilityType)
            {
                case AbilityType.HP_UP:
                    _curMaxHp += value;
                    _curHp += value;
                    break;
                case AbilityType.HP_DOUBLE:
                    int damage = _curMaxHp - _curHp;
                    _curMaxHp *= value;
                    _curHp = _curMaxHp - damage;
                    break;
                case AbilityType.HP_DOWN:
                    _curMaxHp -= value;
                    _curHp -= value;
                    break;
                default:
                    break;
            }
            SetHpText(_curHp);
            
            // TODO 死亡していないかチェック
        }
        
        // 攻撃力の更新
        public void UpdateAtk(AbilityType abilityType, int value)
        {
            PlayerUnit unit = IngameManager.Instance.GetPlayerUnit(_isPlayer);
            switch (abilityType)
            {
                case AbilityType.ATK_UP:
                    unit.Power += value;
                    _curAtk += value;
                    break;
                case AbilityType.ATK_DOUBLE:
                    int _beforeAtk = _curAtk;
                    _curAtk *= value;
                    unit.Power += (_curAtk - _beforeAtk);
                    break;
                case AbilityType.ATK_DOWN:
                    unit.Power -= value;
                    _curAtk -= value;
                    break;
                default:
                    break;
            }
            SetAtkText(_curAtk);
        }

        // カード効果を発動させたことがあるか
        public bool CheckEffectActivation()
        {
            return _cntEffect > 0;
        }

        // カードを非活性化
        public void SetCardActive(bool b)
        {
            SetActiveCardButton(b);
            SetActiveBackImage(b);
        }
        
        // カード選択ボタンの初期化
        public void InitSelectButtonEvent()
        {
            SetSelectButtonEvent(OnClickSelectCard);
        }
        
        // カード選択処理
        public void OnClickSelectCard()
        {
            // 選択中のカードがあるか
            if (!CardManager.Instance.IsSelect())
            {
                if (CheckUse())
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
                else
                {
                    ShowNotUsePopup();
                }
            }
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

        // カードボタンのイベントを削除
        public void SetRemoveCardButtonEvent()
        {
            _cardButton.onClick.RemoveAllListeners();
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
                Color color = _cardSelectFrame.color;
                color.a = 1.0f;
                _cardSelectFrame.color = color;
                _blinkTweener = _cardSelectFrame.DOFade(endValue: 0.0f, duration: 0.5f).SetLoops(-1,LoopType.Yoyo);

            }
            else
            {
                _blinkTweener.Kill();
                Color color = _cardSelectFrame.color;
                color.a = 1.0f;
                _cardSelectFrame.color = color;
            }
        }
        
        // グレーアウトの表示・非表示
        public void SetGrayOut(bool b, bool isButtonEvent = false)
        {
            SetActiveCardButton(isButtonEvent);
            _cardGrayOutImage.SetActive(b);
        }

        // ---------- Private関数 ----------
        
        // カード使用（配置）
        private void UseCardPlacement()
        {
            if (CheckOperationPlayer())
            {
                Vector3 position = gameObject.transform.localPosition;
                CardManager.Instance.IsSelectCardUnit = this;

                // 手札ボタンの設定
                UIManager.Instance.SetHandButtonAction(() => {
                    _isSelect = false;
                    CardManager.Instance.IsSelectCardUnit = null;
                    gameObject.transform.localPosition = position;
                    _cardButton.gameObject.SetActive(true);
                });
                UIManager.Instance.SetHandButtonActive(true);
            
                _cardButton.gameObject.SetActive(false);
                _isSelect = true;
            }
            else
            {
                CardManager.Instance.IsSelectCardUnit = this;
                CardPlacement placement = null;
                switch (CardUtils.GetCardType(_cardModel))
                {
                    case CardType.PERSON:
                        placement = CardManager.Instance.GetCardBattleField(_isPlayer).GetPersonPlacement();
                        break;
                    
                    case CardType.BUILDING:
                        placement = CardManager.Instance.GetCardBattleField(_isPlayer).GetBuildingPlacement();
                        break;
                    
                    default:
                        break;
                }
                if (placement != null)
                {
                    placement.SetCardPlacement();
                }
            }
        }

        // カード使用（消費）
        private void UseCardConsumption()
        {
            Dictionary<string, UnityAction> actions = new Dictionary<string, UnityAction>();
            actions.Add(
                Ch120.Popup.Common.CommonPopup.DECISION_BUTTON_EVENT,
                () => {
                    // カードをトラッシュへ
                    CardManager.Instance.TrashCard(_isPlayer, this);
                    DecreaseFund();
                    // カード効果発動
                    EffectActivation();
                }
            );
            PopupManager.Instance.SetConsumptionPopup(_cardModel, actions);
            PopupManager.Instance.ShowConsumptionPopup();
        }
        
        // コストが足りないことを伝えるポップアップ表示
        private void ShowNotUsePopup()
        {
            PopupManager.Instance.SetSimpleTextPopup("資金が足りず使用できません！");
            PopupManager.Instance.ShowSimpleTextPopup();
        }
        
        // コスト消費
        private void DecreaseFund()
        {
            IngameManager.Instance.GetPlayerUnit(_isPlayer).Fund -= _cardModel.Cost;
        }
        
        // カード使用
        private void UseCard()
        {
            DecreaseFund();

            if ((CardType)_cardModel.CardType == CardType.PERSON)
            {
                PlayerUnit unit = IngameManager.Instance.GetPlayerUnit(_isPlayer);
                unit.Power += _curAtk;
                unit.PeopleNum++;
                unit.TurnFund += GameConst.PLACEMENT_UP_TURN_FUND;
            }
        }
        
        // カード効果発動
        private void EffectActivation()
        {
            bool activate = CardUtils.CardEffectActivation(_effectModel, this);
            if (activate) { _cntEffect++; }
        }

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
        
        // 自分が操作しているかどうか
        private bool CheckOperationPlayer()
        {
            return IngameManager.Instance.CheckPlayer(_isPlayer);
        }

        // ---------- protected関数 ---------
    }
}

