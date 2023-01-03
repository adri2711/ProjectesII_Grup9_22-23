using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesktopIcon : MovableCanvasComponent
{
    bool selected;
    public override Vector2 GetPosition()
    {
        return transform.position;
    }
    public virtual void OnClick(BaseEventData data)
    {
        if (selected)
        {
            ClickSelected();
            return;
        }
        Select();
    }
    protected virtual void ClickSelected()
    {
        Deselect();
    }
    public virtual void Select()
    {
        selected = true;
    }
    public virtual void Deselect()
    {
        selected = false;
    }
    public virtual void Drag(BaseEventData data)
    {
        DragMove(data, transform, transform.parent.GetComponent<Canvas>());
    }
    public override void DragStart(BaseEventData data)
    {
        base.DragStart(data);
        Deselect();
    }
    public override void DragEnd(BaseEventData data)
    {
        base.DragEnd(data);
        Deselect();
    }
}
