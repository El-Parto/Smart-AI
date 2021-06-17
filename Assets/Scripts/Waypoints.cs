using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public Vector3 Position => transform.position;



    private void OnDrawGizmos()
    {

        // to see where way points are)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
        
    }


    
}
