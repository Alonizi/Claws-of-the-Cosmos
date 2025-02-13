//copyright Abdulaziz Alonizi 2025
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
///responsible for reading the user input
///and control the ship accordingly 
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputAction Shoot;
    [SerializeField] private InputAction Boost;
    [SerializeField] private InputAction Thrust;
    [SerializeField] private InputAction Turn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Turn.ReadValue<Quaternion>());
    }
}
