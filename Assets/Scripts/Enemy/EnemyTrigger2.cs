using UnityEngine;

public class EnemyTrigger2 : MonoBehaviour
{
 
  bool isEnemy2Here =false;
 private void OnTriggerEnter2D(Collider2D other) {


    if(other.gameObject.tag =="Enemy"){
        isEnemy2Here = true;
    }
    
 }
     public bool isEnemyHere()
    {
        return isEnemy2Here;
    }
}
