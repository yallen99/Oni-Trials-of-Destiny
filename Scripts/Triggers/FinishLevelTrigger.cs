using Player_Scripts;
using UnityEngine;

namespace Triggers
{
    public class FinishLevelTrigger : MonoBehaviour
    {
    
        private SceneLoader sceneLoader;
    
        private void Start()
        {
            sceneLoader = FindObjectOfType<SceneLoader>();
        }
        private void OnTriggerEnter(Collider other)
        {
            var player = other.GetComponent<PlayerController>();
        

            if (player)
            {
                sceneLoader.LoadTemple();
            }
        }
    }
}
