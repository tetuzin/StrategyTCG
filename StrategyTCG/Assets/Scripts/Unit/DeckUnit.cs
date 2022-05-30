using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Model.CardMain;

namespace UK.Unit.Deck
{
    public class DeckUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // デッキのカード枚数
        private int _cardNum = default;
        // デッキに含まれるカード
        private List<CardMainModel> _deckCard = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // デッキ初期化
        public void Initialize(List<CardMainModel> deckCard)
        {
            _deckCard = deckCard;
            SetCardNum(_deckCard.Count);
        }

        // デッキシャッフル
        public void Shuffle()
        {
            // TODO
        }

        // カードを引く
        public void Draw()
        {
            // TODO
        }

        // デッキのカード枚数を設定する
        public void SetCardNum(int num)
        {
            _cardNum = num;
            UpdateCardNum();
        }

        // ---------- Private関数 ----------

        // デッキのカード枚数を更新する
        private void UpdateCardNum()
        {
            SetDeckHeightScale(_cardNum);
        }

        // デッキの高さを変更
        private void SetDeckHeightScale(int height)
        {
            Vector3 vector = this.gameObject.transform.localScale;
            vector.y = height;
            this.gameObject.transform.localScale = vector;
        }

        // ---------- protected関数 ---------
    }
}


