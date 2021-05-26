using UnityEngine;

namespace Scriptable_Objects.Item_scripts
{
    [CreateAssetMenu(menuName = "New Item/Tail", fileName = "Tail")]
    public class Tail : ItemObject
    {
        private void Awake()
        {
            type = ItemType.Tail;
        }
    }
}
