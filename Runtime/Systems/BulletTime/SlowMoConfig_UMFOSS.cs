using UnityEngine;

namespace GameplayMechanicsUMFOSS.Systems
{
    [CreateAssetMenu(fileName = "NewSlowMoConfig", menuName = "UMFOSS/Systems/SlowMoConfig")]
    public class SlowMoConfig_UMFOSS : ScriptableObject
    {
        [Header("Time Settings")]
        [Range(0.01f, 1f)] public float timeScale = 0.2f;
        public float enterDuration = 0.1f;
        public float exitDuration = 0.2f;
        public AnimationCurve transitionCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        [Header("Audio Settings")]
        [Range(0.1f, 1f)] public float audioPitchScale = 0.7f;

        [Header("Resource Settings")]
        public bool usesResource = false;
        public float resourceDrainRate = 20f; // Units per second
        public float resourceRechargeRate = 10f;
        public float maxResource = 100f;
    }
}