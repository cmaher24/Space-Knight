using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public Transform player;
    public Transform placeHolder;
    public Transform grab;
    public Animator animator;
    public Transform PPlace;

    public BoxCollider2D playerBox;
    public BoxCollider2D grabBox;

    float y = 0.5f;
    bool checkHit = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }
    void GrabStart1()
    {
        animator.SetBool("attack1", true);
        Vector3 newPosition = new Vector3(player.position.x, player.position.y + y, grab.position.z);
        grab.position = newPosition;


    }
    void GrabStart2()
    {
        animator.SetBool("attack2", true);
        Vector3 newPosition = new Vector3(player.position.x, player.position.y + y, grab.position.z);
        grab.position = newPosition;

    }
    void GrabStart3()
    {
        animator.SetBool("attack3", true);
        Vector3 newPosition = new Vector3(player.position.x, player.position.y + y, grab.position.z);
        grab.position = newPosition;

    }
    void GrabOver()
    {
        animator.SetBool("attack1", false);
        Vector3 newPosition = new Vector3(12, -6, grab.position.z);
        grab.position = newPosition;
        Vector3 newHoldPosition = new Vector3(-5, placeHolder.position.y, placeHolder.position.z);
        placeHolder.position = newHoldPosition;
    }
    void GrabOver2()
    {
        animator.SetBool("attack2", false);
        Vector3 newPosition = new Vector3(12, -6, grab.position.z);
        grab.position = newPosition;
        Vector3 newHoldPosition = new Vector3(-4, placeHolder.position.y, placeHolder.position.z);
        placeHolder.position = newHoldPosition;
    }
    void GrabOver3()
    {
        animator.SetBool("attack3", false);
        Vector3 newPosition = new Vector3(12, -6, grab.position.z);
        grab.position = newPosition;
        Vector3 newHoldPosition = new Vector3(-3, placeHolder.position.y, placeHolder.position.z);
        placeHolder.position = newHoldPosition;
    }
    void CheckGrabHit()
    {
        checkHit = AreCollidersWithinEachOther(playerBox, grabBox);

        if (checkHit)
        {
            UnityEngine.Debug.Log("PlayerHit");
            Vector3 newHPosition = new Vector3(-2, PPlace.position.y, PPlace.position.z);
            PPlace.position = newHPosition;
            checkHit = false;

        }
    }
    public bool AreCollidersWithinEachOther(BoxCollider2D collider1, BoxCollider2D collider2)
    {
        Bounds bounds1 = collider1.bounds;
        Bounds bounds2 = collider2.bounds;

        bool intersect = bounds2.Intersects(bounds1);

        return intersect;
    }
    // Update is called once per frame
    void Update()
    {
        if (placeHolder.position.x == -6)
        {
            GrabStart1();
            Vector3 newPosition = new Vector3(0, placeHolder.position.y, placeHolder.position.z);
            placeHolder.position = newPosition;
        }
        if (placeHolder.position.x == -5)
        {
            GrabStart2();
            Vector3 newPosition = new Vector3(0, placeHolder.position.y, placeHolder.position.z);
            placeHolder.position = newPosition;
        }
        if (placeHolder.position.x == -4)
        {
            GrabStart3();
            Vector3 newPosition = new Vector3(0, placeHolder.position.y, placeHolder.position.z);
            placeHolder.position = newPosition;
        }
       
        
    }
}
