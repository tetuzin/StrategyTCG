using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UK.Unit.Card;

namespace UK.Unit.Card3D
{
    public class Card3DUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [SerializeField, Tooltip("Canvas")] private Canvas _Canvas = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        public bool IsPlayer
        {
            get { return _isPlayer; }
        }
        
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private CardUnit _cardUnit = default;
        private bool _isPlayer = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(CardUnit cardUnit, bool isPlayer)
        {
            _cardUnit = cardUnit;
            _isPlayer = isPlayer;
            _cardUnit.gameObject.transform.SetParent(this.gameObject.transform);
            _cardUnit.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            
            // 2Dオブジェクトの設定
            _cardUnit.SetCardActive(false);
        }

        // カードユニットの取得
        public CardUnit GetCardUnit()
        {
            return _cardUnit;
        }
        
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

