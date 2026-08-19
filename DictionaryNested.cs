using core.utils.collections;
using Godot;
using Godot.Collections;

namespace core.utils;

/// <summary>
/// Base Class to represent a Disctionary. Used to Export nested Dictionary typed in Godot Editor.
/// </summary>
public abstract partial class DictionaryNested<[MustBeVariant] TKey, [MustBeVariant] TValue> : Resource
{
    public abstract Dictionary<TKey, TValue> Dict { get; }
    
    public TValue this[TKey in_key]
    {
        get => Dict.GetValueOrDefault(in_key);
        set => Dict.TrySetValue(in_key, value);
    }
}