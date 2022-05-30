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
            Shuffle();
            SetCardNum(_deckCard.Count);
        }

        // デッキシャッフル
        public void Shuffle()
        {
            for (int i = _deckCard.Count - 1; i > 0; i--){
                var j = Random.Range(0, i+1);   // ランダムで要素番号を１つ選ぶ（ランダム要素）
                var temp = _deckCard[i];        // 一番最後の要素を仮確保（temp）にいれる
                _deckCard[i] = _deckCard[j];    // ランダム要素を一番最後にいれる
                _deckCard[j] = temp;            // 仮確保を元ランダム要素に上書き
            }
        }

        // カードをn枚引く
        public List<CardMainModel> Draw(int num = 1)
        {
            List<CardMainModel> cardList = new List<CardMainModel>();
            for (int i = 0; i < num; i++)
            {
                cardList.Add(OneDraw());
            }
            return cardList;
        }

        // カードを一枚引く
        public CardMainModel OneDraw()
        {
            CardMainModel card = _deckCard[0];
            _deckCard.RemoveAt(0);
            SetCardNum(_deckCard.Count);
            return card;
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
            vector.y = (height / 10) * 150 / 100;
            this.gameObject.transform.localScale = vector;
        }

        // ---------- protected関数 ---------
    }
}


