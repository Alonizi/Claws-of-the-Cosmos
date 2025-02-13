using Unity.VisualScripting;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public float FireRate;
    public float BulletSpeed;
    [SerializeField] private GameObject BulletPrefab;
    private float TimeCounter;
    private float Axis ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void AutoFire(Vector3 direction,float angle)
    {
        TimeCounter += Time.deltaTime;
        if (TimeCounter > FireRate)
        {
            var bullet = Instantiate(BulletPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, angle));
            bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * direction;
            TimeCounter = 0; 
        }
    }

    public float Aim(Vector3 playerDirection)
    {
        var angle = (Mathf.Rad2Deg * Mathf.Atan2(playerDirection.y, playerDirection.x)) - 90;
        //Debug.DrawRay(objectPosition,GetMouseDirection(),Color.cyan);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        return angle;
    }

    public void FireDiagonally(float axisDegree )
    {
        TimeCounter += Time.deltaTime;
        if (TimeCounter > FireRate)
        {
            for (int i = 0; i <= 3; i++)
            {
                var barrelPosition = (new Vector3(Mathf.Cos((i * 90) * Mathf.Deg2Rad),
                    Mathf.Sin((i * 90) * Mathf.Deg2Rad), 0));
                var angle = ((i * 90)+axisDegree)*Mathf.Deg2Rad;
                var direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);
                var bullet = Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle));
                    //bullet.transform.localPosition = barrelPosition;
                bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * direction;
                //Debug.Log(angle);
            }
            TimeCounter = 0;
        }
    }
}
