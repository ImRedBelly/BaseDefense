using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Library
{
    public class LoaderScene : MonoBehaviour
    {
        [SerializeField] private Slider downloadSlider;

        private void Start()
        {
            downloadSlider.DOValue(1, 2).OnComplete(() => SceneManager.LoadScene(1));
        }
    }
}