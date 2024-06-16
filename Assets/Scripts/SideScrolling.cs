using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private Transform player;

    public float FloorHeigt = 7;

    public float UndergroundHeigt = -9;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(player.position.x, cameraPosition.x);
        transform.position = cameraPosition;
    }

    public void SetUnderground(bool state)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = state ? UndergroundHeigt : FloorHeigt;
        transform.position = cameraPosition;
    }
}
