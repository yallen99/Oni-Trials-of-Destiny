using UnityEngine;

namespace Scriptable_Objects.Item_scripts
{
    [CreateAssetMenu(menuName = "New Item/Max Heal", fileName = "Max Heal")]
    public class MaxHeal : ItemObject
    {
        public int healthRestored;
        private void Awake()
        {
            type = ItemType.MaxHeal;
        }
    }
}
