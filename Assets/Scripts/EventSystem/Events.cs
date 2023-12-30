
using UnityEngine;

public static class Events
{
    public static readonly EventActions OnGameStart = new EventActions();
    public static readonly EventActions OnGameEnd = new EventActions();
    public static readonly EventActions OnBallThrowing = new EventActions();
    public static readonly EventActions OnBallHiting = new EventActions();
    public static readonly EventActions OnBallAtHitArea = new EventActions();
    public static readonly EventActions<Vector3> OnBallHitted = new EventActions<Vector3>();
    public static readonly EventActions<Vector3> OnBallCatched = new EventActions<Vector3>();
    public static readonly EventActions<Vector3> OnBallGoesToNextBaseCatcher= new EventActions<Vector3>();
    public static readonly EventActions OnBallAtLastBaseCatcher = new EventActions();
    public static readonly EventActions OnPitcherAtLastBase = new EventActions();
}
