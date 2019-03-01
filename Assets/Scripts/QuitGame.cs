using UnityEngine;

public class QuitGame : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var inEditor = false;
#if UNITY_EDITOR
        inEditor = true;
#endif
        if(inEditor)
            UnityEditor.EditorApplication.isPlaying = false;
        else
            Application.Quit();
    }
}
