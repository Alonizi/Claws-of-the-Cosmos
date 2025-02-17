using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    [SerializeField] private InputAction Thrusters;
    [SerializeField] private InputAction Fire;
    [SerializeField] private InputAction Aim;
    [SerializeField] private InputAction Boost;

    private Rigidbody2D RigidComp;
    
    
    private void OnEnable()
    {
        Aim.Enable();
        Thrusters.Enable();
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
            // var angleRad = (Mathf.Atan2(Aim.ReadValue<Vector2>().y, Aim.ReadValue<Vector2>().x)) - (90*Mathf.Deg2Rad);
            //Mathf.SmoothDampAngle()
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(Aim.ReadValue<Vector2>().y, Aim.ReadValue<Vector2>().x)) - 90;
            //RigidComp.angularVelocity=angle;
            RigidComp.SetRotation(angle);
            //RigidComp.AddTorque(angleRad*RigidComp.inertia);
            // var m_EulerAngleVelocity = new Vector3(0,0, angle);
            // Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            //RigidComp.MoveRotation(RigidComp.rotation );
            Debug.Log($"Aim : {angle}");
            

        }

    }

    private void FixedUpdate()
    {
        AimShip();
    }
}


