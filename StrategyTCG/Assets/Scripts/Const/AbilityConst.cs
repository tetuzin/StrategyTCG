using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UK.Const.Ability
{
    public class AbilityConst
    {
        public Dictionary<AbilityType, System.Action> AbilityDictionary = new Dictionary<AbilityType, System.Action>();
    }

    // カード効果
    public enum AbilityType
    {
        NONE = 0,

        // 山札系
        DECK_CARD_DRAW = 100001,       // 山札をN枚ドロー
        DECK_CARD_GET = 101001,        // 山札を閲覧してN枚選択してもってくる
        DECK_PEASON_GET = 102001,      // 山札を閲覧して人物カードをN枚選択してもってくる
        DECK_BUILDING_GET = 103001,    // 山札を閲覧して建造物カードをN枚選択してもってくる
        DECK_GOODS_GET = 104001,       // 山札を閲覧して物資カードをN枚選択してもってくる
        DECK_POLICY_GET = 105001,      // 山札を閲覧して政策カードをN枚選択してもってくる

        // トラッシュ系
        TRASH_CARD_RETURN_DECK = 200001,       // 選択したトラッシュのカードをN枚山札にもどす
        TRASH_PEASON_RETURN_DECK = 201001,     // 選択したトラッシュの人物カードをN枚山札にもどす
        TRASH_BUILDING_RETURN_DECK = 202001,   // 選択したトラッシュの建造物カードをN枚山札にもどす
        TRASH_GOODS_RETURN_DECK = 203001,      // 選択したトラッシュの物資カードをN枚山札にもどす
        TRASH_POLICY_RETURN_DECK = 204001,     // 選択したトラッシュの政策カードをN枚山札にもどす

        TRASH_CARD_RETURN_HAND = 210001,       // 選択したトラッシュのカードをN枚手札にもどす
        TRASH_PEASON_RETURN_HAND = 211001,     // 選択したトラッシュの人物カードをN枚手札にもどす   
        TRASH_BUILDING_RETURN_HAND = 212001,   // 選択したトラッシュの建造物カードをN枚手札にもどす
        TRASH_GOODS_RETURN_HAND = 213001,      // 選択したトラッシュの物資カードをN枚手札にもどす
        TRASH_POLICY_RETURN_HAND = 214001,     // 選択したトラッシュの政策カードをN枚手札にもどす

        // 手札系

        // フィールド系

        // 回復系
        PLAYER_HP_HEAL = 400001,       // プレイヤーのHPをN回復

        CARD_HP_HEAL = 410001,         // 選択したカードユニットのHPをN回復
        ALL_CARD_HP_HEAL = 410002,     // 全カードユニットのHPをN回復

        // パラメータ系
        // 軍事力系
        POWER_UP = 510001,         // 軍事力をN増加させる
        // 国民数系
        PEOPLENUM_UP = 520001,     // 資金をN増加させる
        // 資金系
        FUND_UP = 530001,          // 国民数をN増加させる

        // ダメージ系
        PLAYER_DAMAGE_DOWN = 610001,    // プレイヤーが次の相手ターンに受けるダメージをN減少させる
        CARD_DAMAGE_DOWN = 610002,      // 選択したユニットが次の相手ターンに受けるダメージをN減少させる

        CARD_DAMAGE = 620001,    // ユニット1体にNダメージ

        // その他
    }
}