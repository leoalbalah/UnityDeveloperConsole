/*
 * Created by LeonardoMartin
 * Created 3/3/2022 1:51:09 PM
 */
using System.Linq;
using TMPro;
using UnityEngine;

namespace DevConsole.FPSCounter
{
    /// <summary>  
    /// Debug tool to display the current frame rate on screen
    /// </summary>
    public class FPSCounter : MonoBehaviour
    {
        #region Properties

        [Header("Properties")] [Tooltip("Object To Enable (Canvas)")]
        public Canvas canvas;

        [Tooltip("Text Object")] public TextMeshProUGUI text;

        #endregion

        #region Variables

        private int _lastFrameIndex;
        private float[] _frameDeltaTimeArray = new float[50];

        #endregion

        #region LifeCycle

        private void Update()
        {
            StableFramerate();
        }

        #endregion

        private void StableFramerate()
        {
            _frameDeltaTimeArray[_lastFrameIndex] = Time.deltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;

            var total = _frameDeltaTimeArray.Sum();

            text.SetText(Mathf.RoundToInt(_frameDeltaTimeArray.Length / total).ToString() + " fps");
        }

        public void Enable()
        {
            canvas.gameObject.SetActive(true);
        }

        public void Disable()
        {
            canvas.gameObject.SetActive(false);
        }
    }
}