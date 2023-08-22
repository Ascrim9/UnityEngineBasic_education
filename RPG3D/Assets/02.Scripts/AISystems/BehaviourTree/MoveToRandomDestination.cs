using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.AISystems.BehaviourTree
{
    public class MoveToRandomDestination : Node
    {
        private float _movementRadian; 

        private bool _isMoving = false;


        private float _tolerance = 1.0f ;

        public MoveToRandomDestination(BehaviourTreeBuilder tree, BlackBoard blackBoard, float moveRadian)
            : base(tree, blackBoard)
        {
            _movementRadian = moveRadian;
        }
        
        public override Result Invoke()
        {
            //float l = Random.Range(0, _movementRadian);
            //float theta = Random.Range(0, 2.0f * Mathf.PI);
            //LayerMask mask = 0;

            //Vector3 expected = blackBoard.transform.position + new Vector3(l *Mathf.Cos(theta), 0.0f, l * Mathf.Sin(theta));



            //if (Physics.SphereCast(expected + (Vector3.up * 5.0f),
            //                        _tolerance,
            //                        Vector3.down,
            //                        out RaycastHit hit,
            //                        5.0f * 2.0f,
            //                        mask
            //                        ) == false)
            //{
            //    return Result.Failure;
            //}

            
            //    if(NavMesh.SamplePosition(hit.point, out NavMeshHit areahit,  _tolerance, 1 << 0))
            //    {
            //        blackBoard.agent.SetDestination(areahit.position);
            //    }

            //    return Result.Running;


            if (!_isMoving)
            {
                Debug.Log("Start MOVE");
                Vector3 dirRandomPoint = Random.insideUnitSphere * _movementRadian;
                dirRandomPoint += blackBoard.transform.position;

                NavMesh.SamplePosition(dirRandomPoint, out var navMeshHit, _movementRadian, NavMesh.AllAreas);
                
                Vector3 targetPosition = navMeshHit.position;

                blackBoard.agent.SetDestination(targetPosition);

                _isMoving = true;
            }

            if (!IsPointArrive()) return Result.Running; 
                        
            Debug.Log("Success Move");
            _isMoving = false;
            return Result.Success; 

        }

        //https://wlsdn629.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-Bake%EB%90%9C-Navmesh-%EC%98%81%EC%97%AD%EB%82%B4%EC%97%90%EC%84%9C-%EB%9E%9C%EB%8D%A4%ED%95%98%EA%B2%8C-%EC%9B%80%EC%A7%81%EC%9D%B4%EA%B8%B0

        private bool IsPointArrive()
        {
            return blackBoard.agent.remainingDistance <= blackBoard.agent.stoppingDistance
                   && !blackBoard.agent.pathPending;
        }
    }
}