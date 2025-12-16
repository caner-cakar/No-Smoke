using UnityEngine;
using UnityEngine.UI;

public class XRayToggle : MonoBehaviour
{
    public Material baseTransparentMaterial;
    public float alpha = 0.3f;

    public Image iconImage;
    public Sprite openEyeSprite;
    public Sprite closedEyeSprite;

    Renderer[] renderers;
    Material[][] originalMaterials;
    Material[][] xrayMaterials;
    bool xray;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[renderers.Length][];
        xrayMaterials = new Material[renderers.Length][];

        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] mats = renderers[i].sharedMaterials;
            originalMaterials[i] = mats;
            xrayMaterials[i] = new Material[mats.Length];

            for (int j = 0; j < mats.Length; j++)
            {
                Material src = mats[j];
                if (src == null || baseTransparentMaterial == null)
                    continue;

                Material inst = new Material(baseTransparentMaterial);
                if (src.HasProperty("_Color"))
                {
                    Color c = src.color;
                    c.a = alpha;
                    inst.color = c;
                }

                xrayMaterials[i][j] = inst;
            }
        }

        UpdateIcon();
    }

    public void ToggleXRay()
    {
        xray = !xray;

        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i] == null)
                continue;

            if (xray)
                renderers[i].materials = xrayMaterials[i];
            else
                renderers[i].materials = originalMaterials[i];
        }

        UpdateIcon();
    }

    void UpdateIcon()
    {
        if (iconImage == null)
            return;

        if (xray && closedEyeSprite != null)
            iconImage.sprite = closedEyeSprite;
        else if (!xray && openEyeSprite != null)
            iconImage.sprite = openEyeSprite;
    }
}
