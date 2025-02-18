using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float BulletSpeed = 1f;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private InputAction Thrusters;
    [SerializeField] private InputAction Fire;
    [SerializeField] private InputAction Aim;
    [SerializeField] private InputAction Boost;
    [SerializeField] private float RotationDampening =.5f;
    private Rigidbody2D RigidComp;

    private void OnEnable()
    {
        Aim.Enable();
        Thrusters.Enable();
        Fire.Enable();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RigidComp = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveShip();
        FireBullet(RigidComp.rotation);
    }
    
    private void FixedUpdate()
    {
        AimShip();
    }

    private void MoveShip()
    {
        //thrusters 
        RigidComp.linearVelocity = Thrusters.ReadValue<Vector2>();
        Debug.Log($"Thruster : {Thrusters.ReadValue<Vector2>()}");
    }

    private void AimShip()
    {
        if (Aim.IsInProgress())
        {
            var velocity = 0f;
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(Aim.ReadValue<Vector2>().y, Aim.ReadValue<Vector2>().x)) - 90;
            var dampenedAngle = Mathf.SmoothDampAngle(RigidComp.rotation, angle, ref velocity, RotationDampening);
            RigidComp.SetRotation(dampenedAngle);
            Debug.Log($"Aim : {dampenedAngle}");
        }
    }
    
    private void FireBullet(float angle)
    {
        if (Fire.WasReleasedThisFrame())
        {
            
            var bullet = Instantiate(BulletPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, angle));
            var direction = new Vector3(Mathf.Cos((angle+90)*Mathf.Deg2Rad), Mathf.Sin((angle+90)*Mathf.Deg2Rad), 0).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * direction;
            
        }
    }
}


