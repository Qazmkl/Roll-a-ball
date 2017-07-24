using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int count;
    private bool onGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        winText.text = "";
        onGround = true;
        SetCountText();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical= Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (rb.velocity.z >= 0 && moveVertical < 0)
        {
            movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        }
        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Space) && onGround == true)
        {
            rb.velocity += 10 * Vector3.up;
            onGround = false;
        }
    }

    private void Update()
    {
        if (rb.position.y < -20)
        {
            rb.position = new Vector3(0, 5, 0);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "YOU WIN";
        }
    }
}
