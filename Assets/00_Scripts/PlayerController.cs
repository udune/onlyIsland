using System;
using Photon.Pun;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    public float moveSpeed = 3.5f;
    private Animator animator;
    
    private Vector3 velocity;
    
    public int OwnerActorNumber { get; private set; }
    
    private PhotonView pv;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        
        animator = GetComponent<Animator>();
        pv = GetComponent<PhotonView>();
    }

    public void Initialize(int actorNumber)
    {
        if (IsMinePhoton())
        {
            OwnerActorNumber = actorNumber;
            pv.RPC("SetActorNumber", RpcTarget.AllBuffered, actorNumber);
        }
    }

    public bool IsMinePhoton()
    {
        return pv.IsMine;
    }
    
    // RPC : Remote Procedure Call - 네트워크 상의 다른 플레이어가 실행 중인 특정 메서드를 호출
    [PunRPC]
    public void SetActorNumber(int actorNumber)
    {
        OwnerActorNumber = actorNumber;
    }

    private void Update()
    {
        if (!pv.IsMine)
            return;
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        Vector3 inputDir = new Vector3(h, 0, v);

        if (inputDir.magnitude >= 0.1f)
        {
            Vector3 move = inputDir * moveSpeed * Time.deltaTime;
            agent.Move(move);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputDir), Time.deltaTime * 10.0f);
            animator.SetFloat("Movement", move.magnitude);
        }
        else
        {
            animator.SetFloat("Movement", 0);
        }
    }
}