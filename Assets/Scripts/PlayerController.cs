using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private int count;
    public float speed = 0; 
    public TextMeshProUGUI countText;
    private float movementX;
    private float movementY;
    public GameObject winTextObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCountText();
        rb = GetComponent <Rigidbody>(); 
        count = 0;
        winTextObject.SetActive(false);
    }
    private void FixedUpdate() 
    {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }
    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }
    void SetCountText() 
    {
        countText.text =  "Counter: " + count.ToString();
        if (count >= 12)
        {
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            winTextObject.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            count = count + 1;
            SetCountText();
            other.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
   if (collision.gameObject.CompareTag("Enemy"))
   {
       // Destroy the current object
       Destroy(gameObject); 
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
   }
   }

    // Update is called once per frame
    void Update()
    {
        
    }
}
