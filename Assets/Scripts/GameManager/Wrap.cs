using Unity.Collections;
using UnityEngine;

public class Wrap : MonoBehaviour
{
   /// <summary>
   /// This script going to handel every object and make it rotate 
   /// </summary>
    void Update()
    {
        //convert wold tp viewport
        Vector3 viewportPositon =Camera.main.WorldToViewportPoint(transform.position);
        //move it to oppositsise
        Vector3 moveAdjustment= Vector3.zero;

        if(viewportPositon.x < 0){
            moveAdjustment.x += 1;
        }
        else if (viewportPositon.x >1){
            moveAdjustment.x -= 1;
        }

        if (viewportPositon.y < 0 ){
            moveAdjustment.y += 1;
        }
        else if (viewportPositon.y > 1){
            moveAdjustment.y -= 1;
        }

        if(moveAdjustment != Vector3.zero){
            Vector3 newPos = viewportPositon + moveAdjustment;
            transform.position = Camera.main.ViewportToWorldPoint(newPos);
       }
    }

}
