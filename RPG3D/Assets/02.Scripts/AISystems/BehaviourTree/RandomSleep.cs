using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.AISystems.BehaviourTree
{
    public class RandomSleep : Node
    {
        private float _minSleep;
        private float _maxSleep;

        private float _randomSleep;

        protected bool isSleeping = false;

        public RandomSleep(BehaviourTreeBuilder tree, BlackBoard blackBoard, float minSleep, float maxSleep)
            : base(tree, blackBoard)
        {
            _minSleep = minSleep;
            _maxSleep = maxSleep;
        }
        public override Result Invoke()
        {
            _randomSleep = UnityEngine.Random.Range(_minSleep, _maxSleep);
            Debug.Log($"Start Sleep{_randomSleep}");

            tree.Sleep(_randomSleep);
            
            return Result.Running;

            // if (!_isSleeping)
            // {
            //     Debug.Log("Start Sleep" + _remainingSleepTime);
            //     _remainingSleepTime = UnityEngine.Random.Range(_minSleepDuration, _maxSleepDuration);
            //     _isSleeping = true;
            //     
            //     tree.Sleep(_remainingSleepTime);
            // }
            //
            // if (_remainingSleepTime > 0)
            // {
            //     Debug.Log("Running Sleep" +  _remainingSleepTime);
            //     _remainingSleepTime -= Time.deltaTime;
            //     return Result.Running;
            // }
            // else
            // {
            //     _isSleeping = false;
            //     return Result.Success;
            // }
        }
    }
}