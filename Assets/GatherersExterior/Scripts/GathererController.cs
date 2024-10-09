using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GatherersExterior
{
    public class GathererController : MonoBehaviour
    {
        [field: SerializeField, Tooltip("How much force to add to the player's movement when move keys are pressed down.")]
        public float WalkForce { get; private set; }

        [field: SerializeField, Header("Player Input")] public string SwingInputAction { get; private set; } = "Fire1";

        // Characters States are controlled through Change State Behaviours on the Gatherer's Animator.
        // When a animation state is entered, the character state is set on this controller which controls how
        // the character will be able to move depending on if the state is considered movable or not

        public enum GathererState
        {
            Idle,
            Walk,
            Swing
        }

        private GathererState _state = GathererState.Idle;

        public GathererState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                }
            }
        }

        [field: SerializeField] public SerializableDictionary<GathererState, AnimationSet4Dir> AnimationsMapping { get; private set; }
        public bool StateLocked { get; private set; } = false;

        private Rigidbody2D _rb;
        private Animator _animator;
        private Vector2 _facing;
        private bool _swingInput = false;
        private Vector2 _inputDir;
        private bool _hasInput;
        private AnimationClip _currentClip;

        // Start is called before the first frame update
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            // Handle input on Update
            GetInput();

            // Automatic State Switching
            if (!StateLocked)
            {
                // Order by priority
                if (_swingInput)
                {
                    State = GathererState.Swing;
                    StateLocked = true;
                    _swingInput = false;
                    AnimationClip currentClip = PlayDirectionAnimation(_facing);
                    StartCoroutine(UnlockStateAfterAnimation(currentClip.length));
                }
                else if (_hasInput)
                {
                    State = GathererState.Walk;
                }
                else
                {
                    State = GathererState.Idle;
                    PlayDirectionAnimation(_facing);
                }
            }
        }

        private void FixedUpdate()
        {
            // Input and calculations

            // _animator.SetBool("hasInput", hasInput);

            // Run code depending on current state
            switch (State)
            {
                case GathererState.Idle:
                    // Do nothing
                    break;

                case GathererState.Walk:
                    Walk(_inputDir);
                    break;

                case GathererState.Swing:
                    // Unlock state with AnimationEvent UnlockState
                    break;
            }
        }

        /// <summary>
        /// Handles necessary input for this character
        /// </summary>
        private void GetInput()
        {
            _inputDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _hasInput = _inputDir.x != 0 || _inputDir.y != 0;

            // Be sure to consume triggers on update not on fixed update since this will change every frame
            _swingInput = Input.GetButtonDown(SwingInputAction);
        }

        public IEnumerator UnlockStateAfterAnimation(float animDuration)
        {
            yield return new WaitForSeconds(animDuration);

            StateLocked = false;
        }

        private void Walk(Vector2 inputDir)
        {
            PlayDirectionAnimation(inputDir);

            if (inputDir != Vector2.zero)
            {
                _rb.AddForce(WalkForce * inputDir);
                _facing = inputDir;
            }
        }

        /// <summary>
        /// Uses directions to select an animation clip from the current state
        /// </summary>
        /// <param name="inputDir">Direction to find the closest animation match for</param>
        /// <returns>The clip being played</returns>
        private AnimationClip PlayDirectionAnimation(Vector2 inputDir)
        {
            Vector2 closestDirection = GetClosestDirection(inputDir);
            AnimationClip nextClip = null;

            if (closestDirection == Vector2.left)
            {
                nextClip = AnimationsMapping[State].Left;
            }
            else if (closestDirection == Vector2.right)
            {
                nextClip = AnimationsMapping[State].Right;
            }
            else if (closestDirection == Vector2.up)
            {
                nextClip = AnimationsMapping[State].Up;
            }
            else if (closestDirection == Vector2.down)
            {
                nextClip = AnimationsMapping[State].Down;
            }
            else
            {
                Debug.LogError($"Closest Direction Value {closestDirection} does not map to Left, Right, Up, Down Vector2s");
            }

            Debug.Log(nextClip.name);

            if (_currentClip != null && nextClip != _currentClip)
            {
                _animator.Play(nextClip.name);
            }

            _currentClip = nextClip;
            return nextClip;
        }

        private Vector2 GetClosestDirection(Vector2 inputDir)
        {
            // Get closest point in direction list
            Vector2 normInput = inputDir.normalized;

            Vector2 closestDirection = Vector2.zero;
            float closestDistance = 0;

            // Use direction you want character to face by default first in the array
            // Check left right directions first so they are prioritized over up and down if x, y directional inputs have matching magnitudes
            Vector2[] directions = new Vector2[4] { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

            for (int i = 0; i < directions.Length; i++)
            {
                Vector2 directionVec = directions[i];

                // Default first one
                if (closestDirection == Vector2.zero)
                {
                    closestDirection = directionVec;
                    closestDistance = Vector2.Distance(normInput, directionVec);
                }
                else
                {
                    // Test for closer distance
                    float nextDistance = Vector2.Distance(normInput, directionVec);

                    bool nextIsCloser = nextDistance < closestDistance;
                    // Debug.Log($"Direction [{directionVec}] | {nextDistance} is closer than {closestDistance}?: " + nextIsCloser);

                    if (nextIsCloser)
                    {
                        // Found closer
                        closestDistance = nextDistance;
                        closestDirection = directionVec;
                    }
                }
            }

            // Debug.Log(closestDirection);
            return closestDirection;
        }
    }
}