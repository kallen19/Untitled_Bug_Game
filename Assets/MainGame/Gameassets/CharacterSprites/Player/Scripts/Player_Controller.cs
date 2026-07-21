using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

[SelectionBase]
public class Player_Controller : MonoBehaviour
{
#region 

private enum Directions {UP, DOWN, LEFT, RIGHT}

#endregion

#region Editor Data
[Header("Movement Attributes")]
[SerializeField] float _moveSpeed = 50f;
 [Header("Dependencies")]
 [SerializeField] Rigidbody2D _rb;

[SerializeField] Animator _animator;
[SerializeField] SpriteRenderer _spriteRenderer;
#endregion

#region Internal Data
private Vector2 _moveDir = Vector2.zero;
private Directions _facingDirection = Directions.RIGHT;
private readonly int _animMoveRight = Animator.StringToHash("Walking_Right");
private readonly int _animIdeleRight = Animator.StringToHash("Idle_eight");
    #endregion

    #region Jaden Adds Stuff
     
    public PlayerState state;
    public Vector3 knockbackDirection;
    public float knockbackMaxTime = 1f;
    private float knockbackTimeLeft;
    public float knockbackMaxSpeed = 50f;
    private float knockbackSpeed;
    public float knockbackFallOffRatio = 0.95f;
    #endregion

    
    private void Start()
    {
        state = PlayerState.InControl;
    }

    #region Tick
    private void Update()
    {
       GatherInput();
       CalculateFacingDirection();
       UpdateAnimation(); 
    }


    private void FixedUpdate() //Fixed means anything with the physical system
    {
        switch (state)
        {
            case PlayerState.InControl:
                MovementUpdate();
                break;
            case PlayerState.Knockback:
                transform.Translate(knockbackSpeed * Time.deltaTime * knockbackDirection);

                // faux friction

                knockbackSpeed *= knockbackFallOffRatio;

                knockbackTimeLeft -= Time.fixedDeltaTime;
                if (knockbackTimeLeft <= 0)
                {
                    state = PlayerState.InControl;
                }
                break;
        }
    }

    #endregion

    #region Input Logic
    private void GatherInput()
    {
        _moveDir.x = Input.GetAxisRaw("Horizontal");
        _moveDir.y = Input.GetAxisRaw("Vertical");
 
    }
    #endregion

    #region Movement Logic
    private void MovementUpdate()
    {
        // removed time delta time because pretty sure lowkey we don't need it and the number makse more sense to me
        _rb.linearVelocity = _moveDir.normalized * _moveSpeed; //normalized helps to stop the player from moving faster in the diagonial direction
    }
    #endregion

    #region Animation Logic
    private void CalculateFacingDirection()
    {
        if(_moveDir.x != 0)
        {
            if (_moveDir.x > 0) //Moving Right
            {
                _facingDirection = Directions.RIGHT;
            }
            else if (_moveDir.x < 0) //Moving Left
            {
                _facingDirection = Directions.LEFT;
            }

        }
    
    }

    private void UpdateAnimation()
    {
        if(_facingDirection == Directions.LEFT)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_facingDirection == Directions.RIGHT)
        {
            _spriteRenderer.flipX = false;
        }

        if(_moveDir.SqrMagnitude() > 0) //We're moving
        {
            _animator.CrossFade(_animMoveRight, 0);
        }
        else
        {
            _animator.CrossFade(_animIdeleRight, 0);
        }
    }
    #endregion

    #region take damage and get knockbacked

    public void Knockback(Transform awayFrom)
    {
        knockbackDirection = transform.position - awayFrom.position;
        state = PlayerState.Knockback;
        knockbackTimeLeft = knockbackMaxTime;
        _rb.linearVelocity = knockbackDirection;
        knockbackSpeed = knockbackMaxSpeed;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("ouch");
            Knockback(other.transform);
        }
    }

    #endregion
} 