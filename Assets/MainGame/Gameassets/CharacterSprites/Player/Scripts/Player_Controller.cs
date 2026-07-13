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

    #region Tick
    private void Update()
    {
       GatherInput();
       CalculateFacingDirection();
       UpdateAnimation(); 
    }

    private void FixedUpdate() //Fixed means anything with the physical system
    {
        MovementUpdate();
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
        _rb.velocity = _moveDir.normalized * _moveSpeed * Time.fixedDeltaTime; //normalized helps to stop the player from moving faster in the diagonial direction
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
} 