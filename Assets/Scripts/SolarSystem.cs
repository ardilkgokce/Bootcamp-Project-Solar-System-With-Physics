using UnityEngine;

public class SolarSystem : MonoBehaviour
{
    readonly float G = 100f;
    GameObject[] celestials;
    
    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();
    }

    private void Update()
    {
        PlanetTurn();
    }
    private void FixedUpdate()
    {
        Gravity();
        
    }

    void Gravity()//It is calculated for all celestials and meteorics in each frame.
    {
        foreach (GameObject a in celestials)//GameObject with tag celestials.
        {
            foreach (GameObject b in celestials)//GameObject with tag celestials.
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));//Circular orbit instant velocity.
                }
            }
        }

    }

    void InitialVelocity()//Instant velocity.
    {
        foreach (GameObject a in celestials)
        {
            foreach (GameObject b in celestials)
            {
                if (!a.Equals(b))
                {
                    float m2 = b.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(a.transform.position, b.transform.position);
                    a.transform.LookAt(b.transform);

                    a.GetComponent<Rigidbody>().velocity += a.transform.right * Mathf.Sqrt((G * m2) / r);
                }
            }
        }
    }
    void PlanetTurn()
    {
        foreach (GameObject planet in celestials)
        {
            planet.transform.Rotate(Vector3.down.normalized);
        }
    }
    
}
