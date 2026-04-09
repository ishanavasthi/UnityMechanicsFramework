using UnityEngine;
using GameplayMechanicsUMFOSS.Systems;

public class BulletTimeTester : MonoBehaviour
{
    [SerializeField] private SlowMoConfig_UMFOSS myConfig;

    void Update()
    {
        // Press T to start Bullet Time
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (BulletTimeSystem_UMFOSS.Instance != null)
            {
                BulletTimeSystem_UMFOSS.Instance.Enter(myConfig);
                Debug.Log("Entering Bullet Time!");
            }
        }

        // Press Y to stop Bullet Time
        if (Input.GetKeyDown(KeyCode.Y))
        {
            if (BulletTimeSystem_UMFOSS.Instance != null)
            {
                BulletTimeSystem_UMFOSS.Instance.Exit();
                Debug.Log("Exiting Bullet Time!");
            }
        }

        if (Input.GetKeyDown(KeyCode.R)) // Press R to Reset
{
    // Reset Cube Position
    transform.position = new Vector3(0, 10, 0);
    // Reset Time
    BulletTimeSystem_UMFOSS.Instance.Exit();
    Debug.Log("System Reset");
}
    }
}