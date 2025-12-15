using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PartDescription
{
    public string objectName;
    public string displayName;
    public string description;
}

public class PartInfoPanel : MonoBehaviour
{
    public RectTransform panel;
    public Canvas canvas;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TouchSelectionManager selectionManager;
    public PartDescription[] descriptions;
    public Vector2 offset = new Vector2(40f, -40f);

    Transform current;

    void Awake()
    {
        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();
        if (panel != null)
            panel.gameObject.SetActive(false);
    }

    public void Show(Transform target, Vector2 screenPos)
    {
        current = target;

        if (panel != null)
            panel.gameObject.SetActive(true);

        if (titleText != null)
            titleText.text = GetDisplayName(target.name);

        if (descriptionText != null)
            descriptionText.text = GetDescription(target.name);

        SetPosition(screenPos);
    }

    public void Hide()
    {
        current = null;
        if (panel != null)
            panel.gameObject.SetActive(false);
    }

    public void OnHideButton()
    {
        if (selectionManager != null)
            selectionManager.HideCurrent();
        else if (current != null)
            current.gameObject.SetActive(false);

        Hide();
    }

    string GetDisplayName(string name)
    {
        if (descriptions == null)
            return name;

        for (int i = 0; i < descriptions.Length; i++)
        {
            if (descriptions[i].objectName == name)
            {
                if (!string.IsNullOrEmpty(descriptions[i].displayName))
                    return descriptions[i].displayName;
                return name;
            }
        }

        return name;
    }

    string GetDescription(string name)
    {
        if (descriptions == null)
            return "";

        for (int i = 0; i < descriptions.Length; i++)
        {
            if (descriptions[i].objectName == name)
                return descriptions[i].description;
        }

        return "";
    }

    void SetPosition(Vector2 screenPos)
    {
        if (panel == null || canvas == null)
            return;

        RectTransform canvasRect = canvas.transform as RectTransform;
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            null,
            out localPos
        );

        Vector2 pos = localPos + offset;

        float canvasHalfW = canvasRect.rect.width * 0.5f;
        float canvasHalfH = canvasRect.rect.height * 0.5f;

        float panelHalfW = panel.rect.width * 0.5f;
        float panelHalfH = panel.rect.height * 0.5f;

        float minX = -canvasHalfW + panelHalfW;
        float maxX =  canvasHalfW - panelHalfW;
        float minY = -canvasHalfH + panelHalfH;
        float maxY =  canvasHalfH - panelHalfH;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        panel.anchoredPosition = pos;
    }
}
