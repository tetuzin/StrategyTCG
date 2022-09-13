using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Dict;
using ShunLib.Const.Audio;

namespace ShunLib.Dict.Audio
{
    [System.Serializable]
    public class AudioDictionary : TableBase<AudioEnum, AudioClip, KeyAndValue<AudioEnum, AudioClip>>
    {
    
    }
}
