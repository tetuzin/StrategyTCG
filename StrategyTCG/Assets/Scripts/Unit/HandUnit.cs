using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Manager.Card;
using UK.Unit.Card;
using UK.Model.CardMain;

namespace UK.Unit.Hand
{
    public class HandUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("カード配置パネル")] private GameObject _panel = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        public List<CardMainModel> HandCard
        {
            get { return _handCard; }
            set { _handCard = value; }
        }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // 手札
        private List<CardMainModel> _handCard = default;
        // プレイヤーフラグ
        private bool _isPlayer = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(bool isPlayer)
        {
            _isPlayer = isPlayer;
            _handCard = new List<CardMainModel>();
        }

        // カードをパネルに配置（手札表示
        public void SetCardPanel(CardUnit card)
        {
            card.gameObject.transform.SetParent(_panel.transform);
            card.gameObject.transform.localPosition = Vector3.zero;
            card.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            card.SetActiveBackImage(!_isPlayer);
        }

        // カードリストを手札に追加
        public void AddHandCard(List<CardMainModel> addCardList)
        {
            _handCard.AddRange(addCardList);
            foreach (CardMainModel cardModel in addCardList)
            {
                CardUnit cardUnit = CardManager.Instance.Instantiate2DCardUnit(cardModel);
                SetCardPanel(cardUnit);
            }
        }

        // 手札を削除
        public void RemoveHandCard(CardMainModel model)
        {
            CardMainModel result = _handCard.Find(n => n == model);
            if (result != null)
            {
                _handCard.Remove(result);
            }
            else
            {
                Debug.LogWarning("Listに指定の値は存在しません。");
            }
            
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}


