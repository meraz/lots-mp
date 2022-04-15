using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lots
{
    public class PlayerState : MonoBehaviour
    {
         // TODO meraz better system is needed, a collection of bools is not... goood
        bool movingLeft = false;
        bool movingRight = false;
        bool movingUp = false;
        bool movingDown = false;

        public float movingSpeed = 0; // TODO meraz public get, private set

        public bool IsMovingLeft()
        {
            return movingLeft;
        }

        public bool IsMovingRight()
        {
            return movingRight;
        }

        public void UpdateState(Vector2 deltaStep)
        {
            movingSpeed = deltaStep.magnitude;

            if(deltaStep.x > 0)
            {
                movingRight = true;
                movingLeft = false;
            }
            else if(deltaStep.x < 0)
            {
                movingRight = false;
                movingLeft = true;
            }

            if(deltaStep.y > 0)
            {
                movingUp = true;
                movingDown = false;
            }
            else if(deltaStep.y < 0)
            {
                movingUp = false;
                movingDown = true;
            }
        }
    }
}
