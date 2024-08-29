
using System;
using FriedSynapse.FlowEnt;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenBehaviour : MonoBehaviour
{
    [SerializeField] 
    private string nextScene;

    private void Awake()
    {
        // NOTE:Tween initialization trick & a bit of delay aesthetics
        new Tween().OnCompleted(LoadNext).Start();
    }

    private void LoadNext()
    {
        SceneManager.LoadScene(nextScene);
    }

}
