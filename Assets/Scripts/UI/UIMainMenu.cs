using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Image _reImage;
    [SerializeField] float _fadeTime;

    Material _dissolveMaterial;

    bool _hasStarted;
    float _fading;

    void Start()
    {
        _dissolveMaterial = _reImage.material;
        _dissolveMaterial.SetFloat("_Fade", 0.0f);
    }
    void Update()
    {
        if(_hasStarted == true)
        {
            _fading += Time.deltaTime / _fadeTime;
            _dissolveMaterial.SetFloat("_Fade", _fading);
            if (_fading >= 1.0f)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void PlayGame() => _hasStarted = true;

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
