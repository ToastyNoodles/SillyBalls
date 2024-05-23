using UnityEngine;

namespace CustomPlugin
{
    public class BallLifeTimeComponent : MonoBehaviour
    {
        void Update()
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, Vector3.zero, Plugin.scaleDownSpeed.Value * Time.deltaTime);
            if (gameObject.transform.localScale.x <= 0.0f)
                Destroy(gameObject);
        }
    }
}
