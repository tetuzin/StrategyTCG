using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UK.Manager.Card;
using UK.Manager.UI;
using UK.Unit.Card;
using UK.Unit.Card3D;
using UK.Const.Game;
using UK.Const.Card.Type;

namespace UK.Unit.Place
{
    public class CardPlacement : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード配置ボタン")] private Button _placementButton = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }

        public CardType FieldType
        {
            get { return _fieldType; }
            set { _fieldType = value; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // カードユニット
        private Card3DUnit _card3DUnit = default;
        // 配置フラグ
        private bool _isPlacement = default;
        // プレイヤーフラグ
        private bool _isPlayer = default;
        // 場種
        private CardType _fieldType = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(bool isPlayer)
        {
            _isPlayer = isPlayer;
            _placementButton.onClick.RemoveAllListeners();
            _placementButton.onClick.AddListener(SetCardPlacement);
            _isPlacement = false;
        }

        // カードを配置する
        public void SetCardPlacement()
        {
            // カードが選択されていないなら
            if (!CardManager.Instance.IsSelect()) { return; }

            // この場が自分の場でないなら
            if (!_isPlayer) { return; }

            // 選択中カードユニットを取得
            CardUnit cardUnit = CardManager.Instance.IsSelectCardUnit;

            // カード種別が一致しているなら
            if ((int)_fieldType != cardUnit.CardModel.CardType) { return; }

            // カードを3Dで生成する
            _card3DUnit = CardManager.Instance.Instantiate3DCardUnit(cardUnit.CardModel, _isPlayer);

            // 手札ボタンの非活性化
            UIManager.Instance.SetHandButtonActive(false);

            // 選択中カードを削除する
            CardUnit selectCard = CardManager.Instance.IsSelectCardUnit;
            CardManager.Instance.RemoveCard(_isPlayer, selectCard);

            // カードを配置表示
            _card3DUnit.gameObject.transform.SetParent(this.gameObject.transform);
            _card3DUnit.gameObject.transform.localPosition = new Vector3(0.0f, 0.2f, 0.0f);
            _card3DUnit.gameObject.transform.localRotation = Quaternion.Euler(90.0f, -90.0f, 0.0f);
            _card3DUnit.gameObject.transform.localScale = new Vector3(0.14f, 0.14f, 1.0f);

            _isPlacement = true;
        }

        // カードユニットを設定
        public void SetCard3DUnit(Card3DUnit card3DUnit)
        {
            _card3DUnit = card3DUnit;
        }

        // カードユニットを取得
        public Card3DUnit GetCard3DUnit()
        {
            return _card3DUnit;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

