using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private Animator animator_Tree;
    private Tree tree;
    public AxeController AxeController;
    private void Start()
    {
        animator_Tree = GetComponent<Animator>();
        tree = GetComponent<Tree>();
        animator_Tree.enabled = false; // Disabilita l'animatore inizialmente
    }

    public void Update()
    {
        if (tree.currentStage >= 5)
        {
            animator_Tree.enabled = true;
        }
    }

    public void StartFallingAnimation()
    {
        if (animator_Tree.enabled)
        {
            StartCoroutine(PlayFallingLeftAnimation());
        }
    }

    private IEnumerator PlayFallingLeftAnimation()
    {
        animator_Tree.SetBool("Fall_Left", true);
        yield return new WaitForSeconds(2f);
        animator_Tree.SetBool("Fall_Left", false);
        animator_Tree.enabled = false;

        // Dopo l'animazione, attiva la funzione DestroyTree nella classe Tree
        tree.DestroyTree();
    }
}
