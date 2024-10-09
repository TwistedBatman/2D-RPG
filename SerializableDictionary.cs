using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }
}
