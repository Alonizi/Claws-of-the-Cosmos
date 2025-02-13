using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
public float Hitpoints;
public float MaxPoints = 3 ; 
public HealthBarEnemy healthBar;

private void Start() {
    Hitpoints = MaxPoints;
    healthBar.SetHealth(Hitpoints,MaxPoints);
}
public void TakeHit(float damage){
    Hitpoints -= damage ;
    healthBar.SetHealth(Hitpoints,MaxPoints);
    if(Hitpoints == 0){
        Destroy(gameObject);
    }
}
}
