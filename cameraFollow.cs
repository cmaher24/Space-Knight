using System.Collections.Specialized;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class cameraFollow : MonoBehaviour
{
    public Canvas Health;
    public Transform DHolder;
    public Transform Lose;
    public Transform Win;
    public Transform target;
    public Transform start;// The target object to follow
    public float smoothSpeed = 0.125f; // The speed with which the camera will follow the target
    public Vector3 offset; // The offset between the camera and the target
    private bool followTargetX = true; // Boolean flag to control whether to follow the target on the x-axis
    private bool followTargetY = true; // Boolean flag to control whether to follow the target on the y-axis
    private bool enter = false;
    private bool death = false;

    public float timer1 = 0f;
    public float timer2 = 0f;
    public bool timerIsRunning = false;
    public bool afterDeathTimer = false;
    void Update()
    {

        Vector3 desiredPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (Input.GetKey(KeyCode.Return) && death == false)
        {
            enter = true;
            Health.sortingLayerName = "Third";
        }

        if (enter == false && death == false)
        {
            transform.position = new Vector3(start.position.x, start.position.y - 1, transform.position.z);
        }

        
        if(death)
        {
            Health.sortingLayerName = "Default";
          
            if (DHolder.position.x == 1)
            {
                transform.position = new Vector3(Win.position.x, Win.position.y, -2);

                timer2 = 10f;
                afterDeathTimer = true;

            }
           
            if (DHolder.position.x == -2)
            {
                transform.position = new Vector3(Lose.position.x, Lose.position.y, -2);
                
                timer2 = 10f;
                afterDeathTimer = true;
            }

            if (timerIsRunning)
            {
                timer1 -= Time.deltaTime;
                if (timer1 <= 0)
                {
                    transform.position = new Vector3(Lose.position.x, Lose.position.y, -2);
                    timerIsRunning = false;
                    timer1 = 0f;
                }
            }
            if(afterDeathTimer)
            {
                timer2 -= Time.deltaTime;
                if (timer2 <= 0)
                {
                    transform.position = new (start.position.x, start.position.y - 1, 0);
                    afterDeathTimer = false;
                    timer2 = 0f;
                    enter = false;
                    death = false;

                }
            }
        }  
        if (enter)
            {
                if (target != null)
                {
                    // Calculate the desired position for the camera

                    // Check if the target's x position is within the range (-2, 2)
                    if (target.position.x >= -1.75 && target.position.x <= 1.75)
                    {
                        followTargetX = true; // Resume following on the x-axis if within the range
                    }
                    else
                    {
                        followTargetX = false; // Stop following on the x-axis if outside the range
                    }

                    // Check if the target's y position is within the range (-2, 2)
                    if (target.position.y >= -1.75 && target.position.y <= 1.75)
                    {
                        followTargetY = true; // Resume following on the y-axis if within the range
                    }
                    else
                    {
                        followTargetY = false; // Stop following on the y-axis if outside the range
                    }

                    // Update the desired position based on followTargetX and followTargetY flags
                    if (followTargetX)
                    {
                        desiredPosition.x = target.position.x + offset.x;
                    }
                    if (followTargetY)
                    {
                        desiredPosition.y = target.position.y + offset.y;
                    }

                    // Smoothly interpolate between current camera position and desired position
                    Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
                    transform.position = smoothedPosition; // Update the camera position


                    if (DHolder.position.x == -1)
                    {
                        death = true;
                        enter = false;
                    }
                    if (DHolder.position.x == 1)
                    {
                        death = true;
                        enter = false;

                    }

                }

            }
        
    }
}