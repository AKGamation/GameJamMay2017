using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(AudioSource))]
    public class FirstPersonController1 : MonoBehaviour
    {
        [SerializeField]
        private bool m_IsWalking;
        [SerializeField]
        private float m_WalkSpeed;
        [SerializeField]
        private float m_RunSpeed;
        [SerializeField]
        [Range(0f, 1f)]
        private float m_RunstepLenghten;
        [SerializeField]
        private float m_JumpSpeed;
        [SerializeField]
        private float m_StickToGroundForce;
        [SerializeField]
        private float m_GravityMultiplier;
        [SerializeField]
        private MouseLook m_MouseLook;
        [SerializeField]
        private bool m_UseFovKick;
        [SerializeField]
        private FOVKick m_FovKick = new FOVKick();
        [SerializeField]
        private bool m_UseHeadBob;
        [SerializeField]
        private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField]
        private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField]
        private float m_StepInterval;
        [SerializeField]
        private AudioClip[] m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField]
        private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField]
        private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

        public bool GravityOff;

        private float verticalDir;
        private Vector2 minmax;
        private Vector3 desiredMove;
        private Vector3 prevVelocity;

        private float horizontal;
        private float vertical;

        private float dirHorizontal;
        private float dirForward;

        private Vector2 horiClamp;
        private Vector2 forwardClamp;

        public Vector3 slideForce;
        private Vector3 slideRightForce;
        private Vector3 slideForwardForce;

        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle / 2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
            m_MouseLook.Init(transform, m_Camera.transform);

            GravityOff = false;
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            // if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButton("Jump");
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                GravityOff = !GravityOff;

                if (GravityOff)
                {
                    m_GravityMultiplier = 0;
                }
                else
                {
                    m_GravityMultiplier = 2;
                }
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(m_JumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = m_LandSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at

            desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            if (m_Input != Vector2.zero || !GravityOff)
            {
                m_MoveDir.x = desiredMove.x * (speed);
                m_MoveDir.z = desiredMove.z * (speed);
                SetSlowDownDirection();
            }
            else if (GravityOff)
            {
                m_MoveDir.x = Mathf.Abs(m_MoveDir.x) - Time.deltaTime;
                m_MoveDir.x = Mathf.Clamp(m_MoveDir.x, 0, m_WalkSpeed) * dirHorizontal;

                m_MoveDir.z = Mathf.Abs(m_MoveDir.z) - Time.deltaTime;
                m_MoveDir.z = Mathf.Clamp(m_MoveDir.z, 0, m_WalkSpeed) * dirForward;
            }

            if (m_CharacterController.isGrounded && !GravityOff)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    m_MoveDir.y = m_JumpSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else if (GravityOff)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    minmax.x = -m_JumpSpeed / 3.0f;
                    minmax.y = 0;
                    m_MoveDir.y = minmax.x;
                    verticalDir = 1;
                }
                if (m_Jump)
                {
                    minmax.x = 0;
                    minmax.y = m_JumpSpeed / 3.0f;
                    m_MoveDir.y = minmax.y;
                    verticalDir = -1;
                }

                m_MoveDir.y += Time.deltaTime * verticalDir;

                m_MoveDir.y = Mathf.Clamp(m_MoveDir.y, minmax.x, minmax.y);
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            prevVelocity = m_MoveDir;

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }

        private void SetSlowDownDirection()
        {
            if (desiredMove.x > 0)
            {
                dirHorizontal = 1;
            }
            else if (desiredMove.x < 0)
            {
                dirHorizontal = -1;
            }

            if (desiredMove.z > 0)
            {
                dirForward = 1;
            }
            else if (desiredMove.z < 0)
            {
                dirForward = -1;
            }
        }

        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed * (m_IsWalking ? 1f : m_RunstepLenghten))) *
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            m_FootstepSounds[n] = m_FootstepSounds[0];
            m_FootstepSounds[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!m_UseHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            vertical = CrossPlatformInputManager.GetAxis("Vertical");

            if (horizontal > 0)
            {
               // dirHorizontal = 1;
                horiClamp = new Vector2(0, m_WalkSpeed);
            }
            else if (horizontal < 0)
            {
                //dirHorizontal = -1;
                horiClamp = new Vector2(-m_WalkSpeed, 0);
            }

            if (vertical > 0)
            {
                //dirForward = 1;
                forwardClamp = new Vector2(0, m_WalkSpeed);
            }
            else if (vertical < 0)
            {
                //dirForward = -1;
                forwardClamp = new Vector2(-m_WalkSpeed, 0);
            }

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(KeyCode.LeftShift);
#endif

            if (GravityOff)
            {
                m_IsWalking = true;
            }

            // set the desired speed to be walking or running
            speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;

            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation(transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity * 0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
