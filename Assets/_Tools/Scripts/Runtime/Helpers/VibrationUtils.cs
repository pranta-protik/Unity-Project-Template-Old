using MoreMountains.NiceVibrations;
using UnityEngine;

namespace _Tools.Helpers
{
    public static class VibrationUtils
    {
        #region Variables

        private static int _VibrationStatus;

        private const string VIBRATION_STATUS = "VibrationStatus";

        #endregion

        #region Constructor

        static VibrationUtils() => _VibrationStatus = PlayerPrefs.GetInt(VIBRATION_STATUS, 1);

        #endregion

        #region Custom Methods

        public static void VibrateDevice(VibrationScale vibrationScale)
        {
            if (_VibrationStatus == 0) return;

            MMVibrationManager.Haptic((HapticTypes)vibrationScale);
            DebugUtils.Log($"{vibrationScale} Vibration");
        }

        public static void SetVibrationStatus(int status)
        {
            _VibrationStatus = status;
            PlayerPrefs.SetInt(VIBRATION_STATUS, status);
        }

        #endregion
    }
}