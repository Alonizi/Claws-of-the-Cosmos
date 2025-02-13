using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
public bool isEnemyHere ;

 private void OnTriggerEnter2D(Collider2D other) {
    if(other.gameObject.tag == "Enemy"){
        Debug.Log("EnemyHere");
       
    }

    
 }
     public bool IsEnemyHere()
    {
        return isEnemyHere;
    }
}
