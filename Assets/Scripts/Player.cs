using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

public class Player : Agent
{
    [SerializeField]
    PlayerController pc;
    [SerializeField]
    GameManager gameManager;

    public override void Initialize()
    {
        pc = GetComponent<PlayerController>();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (Mathf.FloorToInt(actions.DiscreteActions[0]) == 1)
        {
            pc.Jump();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = 0;

        if (Input.GetButton("Jump"))
        {
            discreteActions[0] = 1;
        }
    }

    public override void OnEpisodeBegin()
    {
        gameManager.Start();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && gameManager.isRunning)
        {
            SetReward(-10.0f);
            gameManager.GameOver();
            EndEpisode();
        }
    }

    public void GiveReward(float r)
    {
        AddReward(r);
    }
}
