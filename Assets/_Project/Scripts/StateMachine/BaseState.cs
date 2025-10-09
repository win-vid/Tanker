using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState : MonoBehaviour
{
    public abstract void onEnter(StateMachine stateMachine);
    public abstract void onUpdate(StateMachine stateMachine);
    public abstract void onFixedUpdate(StateMachine stateMachine);
    public abstract void onExit(StateMachine stateMachine);
}
