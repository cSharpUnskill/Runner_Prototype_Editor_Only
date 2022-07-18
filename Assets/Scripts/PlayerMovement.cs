using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float sideSpeed;
    private float forwardSpeed = 9f;
    [Header("KeyBinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Jumping")]
    public float jumpForce = 0.5f;
   
    bool canJump;
    Rigidbody rb;
    private float health = 5;

    [SerializeField] public Camera cam;
    [SerializeField] public Transform orientation;

    bool canTakeDamage = true;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();       
    }

    private void Update()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);
        
        /*if(rb.velocity.magnitude > 9f)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 9f);
        }*/

        canJump = Physics.Raycast(orientation.position, Vector3.down, 1.2f);
     
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(orientation.right * sideSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-orientation.right * sideSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(orientation.forward * sideSpeed, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-orientation.forward * sideSpeed, ForceMode.Force);
        }

        if (Input.GetKeyDown(jumpKey) && canJump == true)
        {
            Jump();
        }

    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            Damage();
            var start = Quaternion.Euler(0, 0, 0);
            
            if (collision.gameObject.tag == "Barrier")
            {
                StartCoroutine(Cor1());
                StartCoroutine(CamShake(start, Quaternion.Euler(0f, 0f, 10f), 0.05f));
            }
        }
    }

    public void SpeedUp()
    {
        //forwardSpeed = 11f;
        forwardSpeed += Time.deltaTime / 10;
    }

    private IEnumerator Wait()
    {
        canTakeDamage = false;
        yield return new WaitForSecondsRealtime(1);
        canTakeDamage = true;
    }
    void Damage()
    {
        health--;
        print("Ouch! Health is " + health);
        if (health == 0)
        {
            print("You Died");
            UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "DeadZone")
        { 
            var start = Quaternion.Euler(0, 0, 0);
            StartCoroutine(CamShake(start, Quaternion.Euler(0f, 0f, 3f), 0.05f));
            if(canTakeDamage == true)
            {
                StartCoroutine(Wait());
                Damage();
            }
        }
    }

    private IEnumerator Cor1()
    {
        float lastSpeed = 9f;
        forwardSpeed = -7f;

        yield return new WaitForSeconds(0.8f);
        forwardSpeed = lastSpeed;
    } 

    private IEnumerator CamShake(Quaternion originalRotation, Quaternion finalRotation, float duration)
    {
        var start = Quaternion.Euler(0, 0, 0);
        if (duration > 0f)
        {
            float startTime = Time.time;
            float endTime = startTime + duration;
            cam.transform.rotation = originalRotation;
            yield return null;
            while (Time.time < endTime)
            {
                float progress = (Time.time - startTime) / duration;
                
                cam.transform.rotation = Quaternion.Slerp(originalRotation, finalRotation, progress);
                yield return null;
            }
        }
        cam.transform.rotation = start;
    }
}

