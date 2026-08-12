using System.Collections.Generic;
using Godot;

namespace core.utils.collections;

public static class Extensions
{
    public static void TrySetValue<[MustBeVariant] TKey, [MustBeVariant] TValue>(this Godot.Collections.Dictionary<TKey, TValue> in_dictionary, TKey in_key, TValue in_value)
    {
        if (in_dictionary.TryAdd(in_key, in_value))
        {
            return;
        }
        in_dictionary[in_key] = in_value;
    }
    
    public static TValue GetValueOrDefault<[MustBeVariant] TKey, [MustBeVariant] TValue>(this Godot.Collections.Dictionary<TKey, TValue> in_dictionary, TKey in_key)
    {
        return in_dictionary.TryGetValue(in_key, out TValue outValue) ? outValue : default(TValue);
    }

    public static bool FindKey<[MustBeVariant] TKey, [MustBeVariant] TValue>(this Godot.Collections.Dictionary<TKey, TValue> in_dictionary, TValue in_value, out TKey out_key)
    {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in in_dictionary)
        {
            if (keyValuePair.Value.Equals(in_value))
            {
                out_key = keyValuePair.Key;
                return true;
            }
        }
        out_key = default(TKey);
        return false;
    }
}