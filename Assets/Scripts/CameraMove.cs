using TMPro;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] TextMeshProUGUI planetInfo;

    bool isIdle;
    float timer;

    float cameraHorizontal;
    float cameraVertical;


    private void Start()
    {
        timer = 0;
        isIdle = true;
        

    }
    void Update()
    {
        IdleCamera();
        CameraMovements();
        ZoomCamera();
        RotateCamera();

    }
    bool IdleCamera()//Call update
    {
        if (Input.GetAxis("Mouse X") == 0 && Input.GetAxis("Mouse Y") == 0)//Inputs for mouse movements.
        {
            timer += Time.deltaTime;
            if (timer >= 3f)
            {
                isIdle = true;//For camera can move. 
            }
        }
        else if (Input.GetKey(KeyCode.LeftControl))//Extra bool control for camera move. 
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
            timer = 0;
        }
        return isIdle;
    }
    void CameraMovements()//Call update 
    {

        if (planetInfo.IsActive() == false && isIdle == true)
        {
           
            transform.LookAt(target.transform); //Target transform is "Sun". 
            transform.Translate(Vector3.right.normalized); 
            
            
        }
    }
    void ZoomCamera()//Call update
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) //Zoom camera if true.
        {
            gameObject.GetComponent<Camera>().fieldOfView--;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) //Zoom out camera if true. 
        {
            gameObject.GetComponent<Camera>().fieldOfView++;
        }
    }
    void RotateCamera()//Call update
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            cameraHorizontal = transform.eulerAngles.y;
            cameraVertical = transform.eulerAngles.x;
        }
        
        if (Input.GetKey(KeyCode.Mouse2))
        {
            cameraHorizontal += Input.GetAxis("Mouse X");
            cameraVertical -= Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(cameraVertical, cameraHorizontal, 0);
        }

    }

}
