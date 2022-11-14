using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

public class UIMANAGER : MonoBehaviour
{
    public TextMeshProUGUI SelectedText;

    public GameObject BASEPREFAB;

    public GameObject Level;

    public GameObject CamMan;

    public string Selected;

    void Update()
    {
        Selected = CamMan.name;

        SelectedText.text = "Selected: " + Selected;
    }

    //when spawn button is clicked
    public void Spawn()
    {
        RaycastHit hit;
        //ray is cast from camera forward
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        //raycast 5 meters and spawn object
        if (Physics.Raycast(ray, out hit, 5))
        {
            //spawn object
            GameObject obj = Instantiate(BASEPREFAB, hit.point, Quaternion.identity);
            obj.transform.parent = Level.transform;
        }
        else
        {
            //spawn object
            GameObject obj = Instantiate(BASEPREFAB, ray.origin + ray.direction * 5, Quaternion.identity);
            obj.transform.parent = Level.transform;
        }
    }
}
