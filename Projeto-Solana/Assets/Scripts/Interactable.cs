using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isMouseOn;
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    public GameObject target;
    public Renderer targetRenderer;

    
    

    void Start()
    {
         target.GetComponent<changecolorbox>();
    }


    void Update()
    {
        //se o player esta no range e clicar com o botao do mouse, chama a acao atribuida (acao por fora/changecolor)
        if (isInRange && isMouseOn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                interactAction.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //se o player estiver perto isRange = true;
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player now in Range");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            Debug.Log("Player now not in range");
        }
    }
    private void OnMouseEnter()
    {
        isMouseOn = true;
        if (isInRange)
        {
            
            target.GetComponent<changecolorbox>().x=2;
            
        }

    }
    private void OnMouseExit()
    {
        isMouseOn=false;
        target.GetComponent<changecolorbox>().x=0;
        
    }

}
