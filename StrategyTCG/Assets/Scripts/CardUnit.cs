using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Model.Card;

namespace UK.Unit.Card
{
    public class CardUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private CardModel _model = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize(CardModel model)
        {
            _model = model;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}

