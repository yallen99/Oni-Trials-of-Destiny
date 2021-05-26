using UnityEngine;

namespace Scriptable_Objects.Item_scripts
{
    [CreateAssetMenu(menuName = "New Item/Crystal", fileName = "Crystal")]
    public class Crystal : ItemObject
    {
        private void Awake()
        {
            type = ItemType.Crystal;
        }
    }
}
