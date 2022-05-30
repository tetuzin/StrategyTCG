using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Unit.Card;

namespace UK.Unit.Place
{
    public class CardPlacement : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // カードユニット
        private CardUnit _cardUnit = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // カードを設定
        public void SetCardUnit(CardUnit cardUnit)
        {
            _cardUnit = cardUnit;
        }

        // カードを取得
        public CardUnit GetCardUnit()
        {
            return _cardUnit;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

