using UnityEngine;

public class followtarget : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private Vector3 Offset;

    private void Update()
    {
        transform.position = Target.position + Offset;
    }
    private void Start()
    {
        Target = GameObject.Find("Main Camera").GetComponent<Transform>();
    }
}