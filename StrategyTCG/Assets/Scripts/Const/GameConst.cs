// ゲームで使用する定数
namespace UK.Const.Game
{
    public class GameConst
    {
        // 自分
        public static readonly bool PLAYER = true;
        // 相手
        public static readonly bool OPPONENT = false;
        // 自分の場における人物カードの最大数
        public static readonly int MAX_PERSON_CARD = 5;
        // 自分の場における建造物カードの最大数
        public static readonly int MAX_BUILDING_CARD = 5;
        // ゲーム開始時にひく手札の枚数
        public static readonly int START_HAND_CARD = 5;
        // 人物カードが配置された際に増える毎ターン取得する資金の値
        public static readonly int PLACEMENT_UP_TURN_FUND = 5;
    }
}

// カードタイプ
namespace UK.Const.Card.Type
{
    public enum CardType
    {
        // なし
        NONE = 0,
        // 人物
        PERSON = 1,
        // 政策
        POLICY = 2,
        // 物資
        GOODS = 3,
        // 建造物
        BUILDING = 4,

        MAX = 5
    }
}

// カード使用タイプ
namespace UK.Const.Card.UseType
{
    public enum CardUseType
    {
        // なし
        NONE = 0,
        // 配置
        PLACEMENT = 1,
        // 消費
        CONSUMPTION = 2,
        
        MAX = 3
    }
}