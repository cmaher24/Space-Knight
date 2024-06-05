using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Boss : MonoBehaviour
{
    public Animator animator;
    
    public BoxCollider2D bossBox;
    public SpriteRenderer render;
    public Transform boss;
    public Transform player;
    public Transform placeHolder;
    public Transform TPlace;
    public Transform PPlace;


    public BoxCollider2D playerBox;
    public BoxCollider2D bossHit1;
    public BoxCollider2D bossHit2;
    public BoxCollider2D bossHit3;
    
    public BoxCollider2D bossHit4;
    public BoxCollider2D bossHit5;
    public BoxCollider2D bossHit6;

    float x = 0;
    float y = 0;
    
    private bool TeleportInOver = false;
    private bool TeleportIn = false;
    private bool flip = false;
    private bool hit = false;
    public bool hitFirst = false;
    public bool hitSecond = false;
    public bool spellFirst = false;

    public bool checkHit = false;

    


    private float timer1 = 3f;
    private bool Timer1Start = false;
    private bool timerIsRunning = false;
    

    private float distanceX = 0;
    public float absX = 0; 
    private float distanceY = 0;
    public float absY = 0;
    


    void Start()
    {
        
        
            
            

    }
    void Update()
    {
        //Distance Calculator
        distanceX = player.position.x - boss.position.x;
        absX = Mathf.Abs(distanceX);
        distanceY = player.position.y - boss.position.y;
        absY = Mathf.Abs(distanceY);
        //Teleport Function
        if (absY < 1 && absX < 1 && Timer1Start == false)
        {
            Timer1Start = true;
            StartTimer1();
        }
        if (timer1 == 0 && absY > 1 && absX > 1 && timerIsRunning == false)
        {
            Timer1Start = false;
            timer1 = 3f;

        }
        //Teleport if Hit
        if(TPlace.position.x == -1)
        {
            FirstPart();
            timer1 = 0;
            Vector3 newPosition = new Vector3(0, TPlace.position.y, TPlace.position.z);
            TPlace.position = newPosition;
        }

        //Hit Function
        if ((absX < 2 && absX > 0.55 && absY < 1) || (absX < 0.2 && absY < 1.2 && (player.position.y > boss.position.y)) && hitFirst == false)
        {
            animator.SetBool("hit", true);
            hit = true;
            hitFirst = true;
        }
        if ((absX < 2 && absX > 0.55 && absY < 1) || (absX < 0.2 && absY < 1.2 && (player.position.y > boss.position.y)) && hitFirst == false)
        {
            
            hitFirst = false;
        }
        //Flip Functions
        if (boss.position.x < player.position.x && hit == false)
        {
            render.flipX = true;
            flip = true;
        }
        if (boss.position.x > player.position.x && hit == false)
        {
            render.flipX = false;
            flip = false;

        }
        //Spell Functions
        if ((absX > 3 || absY > 3) && spellFirst == false)
        {
            spellFirst = true;
            animator.SetBool("spell", true);

        }
        if (placeHolder.position.x == -3)
        {
            spellFirst = false;
            Vector3 newPosition = new Vector3(12, -6, placeHolder.position.z);
            placeHolder.position = newPosition;
        }



        //Timer Function
        if (timerIsRunning)
        {
            timer1 -= Time.deltaTime;
            if (timer1 <= 0)
            {
                FirstPart();
                timerIsRunning = false;
                timer1 = 0f;
               
                
            }
        }





    }

    public void StartTimer1()
    {
       
        timer1 = 3f;
        timerIsRunning = true;
    }


    public void OnAnimationEndIN()
    {
        
        TeleportInOver = true;
        SecondPart();

    }
    public void OnAnimationEndOut()
    {
        animator.SetBool("teleportIN", false);
        animator.SetBool("teleportOUT", false);
        TeleportIn = false;
    }


    void HitOver()
    {
        animator.SetBool("hit", false);
        hit = false;
    }
    void CheckHit()
    {
        if (flip == false)
        {
            checkHit = AreCollidersWithinEachOther(playerBox, bossHit1);
            if (checkHit == false)
            {
                checkHit = AreCollidersWithinEachOther(playerBox, bossHit2);
            }
            if (checkHit == false)
            {
                checkHit = AreCollidersWithinEachOther(playerBox, bossHit3);
            }
            if (checkHit == true)
            {
                UnityEngine.Debug.Log("PlayerHit");
                Vector3 newHPosition = new Vector3(-1, PPlace.position.y, PPlace.position.z);
                PPlace.position = newHPosition;
            }
        }
        if (flip == true)
        {
            checkHit = AreCollidersWithinEachOther(playerBox, bossHit4);
            if (checkHit == false)
            {
                checkHit = AreCollidersWithinEachOther(playerBox, bossHit5);
            }
            if (checkHit == false)
            {
                checkHit = AreCollidersWithinEachOther(playerBox, bossHit6);
            }
            if (checkHit == true)
            {
                UnityEngine.Debug.Log("PlayerHit");
                Vector3 newHPosition = new Vector3(-1, PPlace.position.y, PPlace.position.z);
                PPlace.position = newHPosition;
            }
        }
    }
    public bool AreCollidersWithinEachOther(BoxCollider2D collider1, BoxCollider2D collider2)
    {
        Bounds bounds1 = collider1.bounds;
        Bounds bounds2 = collider2.bounds;

        bool intersect = bounds2.Intersects(bounds1);

        return intersect;
    }
    void SpellOver()
    {
        animator.SetBool("spell", false);
        
        Vector3 newPosition = new Vector3(-6, placeHolder.position.y, placeHolder.position.z);
        placeHolder.position = newPosition;
    }
    
    
    void FirstPart()
    {
        timer1 = 3f;
        animator.SetBool("teleportIN", true);
        TeleportIn = true;

    }
    void SecondPart() 
    {
        int rnd = 0;
        int previous = 0;

        rnd = UnityEngine.Random.Range(1, 10);

        if (rnd == 1 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 2;
            }
            else
            {
                x = -2.2f;
                y = 2.2f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 1;
            }
        }
        else if (rnd == 2 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 3;
            }
            else
            {
                x = 0f;
                y = 2.4f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 2;
            }
        }
        else if (rnd == 3 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 4;
            }
            else
            {
                x = 2.2f;
                y = 2.2f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 3;
            }

        }
        else if (rnd == 4 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 5;
            }
            else
            {


                x = -2.2f;
                y = 0f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 4;
            }

        }
        else if (rnd == 5 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 6;
            }
            else
            {


                x = 0f;
                y = 0f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 5;
            }
        }
        else if (rnd == 6 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 7;
            }
            else
            {
                x = 2.2f;
                y = 0f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 6;
            }
        }
        else if (rnd == 7 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 8;
            }
            else
            {


                x = -2.2f;
                y = -2f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 7;
            }

        }
        else if (rnd == 8 && TeleportInOver)
        {
            if (rnd == previous)
            {
                rnd = 9;
            }
            else
            {


                x = -0f;
                y = -2f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 8;
            }

        }
        else if (rnd == 9 && TeleportInOver)
        {
            if (rnd == previous)
            {
                x = -2.2f;
                y = 2.2f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 9;
            }
            else
            {


                x = 2.2f;
                y = -2f;
                TeleportInOver = false;
                animator.SetBool("teleportOUT", true);
                previous = 9;
            }

        }


        Vector3 newPosition = new Vector3(x, y, boss.position.z);
        boss.position = newPosition;
        
        



    }
   
}