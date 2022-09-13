using System;
using System.Collections.Generic;
using ShunLib.Const.Audio;
using ShunLib.Const.Game;
using UnityEngine;
using UnityEngine.Events;

using UK.Manager.Master;
using UK.Manager.Card;
using UK.Manager.Ingame;
using UK.Manager.Popup;
using UK.Manager.Particle;

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
using UK.Manager.Audio;
using UK.Manager.UI;
using UK.Unit.Card;
using UK.Unit.Field;
using UK.Unit.Player;
using UK.Utils.Unit;

namespace UK.Utils.Card
{
    public class CardUtils
    {
        // 効果番号から効果を取得
        public static EffectMainModel GetEffectMainModel(int effectId)
        {
            return ((EffectMainDao)UKMasterManager.Instance.GetDao("EffectMainDao")).GetModelById(effectId);
        }

        // 効果番号から効果リストを取得
        public static List<EffectGroupModel> GetEffectGroupModelList(int effectId)
        {
            return ((EffectGroupDao)UKMasterManager.Instance.GetDao("EffectGroupDao")).GetModelAllById(effectId);
        }

        // 効果能力番号から能力を取得
        public static EffectAbilityModel GetEffectAbilityModel(int effectAbilityId)
        {
            return ((EffectAbilityDao)UKMasterManager.Instance.GetDao("EffectAbilityDao")).GetModelById(effectAbilityId);
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
            if (!CheckEffectCondition(
                    model.EffectConditionType, 
                    model.EffectConditionParameter,
                    unit.IsPlayer
                    )) return false;

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
            if (CheckSelectEffectTrigger((TriggerType)model.EffectTriggerType))
            {
                Dictionary<string, Action> actions = new Dictionary<string, Action>();
                actions.Add(
                    ShunLib.Popup.Common.CommonPopup.DECISION_BUTTON_EVENT,
                    () => {
                        // 能力発動
                        CardAbilityActivate(effectList, abilityList, unit);
                    }
                );
                PopupManager.Instance.SetCheckEffectPopup(unit.CardModel, actions);
                PopupManager.Instance.ShowCheckEffectPopup();
                return true;
            }
            else
            {
                // 能力発動
                CardAbilityActivate(effectList, abilityList, unit);
                return true;
            }
        }

        // カード能力発動
        public static void CardAbilityActivate(List<EffectGroupModel> effectList, List<EffectAbilityModel> abilityList, CardUnit unit)
        {
            for (int i = 0; i < abilityList.Count; i++)
            {
                // 発動条件のチェック
                if (!CheckEffectCondition(
                    effectList[i].AbilityConditionType, 
                    effectList[i].AbilityConditionParameter,
                    unit.IsPlayer)
                ){ continue; }

                // カード効果の関数を呼び出し
                Debug.Log(abilityList[i].AbilityName + "の効果発動[" + (i+1) + "]");

                int index = i;
                var parameter = new
                {
                    mainText = GetEffectMainModel(unit.CardModel.EffectId).EffectText,
                    titleText = unit.CardModel.CardName + "の効果発動",
                    cardMainModel = unit.CardModel,
                    isPlayer = unit.IsPlayer
                };
                Dictionary<string, Action> actions = new Dictionary<string, Action>();
                actions.Add(
                    UK.Popup.CardEffectActivate.CardEffectActivatePopup.CALLBACK_EVENT,
                    () =>
                    {
                        EffectActivate(abilityList[index]);
                    }
                );
                PopupManager.Instance.SetCardEffectActivatePopup(parameter, actions);
                PopupManager.Instance.ShowCardEffectActivatePopup();
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
        public static bool CheckEffectCondition(int type, int param, bool isPlayer)
        {
            ConditionType conditionType = (ConditionType)type;

            switch(conditionType)
            {
                case ConditionType.NONE:
                    Debug.Log("効果発動条件 : TRUE");
                    return true; 
                
                // 自分の資金がN以上なら
                case ConditionType.FUND_HIGHER_PLAYER:
                    return IngameManager.Instance.GetPlayerUnit(isPlayer).Fund >= param;
                
                // 自分の国民数がN以上なら
                case ConditionType.PEOPLENUM_HIGHER_PLAYER:
                    return IngameManager.Instance.GetPlayerUnit(isPlayer).PeopleNum >= param;
                
                // 自分の場にカード名「XXXX」があるなら
                case ConditionType.PLACE_PLAYER_FIELD_NAME:
                    return CardManager.Instance.GetCardBattleField(isPlayer).CheckPlacementCardById(param);
                
                default:
                    Debug.Log("効果発動条件 : FALSE");
                    return false;
            }
        }

        // カード効果発動
        public static void EffectActivate(EffectAbilityModel model)
        {
            AbilityType abilityType = (AbilityType)model.AbilityType;
            switch(abilityType)
            {
                // 山札をN枚ドロー
                case AbilityType.DECK_CARD_DRAW:
                    EffectActionByUserType((bool b) =>
                    {
                        CardManager.Instance.DeckDraw(b, model.AbilityParameter1);
                    }, (UserType)model.UserType);
                    break;
                
                // 手札がN枚になるように山札をドロー
                case AbilityType.DECK_CARD_DRAW_HAND:
                    EffectActionByUserType((bool b) =>
                    {
                        int handNum = CardManager.Instance.GetCardBattleField(b).GetHandUnit().HandCard.Count;

                        if (model.AbilityParameter1 > handNum)
                        {
                            CardManager.Instance.DeckDraw(b, (model.AbilityParameter1 - handNum));
                        }
                    }, (UserType)model.UserType);
                    break;
                
                // 手札を全て山札に戻し、山札をN枚ドロー
                case AbilityType.DECK_SHUFFLE_DRAW:
                    EffectActionByUserType((bool b) =>
                    {
                        CardBattleField battleField = CardManager.Instance.GetCardBattleField(b);
                        battleField.BackAllHandCard();
                        battleField.GetDeckUnit().Shuffle();
                        CardManager.Instance.DeckDraw(b, model.AbilityParameter1);
                    }, (UserType)model.UserType);
                    break;
                
                // ユニットパラメータ変更系
                case AbilityType.POWER_UP:
                case AbilityType.POWER_DOUBLE:
                case AbilityType.POWER_DOWN:
                case AbilityType.PEOPLENUM_UP:
                case AbilityType.PEOPLENUM_DOUBLE:
                case AbilityType.PEOPLENUM_DOWN:
                case AbilityType.FUND_UP:
                case AbilityType.FUND_DOUBLE:
                case AbilityType.FUND_DOWN:
                case AbilityType.TURN_FUND_UP:
                case AbilityType.TURN_FUND_DOUBLE:
                case AbilityType.TURN_FUND_DOWN:
                    UnitUtils.UpdateParameter(model);
                    break;

                // 山札からカードを取得
                case AbilityType.DECK_CARD_GET:
                case AbilityType.DECK_CARD_GET_NAME:
                case AbilityType.DECK_CARD_PLACE_NAME:
                case AbilityType.DECK_PEASON_GET:
                case AbilityType.DECK_PEASON_PLACE:
                case AbilityType.DECK_BUILDING_GET:
                case AbilityType.DECK_BUILDING_PLACE:
                case AbilityType.DECK_GOODS_GET:
                case AbilityType.DECK_GOODS_PLACE:
                case AbilityType.DECK_POLICY_GET:
                case AbilityType.DECK_POLICY_PLACE:
                    var popupParam = new
                    {
                        ability = abilityType,
                        activeCardList = GetActiveCardList(abilityType),
                        abilityParameter = model.AbilityParameter1,
                        cardId = model.AbilityParameter2
                    };
                    EffectActionByUserType((bool b) =>
                    {
                        Dictionary<string, Action> actions = new Dictionary<string, Action>();
                        PopupManager.Instance.SetDeckCardViewPopup(
                            CardManager.Instance.GetCardBattleField(b).GetDeckUnit(),
                            actions,
                            popupParam
                        );
                        PopupManager.Instance.ShowDeckCardViewPopup();
                    }, (UserType)model.UserType);
                    break;
                
                // プレイヤーのHP回復
                case AbilityType.PLAYER_HP_HEAL:
                    IngameManager.Instance.GetPlayerUnit((UserType)model.UserType).HealHp(model.AbilityParameter1);
                    break;
                
                // 人物カードのHPを回復
                case AbilityType.CARD_HP_HEAL:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlacePersonCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.Heal(model.AbilityParameter1);
                            }
                        }
                    );
                    break;
                
                // 人物カードのATKを変更
                case AbilityType.ATK_UP:
                case AbilityType.ATK_DOUBLE:
                case AbilityType.ATK_DOWN:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlacePersonCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.UpdateAtk(abilityType, model.AbilityParameter1);
                            }
                        }
                    );
                    break;
                
                // 人物カードのHPを変更
                case AbilityType.HP_UP:
                case AbilityType.HP_DOUBLE:
                case AbilityType.HP_DOWN:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlacePersonCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.UpdateHp(abilityType, model.AbilityParameter1);
                            }
                        }
                    );
                    break;
                
                // 人物カードの受けるダメージを軽減
                case AbilityType.CARD_DAMAGE_DOWN:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlacePersonCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                // カードユニット軽減
                                cardUnit.EffectList.AddEffectUnit(model);
                            }
                        }
                    );
                    break;
                
                // 自分のすべてのユニットが受けるダメージをN減少させる
                case AbilityType.ALL_CARD_DAMAGE_DOWN:
                    foreach (CardUnit cardUnit in GetPlacePersonCardList((UserType)model.UserType))
                    {
                        // カードユニット軽減
                        cardUnit.EffectList.AddEffectUnit(model);
                    }
                    break;

                // カードを破壊
                case AbilityType.DESTORY_FIELD_CARD:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlaceCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.DestroyCard();
                            }
                        }
                    );
                    break;
                    
                // 人物カードを破壊
                case AbilityType.DESTORY_FIELD_PERSON_CARD:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlacePersonCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.DestroyCard();
                            }
                        }
                    );
                    break;

                // 建造物カードを破壊
                case AbilityType.DESTORY_FIELD_BUILDING_CARD:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlaceBuildingCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.DestroyCard();
                            }
                        }
                    );
                    break;
                
                // プレイヤーにバフ持たせる系
                case AbilityType.PLAYER_DAMAGE_DOWN:
                    IngameManager.Instance.GetPlayerUnit((UserType)model.UserType).EffectList.AddEffectUnit(model);
                    break;
                
                // 人物カードにダメージを与える
                case AbilityType.CARD_DAMAGE:
                    CardManager.Instance.SelectPlaceCard(
                        GetPlacePersonCardList((UserType)model.UserType),
                        model.AbilityParameter2,
                        (List<CardUnit> selectCardList) =>
                        {
                            foreach (CardUnit cardUnit in selectCardList)
                            {
                                cardUnit.Damage(model.AbilityParameter1);
                                IngameParticleManager.Instance.CreateParticle(
                                    ParticleType.CANNON_DAMAGE, cardUnit.gameObject.transform.position, Quaternion.identity
                                );
                                UKAudioManager.Instance.PlaySE(AudioConst.SE_CANNON);
                            }
                        }
                    );
                    break;

                default:
                    break;
            }
        }
        
        // UserType別に場のカードリストを返す
        public static List<CardUnit> GetPlaceCardList(UserType userType)
        {
            switch (userType)
            {
                case UserType.USE_PLAYER:
                    return CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.PLAYER).GetPlaceCardList();
                case UserType.USE_OPPONENT:
                    return CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.OPPONENT).GetPlaceCardList();
                case UserType.USE_ALL:
                    List<CardUnit> cardList = new List<CardUnit>();
                    cardList.AddRange(CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.PLAYER).GetPlaceCardList());
                    cardList.AddRange(CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.OPPONENT).GetPlaceCardList());
                    return cardList;
                default:
                    return new List<CardUnit>();
            }
        }
        
        // UserType別に場の人物カードリストを返す
        public static List<CardUnit> GetPlacePersonCardList(UserType userType)
        {
            switch (userType)
            {
                case UserType.USE_PLAYER:
                    return CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.PLAYER).GetPlacePersonCardList();
                case UserType.USE_OPPONENT:
                    return CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.OPPONENT).GetPlacePersonCardList();
                case UserType.USE_ALL:
                    List<CardUnit> cardList = new List<CardUnit>();
                    cardList.AddRange(CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.PLAYER).GetPlacePersonCardList());
                    cardList.AddRange(CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.OPPONENT).GetPlacePersonCardList());
                    return cardList;
                default:
                    return new List<CardUnit>();
            }
        }
        
        // UserType別に場の人物カードリストを返す
        public static List<CardUnit> GetPlaceBuildingCardList(UserType userType)
        {
            switch (userType)
            {
                case UserType.USE_PLAYER:
                    return CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.PLAYER).GetPlaceBuildingCardList();
                case UserType.USE_OPPONENT:
                    return CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.OPPONENT).GetPlaceBuildingCardList();
                case UserType.USE_ALL:
                    List<CardUnit> cardList = new List<CardUnit>();
                    cardList.AddRange(CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.PLAYER).GetPlaceBuildingCardList());
                    cardList.AddRange(CardManager.Instance.GetCardBattleField(UK.Const.Game.GameConst.OPPONENT).GetPlaceBuildingCardList());
                    return cardList;
                default:
                    return new List<CardUnit>();
            }
        }

        // カード効果を受けるプレイヤーを判断し実行
        public static void EffectActionByUserType(UnityAction<bool> effectAction, UserType userType)
        {
            switch (userType)
            {
                case UserType.USE_PLAYER:
                    effectAction(UK.Const.Game.GameConst.PLAYER);
                    break;
                case UserType.USE_OPPONENT:
                    effectAction(UK.Const.Game.GameConst.OPPONENT);
                    break;
                case UserType.USE_ALL:
                    effectAction(UK.Const.Game.GameConst.PLAYER);
                    effectAction(UK.Const.Game.GameConst.OPPONENT);
                    break;
                default:
                    break;
            }
        }
        
        // 表示するカード種別リストの取得
        public static List<CardType> GetActiveCardList(AbilityType abilityType)
        {
            List<CardType> activeCardList = new List<CardType>();
            switch (abilityType)
            {
                case AbilityType.DECK_CARD_GET:
                case AbilityType.DECK_CARD_GET_NAME:
                case AbilityType.DECK_CARD_PLACE_NAME:
                    break;
                case AbilityType.DECK_PEASON_GET:
                case AbilityType.DECK_PEASON_PLACE:
                    activeCardList.Add(CardType.PERSON);
                    break;
                case AbilityType.DECK_BUILDING_GET:
                case AbilityType.DECK_BUILDING_PLACE:
                    activeCardList.Add(CardType.BUILDING);
                    break;
                case AbilityType.DECK_GOODS_GET:
                case AbilityType.DECK_GOODS_PLACE:
                    activeCardList.Add(CardType.GOODS);
                    break;
                case AbilityType.DECK_POLICY_GET:
                case AbilityType.DECK_POLICY_PLACE:
                    activeCardList.Add(CardType.POLICY);
                    break;
            }
            return activeCardList;
        }
        
        // 条件に一致するカードのみ返す
        public static List<CardUnit> GetMatchCardUnit(List<CardUnit> cardList, List<CardType> conditionList = default)
        {
            List<CardUnit> matchCardList = new List<CardUnit>();

            if (conditionList == default || conditionList.Count <= 0)
            {
                matchCardList = cardList;
                return matchCardList;
            }

            foreach (CardUnit cardUnit in cardList)
            {
                foreach (CardType condition in conditionList)
                {
                    CardType cardType = (CardType)cardUnit.CardModel.CardType;
                    if (cardType == condition)
                    {
                        if (!matchCardList.Contains(cardUnit))
                        {
                            matchCardList.Add(cardUnit);
                        }
                    }
                }
            }
            return matchCardList;
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
        
        // カードタイプを取得
        public static CardType GetCardType(CardMainModel model)
        {
            return (CardType)model.CardType;
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
        
        // カードの国名を取得
        public static string GetCardCountryName(int countryId)
        {
            CountryMainModel model = ((CountryMainDao)UKMasterManager.Instance.GetDao("CountryMainDao")).GetModelById(countryId);
            return model.CountryName;
        }

        // カード効果発動が任意かどうか
        public static bool CheckSelectEffectTrigger(TriggerType triggerType)
        {
            switch(triggerType)
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
        
        // 場に出ているカードのグレーアウト
        public static void SetGrayOutPlaceCard(List<CardUnit> placeCardList)
        {
            foreach (CardUnit cardUnit in placeCardList)
            {
                cardUnit.SetBlinkFrame(false);
                cardUnit.SetGrayOut(true);
            }
        }
        
        // 場に出ているカードの点滅
        public static void SetBlinkPlaceCard(List<CardUnit> placeCardList)
        {
            foreach (CardUnit cardUnit in placeCardList)
            {
                cardUnit.SetGrayOut(false, isButtonEvent:true);
                cardUnit.SetBlinkFrame(true);
            }
        }
    }
}