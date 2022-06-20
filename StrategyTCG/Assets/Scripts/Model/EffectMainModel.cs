using System;
using System.Collections.Generic;
using UnityEngine;
using Ch120.Model;

namespace UK.Model.EffectMain
{
    [Serializable]
    public class EffectMainModel : BaseModel
    {
        [SerializeField] private int _effectId = default;
        [SerializeField] private string _effectName = default;
        [SerializeField] private int _effectConditionType = default;
        [SerializeField] private int _effectConditionParameter = default;
        [SerializeField] private int _effectTimingType = default;
        [SerializeField] private int _effectTimingParameter = default;
        [SerializeField] private int _effectTriggerType = default;
        [SerializeField] private int _effectTriggerParameter = default;
        [SerializeField] private string _effectText = default;

        public int EffectId
        {
            get { return _effectId; }
            set { _effectId = value; }
        }
        public string EffectName
        {
            get { return _effectName; }
            set { _effectName = value; }
        }
        public int EffectConditionType
        {
            get { return _effectConditionType; }
            set { _effectConditionType = value; }
        }
        public int EffectConditionParameter
        {
            get { return _effectConditionParameter; }
            set { _effectConditionParameter = value; }
        }
        public int EffectTimingType
        {
            get { return _effectTimingType; }
            set { _effectTimingType = value; }
        }
        public int EffectTimingParameter
        {
            get { return _effectTimingParameter; }
            set { _effectTimingParameter = value; }
        }
        public int EffectTriggerType
        {
            get { return _effectTriggerType; }
            set { _effectTriggerType = value; }
        }
        public int EffectTriggerParameter
        {
            get { return _effectTriggerParameter; }
            set { _effectTriggerParameter = value; }
        }
        public string EffectText
        {
            get { return _effectText; }
            set { _effectText = value; }
        }
    }
}


