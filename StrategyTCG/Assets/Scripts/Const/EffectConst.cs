namespace UK.Const.Effect
{
    public class EffectConst
    {
        
    }

    // 効果発動条件
    public enum ConditionType
    {
        NONE = 0,

        // パラメータ系
        // 資金系
        FUND_HIGHER_PLAYER = 1001,       // 自分の資金がN以上になったら
        FUND_HIGHER_OPPONENT = 1002,     // 相手の資金がN以上になったら
        FUND_LOWER_PLAYER = 1003,        // 自分の資金がN以下になったら
        FUND_LOWER_OPPONENT = 1004,      // 相手の資金がN以下になったら
        // 軍事力系
        POWER_HIGHER_PLAYER = 1101,      // 自分の軍事力がN以上になったら
        POWER_HIGHER_OPPONENT = 1102,    // 相手の軍事力がN以上になったら
        POWER_LOWER_PLAYER = 1103,       // 自分の軍事力がN以下になったら
        POWER_LOWER_OPPONENT = 1104,     // 相手の軍事力がN以下になったら
        // 国民数系
        PEOPLENUM_HIGHER_PLAYER = 1201,      // 自分の国民数がN以上になったら
        PEOPLENUM_HIGHER_OPPONENT = 1202,    // 相手の国民数がN以上になったら
        PEOPLENUM_LOWER_PLAYER = 1203,       // 自分の国民数がN以下になったら
        PEOPLENUM_LOWER_OPPONENT = 1204,     // 相手の国民数がN以下になったら
        // HP系
        HP_HIGHER_PLAYER = 1301,            // 自分のHPがN以上なら
        HP_HIGHER_OPPONENT = 1302,          // 相手のHPがN以上なら
        HP_LOWER_PLAYER = 1303,             // 自分のHPがN以下なら
        HP_LOWER_OPPONENT = 1304,           // 相手のHPがN以下なら
        HP_CARD_HIGHER_PLAYER = 1305,       // 自分のカードユニットのHPがN以上なら
        HP_CARD_HIGHER_OPPONENT = 1306,     // 相手のカードユニットのHPがN以上なら
        HP_CARD_LOWER_PLAYER = 1307,        // 自分のカードユニットのHPがN以下なら
        HP_CARD_LOWER_OPPONENT = 1308,      // 相手のカードユニットのHPがN以下なら

        // カード系
        // 山札系
        DECK_HIGHER_PLAYER = 2001,          // 自分の山札がN枚以上なら
        DECK_HIGHER_OPPONENT = 2002,        // 相手の山札がN枚以上なら
        DECK_LOWER_PLAYER = 2003,           // 自分の山札がN枚以下なら
        DECK_LOWER_OPPONENT = 2004,         // 相手の山札がN枚以下なら
        // 手札系
        HAND_HIGHER_PLAYER = 2201,          // 自分の手札がN枚以上なら
        HAND_HIGHER_OPPONENT = 2202,        // 相手の手札がN枚以上なら
        HAND_LOWER_PLAYER = 2203,           // 自分の手札がN枚以下なら
        HAND_LOWER_OPPONENT = 2204,         // 相手の手札がN枚以下なら
        // トラッシュ系
        TRASH_HIGHER_PLAYER = 2401,          // 自分のトラッシュがN枚以上なら
        TRASH_HIGHER_OPPONENT = 2402,        // 相手のトラッシュがN枚以上なら
        TRASH_LOWER_PLAYER = 2403,           // 自分のトラッシュがN枚以下なら
        TRASH_LOWER_OPPONENT = 2404,         // 相手のトラッシュがN枚以下なら

        // フィールド系
        // 全体
        PLACE_CARD_HIGHER_PLAYER = 3001,        // 自分の場のカードがN枚以上なら
        PLACE_CARD_HIGHER_OPPONENT = 3002,      // 相手の場のカードがN枚以上なら
    }

    // 効果発動条件タイミング
    public enum TimingType
    {
        NONE = 0,
        ALL_TIMING = 9000,              // いつでも
        START_TURN_PLAYER = 9001,       // 自分のターンスタート
        START_TURN_OPPONENT = 9002,     // 相手のターンスタート
        TURN_PLAYER = 9003,             // 自分のターン
        TURN_OPPONENT = 9004,           // 相手のターン
        END_TURN_PLAYER = 9005,         // 自分のターン終了
        END_TURN_OPPONENT = 9006,       // 相手のターン終了
        END_ATTACK_PLAYER = 9007,       // 自分の攻撃時
        END_ATTACK_OPPONENT = 9008,     // 相手の攻撃時
        SPECIFY_TURN = 9009,            // Nターン目
    }

    // 効果発動条件トリガー
    public enum TriggerType
    {
        NONE = 0,
        ALWAYS = 8000,                      // 常時発動
        PLACEMENT_SELECT = 8001,            // カード配置時に任意で
        PLACEMENT_FORCED = 8002,            // カード配置時に強制で
        USE_SELECT = 8003,                  // カード使用時に任意で
        USE_FORCED = 8004,                  // カード使用時に強制で
        PLAYER_TURN_SELECT = 8005,          // 自分のターンに任意で
        PLAYER_TURN_FORCED = 8006,          // 自分のターンに強制で
        OPPONENT_TURN_SELECT = 8007,        // 相手のターンに任意で
        OPPONENT_TURN_FORCED = 8008,        // 相手のターンに強制で
        SPECIFY_TURN_SELECT = 8009,         // 場に出してからNターン目に任意で
        SPECIFY_TURN_FORCED = 8010,         // 場に出してからNターン目に強制で
        PLAYER_TURN_MANY_SELECT = 8011,     // 自分のターンに何度でも任意で
        PLAYER_TURN_MANY_FORCED = 8012,     // 自分のターンに何度でも強制で
        OPPONENT_TURN_MANY_SELECT = 8013,   // 自分のターンに何度でも任意で
        OPPONENT_TURN_MANY_FORCED = 8014,   // 自分のターンに何度でも強制で
        CARD_DESTORY_SELECT = 8015,         // カードが破壊されたとき任意で
        CARD_DESTORY_FORCED = 8016,         // カードが破壊されたとき強制で
    }
}