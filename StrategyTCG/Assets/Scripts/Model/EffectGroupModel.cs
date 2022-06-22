using System;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Model;

namespace UK.Model.EffectGroup
{
    [Serializable]
    public class EffectGroupModel : BaseModel
    {
        [SerializeField] private int _effectId = default;
        [SerializeField] private int _effectAbilityId = default;
        [SerializeField] private int _order = default;
        [SerializeField] private int _abilityConditionType = default;
        [SerializeField] private int _abilityConditionParameter = default;

        public int EffectId
        {
            get { return _effectId; }
            set { _effectId = value; }
        }
        public int EffectAbilityId
        {
            get { return _effectAbilityId; }
            set { _effectAbilityId = value; }
        }
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }
        public int AbilityConditionType
        {
            get { return _abilityConditionType; }
            set { _abilityConditionType = value; }
        }
        public int AbilityConditionParameter
        {
            get { return _abilityConditionParameter; }
            set { _abilityConditionParameter = value; }
        }
    }
}


