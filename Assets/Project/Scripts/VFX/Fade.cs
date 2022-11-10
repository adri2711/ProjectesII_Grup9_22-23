using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade instance { get; private set; }
    private Animator animator;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void FadeIn()
    {
        animator.Play("fade_in");
    }
    public void FadeOut()
    {
        animator.Play("fade_out");
    }
    public void FadeOutIn()
    {
        animator.Play("fade_out_in");
    }
}
