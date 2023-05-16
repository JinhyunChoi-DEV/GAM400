
//using UnityEngine;

//public class Test : MonoBehaviour
//{
//    [SerializeField] private Rigidbody rigidbody;
//    [SerializeField] private CapsuleCollider collider;

//    public Vector3 Forward => gameObject.transform.forward;
//    public Vector3 Right => gameObject.transform.right;

//    private MovementStateMachine moveStateMachine;

//    public bool IsGround { get; private set; }
//    private Vector3 origin;
//    private float radius;

//    private float minGravity = -0.5f;
//    private float maxGravity = -10.0f;
//    private float gravityTimer = 0.01f;
//    private float gravityIncrement = -1.0f;

//    private float currentGravity = 0.0f;
//    private float fallingTimer = 0.0f;

//    void Start()
//    {
//        radius = collider.radius;
//        origin = collider.center + collider.gameObject.transform.position;
//        fallingTimer = gravityTimer;
//        currentGravity = minGravity;

//        moveStateMachine = new MovementStateMachine(this);
//    }

//    void Update()
//    {
//        moveStateMachine.Update();
//    }

//    void FixedUpdate()
//    {
//        float maxDistance = collider.bounds.extents.y - radius + 0.01f;
//        IsGround = Physics.SphereCast(origin, radius, Vector3.down, out var hit, maxDistance);

//        Vector3 velocity = rigidbody.velocity;
//        moveStateMachine.FixedUpdate(ref velocity);
//        velocity.y += GetGravity();

//        rigidbody.velocity = velocity;
//    }

//    float GetGravity()
//    {
//        float result;

//        if (IsGround)
//        {
//            result = 0.0f;
//            currentGravity = minGravity;
//            fallingTimer = gravityTimer;
//        }
//        else
//        {
//            fallingTimer -= Time.fixedDeltaTime;

//            if (fallingTimer < 0.0f)
//            {
//                if (currentGravity > maxGravity)
//                    currentGravity += gravityIncrement;

//                fallingTimer = gravityTimer;
//            }

//            result = currentGravity;
//        }

//        return result;
//    }



//    void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;

//        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward);

//        origin = collider.center + collider.gameObject.transform.position;
//        radius = collider.radius;

//        float maxDistance = collider.bounds.extents.y - radius + 0.01f;
//        if (Physics.SphereCast(origin, radius, Vector3.down, out var hit, maxDistance))
//        {
//            Gizmos.color = Color.red;
//            Gizmos.DrawWireSphere(origin + Vector3.down * hit.distance, radius);
//        }
//    }
//}
//public class MovementStateMachine
//{
//    private IdleStateA idle;
//    private WalkStateA walk;
//    private SprintStateA sprint;
//    private JumpStateA jump;
//    private AirStateA air;
//    private LandingStateA landing;

//    private MoveState_A current;
//    private Test character;

//    public MoveState_A GetState(MovementState state)
//    {
//        switch (state)
//        {
//            case MovementState.Idle:
//                return idle;

//            case MovementState.Walk:
//                return walk;

//            case MovementState.Sprint:
//                return sprint;

//            case MovementState.Jump:
//                return jump;

//            case MovementState.Air:
//                return air;

//            case MovementState.Landing:
//                return landing;

//            default:
//                return null;
//        }
//    }

//    public MovementStateMachine(Test character)
//    {
//        this.character = character;

//        idle = new IdleStateA(character, this);
//        walk = new WalkStateA(character, this);
//        sprint = new SprintStateA(character, this);
//        jump = new JumpStateA(character, this);
//        air = new AirStateA(character, this);
//        landing = new LandingStateA(character, this);

//        current = idle;
//    }

//    public void Update()
//    {
//        if (current == null)
//            return;

//        current.Update();

//        if (current.HasNext())
//            current = current.Next();
//        else
//            current = FindNext();
//    }

//    public void FixedUpdate(ref Vector3 velocity)
//    {
//        if (current == null)
//            return;

//        var input = InputCommand.GetInput();
//        current.UpdateMove(input.Direction, ref velocity);
//    }

//    private MoveState_A FindNext()
//    {
//        if (!character.IsGround)
//        {
//            return air;
//        }

//        var input = InputCommand.GetInput();
//        if (input.Jump)
//        {
//            return jump;
//        }
//        //else if (input.Dodge)
//        //{

//        //}
//        else if (input.Sprint && input.Direction.magnitude != 0)
//        {
//            return sprint;
//        }
//        else if (input.Direction.magnitude != 0)
//        {
//            return walk;
//        }
//        else
//        {
//            return idle;
//        }
//    }
//}

//public abstract class MoveState_A : IState<MoveState_A>
//{
//    protected MoveState_A next = null;

//    public bool HasNext()
//    {
//        return next != null;
//    }

//    public MoveState_A Next()
//    {
//        return next;
//    }

//    public virtual void Update()
//    {
//    }

//    public void Exit()
//    {
//    }

//    public abstract void UpdateMove(Vector2 direction, ref Vector3 velocity);
//}

//public class IdleStateA : MoveState_A
//{
//    private Test character;
//    private MovementStateMachine stateMachine;

//    public IdleStateA(Test character, MovementStateMachine stateMachine)
//    {
//        this.character = character;
//        this.stateMachine = stateMachine;
//    }

//    public override void UpdateMove(Vector2 direction, ref Vector3 velocity)
//    {
//        velocity = Vector3.zero;
//    }
//}

//public class WalkStateA : MoveState_A
//{
//    private Test character;
//    private MovementStateMachine stateMachine;

//    public WalkStateA(Test character, MovementStateMachine stateMachine)
//    {
//        this.character = character;
//        this.stateMachine = stateMachine;
//    }

//    public override void Update()
//    {
//        Debug.Log("Walk");
//        base.Update();
//    }

//    public override void UpdateMove(Vector2 direction, ref Vector3 velocity)
//    {
//        velocity = (character.Forward * direction.y + character.Right * direction.x) * 2.5f;
//    }
//}

//public class SprintStateA : MoveState_A
//{
//    private Test character;
//    private MovementStateMachine stateMachine;

//    public SprintStateA(Test character, MovementStateMachine stateMachine)
//    {
//        this.character = character;
//        this.stateMachine = stateMachine;
//    }

//    public override void Update()
//    {
//        Debug.Log("Sprint");
//        base.Update();
//    }

//    public override void UpdateMove(Vector2 direction, ref Vector3 velocity)
//    {
//        velocity = (character.Forward * direction.y + character.Right * direction.x) * 5.0f;
//    }
//}

//public class AirStateA : MoveState_A
//{
//    private Test character;
//    private MovementStateMachine stateMachine;

//    public AirStateA(Test character, MovementStateMachine stateMachine)
//    {
//        this.character = character;
//        this.stateMachine = stateMachine;
//    }

//    public override void Update()
//    {
//        if (character.IsGround)
//        {
//            next = stateMachine.GetState(MovementState.Landing);
//            Debug.Log("Here");
//        }

//        Debug.Log("Air");
//        base.Update();
//    }

//    public override void UpdateMove(Vector2 direction, ref Vector3 velocity)
//    {

//    }
//}

//public class LandingStateA : MoveState_A
//{
//    private Test character;
//    private MovementStateMachine stateMachine;

//    public LandingStateA(Test character, MovementStateMachine stateMachine)
//    {
//        this.character = character;
//        this.stateMachine = stateMachine;
//    }
//    public override void Update()
//    {
//        Debug.Log("Landing");
//        base.Update();
//    }

//    public override void UpdateMove(Vector2 direction, ref Vector3 velocity)
//    {
//        velocity.y = 0.0f;
//    }
//}

//public class JumpStateA : MoveState_A
//{
//    private Test character;
//    private MovementStateMachine stateMachine;

//    public JumpStateA(Test character, MovementStateMachine stateMachine)
//    {
//        this.character = character;
//        this.stateMachine = stateMachine;
//    }

//    public override void Update()
//    {
//        Debug.Log("Jump");
//        next = stateMachine.GetState(MovementState.Air);
//        base.Update();
//    }

//    public override void UpdateMove(Vector2 direction, ref Vector3 velocity)
//    {
//        velocity.y += 50.0f;
//    }
//}

using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Test : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            rb.AddForce(Vector3.up * 10);
        }
        else if (Input.GetKey(KeyCode.F2))
        {
            rb.velocity = rb.velocity + Vector3.down;
        }
    }
}