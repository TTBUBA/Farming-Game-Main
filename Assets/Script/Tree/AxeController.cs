using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public Animator animator_Axe;
    public GameObject Axe;
    public Renderer axeRenderer;
    private void Start()
    {
        animator_Axe = GetComponent<Animator>();
        axeRenderer = Axe.GetComponent<Renderer>(); 
        axeRenderer.enabled = false; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.CompareTag("Tree"))
        {
            axeRenderer.enabled = true; 
            
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            axeRenderer.enabled = false; 
            
        }
    }
    public void ButtonDestroyTree(TreeController treeController)
    {
        animator_Axe.SetBool("AxeAttivo", true);
        StartCoroutine(AnimatorFalse(treeController));
    }

    private IEnumerator AnimatorFalse(TreeController treeController)
    {
        yield return new WaitForSeconds(0.3f);

        // Attiva l'animazione di caduta sull'albero specifico
        treeController.StartFallingAnimation();

        yield return new WaitForSeconds(2f);
        animator_Axe.SetBool("AxeAttivo", false);
    }
}
