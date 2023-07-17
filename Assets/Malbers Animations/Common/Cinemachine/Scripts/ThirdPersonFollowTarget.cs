using Cinemachine;
using MalbersAnimations.Scriptables; 
using UnityEngine;

namespace MalbersAnimations
{
    [AddComponentMenu("Malbers/Camera/Third Person Follow Target (Cinemachine)")]
    public class ThirdPersonFollowTarget : MonoBehaviour
    {
        [Tooltip("What object to follow")]
        public TransformReference Target;
      
        [SerializeField,HideInInspector]
        public Transform Pivot;
 
        [Tooltip("Camera Input Values (Look X:Horizontal, Look Y: Vertical)")]
        public Vector2Reference look = new();


        [Tooltip("Invert X Axis of the Look Vector")]
        public BoolReference invertX = new();
        [Tooltip("Invert Y Axis of the Look Vector")]
        public BoolReference invertY = new();

        [Tooltip("Multiplier to rotate the X Axis")]
        public FloatReference XMultiplier = new(1);
        [Tooltip("Multiplier to rotate the Y Axis")]
        public FloatReference YMultiplier = new(1);

        [Tooltip("How far in degrees can you move the camera up")]
        public FloatReference TopClamp = new(70.0f);

        [Tooltip("How far in degrees can you move the camera down")]
        public FloatReference BottomClamp = new(-30.0f);

        private float InvertX => invertX.Value ? -1 : 1;
        private float InvertY => invertY.Value ? 1 : -1;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;
        private const float _threshold = 0.00001f;
    

        // Start is called before the first frame update
        void Awake()
        {
            if (Pivot == null)
            {
                Pivot = new GameObject("Pivot").transform;
                Pivot.parent = transform;
                Pivot.ResetLocal();
            }

            var Cam = GetComponent<CinemachineVirtualCamera>(); 
            if (Cam)
                Cam.Follow = Pivot.transform;

            //if (Target == null || Target.Value == null)
            //{
            //    Debug.LogWarning("There's no Target set on Third Person Follow Target. Disabling Script",this);
            //enabled = false;
            //}
        }


        private void FixedUpdate()
        {
            if (Target.Value)
            {
                Pivot.transform.position = Target.position;
                // Pivot.transform.rotation = Target.rotation;
            }
            CameraRotation();
        }

        public void SetLookX(float x) => look.x = x;
        public void SetLookY(float y) => look.y = y;
        public void SetLook(Vector2 look) => this.look.Value = look;
        


        private void CameraRotation()
        {
             
            // if there is an input and camera position is not fixed
            if (look.Value.sqrMagnitude >= _threshold)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = 1;// Time.deltaTime;

                _cinemachineTargetYaw += look.x * deltaTimeMultiplier * InvertX * XMultiplier;
                _cinemachineTargetPitch += look.y * deltaTimeMultiplier * InvertY * YMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            Pivot.rotation = Quaternion.Euler(_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
        }

        public void SetTarget(Transform target) => Target.Value = target;

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

#if UNITY_EDITOR
        private void Reset()
        {
            Target.UseConstant = false;
            Target.Variable = MTools.GetInstance<TransformVar>("Camera Target");


            if (Pivot == null)
            {
                Pivot = new GameObject("Pivot").transform;
                Pivot.parent = transform;
                Pivot.ResetLocal();
            }
        }
#endif
    }
}
