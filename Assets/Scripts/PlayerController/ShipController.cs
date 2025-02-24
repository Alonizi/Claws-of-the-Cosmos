//copyrights Abdulaziz Alonizi 2025

using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// responsible for controlling the player's ship using an xbox controller
/// </summary>
public class ShipController : MonoBehaviour
{
    [Header("Xbox Controller Mapping")]
    
    [Tooltip("Maps to Right Stick.")]
    [SerializeField] private InputAction Aim;
    [Tooltip("Maps to Left Stick.")]
    [SerializeField] private InputAction Thrusters;
    [Tooltip("Maps to Right Bumper.")]
    [SerializeField] private InputAction Fire;
    [Tooltip("Maps to Right Trigger.")]
    [SerializeField] private InputAction Boost;
    
    [Header("Bullet")]
    [SerializeField] private GameObject BulletPrefab;
    
    [Header("Ship's Settings")]
    [SerializeField] private float RotationDampening =.5f;
    [SerializeField] private float BoostMultiplier = 2f;
    [SerializeField] private float BulletSpeed = 1f;
    
    private Rigidbody2D RigidComp;

    /// <summary>
    /// enable all input sources 
    /// </summary>
    private void OnEnable()
    {
        Aim.Enable();
        Thrusters.Enable();
        Fire.Enable();
        Boost.Enable();
    }
    /// <summary>
    /// cache rigidbody component on start 
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RigidComp = GetComponent<Rigidbody2D>();
    }
    /// <summary>
    /// call the functions responsible for moving , firing and boosting the ship
    /// on each frame
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        MoveShip();
        FireBullet(RigidComp.rotation);
        SpeedBoost();
    }
    
    /// <summary>
    /// call the function responsible for aiming the ship 
    /// </summary>
    private void FixedUpdate()
    {
        AimShip();
    }
    
    /// <summary>
    /// control ship's linear velocity,
    /// using vector2 values captured from controller's left stick
    /// </summary>
    private void MoveShip()
    {
        //thrusters 
        RigidComp.linearVelocity = Thrusters.ReadValue<Vector2>();
        Debug.Log($"Thruster : {Thrusters.ReadValue<Vector2>()}");
    }
    /// <summary>
    /// control ship rotation 
    /// using vector2 values captured from controller's right stick
    /// </summary>
    private void AimShip()
    {
        if (Aim.IsInProgress())
        {
            var velocity = 0f;
            var angle = (Mathf.Rad2Deg * Mathf.Atan2(Aim.ReadValue<Vector2>().y, Aim.ReadValue<Vector2>().x)) - 90;
            //add dampening to smooth out the rotation 
            var dampenedAngle = Mathf.SmoothDampAngle(RigidComp.rotation, angle, ref velocity, RotationDampening);
            RigidComp.SetRotation(dampenedAngle);
            Debug.Log($"Aim : {dampenedAngle}");
        }
    }
    /// <summary>
    /// instantiate 'fire' a bullet
    /// each time the controller's Right Bumper is pressed 
    /// </summary>
    /// <param name="angle"></param>
    private void FireBullet(float angle)
    {
        if (Fire.WasReleasedThisFrame())
        {
            var bullet = Instantiate(BulletPrefab, transform.GetChild(0).position, Quaternion.Euler(0, 0, angle));
            var direction = new Vector3(Mathf.Cos((angle+90)*Mathf.Deg2Rad), Mathf.Sin((angle+90)*Mathf.Deg2Rad), 0).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = BulletSpeed * direction;
        }
    }
    /// <summary>
    /// add force the ship 'Boost'
    /// using float value captured from Controller's Right Trigger 
    /// </summary>
    private void SpeedBoost()
    {
        var boostValue = Boost.ReadValue<float>() * BoostMultiplier;
        Debug.Log($"Boost : {boostValue}");
        RigidComp.AddForce(boostValue*RigidComp.linearVelocity); 
    }
}


