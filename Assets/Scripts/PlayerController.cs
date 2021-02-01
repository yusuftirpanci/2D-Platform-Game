using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool doubleJump;
    public bool doubleJump_anim;
    public bool attack;
    
    //Atılabilecek muz sayısı
    public int attackBanana_Count;

    

    public Behaviour canvas;
   
   
    public Image key;
    
    public bool doorKey;


    public Transform mermi;
    public Transform crosshair;


    public Animator anim;
    public bool isGrounded; 
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;
    public float localScale;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        key.enabled = false;
        doorKey = false;
        canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = false;
        canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
        localScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        //Karakter Yere değiyor mu *
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
        
        //Karakteri sağa sola hareket ettirdiğimiz kısım*
        float move_input = Input.GetAxis("Horizontal");

        Vector2 velocity = new Vector2(move_input,0);
        //Karakterin hızlanmasını sağlayan kısım*
        //rb.velocity = new Vector2(move_input*speed,rb.velocity.y);
        rb.transform.Translate(velocity*speed*Time.deltaTime);
        
        
        //Animasyonların atandığı kısım*
        anim.SetFloat("Speed", Mathf.Abs(move_input));
        // anim.SetBool("Grounded", isGrounded);
        anim.SetBool("DoubleJump", doubleJump_anim);
        anim.SetBool("Attack", attack);
        

        
        canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = attackBanana_Count.ToString();


        //Karakter yerde  ise doublejump yapamasın.
        if (isGrounded) 
        {
            doubleJump_anim = false; 
        }

        //Zıplamayı space tuşuna atadık ve havadaysa doublejump yapabildi.
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
                doubleJump = true;
                
            }
            else 
            {
                if (doubleJump) 
                {
                    doubleJump = false;
                    rb.velocity = Vector2.up * jumpForce;     
                    doubleJump_anim = true;
                }
            }
        }

        //Muza değdimizde mermi artacak.
        
        if(Input.GetKeyDown(KeyCode.X) && attackBanana_Count > 0)        
        { 
            
            Transform tempbullet;
            tempbullet=Instantiate(mermi,crosshair.position, Quaternion.identity);
            tempbullet.GetComponent<Rigidbody2D>().AddForce(crosshair.forward* -1000);
            attackBanana_Count--;
            
        }


        //Karakter yönünü değiştirdiğinde vücudu da döndü
        if (move_input > 0.1) 
        {       
            transform.localScale = new Vector2(-localScale, transform.localScale.y);
        }

        if (move_input < -0.1) 
        {
            transform.localScale = new Vector2(localScale, transform.localScale.y);
        }

    }

    //Yerden muz aldığında muzun yok olması ve sayaçta artması birde bölümü geçmek için gereken muz imajının gözükmesi.
    void OnTriggerEnter2D(Collider2D other) 
    {
        

        if (other.CompareTag("banana")) 
        {
            
            Destroy(other.gameObject);
           
            key.enabled = true;
            doorKey = true;

        }

        if (other.CompareTag("key"))
        {
            Destroy(other.gameObject);
            
        }

        if (other.CompareTag("attackBanana")) 
        {
            attackBanana_Count += 20;
            canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().enabled = true;
            canvas.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = attackBanana_Count.ToString();

            Destroy(other.gameObject,0.1f);


        }
        if (other.CompareTag("water")) 
        {
            Invoke("loadscene", 0.5f);
        }    
    }
    void loadscene() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(index);

    }
    

}
