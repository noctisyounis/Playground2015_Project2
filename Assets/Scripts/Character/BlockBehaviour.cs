using UnityEngine;
using System.Collections;



public class BlockBehaviour : MonoBehaviour
{

    #region Public Properties
    [Range(0.0f,0.4f)]
    public float m_shakeIntensity;
    [Range(2000f,5000f)]
    public float m_forceMove;
    #endregion

    #region Main methods
    void Start()
    {
        m_rgbd2d = GetComponent<Rigidbody2D>();
        m_initialPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Pushable") && other.gameObject.CompareTag("PushWave"))
        {
            float OtherVeloX = other.GetComponent<Rigidbody2D>().velocity.x;

            if (OtherVeloX > 0)
            {
                m_rgbd2d.AddForce(Vector2.right * m_forceMove, ForceMode2D.Impulse);
            }

            else if (OtherVeloX < 0)
            {
                m_rgbd2d.AddForce(Vector2.left * m_forceMove, ForceMode2D.Impulse);
            }
        }

        else if (gameObject.CompareTag("Destructible") && other.gameObject.CompareTag("DestroyWave"))
        {
            Destroy(gameObject);
        }

        else
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        m_initialPosition = transform.position;
        float m_initialShakeIntensity = m_shakeIntensity;

        while (m_shakeIntensity > 0)
        {
            transform.position = m_initialPosition + Random.insideUnitCircle * m_shakeIntensity;
            m_shakeIntensity -= Time.deltaTime * 0.8f;  // to decrease the shake effect
            yield return new WaitForSeconds(0.015f);
        }

        if (m_shakeIntensity <= 0)
        {
            transform.position = m_initialPosition;
            m_shakeIntensity = m_initialShakeIntensity;
        }
    }
    #endregion

    #region Private properties
    Rigidbody2D m_rgbd2d;
    Vector2 m_initialPosition;
    #endregion

}

