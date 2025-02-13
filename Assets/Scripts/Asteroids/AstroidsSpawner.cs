using UnityEngine;

public class AstroidsSpawner : MonoBehaviour
{
    [Header("Referance Astoids")]
    [SerializeField] Astoids asteroidPrefab;
    public float trajectoryVariance =15f;
    public float spawnRate = 2f;
    public float spawnAmount =1;
    public float spawnDistance = 15f ; 
  
    void Start()
    {
          InvokeRepeating(nameof(SpawnAstoids), spawnRate , spawnRate);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void SpawnAstoids()
    {
        for(int i = 0 ; i < spawnAmount ; i++){
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * spawnDistance ; 
            Vector3 spawnPoint = transform.position + spawnDirection;
            float varince = Random.Range(-trajectoryVariance , trajectoryVariance);
            Quaternion rotation = Quaternion.AngleAxis(varince, Vector3.forward);

            Astoids asteroidPrefab = Instantiate(this.asteroidPrefab, spawnPoint,rotation);
            asteroidPrefab.size = Random.Range(asteroidPrefab.minSize , asteroidPrefab.maxSize);
            asteroidPrefab.ResizeColliders(asteroidPrefab);
            asteroidPrefab.SetTrajectory(rotation * - spawnDirection);
        }
    }
}
