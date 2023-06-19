using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private Transform t1;
    [SerializeField] private Transform t2;

    private Vector3 targetPos;


    private void Start()
    {
        targetPos = t1.position;
        targetPos.y = 0;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            if(targetPos == t1.position)
                targetPos = t2.position;
            else
               targetPos = t1.position;
        }

        targetPos.y = 0.0f;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 5.0f);
    }
}