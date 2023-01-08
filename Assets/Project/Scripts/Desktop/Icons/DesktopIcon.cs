using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesktopIcon : MovableCanvasComponent
{
    private bool selected = false;
    public bool unlocked = false;
    public bool completed = false;
    private Color lockedColor = new Color(0.5f, 0.5f, 0.5f);
    private Color completedColor = new Color(0.6f,1.0f,0.8f);
    [SerializeField] private Material grayscaleMaterial;
    public virtual void SetLocked()
    {
        unlocked = false;
        GetComponent<Image>().color = Color.gray;
        GetComponent<Image>().material = grayscaleMaterial;
    }
    public virtual void SetUnlocked()
    {
        unlocked = true;
        GetComponent<Image>().color = Color.white;
        GetComponent<Image>().material = null;
    }
    public virtual void SetCompleted()
    {
        unlocked = true;
        completed = true;
        GetComponent<Image>().color = completedColor;
        GetComponent<Image>().material = null;
    }
    public override Vector2 GetPosition()
    {
        return transform.position;
    }
    public virtual void OnClick(BaseEventData data)
    {
        if (!unlocked) return;
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
        ToggleHighlight(true);
    }
    public virtual void Deselect()
    {
        selected = false;
        ToggleHighlight(false);
    }
    public virtual void ToggleHighlight(bool active)
    {
        var highlight = transform.Find("highlight");
        if (highlight != null)
        {
            highlight.gameObject.SetActive(active);
        }
    }
    public virtual void Drag(BaseEventData data)
    {
        DragMove(data, transform, transform.parent.GetComponent<Canvas>(), GameConstants.screenBottomLeftCorner, GameConstants.screenTopRightCorner);
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
