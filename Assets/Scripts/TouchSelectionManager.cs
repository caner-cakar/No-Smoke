using UnityEngine;
using UnityEngine.EventSystems;

public class TouchSelectionManager : MonoBehaviour
{
    public Camera worldCamera;
    public Transform selectableRoot;
    public Color highlightColor = Color.yellow;
    public PartInfoPanel infoPanel;
    public GameObject hideButtonRoot;

    Transform current;
    Renderer[] currentRenderers;
    Color[][] originalColors;

    void Awake()
    {
        if (worldCamera == null)
            worldCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
                TrySelect(t.position);
        }

#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
            TrySelect(Input.mousePosition);
#endif
    }

    void TrySelect(Vector2 screenPos)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            GameObject go = EventSystem.current.currentSelectedGameObject;

            bool isHideButton = false;
            if (hideButtonRoot != null && go != null)
            {
                if (go == hideButtonRoot || go.transform.IsChildOf(hideButtonRoot.transform))
                    isHideButton = true;
            }

            if (!isHideButton && infoPanel != null)
                infoPanel.Hide();

            return;
        }

        Ray ray = worldCamera.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit, 1000f))
        {
            ClearSelection();
            return;
        }

        Transform target = hit.transform;

        if (selectableRoot != null && target != selectableRoot && !target.IsChildOf(selectableRoot))
        {
            ClearSelection();
            return;
        }

        if (target == current)
        {
            if (infoPanel != null && current != null)
                infoPanel.Show(current, screenPos);
            return;
        }

        ClearSelection();

        current = target;

        currentRenderers = current.GetComponentsInChildren<Renderer>();
        originalColors = new Color[currentRenderers.Length][];

        for (int i = 0; i < currentRenderers.Length; i++)
        {
            if (currentRenderers[i] == null)
                continue;

            Material[] mats = currentRenderers[i].materials;
            originalColors[i] = new Color[mats.Length];

            for (int j = 0; j < mats.Length; j++)
            {
                if (mats[j] != null && mats[j].HasProperty("_Color"))
                {
                    originalColors[i][j] = mats[j].color;
                    mats[j].color = highlightColor;
                }
            }
        }

        if (infoPanel != null)
            infoPanel.Show(current, screenPos);
    }

    public void HideCurrent()
    {
        if (current != null)
            current.gameObject.SetActive(false);
    }

    public void ClearSelection()
    {
        if (currentRenderers != null && originalColors != null)
        {
            for (int i = 0; i < currentRenderers.Length; i++)
            {
                if (currentRenderers[i] == null || originalColors[i] == null)
                    continue;

                Material[] mats = currentRenderers[i].materials;
                int len = Mathf.Min(mats.Length, originalColors[i].Length);

                for (int j = 0; j < len; j++)
                {
                    if (mats[j] != null && mats[j].HasProperty("_Color"))
                        mats[j].color = originalColors[i][j];
                }
            }
        }

        current = null;
        currentRenderers = null;
        originalColors = null;

        if (infoPanel != null)
            infoPanel.Hide();
    }
}
