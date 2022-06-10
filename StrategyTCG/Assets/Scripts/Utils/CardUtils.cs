using UK.Model.CardMain;
using UK.Const.Card.Type;
using UK.Const.Card.UseType;

namespace UK.Utils.Card
{
    public class CardUtils
    {
        // カード使用タイプを取得
        public static CardUseType GetCardUseType(CardMainModel model)
        {
            switch(model.CardType)
            {
                // 配置カード
                case (int)CardType.PERSON:
                case (int)CardType.BUILDING:
                    return CardUseType.PLACEMENT;

                // 消費カード
                case (int)CardType.GOODS:
                case (int)CardType.POLICY:
                    return CardUseType.CONSUMPTION;

                // 未分類
                case (int)CardType.NONE:
                    return CardUseType.NONE;

                default:
                    return CardUseType.NONE;
            }
        }

        // カードタイプ名を取得
        public static string GetCardTypeName(int cardType)
        {
            switch(cardType)
            {
                case (int)CardType.PERSON:
                    return "人物";

                case (int)CardType.POLICY:
                    return "政策";

                case (int)CardType.BUILDING:
                    return "建造物";

                case (int)CardType.GOODS:
                    return "物資";
                
                default:
                    return "未分類";
            }
        }
    }
}