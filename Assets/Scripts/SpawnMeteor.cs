using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpawnMeteor : MonoBehaviour
{
    [SerializeField] GameObject meteorPrefab;
    [SerializeField] Button spawnButton;
    public MeteorMove _meteorMove;
    float xBound;
    float zBound;
    int randomIndex;
    void Start()
    {
        randomIndex = 0;
        spawnButton.onClick.AddListener(Spawn);
        
    }

   
    void Update()
    {
        
    }
    public void Spawn()//Spawn meteor random point.
    {
            randomIndex = Random.Range(0, 9);

            xBound = Random.Range(-1000f, 1000f);
            zBound = Random.Range(-1000f, 1000f);
            Instantiate(meteorPrefab, new Vector3(xBound, 0, zBound), meteorPrefab.transform.rotation);
    }
}
