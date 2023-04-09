using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TD.UI
{
    public class CameraFacing : MonoBehaviour
    {
        private void LateUpdate() // calling this on LateUpdate instead of Update because camera position is being updating on other updates and this might cause glitches.
        {
                transform.forward = Camera.main.transform.forward;
        }
    }
}
