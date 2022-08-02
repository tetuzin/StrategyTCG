using System;
using System.Collections;
using System.Collections.Generic;
using UK.Manager.Ingame;
using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;
using UK.Manager.Card;
using UK.Unit.Card;
using UK.Unit.Deck;
using UK.Unit.Field;
using UK.Unit.Hand;
using UK.Unit.Player;

namespace UK.CpuCtrl
{
    public class CpuController
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private bool _isPlayer = default;
        private UnityAction _attackAction = default;

        private int _debugValue = default;
        
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        
        // 初期化
        public void Initialize(bool isPlayer)
        {
            _isPlayer = isPlayer;
            _attackAction = IngameManager.Instance.GetAttackAction(_isPlayer);
        }

        // ターンの実行
        public async void PlayTurn()
        {
            await Task.Delay(1000);
            
            // デバッグ用
            // _debugValue = 0;
            
            Func<Task> func = await SearchAction();

            while (func != null)
            {
                await func();
                await Task.Delay(1000);
                func = await SearchAction();
            }

            _attackAction();
        }
        
        // ---------- Private関数 ----------

        // 行えるアクションを探して返す
        private async Task<Func<Task>> SearchAction()
        {
            // デバッグ用
            // if (_debugValue != 3)
            // {
            //     ++_debugValue;
            //     return () =>
            //     {
            //         Debug.Log(_debugValue);
            //         return Task.CompletedTask;
            //     };
            // }

            // 人物カード配置
            List<CardUnit> cardList = GetHandUnit().GetPersonCardList();
            if (cardList.Count != 0 && GetBattleField().GetPersonPlacement() != null)
            {
                return () =>
                {
                    CardPlacementAction(ExtractCardUnit(cardList));
                    return Task.CompletedTask;
                };
            }
            
            // 建造物カード配置
            cardList = GetHandUnit().GetBuildingCardList();
            if (cardList.Count != 0 && GetBattleField().GetBuildingPlacement() != null)
            {
                return () =>
                {
                    CardPlacementAction(ExtractCardUnit(cardList));
                    return Task.CompletedTask;
                };
            }
            

            return null;
        }

        private void CardPlacementAction(CardUnit cardUnit)
        {
            cardUnit.OnClickSelectCard();
        }
        
        // TODO カードリストの中から最適なものを一つ選ぶ
        private CardUnit ExtractCardUnit(List<CardUnit> cardList)
        {
            // 一旦、リストの一番最初を選ぶ
            return cardList[0];
        }
        
        // プレイヤーユニット取得
        private PlayerUnit GetPlayerUnit()
        {
            return IngameManager.Instance.GetPlayerUnit(_isPlayer);
        }
        
        // フィールド取得
        private CardBattleField GetBattleField()
        {
            return CardManager.Instance.GetCardBattleField(_isPlayer);
        }
        
        // 手札取得
        private HandUnit GetHandUnit()
        {
            return CardManager.Instance.GetCardBattleField(_isPlayer).GetHandUnit();
        }
        
        // 山札取得
        private DeckUnit GetDeckUnit()
        {
            return CardManager.Instance.GetCardBattleField(_isPlayer).GetDeckUnit();
        }

        // ---------- protected関数 ---------
    }
}