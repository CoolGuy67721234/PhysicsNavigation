using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleChild : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            transform.GetChild(0).gameObject.SetActive(!transform.GetChild(0).gameObject.activeSelf);
        }
    }
}
