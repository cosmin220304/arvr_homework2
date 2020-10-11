namespace VRTK.Examples
{
    using UnityEngine;

    public class ChalkHandler : MonoBehaviour
    {
        public GameObject lineRendererPrefab; 
        VRTK_InteractableObject linkedObject;
        LineRenderer lineRenderer; 
        bool drawing;


        protected virtual void Update()
        {
            if (drawing && lineRenderer != null)
            {
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
            }
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            drawing = true;
            GameObject lineRendererObject = Instantiate(lineRendererPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            lineRenderer = lineRendererObject.GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = true; 
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            drawing = false;
            lineRenderer = null;
        }
        
        protected virtual void OnEnable()
        {
            drawing = false;
            linkedObject = GetComponent<VRTK_InteractableObject>();
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused; 
        }

        protected virtual void OnDisable()
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }
}