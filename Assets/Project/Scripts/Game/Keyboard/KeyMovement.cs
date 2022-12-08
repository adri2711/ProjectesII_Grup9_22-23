using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyMovement : MonoBehaviour
{   
    public Vector2 rootPos;
    public bool inRoot = true;
    public bool held = false;
    public int detachCounter = 0;
    private int clicksToDetach = 4;

    private Key key;
    private Canvas canvas;
    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        key = GetComponent<Key>();
        inRoot = true;
        rootPos = transform.parent.localPosition;
    }
    public void AttachToRoot(int rootIndex)
    {
        key.rootIndex = rootIndex;
        transform.parent.localPosition = rootPos;
        inRoot = true;
    }
    public void DetachFromRoot()
    {
        held = true;
        inRoot = false;
        KeyboardRoots.keyRoots[key.rootIndex].DetachKey();
    }
    public void Drag(BaseEventData data)
    {
        if (!inRoot && held)
        {
            Vector2 position;
            PointerEventData pointerData = (PointerEventData)data;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                pointerData.position,
                GameObject.Find("Main Camera").GetComponent<Camera>(),
                out position
                );
            transform.parent.position = canvas.transform.TransformPoint(position);
        }
    }
    public void DragStart(BaseEventData data)
    {
        held = true;
    }
    public void DragEnd(BaseEventData data)
    {
        held = false;
    }
    public void RightClick(BaseEventData data)
    {
        if (inRoot)
        {
            detachCounter++;
            if (detachCounter > clicksToDetach)
            {
                DetachFromRoot();
                detachCounter = 0;
            }
        }
    }
}
