using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    public GameObject target = null;
    [SerializeField] Canvas talk_canvas;
    void Update()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 15, ~(1<<3)))
        {
            if (hit.collider.gameObject == target)
            {
                return;
            }
            if (hit.collider.gameObject.TryGetComponent<Entity>(out Entity e)) 
            {
                target = hit.collider.gameObject;
                Canvas c = Instantiate(talk_canvas, target.transform);
                c.transform.localPosition = new Vector3(0, 1.2f, 0);
                LookAtConstraint l = c.AddComponent<LookAtConstraint>();
                ConstraintSource cs = new ConstraintSource();
                cs.sourceTransform = cam.transform;
                cs.weight = 1;
                l.AddSource(cs);
                l.constraintActive = true;
                c.GetComponentInChildren<TextMeshProUGUI>().text = "Talk E\n\n" + e.displayName;
            }
            else if (target != null)
            {
                Destroy(target.GetComponentInChildren<Canvas>().gameObject);
                target = null;
            }
        }
        else
        {
            if (target != null)
            {
                Destroy(target.GetComponentInChildren<Canvas>().gameObject);
                target = null;
            }
        }
    }
}
