#region

using FSM;
using GameJam.Core;
using UnityEngine;

#endregion

public class Chasing : State<string>
{
#region Private Variables

    private readonly Monster1  self;
    private readonly float     moveSpeed;
    private readonly Transform target;
    private readonly Animator  animator;
    private readonly float     stopDistance = 0.1f;
    private          int       deltaTime;

#endregion

#region Constructor

    public Chasing(Monster1 self , Transform target , Animator animator , float moveSpeed)
    {
        this.self      = self;
        this.target    = target;
        this.animator  = animator;
        this.moveSpeed = moveSpeed;
    }

#endregion

#region Public Methods

    public override void OnEnter()
    {
        animator.Play("Walk");
    }

    public override void OnLogic()
    {
        var noTarget = target == null;
        if (noTarget) return;

        var targetPosition     = target.position;
        var selfPosition       = self.GetPos();
        var dir                = (targetPosition - selfPosition).normalized;
        var distanceWithTarget = Vector2.Distance(targetPosition , selfPosition);
        var needStop           = distanceWithTarget <= stopDistance;
        if (needStop) return;

        var time = deltaTime != 1 ? Time.deltaTime : deltaTime;

        // moveSpeed: 9999
        var movement         = selfPosition + dir * moveSpeed * time;
        var newDistance      = Vector2.Distance(targetPosition , movement);
        var moveOverPosition = newDistance >= distanceWithTarget;
        var finalPosition    = moveOverPosition ? targetPosition : movement;
        self.SetPos(finalPosition);
        // var newPos = Vector3.MoveTowards(selfPosition , targetPosition , moveSpeed * time);
        // self.SetPos(newPos);
        var facingRight = dir.x > 0;
        self.SetFacing(facingRight ? Facing.Right : Facing.Left);
    }

    public void SetDeltaTime(int deltaTime)
    {
        this.deltaTime = deltaTime;
    }

#endregion
}