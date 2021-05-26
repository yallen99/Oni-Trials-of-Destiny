using System;
using UnityEngine;

namespace Scriptable_Objects
{
    public enum ItemType
    {
        Crystal, Tail, MiniHeal, MaxHeal, Seal
    }

    public class ItemObject : ScriptableObject
    {
        public GameObject prefab;
        public ItemType type;


        [TextArea(15, 20)] public String description;
    }
}