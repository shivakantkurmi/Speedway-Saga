using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class AndroidControl : MonoBehaviour
{
    [Header("Movement Settings")]
    public float forwardSpeed = 10f;
    public float horizontalSpeed = 5f;
    public float leftTurnSpeed = 3f;
    public float rightTurnSpeed = 5f;
    public float speedIncreaseRate = 0.1f;
    public float maxSpeed = 30f;

    private Vector2 moveInput;
    private AudioSource engineSound;

    [Header("Engine Sound Settings")]
    public AudioClip engineClip;
    public float minVolume = 0.2f;
    public float maxVolume = 1.0f;
    public float minPitch = 0.5f;
    public float maxPitch = 2.0f;

    private void Start()
    {
        EnhancedTouchSupport.Enable();
        
        // Initialize engine sound
        engineSound = gameObject.AddComponent<AudioSource>();
        engineSound.clip = engineClip;
        engineSound.loop = true;
        engineSound.playOnAwake = true;
        engineSound.volume = minVolume;
        engineSound.pitch = minPitch;
        engineSound.Play();
    }

    private void Update()
    {
        MoveForward();
        AdjustEngineSound();

        #if UNITY_EDITOR || UNITY_STANDALONE
        HandlePCInput();
        #endif

        HandleTouchInput();
    }

    private void MoveForward()
    {
        forwardSpeed = Mathf.Min(forwardSpeed + speedIncreaseRate * Time.deltaTime, maxSpeed);
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);
    }

    private void AdjustEngineSound()
    {
        if (engineSound != null)
        {
            if (Time.timeScale == 0)
            {
                engineSound.Pause();
            }
            else
            {
                if (!engineSound.isPlaying) engineSound.Play();

                float speedFactor = forwardSpeed / maxSpeed;
                engineSound.volume = Mathf.Lerp(minVolume, maxVolume, speedFactor);
                engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, speedFactor);
            }
        }
    }

    private void HandlePCInput()
    {
        float moveHorizontal = moveInput.x;
        float turnSpeed = moveHorizontal < 0 ? leftTurnSpeed : (moveHorizontal > 0 ? rightTurnSpeed : 0);
        transform.Translate(Vector3.right * moveHorizontal * turnSpeed * Time.deltaTime, Space.World);
    }

    private void HandleTouchInput()
    {
        if (Touchscreen.current == null) return;
        
        var activeTouches = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches;
        if (activeTouches.Count == 1)
        {
            var touch = activeTouches[0];

            if (touch.phase == UnityEngine.InputSystem.TouchPhase.Moved)
            {
                Vector2 touchDelta = touch.delta * 0.1f; // Scaled movement
                float moveHorizontal = touchDelta.x * horizontalSpeed;
                float turnSpeed = moveHorizontal < 0 ? leftTurnSpeed : (moveHorizontal > 0 ? rightTurnSpeed : 0);
                transform.Translate(Vector3.right * moveHorizontal * Time.deltaTime, Space.World);
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context) 
    { 
        moveInput = context.ReadValue<Vector2>(); 
    }
}
