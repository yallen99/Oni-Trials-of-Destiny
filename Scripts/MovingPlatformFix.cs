
using UnityEngine;

public class MovingPlatformFix : MonoBehaviour
{

   //WORKS ONLY FOR VERTICAL MOVEMENT
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.transform.name=="moving platform")
      {
        transform.SetParent(other.transform);
      }

   }
   
   private void OnTriggerExit(Collider other)
   {
      if (other.transform.name=="moving platform")
      {
        transform.SetParent(null);
      }
   }

 
}
