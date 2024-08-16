using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager_Bar_Up : MonoBehaviour
{
    public Image Button_Up;
    public Image Button_Down;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
        Button_Down.gameObject.SetActive(false);
    }

    public void clickButton_Up()
    {
       
        animator.SetBool("Click", true);
    }

    public void clickButton_Down()
    {

        animator.SetBool("Click", false);
    }
}
