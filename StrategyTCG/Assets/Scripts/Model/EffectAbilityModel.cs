using System;
using System.Collections.Generic;
using UnityEngine;
using ShunLib.Model;

namespace UK.Model.EffectAbility
{
    [Serializable]
    public class EffectAbilityModel : BaseModel
    {
        [SerializeField] private int _effectAbilityId = default;
        [SerializeField] private string _abilityName = default;
        [SerializeField] private int _abilityType = default;
        [SerializeField] private int _userType = default;
        [SerializeField] private int _abilityParameter1 = default;
        [SerializeField] private int _abilityParameter2 = default;
        [SerializeField] private int _abilityParameter3 = default;
        [SerializeField] private int _abilityProbability = default;

        public int EffectAbilityId
        {
            get { return _effectAbilityId; }
            set { _effectAbilityId = value; }
        }
        public string AbilityName
        {
            get { return _abilityName; }
            set { _abilityName = value; }
        }
        public int AbilityType
        {
            get { return _abilityType; }
            set { _abilityType = value; }
        }
        public int UserType
        {
            get { return _userType; }
            set { _userType = value; }
        }
        public int AbilityParameter1
        {
            get { return _abilityParameter1; }
            set { _abilityParameter1 = value; }
        }
        public int AbilityParameter2
        {
            get { return _abilityParameter2; }
            set { _abilityParameter2 = value; }
        }
        public int AbilityParameter3
        {
            get { return _abilityParameter3; }
            set { _abilityParameter3 = value; }
        }
        public int AbilityProbability
        {
            get { return _abilityProbability; }
            set { _abilityProbability = value; }
        }
    }
}


