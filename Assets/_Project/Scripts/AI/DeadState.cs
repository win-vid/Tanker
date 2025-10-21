using UnityEngine;

public class DeadAIState : BaseAIState
{
    public override void onEnter(AIStateMachine stateMachine)
    {
        GameObject wrackObject = ObjectPool.instance.GetPooledObject(stateMachine.wrackPrefab);
        wrackObject.transform.position = stateMachine.transform.position;
        wrackObject.transform.rotation = stateMachine.transform.rotation;
        wrackObject.GetComponent<Wrack>().playParticleSystem();

    }

    public override void onExit(AIStateMachine stateMachine)
    {

    }

    public override void onFixedUpdate(AIStateMachine stateMachine)
    {

    }

    public override void onUpdate(AIStateMachine stateMachine)
    {

    }
}
