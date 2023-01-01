using System;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public class CustomKeyValuePair<TA, TB>
    {
        [field: SerializeField] public TA Key { get; set; }
        [field: SerializeField] public TB Value { get; set; }
    }
}