using System.Collections;
using UnityEngine;

public class MeteorMove : MonoBehaviour
{

    readonly float G = 100f;//Gravitational constant.
    int randomIndex;
    float meteorTimer;
    bool isCollide;
    float meteorSpeed;
    GameObject colliderPlanet;
    Vector3 distance;//Meteor contact point referance variable for Particle effect.

    public GameObject[] meteoric;
    GameObject[] celestial;


    [SerializeField] ParticleSystem explosion;



    void Start()
    {
        isCollide = false;//First collide is not turn.
        meteorSpeed = 0.002f;
        celestial = GameObject.FindGameObjectsWithTag("Celestial");
        randomIndex = Random.Range(0, 9);

    }
    private void Update()
    {
        //SelfExplosionMeteors();
        StickContactPoint();//meteor explosion particle location fix.
    }

    void FixedUpdate()
    {
        MeteorGravity();

        MeteorVelocity(randomIndex);

    }

    void MeteorGravity()//It is calculated for all celestials and meteorics in each frame.
    {
        foreach (GameObject a in meteoric)
        {
            foreach (GameObject b in celestial)
            {
                if (!a.Equals(b))
                {
                    float m1 = a.GetComponent<Rigidbody>().mass;//Meteoric mass
                    float m2 = b.GetComponent<Rigidbody>().mass;//Celestial mass
                    float r = Vector3.Distance(a.transform.position, b.transform.position);

                    a.GetComponent<Rigidbody>().AddForce((b.transform.position - a.transform.position).normalized * (G * (m1 * m2) / (r * r)));//Circular orbit instant velocity.
                }
            }
        }

    }
    void MeteorVelocity(int randomIndex)//Instant velocity.
    {

        GameObject targetPlanet = celestial[randomIndex];
        gameObject.transform.LookAt(targetPlanet.transform);
        Debug.Log(targetPlanet);


        gameObject.GetComponent<Rigidbody>().velocity += (targetPlanet.transform.position - gameObject.transform.position).normalized * Time.deltaTime * meteorSpeed;
        gameObject.GetComponent<Rigidbody>().AddForce(targetPlanet.transform.position - gameObject.transform.position, ForceMode.Acceleration);


    }

    private void OnCollisionEnter(Collision collision)//Meteor collision on other objects
    {
        if (!collision.gameObject.CompareTag("Meteoric"))
        {
            colliderPlanet = collision.gameObject;
            distance = colliderPlanet.transform.position - transform.position;
            isCollide = true;
            explosion.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            gameObject.GetComponent<AudioSource>().Play();

            Debug.Log(collision.gameObject.name + " gezegenine çarpt?.");
            StartCoroutine("WaitForDestroy");
        }


    }
    void StickContactPoint()//meteor explosion location fix
    {
        if (isCollide)
        {
            explosion.gameObject.transform.position = colliderPlanet.transform.position + distance;
        }
    }
    IEnumerator WaitForDestroy()//wait for animation end.
    {

        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
    void SelfExplosionMeteors()
    {
        meteorTimer += Time.deltaTime;
        if (meteorTimer > 10)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<TrailRenderer>().enabled = false;
            StartCoroutine("WaitForDestroy");
        }
    }


}
