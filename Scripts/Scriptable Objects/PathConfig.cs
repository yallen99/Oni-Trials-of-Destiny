using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(menuName = "New Path", fileName = "Path #1")]
    public class PathConfig : ScriptableObject
    {
        public GameObject pathParent;
        public float moveSpeed = 2f;

        [TextArea(15, 20)] public string pathDescription;
    
        //get the actual points the enemy will follow
        public List<Transform> GetWayPoints()
        {
            var waveWayPoints = new List<Transform>();
            foreach(Transform child in pathParent.transform)
            {
                waveWayPoints.Add(child);
            }
            //Debug.Log("Following " + pathParent.name + " through " + waveWayPoints.Count + " way points.");
            return waveWayPoints;
        
        }

        public float GetSpeed() => moveSpeed;
        
    }
}