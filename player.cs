using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine.Windows.Speech;
using System.Security.Cryptography;
using System;
using System.Diagnostics;
using UnityEngine.UI;



public class PlayerMov : MonoBehaviour
{
    public float movSpeed;
    float speedX = 0;
    float speedY = 0;
    bool attacked = false;


    public Transform Teleport;
    public Rigidbody2D rb; 
    public Animator animator;
    public SpriteRenderer render;
    public CapsuleCollider2D Front;
    public CapsuleCollider2D Back;
    public CapsuleCollider2D SideL;
    public CapsuleCollider2D SideR;
    public BoxCollider2D BossArea;
    public Transform Health;
    public Transform DHolder;
    
    
    bool side = true;
    bool forward = false;
    bool backward = false;
    bool flipX = false;
    bool death = false;

    public int hitNum = 1;
    public int hitCount = 0;
    public int hitRand = 10;
    public bool hitTrue = false;

    public bool timerIsRunning = false;
    float timer1 = 0f;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (death == false)
        {
            rb.velocity = new Vector2(speedX, speedY);
            rb.rotation = 0;
            rb.angularVelocity = 0;
            if (attacked == false)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    speedY = 1;
                    animator.SetBool("sideView", false);
                    animator.SetBool("backView", true);
                    animator.SetBool("forwardView", false);
                    animator.SetFloat("speed", 1);

                    backward = true;
                    forward = false;
                    side = false;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    speedY = -1;
                    animator.SetBool("sideView", false);
                    animator.SetBool("backView", false);
                    animator.SetBool("forwardView", true);
                    animator.SetFloat("speed", 1);

                    backward = false;
                    forward = true;
                    side = false;
                }
                else
                {
                    speedY = 0;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    speedX = -1;
                    render.flipX = true;
                    flipX = true;
                    animator.SetBool("sideView", true);
                    animator.SetBool("backView", false);
                    animator.SetBool("forwardView", false);
                    animator.SetFloat("speed", 1);

                    backward = false;
                    forward = false;
                    side = true;


                }
                else if (Input.GetKey(KeyCode.D))
                {
                    speedX = 1;
                    render.flipX = false;
                    flipX = false;
                    animator.SetBool("sideView", true);
                    animator.SetBool("backView", false);
                    animator.SetBool("forwardView", false);
                    animator.SetFloat("speed", 1);

                    backward = false;
                    forward = false;
                    side = true;



                }
                else
                {
                    speedX = 0;

                }
            }

            if (speedX == 0 && speedY == 0)
            {
                animator.SetFloat("speed", 0);
            }



            if (Input.GetKey(KeyCode.K) && side == true)
            {
                animator.SetBool("attackS", true);
                speedX = 0;
                speedY = 0;
                attacked = true;

            }

            if (Input.GetKey(KeyCode.K) && forward == true)
            {
                animator.SetBool("attackF", true);
                speedX = 0;
                speedY = 0;
                attacked = true;
            }

            if (Input.GetKey(KeyCode.K) && backward == true)
            {
                animator.SetBool("attackB", true);
                speedX = 0;
                speedY = 0;
                attacked = true;
            }

            if (Input.GetKeyUp(KeyCode.K))
            {
                animator.SetBool("attackS", false);
                animator.SetBool("attackB", false);
                animator.SetBool("attackF", false);
                attacked = false;

            }

            

        }
        if (timerIsRunning)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
                timerIsRunning = false;
                timer1 = 0f;
                
            }
        }

    }

    void Death()
    {
        rb.velocity = new Vector2(speedX, speedY);
        death = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        timer1 = 5f;
        timerIsRunning = true;
        
        
    }
    void DeathOver()
    {
        animator.SetBool("death", false);
        Vector3 newHPosition = new Vector3(-2, DHolder.position.y, DHolder.position.z);
        DHolder.position = newHPosition;

    }
    void DCF()
    {
        hitTrue = AreCollidersWithinEachOther(BossArea, Front);
        if (hitTrue)
        {
            UnityEngine.Debug.Log("Hit");
            Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
            Health.position = newHPosition;
            hitRand = UnityEngine.Random.Range(1, 6 - hitCount);
            if (hitRand == 1)
            {
                hitCount = 0;
                Vector3 newPosition = new Vector3(-1, Teleport.position.y, Teleport.position.z);
                Teleport.position = newPosition;
            }
            else
            {
                hitCount++;
            }
            hitTrue = false;
        }
    }
    void DCB()
    {
        hitTrue = AreCollidersWithinEachOther(BossArea, Back);
        if (hitTrue)
        {
            UnityEngine.Debug.Log("Hit");
            Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
            Health.position = newHPosition;
            hitRand = UnityEngine.Random.Range(1, 6 - hitCount);
            if (hitRand == 1)
            {
                hitCount = 0;
                Vector3 newPosition = new Vector3(-1, Teleport.position.y, Teleport.position.z);
                Teleport.position = newPosition;
            }
            else
            {
                hitCount++;
            }
            hitTrue = false;
        }
    }
    void DCS()
    {
        if (flipX)
        {
            hitTrue = AreCollidersWithinEachOther(BossArea, SideL);
            if (hitTrue)
            {
                UnityEngine.Debug.Log("Hit");
                Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
                Health.position = newHPosition;
            }
        }
        else
        {
            hitTrue = AreCollidersWithinEachOther(BossArea, SideR);
            if (hitTrue)
            {
                UnityEngine.Debug.Log("Hit");
                Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
                Health.position = newHPosition;
            }
        }
        if (hitTrue)
        {
            hitRand = UnityEngine.Random.Range(1, 6 - hitCount);
            if (hitRand == 1)
            {
                hitCount = 0;
                Vector3 newPosition = new Vector3(-1, Teleport.position.y, Teleport.position.z);
                Teleport.position = newPosition;
            }
            else
            {
                hitCount++;
            }
            hitTrue = false;
        }
    }
    void DCF2()
    {
        hitTrue = AreCollidersWithinEachOther(BossArea, Front);
        if (hitTrue)
        {
            UnityEngine.Debug.Log("Hit");
            Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
            Health.position = newHPosition;
            hitRand = UnityEngine.Random.Range(1, 6 - hitCount);
            if (hitRand == 1)
            {
                hitCount = 0;
                Vector3 newPosition = new Vector3(-1, Teleport.position.y, Teleport.position.z);
                Teleport.position = newPosition;
            }
            else
            {
                hitCount++;
            }
            hitTrue = false;
        }
    }
    void DCB2()
    {
        hitTrue = AreCollidersWithinEachOther(BossArea, Back);
        if (hitTrue)
        {
            UnityEngine.Debug.Log("Hit");
            Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
            Health.position = newHPosition;
            hitRand = UnityEngine.Random.Range(1, 6 - hitCount);
            if (hitRand == 1)
            {
                hitCount = 0;
                Vector3 newPosition = new Vector3(-1, Teleport.position.y, Teleport.position.z);
                Teleport.position = newPosition;
            }
            else
            {
                hitCount++;
            }
            hitTrue = false;
        }
    }
    void DCS2()
    {
        if (flipX)
        {
            hitTrue = AreCollidersWithinEachOther(BossArea, SideL);
            if (hitTrue)
            {
                UnityEngine.Debug.Log("Hit");
                Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
                Health.position = newHPosition;
            }
        }
        else
        {
            hitTrue = AreCollidersWithinEachOther(BossArea, SideR);
            if (hitTrue)
            {
                UnityEngine.Debug.Log("Hit");
                Vector3 newHPosition = new Vector3(-1, Health.position.y, Health.position.z);
                Health.position = newHPosition;
            }
        }
        if (hitTrue)
        {
            hitRand = UnityEngine.Random.Range(1, 6 - hitCount);
            if (hitRand == 1)
            {
                hitCount = 0;
                Vector3 newPosition = new Vector3(-1, Teleport.position.y, Teleport.position.z);
                Teleport.position = newPosition;
            }
            else
            {
                hitCount++;
            }
            hitTrue = false;
        }
    }

    public bool AreCollidersWithinEachOther(BoxCollider2D collider1, CapsuleCollider2D collider2)
    {
        // Get the bounds of the colliders
        Bounds bounds1 = collider1.bounds;
        Bounds bounds2 = collider2.bounds;

        // Check for intersection of bounds
        bool intersect = bounds2.Intersects(bounds1);

        return intersect;
    }




}