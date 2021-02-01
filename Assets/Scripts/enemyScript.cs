using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public int health;
    public float localScale;

    public GameObject effect;
    public GameObject banana;
    bool isTrigger=false;
    public Transform player;
    public float speed;
   

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        followCharacter();
        if (health <= 0)
        { 
            Destroy(this.gameObject);
            Instantiate(effect, transform.position,Quaternion.identity);
            Instantiate(banana, transform.position, Quaternion.identity);
        }        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {

        
        if (other.tag=="Player")
        { 
            isTrigger = true;
        }     
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag=="mermi")
        { 
            health -= 20;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag=="Player")
        {
            isTrigger = false;
        }
    }


    public void followCharacter() 
    {

        if (isTrigger)
        {
            if (player.position.x>this.transform.position.x)
            {
                this.transform.Translate(new Vector2(1*speed*Time.deltaTime,0));
                transform.localScale = new Vector2(localScale, transform.localScale.y);

            }
            else 
            {
                this.transform.Translate(new Vector2(-1* speed * Time.deltaTime, 0));
                transform.localScale = new Vector2(-localScale, transform.localScale.y);
            }
           
        }
    }

}
