using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Interruption : MonoBehaviour
{
    public Animator animator;
    protected string id = "invalid";
    public float closeTime = 0f;
    public virtual void Spawn()
    {
        animator = this.GetComponentInChildren<Animator>();
        animator.Play("open");
    }
    public virtual void Close() {
        StartCoroutine(CloseThread(closeTime));
    }
    protected IEnumerator CloseThread(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
    public void SetPosition(Vector3 newPos)
    {
        GetComponentInChildren<Image>().transform.localPosition += newPos;
    }
}
