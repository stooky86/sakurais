using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Lamp : MonoBehaviour
{
    private bool _isLightOn = true;
    public bool LightOn
    {
        get { return _isLightOn; }
        set { _isLightOn = value; }
    }

    [SerializeField]
    private GameObject _light;
    [SerializeField]
    private GameObject _bulbLight;
    [SerializeField]
    private float _angleOfDetection = 180.0f;

    private List<AIController> _aiControllers = new List<AIController>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add the AIControllers to the list and increase their angle of detection if they overlap with the light source
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && _isLightOn)
        {
            if(other.isTrigger && other.GetType() == typeof(CapsuleCollider))
            {
                AIController aiController = other.GetComponent<AIController>();
                if(aiController != null)
                {
                    aiController.SetAngleOfDetection(_angleOfDetection);
                    if(!_aiControllers.Contains(aiController))
                    {
                        _aiControllers.Add(aiController);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // Remove the AIEnemies from the list and reset their angle of detection
    public void ResetAIControllers()
    {
        foreach(AIController aiController in _aiControllers)
        {
            if(aiController != null)
            {
                aiController.ResetAngleOfDetection();
            }
        }
        _aiControllers.Clear();
    }

    // Reset angle of detection of enemy if they exit the light
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(other.isTrigger && other.GetType() == typeof(CapsuleCollider))
            {
                AIController aiController = other.GetComponent<AIController>();
                if(aiController != null)
                {
                    aiController.ResetAngleOfDetection();
                }
            }
        }
    }
}
