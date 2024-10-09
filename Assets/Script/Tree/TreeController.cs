using System.Collections;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public AxeController AxeController;
    public Move_Player MovePlayer;
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
        if (tree.currentStage >= 4)
        {
            animator_Tree.enabled = true;
        }
    }

    public void StartFallingAnimation()
    {
        if (animator_Tree.enabled && MovePlayer.lastHorizontal == -1)
        {
            StartCoroutine(PlayFalling_LeftAnimation());
        }
        else if(animator_Tree.enabled && MovePlayer.lastHorizontal == 1)
        {
            StartCoroutine(PlayFalling_RightAnimation());
        }
    }

    public void StartFalling_RightAnimation()
    {
        if (animator_Tree.enabled)
        {
            StartCoroutine(PlayFalling_RightAnimation());
        }
    }

    private IEnumerator PlayFalling_LeftAnimation()
    {
        animator_Tree.SetBool("Fall_Left", true);
        yield return new WaitForSeconds(2f);
        animator_Tree.SetBool("Fall_Left", false);
        animator_Tree.enabled = false;

        // Dopo l'animazione, attiva la funzione DestroyTree nella classe Tree
        tree.DestroyTree();
    }

    private IEnumerator PlayFalling_RightAnimation()
    {
        animator_Tree.SetBool("Fall_Right", true);
        yield return new WaitForSeconds(2f);
        animator_Tree.SetBool("Fall_Right", false);
        animator_Tree.enabled = false;

        // Dopo l'animazione, attiva la funzione DestroyTree nella classe Tree
        tree.DestroyTree();
    }
}
