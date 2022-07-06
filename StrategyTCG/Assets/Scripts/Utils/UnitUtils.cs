using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UK.Const.Ability;
using UK.Manager.Ingame;
using UK.Manager.UI;
using UK.Unit.Player;
using UK.Utils.Card;
using UK.Model.EffectAbility;

namespace UK.Utils.Unit
{
    public class UnitUtils
    {
        // 各種プレイヤーパラメータの増加
        public static void UpdateParameter(EffectAbilityModel model)
        {
            AbilityType abilityType = (AbilityType)model.AbilityType;
            bool isPlayer = CardUtils.GetUserType(model.UserType);
            PlayerUnit unit = IngameManager.Instance.GetPlayerUnit(isPlayer);
            switch(abilityType)
            {
                case AbilityType.POWER_UP:
                    unit.Power += model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetPowerText(unit.Power);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.POWER_DOUBLE:
                    unit.Power *= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetPowerText(unit.Power);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.POWER_DOWN:
                    unit.Power -= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetPowerText(unit.Power);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.PEOPLENUM_UP:
                    unit.PeopleNum += model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetPeopleNumText(unit.PeopleNum);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.PEOPLENUM_DOUBLE:
                    unit.PeopleNum *= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetPeopleNumText(unit.PeopleNum);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.PEOPLENUM_DOWN:
                    unit.PeopleNum -= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetPeopleNumText(unit.PeopleNum);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.FUND_UP:
                    unit.Fund += model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetFundText(unit.Fund);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.FUND_DOUBLE:
                    unit.Fund *= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetFundText(unit.Fund);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.FUND_DOWN:
                    unit.Fund -= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetFundText(unit.Fund);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.TURN_FUND_UP:
                    unit.TurnFund += model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetTurnNumText(unit.TurnFund);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.TURN_FUND_DOUBLE:
                    unit.TurnFund *= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetTurnNumText(unit.TurnFund);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                case AbilityType.TURN_FUND_DOWN:
                    unit.TurnFund -= model.AbilityParameter1;
                    UIManager.Instance.GetStatusGroup(isPlayer).SetTurnNumText(unit.TurnFund);
                    UIManager.Instance.GetStatusGroup(isPlayer).PlayerUnit = unit;
                    return;

                default:
                    return;
            }
        }
    }
}