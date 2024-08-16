using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    private Animator animator_Tree;
    private Tree tree;

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
            StartCoroutine(PlayFallingAnimation());
        }
    }

    private IEnumerator PlayFallingAnimation()
    {
        animator_Tree.SetBool("Fall", true);
        yield return new WaitForSeconds(2f);
        animator_Tree.SetBool("Fall", false);
        animator_Tree.enabled = false;

        // Dopo l'animazione, attiva la funzione DestroyTree nella classe Tree
        tree.DestroyTree();
    }
}
