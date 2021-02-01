using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{

    public BoxCollider2D boxCol;
    public Behaviour canvas;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = false;
        boxCol.enabled = true;
        index = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController play = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (play.doorKey) 
        {
            boxCol.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            canvas.transform.GetChild(3).GetComponent<TextMeshProUGUI>().enabled = true;
            Invoke("loadscene", 2);
            
        }
    }
    void loadscene() 
    {
        if (index!=3)
        {
            SceneManager.LoadScene(index + 1);
        }    
        else
        {
            SceneManager.LoadScene(0);
        }
    }

}
