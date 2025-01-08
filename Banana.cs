using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Banana : MonoBehaviour
{
    //Banana
    CharacterController controller;
    private float speed = 5f;
    private bool isGrounded;
    private Vector3 velocity;
    private float jumpHeight = 5;
    private float flipSpeed = 6f;
    public bool Up = false;
    private bool Flip1;
    private bool Flip2;
    private bool Flip3;
    public GameObject bouncer;

    public bool Trick1 = false;
    public bool Trick2 = false;
    private bool Trick21 = false;
    private bool Trick22 = false;
    private bool Trick23 = false;
    public bool Trick3 = false;
    private bool Trick31 = false;
    private bool Trick32 = false;
    private bool Trick33 = false;
    public bool Trick4 = false;

    public TextMeshProUGUI moneyText;
    public int money;
    public float currentTime;
    public TextMeshProUGUI timer;

    private bool points1 = false;
    private bool points2 = false;
    private bool points3 = false;
    private bool points4 = false;

    public AudioClip moneySound;
    public AudioClip winSound;
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    public bool timerRunning;
    public TextMeshProUGUI win;

    //Camera
    public Transform cam;

    //Animations
    Animator animator;
    public Transform skateboard;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Banana
        controller = GetComponent<CharacterController>();

        //Animate
        animator = GetComponent<Animator>();
        animator.SetBool("Flip", false);

        Application.targetFrameRate = 60;

        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSource1 = audioSources[0];
        audioSource2 = audioSources[1];

        timerRunning = true;

        win.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //MOVEMENT

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = 10f;
        }
        else
        {
            speed = 5f;
        }

        float forward = Input.GetAxis("Vertical");
        float sideways = Input.GetAxis("Horizontal");
        Vector3 move = (cam.forward * forward + cam.right * sideways);
        Vector3 moveVelocity = move * speed;

        if (isGrounded)
        {
            velocity.y = -1f;
        }
        else
        {
            velocity.y += Physics.gravity.y * Time.deltaTime;
        }

        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            velocity.y = jumpHeight;
        }

        if (moveVelocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveVelocity);
            targetRotation.x = 0f;
            targetRotation.z = 0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 500f * Time.deltaTime);
        }

        Vector3 movement = moveVelocity;
        movement.y = velocity.y;
        controller.Move(movement * Time.deltaTime);

        isGrounded = controller.isGrounded;

        //ANIMATION
        if (!isGrounded && Input.GetKeyDown(KeyCode.Q))  //KickFlip
        {
            Up = true;
            Flip1 = true;

        }
        if (!isGrounded && Input.GetKeyDown(KeyCode.E))  //Pop Shove It
        {
            Up = true;
            Flip2 = true;
        }
        if (!isGrounded && Input.GetKeyDown(KeyCode.R))  //Varial Flip
        {
            Up = true;
            Flip3 = true;
        }

        if (Up)
        {
            FlipBoard();
            if (isGrounded)
            {
                Invoke("StopFlipping", 0f);
            }
        }

        //Debug.Log(isGrounded);

        if(Trick21 && Trick22 && Trick23)
        {
            Trick2 = true;
        }

        if (Trick31 && Trick32 && Trick33)
        {
            Trick3 = true;
        }

        if (Trick1 && Trick2 && Trick3 && Trick4)
        {
            bouncer.gameObject.SetActive(false);
        }

        if(Trick1 && !points1)
        {
            AddMoney(10);
            points1 = true;
        }
        if (Trick2 && !points2)
        {
            AddMoney(10);
            points2 = true;
        }
        if (Trick3 && !points3)
        {
            AddMoney(10);
            points3 = true;
        }
        if (Trick4 && !points4)
        {
            AddMoney(10);
            points4 = true;
        }

        moneyText.text = ("Cash: $" + money.ToString());

        if (timerRunning)
        {
            currentTime += Time.deltaTime;

            int mins = Mathf.FloorToInt(currentTime / 60);
            int sec = Mathf.FloorToInt(currentTime % 60);
            int millisec = Mathf.FloorToInt((currentTime * 100) % 100);
            timer.text = string.Format("{0:00}:{1:00}:{2:00}", mins, sec, millisec);
        }

    }
    private void FlipBoard()
    {
        if (Flip1)
        {
            Vector3 currentRotation = skateboard.localEulerAngles;
            currentRotation.z += flipSpeed;
            skateboard.localEulerAngles = currentRotation;
            animator.SetBool("Flip", true);
        }
        if (Flip2)
        {
            Vector3 currentRotation = skateboard.localEulerAngles;
            currentRotation.y += flipSpeed;
            skateboard.localEulerAngles = currentRotation;
            animator.SetBool("Flip", true);
        }
        if (Flip3)
        {
            Vector3 currentRotation = skateboard.localEulerAngles;
            currentRotation.y += flipSpeed * 1.3f;
            currentRotation.z += flipSpeed * 1.3f;
            skateboard.localEulerAngles = currentRotation;
            animator.SetBool("Flip", true);
        }
    }
    private void StopFlipping()
    {
        Vector3 newRotation = skateboard.localEulerAngles;
        newRotation = new Vector3(0, 0, 0);
        skateboard.localEulerAngles = newRotation;
        animator.SetBool("Flip", false);

        Up = false;
        Flip1 = false;
        Flip2 = false;
        Flip3 = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("TrickCheck1"))
        {
            if(Flip1)
            {
                Trick1 = true;
            }
        }
        if (other.CompareTag("TrickCheck21"))
        {
            if (Flip2)
            {
                Trick21 = true;
            }
        }
        if (other.CompareTag("TrickCheck22"))
        {
            if (Flip2)
            {
                Trick22 = true;
            }
        }
        if (other.CompareTag("TrickCheck23"))
        {
            if (Flip2)
            {
                Trick23 = true;
            }
        }
        if (other.CompareTag("TrickCheck3"))
        {
            if (Flip1)
            {
                Trick31 = true;
            }
            if (Flip2)
            {
                Trick32 = true;
            }
            if (Flip3)
            {
                Trick33 = true;
            }
        }
        if (other.CompareTag("TrickCheck4"))
        {
            if (Flip3)
            {
                Trick4 = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinWall"))
        {
            timerRunning = false;
            win.gameObject.SetActive(true);
            audioSource2.PlayOneShot(winSound);
        }
    }
    private void AddMoney(int amount)
    {
        money += amount;
        audioSource1.PlayOneShot(moneySound);
    }

}
