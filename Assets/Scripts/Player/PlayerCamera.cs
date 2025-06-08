using System;
using TMPro;
using Unity.Hierarchy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    public GameObject target = null;
    private GameObject g;
    [SerializeField] Canvas talk_canvas;
    public Type targetType;
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
                if (g != null)
                {
                    Destroy(g);
                }
                g = new GameObject("target", typeof(PositionConstraint));
                g.GetComponent<PositionConstraint>().AddSource(new ConstraintSource()
                {
                    sourceTransform = target.transform,
                    weight = 1
                });
                g.GetComponent<PositionConstraint>().constraintActive = true;
                Canvas c = Instantiate(talk_canvas, g.transform);
                c.transform.localPosition = new Vector3(0, 2.2f, 0);
                LookAtConstraint l = c.AddComponent<LookAtConstraint>();
                ConstraintSource cs = new ConstraintSource();
                cs.sourceTransform = cam.transform;
                cs.weight = 1;
                l.AddSource(cs);
                l.constraintActive = true;
                c.GetComponentInChildren<TextMeshProUGUI>().text = "Talk E\n\n" + e.displayName;
                targetType = typeof(Entity);
            }
            else if (hit.collider.gameObject.TryGetComponent<Collectable>(out Collectable col))
            {
                if (g != null)
                {
                    Destroy(g);
                }
                target = hit.collider.gameObject;
                g = new GameObject("target", typeof(PositionConstraint));
                g.transform.position = target.transform.position;
                g.GetComponent<PositionConstraint>().AddSource(new ConstraintSource()
                {
                    sourceTransform = target.transform,
                    weight = 1
                });
                g.GetComponent<PositionConstraint>().constraintActive = true;
                Canvas c = Instantiate(talk_canvas, g.transform);
                c.transform.localPosition = new Vector3(0, target.transform.localScale.y/1.75f, 0);
                LookAtConstraint l = c.AddComponent<LookAtConstraint>();
                ConstraintSource cs = new ConstraintSource();
                cs.sourceTransform = cam.transform;
                cs.weight = 1;
                l.AddSource(cs);
                l.constraintActive = true;
                c.GetComponentInChildren<TextMeshProUGUI>().text = "Collect E\n\n" + col.GetItem().displayName;
                targetType = typeof(Collectable);
            }

            else if (target != null)
            {
                Destroy(g);
                target = null;
            }
        }
        else
        {
            if (target != null)
            {
                Destroy(g);
                target = null;
            }
        }
    }
}
