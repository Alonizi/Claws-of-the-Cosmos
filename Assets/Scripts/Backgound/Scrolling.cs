using Unity.Mathematics;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public GameObject ActiveBackground;
    public GameObject BackgroundPrefab;
    private Vector3 InitialPosition = new Vector3(-31.88f,3.894935f,0f); 
    private Vector3 FinalPosition = new Vector3(40f,3.894935f,0f); 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActiveBackground.transform.position += new Vector3(.5f, 0, 0)*Time.deltaTime;//alterded value 

        if (ActiveBackground.transform.position.x >= FinalPosition.x)
        {
            Destroy(ActiveBackground);
            ActiveBackground = Instantiate(BackgroundPrefab, InitialPosition, quaternion.identity);
        }
    }
}
