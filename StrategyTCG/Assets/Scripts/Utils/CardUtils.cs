using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Ch120.Manager.Master;

using UK.Manager.Card;
using UK.Manager.Ingame;

using UK.Model.CardMain;
using UK.Model.EffectMain;
using UK.Model.EffectGroup;
using UK.Model.EffectAbility;
using UK.Model.CountryMain;

using UK.Dao;

using UK.Const.Card.Type;
using UK.Const.Card.UseType;
using UK.Const.Effect;
using UK.Const.Ability;

namespace UK.Utils.Card
{
    public class CardUtils
    {
        // 効果番号から効果を取得
        public static EffectMainModel GetEffectMainModel(int effectId)
        {
            return ((EffectMainDao)MasterManager.Instance.GetDao("EffectMainDao")).GetModelById(effectId);
        }

        // 効果番号から効果リストを取得
        public static List<EffectGroupModel> GetEffectGroupModelList(int effectId)
        {
            return ((EffectGroupDao)MasterManager.Instance.GetDao("EffectGroupDao")).GetModelAllById(effectId);
        }

        // 効果能力番号から能力を取得
        public static EffectAbilityModel GetEffectAbilityModel(int effectAbilityId)
        {
            return ((EffectAbilityDao)MasterManager.Instance.GetDao("EffectAbilityDao")).GetModelById(effectAbilityId);
        }

        // 効果番号から能力リストを取得
        public static List<EffectAbilityModel> GetEffectAbilityModelList(int effectId)
        {
            List<EffectGroupModel> effectList = GetEffectGroupModelList(effectId);
            List<EffectAbilityModel> abilityList = new List<EffectAbilityModel>();
            foreach (EffectGroupModel effect in effectList)
            {
                abilityList.Add(GetEffectAbilityModel(effect.EffectAbilityId));
            }
            return abilityList;
        }

        // カード効果発動処理
        public static void CardEffectActivation(EffectMainModel model)
        {
            List<EffectGroupModel> effectList = GetEffectGroupModelList(model.EffectId);
            List<EffectAbilityModel> abilityList = GetEffectAbilityModelList(model.EffectId);

            // 発動条件タイミングのチェック
            if (!CheckEffectTiming(model)){ return; }

            // 発動条件トリガーのチェック
            if (!CheckEffectTrigger(model)){ return; }

            // 発動条件のチェック
            if (!CheckEffectCondition(model.EffectConditionType, model.EffectConditionParameter)){ return; }

            // 能力発動
            for (int i = 0; i < abilityList.Count; i++)
            {
                // カード効果の関数を呼び出し
                EffectActivate(abilityList[i]);
            }
        }

        // 効果発動タイミングチェック
        public static bool CheckEffectTiming(EffectMainModel model)
        {
            TimingType timingType = (TimingType)model.EffectTimingType;

            switch(timingType)
            {
                case TimingType.ALL_TIMING:
                    Debug.Log(model.EffectName + "効果発動タイミング : TRUE");
                    return true;
                    
                case TimingType.SPECIFY_TURN:
                    int turnNum = model.EffectTimingParameter;
                    int curTurn = IngameManager.Instance.CurTurn;
                    if (turnNum == curTurn)
                    {
                        Debug.Log(model.EffectName + "効果発動タイミング : TRUE");
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    TimingType CurTiming = IngameManager.Instance.CurTiming;
                    if (timingType == CurTiming)
                    {
                        Debug.Log(model.EffectName + "効果発動タイミング : TRUE");
                        return true;
                    }
                    else
                    {
                        Debug.Log(model.EffectName + "効果発動タイミング : FALSE");
                        return false;
                    }
            }
        }

        // TODO 効果発動トリガーチェック
        public static bool CheckEffectTrigger(EffectMainModel model)
        {
            return true;
        }

        // TODO 効果発動条件チェック
        public static bool CheckEffectCondition(int type, int param)
        {
            return true;
        }

        // TODO カード効果発動
        public static void EffectActivate(EffectAbilityModel model)
        {
            AbilityType abilityType = (AbilityType)model.AbilityType;
            switch(abilityType)
            {
                // 山札をN枚ドロー
                case AbilityType.DECK_CARD_DRAW:
                    bool isPlayer = GetUserType(model.UserType);
                    CardManager.Instance.DeckDraw(isPlayer, model.AbilityParameter1);
                    break;
                default:
                    break;
            }
        }

        // カード使用者タイプから真偽値を取得
        public static bool GetUserType(int userType)
        {
            switch(userType)
            {
                case (int)UserType.USE_PLAYER:
                    return true;
                case (int)UserType.USE_OPPONENT:
                    return false;
                case (int)UserType.NONE:
                    return true;
                default:
                    return true;
            }
        }

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