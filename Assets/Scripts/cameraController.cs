using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{

    
    public Transform player;
    public Vector3 offset;
    public float offsetX;





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraMovement();
    }

    void cameraMovement() 
    {
        if (player.localScale == new Vector3(-transform.localScale.x,transform.localScale.y,0))
        { 
            offset.x = Mathf.MoveTowards(offset.x,offsetX, 0.1f);
            
        }
        else if (player.localScale == new Vector3(transform.localScale.x, transform.localScale.y, 0))
        {
            offset.x = Mathf.MoveTowards(offset.x,-offsetX,0.1f);
            
        }
        this.transform.position = offset + player.position;
    }

}
