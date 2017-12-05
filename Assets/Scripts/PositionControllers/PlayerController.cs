using UnityEngine;
using UnityEngine.UI;
using System.Collections;
//The class for controlling the player. WASD and arrow keys. The basic movement of player.
public class PlayerController : MonoBehaviour
{
    
    //private bool isHost = false;
    public bool noBackwards = true;
    public RotateAbout camScript;
    private float moveHorizontal = 0;
    private int count;
    public float maxCamera = 0.5f;
    private Movement ms;
    public float sensitivity = 1.0f;
    public Rigidbody rb;

    public void OnDeath()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    camScript = GameObject.Find("Main Camera").GetComponent<RotateAbout>();
        ms = GetComponent<Movement>();
        Invoke("StartingWeapon",1);
    }
    void StartingWeapon()
    {

        Create.AddLoadout("Swordy", gameObject, true);
    }

    void FixedUpdate()
    {
        
        bool slow = Input.GetKey(KeyCode.LeftShift);
        if (slow)
        {
            Movement.Slow(rb:ms.rb,speed:ms.grip*10);
        }
        float moveV = Input.GetAxis("Vertical");

        float moveH = Input.GetAxis("Horizontal");
        Vector3 forward = transform.forward * moveV;
        Vector3 right = transform.right * moveH;
        Vector3 direction = Vector3.Normalize((forward + right) * 0.5f);
        //rb.AddTorque(Vector3.up * 4.5f * Input.GetAxis("Mouse X"));
        float mouse = Input.GetAxis("Mouse X");
            rb.angularVelocity += Vector3.up * sensitivity * mouse ;
        
        ms.defaultMovement(direction, ms.rb, ms.speed);
        
    }
    void Update()
    {
        float  newDistance = camScript.distance - Input.GetAxis("Mouse ScrollWheel") * 5;
        if (newDistance < 0.5f)
        {
            newDistance = 0.5f;
        }
        if(newDistance > maxCamera)
        {

            newDistance = maxCamera;
        }
        camScript.distance =newDistance;
    }
}