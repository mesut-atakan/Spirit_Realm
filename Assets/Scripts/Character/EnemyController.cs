using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;



namespace Character
{
    internal class EnemyController : CharacterProperties
    {
        #region SerializeFields

        [Header("Positions")]

        [Tooltip("Enter the Transformations in which the character will go back and forth between 2 points!")]
        [SerializeField] private Transform[] targetPositions = new Transform[2];
        #endregion






        private void Update() {
            Move();    
        }


        internal override void Damage(CharacterProperties characterProperties)
        {
            
        }

        internal override void Move()
        {
            foreach(Transform _targetTransform in this.targetPositions)
            {
                while(Mathf.Clamp(_targetTransform.position.x, (this.transform.position.x - _targetTransform.position.x) - 1, this.transform.position.x -_targetTransform.position.x + 1 ) >0.1f)
                {
                    this.transform.Translate(Vector2.Lerp(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(_targetTransform.position.x, this.transform.position.y), 0.005f));
                } 
            }
        }

        internal override void TakeDamage(CharacterProperties characterProperties)
        {
            
        }
    }
}