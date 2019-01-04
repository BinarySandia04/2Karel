using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerLight : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 10000.0f)) {
            GetComponent<Light>().enabled = true;
            Vector3 point = hit.point;
            point.y = point.y + 1;
            transform.position = point;
        } else
        {
            GetComponent<Light>().enabled = false;
        }
    }
}
