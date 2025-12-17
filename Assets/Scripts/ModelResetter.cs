using UnityEngine;

public class ModelResetter : MonoBehaviour
{
    [System.Serializable]
    struct TransformState
    {
        public Transform transform;
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;
        public bool activeSelf;
    }

    public Transform root;
    public TouchSelectionManager selectionManager;

    TransformState[] states;

    void Awake()
    {
        if (root == null)
            root = transform;

        Transform[] all = root.GetComponentsInChildren<Transform>(true);
        states = new TransformState[all.Length];

        for (int i = 0; i < all.Length; i++)
        {
            Transform t = all[i];
            states[i] = new TransformState
            {
                transform = t,
                localPosition = t.localPosition,
                localRotation = t.localRotation,
                localScale = t.localScale,
                activeSelf = t.gameObject.activeSelf
            };
        }
    }

    public void ResetModel()
    {
        if (selectionManager != null)
            selectionManager.ClearSelection();

        if (states == null)
            return;

        for (int i = 0; i < states.Length; i++)
        {
            TransformState s = states[i];
            if (s.transform == null)
                continue;

            s.transform.localPosition = s.localPosition;
            s.transform.localRotation = s.localRotation;
            s.transform.localScale = s.localScale;
            s.transform.gameObject.SetActive(s.activeSelf);
        }
    }
}
