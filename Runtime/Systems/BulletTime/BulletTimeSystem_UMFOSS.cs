using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using GameplayMechanicsUMFOSS.Core; // For MonoSingletonGeneric

namespace GameplayMechanicsUMFOSS.Systems
{
    public class BulletTimeSystem_UMFOSS : MonoSingletonGeneric<BulletTimeSystem_UMFOSS>
    {
        [Header("Setup")]
        [SerializeField] private AudioMixer gameMixer;
        [SerializeField] private string pitchParameter = "MasterPitch";
        
        private SlowMoConfig_UMFOSS activeConfig;
        private Coroutine transitionCoroutine;
        private float currentResource;
        private bool isActive;

        private void Start()
        {
            // Initialize resource if needed
            currentResource = 100f; 
        }

        public void Enter(SlowMoConfig_UMFOSS config)
        {
            if (isActive || config == null) return;
            
            activeConfig = config;
            isActive = true;
            StopActiveTransition();
            transitionCoroutine = StartCoroutine(Transition(activeConfig.timeScale, activeConfig.audioPitchScale, activeConfig.enterDuration));
        }

        public void Exit()
        {
            
            
            isActive = false;
            StopActiveTransition();
            transitionCoroutine = StartCoroutine(Transition(1f, 1f, activeConfig.exitDuration));
        }

        private IEnumerator Transition(float targetTime, float targetPitch, float duration)
        {
            float startTimeScale = Time.timeScale;
            gameMixer.GetFloat(pitchParameter, out float startPitch);
            float elapsed = 0;

            while (elapsed < duration)
            {
                // We MUST use unscaledDeltaTime so the transition speed is constant
                elapsed += Time.unscaledDeltaTime;
                float t = elapsed / duration;
                float evaluatedT = activeConfig.transitionCurve.Evaluate(t);

                // Update TimeScale
                Time.timeScale = Mathf.Lerp(startTimeScale, targetTime, evaluatedT);
                
                // CRITICAL: Keep physics simulation consistent
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                // Update Audio Pitch
                gameMixer.SetFloat(pitchParameter, Mathf.Lerp(startPitch, targetPitch, evaluatedT));

                yield return null;
            }

            Time.timeScale = targetTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            gameMixer.SetFloat(pitchParameter, targetPitch);
        }

        private void StopActiveTransition()
        {
            if (transitionCoroutine != null) StopCoroutine(transitionCoroutine);
        }

        private void Update()
        {
            // Simple Resource Logic
            if (isActive && activeConfig != null && activeConfig.usesResource)
            {
                currentResource -= activeConfig.resourceDrainRate * Time.unscaledDeltaTime;
                if (currentResource <= 0) Exit();
            }
            else if (!isActive && currentResource < 100f)
            {
                currentResource += 10.0f * Time.unscaledDeltaTime; // Default recharge
            }
        }
    }
}