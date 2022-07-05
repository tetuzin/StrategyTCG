using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Ch120.Manager.Master;

using UK.Manager.Card;
using UK.Manager.Ingame;
using UK.Manager.Popup;

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

using UK.Unit.Card;

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

        // カード効果発動チェック
        public static bool CheckEffectActivation(EffectMainModel model, CardUnit unit)
        {
            // 発動条件タイミングのチェック
            if (!CheckEffectTiming(model)){ return false; }

            // 発動条件トリガーのチェック
            if (!CheckEffectTrigger(model, unit)){ return false; }

            // 発動条件のチェック
            if (!CheckEffectCondition(model.EffectConditionType, model.EffectConditionParameter))
                { return false; }

            return true;
        }

        // カード効果発動処理
        public static bool CardEffectActivation(EffectMainModel model, CardUnit unit)
        {
            // カード効果が無い場合
            if (model.EffectId == 0) { return false; }
            
            List<EffectGroupModel> effectList = GetEffectGroupModelList(model.EffectId);
            List<EffectAbilityModel> abilityList = GetEffectAbilityModelList(model.EffectId);

            // カード効果発動チェック
            if (!CheckEffectActivation(model, unit)){ return false; }

            // 能力使用確認（任意能力用）
            if (CheckSelectEffectTrigger(model))
            {
                Dictionary<string, UnityAction> actions = new Dictionary<string, UnityAction>();
                actions.Add(
                    Ch120.Popup.Common.CommonPopup.DECISION_BUTTON_EVENT,
                    () => {
                        // 能力発動
                        CardAbilityActivate(effectList, abilityList);
                    }
                );
                PopupManager.Instance.SetCheckEffectPopup(unit.CardModel, actions);
                PopupManager.Instance.ShowCheckEffectPopup();
                return true;
            }
            else
            {
                // 能力発動
                CardAbilityActivate(effectList, abilityList);
                return true;
            }
        }

        // カード能力発動
        public static void CardAbilityActivate(List<EffectGroupModel> effectList, List<EffectAbilityModel> abilityList)
        {
            for (int i = 0; i < abilityList.Count; i++)
            {
                // 発動条件のチェック
                if (!CheckEffectCondition(
                    effectList[i].AbilityConditionType, 
                    effectList[i].AbilityConditionParameter)
                ){ continue; }

                // カード効果の関数を呼び出し
                Debug.Log(abilityList[i].AbilityName + "の効果発動[" + (i+1) + "]");
                EffectActivate(abilityList[i]);
            }
        }

        // 効果発動タイミングチェック
        public static bool CheckEffectTiming(EffectMainModel model)
        {
            TimingType timingType = (TimingType)model.EffectTimingType;

            switch(timingType)
            {
                case TimingType.ALL_TIMING:// いつでも
                    Debug.Log(model.EffectName + "効果発動タイミング : TRUE");
                    return true;

                case TimingType.SPECIFY_TURN:// Nターン目
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

                default:// その他のタイミング
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

        // 効果発動トリガーチェック
        public static bool CheckEffectTrigger(EffectMainModel model, CardUnit unit)
        {
            TriggerType triggerType = (TriggerType)model.EffectTriggerType;

            switch(triggerType)
            {
                case TriggerType.NONE:
                    Debug.Log(model.EffectName + "効果発動トリガー : FALSE");
                    return false;

                case TriggerType.ALWAYS:// 常時発動
                    Debug.Log(model.EffectName + "効果発動トリガー : TRUE");
                    return true;

                case TriggerType.PLACEMENT_SELECT:// カード配置時に任意で
                case TriggerType.PLACEMENT_FORCED:// カード配置時に強制で
                case TriggerType.USE_SELECT:// カード使用時に任意で
                case TriggerType.USE_FORCED:// カード使用時に強制で
                    Debug.Log(model.EffectName + "効果発動トリガー : TRUE");
                    return true;

                case TriggerType.PLAYER_TURN_SELECT:// 自分のターンに任意で
                case TriggerType.PLAYER_TURN_FORCED:// 自分のターンに強制で
                    Debug.Log(model.EffectName + "効果発動トリガー : " + (CheckPlayerTurn() && !unit.CheckEffectActivation()));
                    return CheckPlayerTurn() && !unit.CheckEffectActivation();
                
                case TriggerType.OPPONENT_TURN_SELECT:// 相手のターンに任意で
                case TriggerType.OPPONENT_TURN_FORCED:// 相手のターンに強制で
                    Debug.Log(model.EffectName + "効果発動トリガー : " + (!CheckPlayerTurn() && !unit.CheckEffectActivation()));
                    return !CheckPlayerTurn() && !unit.CheckEffectActivation();

                case TriggerType.SPECIFY_TURN_SELECT:// 場に出してからNターン目に任意で
                case TriggerType.SPECIFY_TURN_FORCED:// 場に出してからNターン目に強制で
                    int turnNum = model.EffectTriggerParameter;
                    Debug.Log(model.EffectName + "効果発動トリガー : " + (turnNum == unit.Turn));
                    return turnNum == unit.Turn;

                case TriggerType.PLAYER_TURN_MANY_SELECT:// 自分のターンに何度でも任意で
                case TriggerType.PLAYER_TURN_MANY_FORCED:// 自分のターンに何度でも強制で
                    Debug.Log(model.EffectName + "効果発動トリガー : " + CheckPlayerTurn());
                    return CheckPlayerTurn();
                
                case TriggerType.OPPONENT_TURN_MANY_SELECT:// 相手のターンに何度でも任意で
                case TriggerType.OPPONENT_TURN_MANY_FORCED:// 相手のターンに何度でも強制で
                    Debug.Log(model.EffectName + "効果発動トリガー : " + !CheckPlayerTurn());
                    return !CheckPlayerTurn();

                case TriggerType.CARD_DESTORY_SELECT:// カードが破壊されたとき任意で
                case TriggerType.CARD_DESTORY_FORCED:// カードが破壊されたとき強制で
                    Debug.Log(model.EffectName + "効果発動トリガー : " + unit.IsDestroy);
                    return unit.IsDestroy;

                default:// その他のトリガー
                    Debug.Log(model.EffectName + "効果発動トリガー : FALSE");
                    return false;
            }
            return true;
        }

        // TODO 未完成 効果発動条件チェック
        public static bool CheckEffectCondition(int type, int param)
        {
            ConditionType conditionType = (ConditionType)type;

            switch(conditionType)
            {
                case ConditionType.NONE:
                    Debug.Log("効果発動トリガー : TRUE");
                    return true;
                default:
                    Debug.Log("効果発動トリガー : FALSE");
                    return false;
            }
        }

        // TODO 未完成 カード効果発動
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

        // プレイヤーのターンかどうか
        public static bool CheckPlayerTurn()
        {
            TimingType CurTiming = IngameManager.Instance.CurTiming;
            switch(CurTiming)
            {
                case TimingType.START_TURN_PLAYER:
                case TimingType.TURN_PLAYER:
                case TimingType.END_TURN_PLAYER:
                case TimingType.END_ATTACK_PLAYER:
                    return true;
                default:
                    return false;
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

        // カード効果発動が任意かどうか
        public static bool CheckSelectEffectTrigger(EffectMainModel model)
        {
            switch((TriggerType)model.EffectTriggerType)
            {
                case TriggerType.PLACEMENT_SELECT:
                case TriggerType.USE_SELECT:
                case TriggerType.PLAYER_TURN_SELECT:
                case TriggerType.OPPONENT_TURN_SELECT:
                case TriggerType.SPECIFY_TURN_SELECT:
                case TriggerType.PLAYER_TURN_MANY_SELECT:
                case TriggerType.OPPONENT_TURN_MANY_SELECT:
                case TriggerType.CARD_DESTORY_SELECT:
                    Debug.Log("発動が任意カード");
                    return true;
                default:
                    Debug.Log("発動が強制カード");
                    return false;
            }
        }
    }
}