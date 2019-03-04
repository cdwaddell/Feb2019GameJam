using UnityEngine;

public class QuitGame : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var inEditor = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        inEditor = true;
#endif
        if(!inEditor)
            Application.Quit();
    }
}
