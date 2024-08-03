using UnityEngine;

namespace SillyBalls
{
    public class SillyBallComponent : MonoBehaviour
    {
        void Update()
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, Vector3.zero, Plugin.SillyBallsShrinkSpeed.Value * Time.deltaTime);
            if (gameObject.transform.localScale.x <= Plugin.SillyBallsMinSize.Value)
            {
                Destroy(gameObject);
            }
        }
    }
}
