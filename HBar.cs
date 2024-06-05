using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class HBar : MonoBehaviour
{
    public RectTransform Red;
    public RectTransform Health;
    public RectTransform Border;

   

    public Transform Camera;
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(0, 4, 0);

    // Update is called once per frame
    void Update()
    {
        // Ensure the camera is assigned
       

        // Set the position of Red, Health, and Border
        SetPosition(Red);
        SetPosition(Health);
        SetPosition(Border);
    }

    // Function to set the position of the RectTransform
    private void SetPosition(RectTransform target)
    {
        // Calculate the offset position relative to the camera
        Vector3 targetPos = Camera.position + offset;

        // Set the position of the RectTransform
        target.position = RectTransformUtility.WorldToScreenPoint(Camera.GetComponent<Camera>(), targetPos);
    }
}
