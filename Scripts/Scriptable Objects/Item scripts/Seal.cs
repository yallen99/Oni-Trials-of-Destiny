using UnityEngine;

namespace Scriptable_Objects.Item_scripts
{
    [CreateAssetMenu(menuName = "New Item/Seal", fileName = "Seal" )]
    public class Seal : ItemObject
    {
        private void Awake()
        {
            type = ItemType.Seal;
        }
    }
}