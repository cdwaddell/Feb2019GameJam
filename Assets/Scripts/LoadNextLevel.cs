using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene("StoreScene");
    }
    
}
