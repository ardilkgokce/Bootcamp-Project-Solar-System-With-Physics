using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI planetInfo;//Planet info text
    [SerializeField] Button closeButton;//Planet info exit button.
    [SerializeField] Image planetInfoImage;
    
    void Start()
    {
        closeButton.onClick.AddListener(ExitUI);
    }

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            RaycastHit hit;
            Ray ray= Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100000000f))//Raycast hit imput mouse position.
            {
                if (hit.transform != null)
                {
                    Debug.Log(hit.transform.gameObject);
                    planetInfo.gameObject.SetActive(true);
                    planetInfoImage.gameObject.SetActive(true);
                    planetInfo.GetComponent<TextMeshProUGUI>().SetText(hit.transform.gameObject.name);

                }
            }

        }
    }

    public void ExitUI()
    {
        planetInfo.gameObject.SetActive(false);
        planetInfoImage.gameObject.SetActive(false);
    }
}
