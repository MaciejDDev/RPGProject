using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueKey : MonoBehaviour
{
    
    
    public string Id;
    
    
    static Dictionary<string, UniqueKey> _usedIds = new Dictionary<string, UniqueKey>();

    private void OnValidate()
    {
        if (string.IsNullOrWhiteSpace(Id))
            Id = Guid.NewGuid().ToString();
        while (_usedIds.TryGetValue(Id, out var usedKey))
        {
            if (usedKey == this)
                return;

            Id = Guid.NewGuid().ToString();

        }
        _usedIds.Add(Id, this);

    }
}
