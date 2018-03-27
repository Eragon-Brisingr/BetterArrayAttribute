#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, Inherited = true)]
public class ReorderableAttribute : PropertyAttribute
{
    /// <summary>
    /// Display a List/Array as a sortable list in the inspector
    /// </summary>
    public ReorderableAttribute()
    {

    }
}
#endif
