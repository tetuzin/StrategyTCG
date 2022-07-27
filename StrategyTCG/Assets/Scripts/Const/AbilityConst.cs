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
        DECK_CARD_DRAW_HAND = 100002,  // 手札がN枚になるように山札ドロー
        DECK_SHUFFLE_DRAW = 100003,    // 手札をすべて山札に戻してN枚ドロー
        DECK_CARD_GET = 101001,        // 山札を閲覧してN枚選択してもってくる
        DECK_CARD_GET_NAME = 101002,   // 山札を閲覧してカード名「XXXX」をN枚選択してもってくる
        DECK_CARD_PLACE_NAME = 101003, // 山札を閲覧してカード名「XXXX」をN枚選択して場に出す
        DECK_PEASON_GET = 102001,      // 山札を閲覧して人物カードをN枚選択してもってくる
        DECK_PEASON_PLACE = 102002,    // 山札を閲覧して人物カードをN枚選択して場に出す
        DECK_BUILDING_GET = 103001,    // 山札を閲覧して建造物カードをN枚選択してもってくる
        DECK_BUILDING_PLACE = 103002,  // 山札を閲覧して建造物カードをN枚選択して場に出す
        DECK_GOODS_GET = 104001,       // 山札を閲覧して物資カードをN枚選択してもってくる
        DECK_GOODS_PLACE = 104002,     // 山札を閲覧して物資カードをN枚選択して場に出す
        DECK_POLICY_GET = 105001,      // 山札を閲覧して政策カードをN枚選択してもってくる
        DECK_POLICY_PLACE = 105002,    // 山札を閲覧して政策カードをN枚選択して場に出す

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
        POWER_DOUBLE = 510002,     // 軍事力をN倍にする
        POWER_DOWN = 510003,       // 軍事力をN減少させる
        // 国民数系
        PEOPLENUM_UP = 520001,     // 国民数をN増加させる
        PEOPLENUM_DOUBLE = 520002, // 国民数をN倍にする
        PEOPLENUM_DOWN = 520003,   // 国民数をN減少させる
        // 資金系
        FUND_UP = 530001,          // 資金をN増加させる
        FUND_DOUBLE = 530002,      // 資金をN倍にする
        FUND_DOWN = 530003,        // 資金をN減少させる
        // ターン毎の資金系
        TURN_FUND_UP = 540001,     // ターン毎資金をN増加させる
        TURN_FUND_DOUBLE = 540002, // ターン毎資金をN倍にする
        TURN_FUND_DOWN = 540003,   // ターン毎資金をN減少させる
        // ユニットATK
        ATK_UP = 550001,           // 選択したX枚のカードユニットの攻撃力をN増加
        ATK_DOUBLE = 550002,       // 選択したX枚のカードユニットの攻撃力をN倍
        ATK_DOWN = 550003,         // 選択したX枚のカードユニットの攻撃力をN減少
        // ユニットHP
        HP_UP = 560001,            // 選択したX枚のカードユニットのHPをN増加
        HP_DOUBLE = 560002,        // 選択したX枚のカードユニットのHPをN倍
        HP_DOWN = 560003,          // 選択したX枚のカードユニットのHPをN減少

        // ダメージ系
        PLAYER_DAMAGE_DOWN = 610001,    // プレイヤーが受けるダメージをN減少させる
        CARD_DAMAGE_DOWN = 610002,      // 選択したユニットが受けるダメージをN減少させる
        ALL_CARD_DAMAGE_DOWN = 610003,  // 自分のすべてのユニットが受けるダメージをN減少させる

        CARD_DAMAGE = 620001,           // ユニットX体にNダメージ
        CARD_PEASON_DAMAGE = 621001,    // 人物ユニットX体にNダメージ

        // カード破壊系
        DESTORY_FIELD_CARD = 710001,       // 場のカードをN枚選択して破壊する
        DESTORY_FIELD_PERSON_CARD = 710002,       // 場の人物カードをN枚選択して破壊する
        DESTORY_FIELD_BUILDING_CARD = 710003,       // 場の建造物カードをN枚選択して破壊する

        // その他
    }
}