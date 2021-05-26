using UnityEngine;

namespace Scriptable_Objects.Item_scripts
{
    [CreateAssetMenu(menuName = "New Item/Mini Heal", fileName = "Mini Heal")]
    public class MiniHeal : ItemObject
    {

        public int healthRestored;
        private void Awake()
        {
            type = ItemType.MiniHeal;
        }
    }
}

