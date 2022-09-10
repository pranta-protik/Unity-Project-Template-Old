using _Tools.Helpers;
using UnityEngine;

namespace _Tools.Managers
{
    public class LogManager : Singleton<LogManager>
    {
        #region Variables

        [SerializeField] private bool _isLogEnabled = true;
        [SerializeField] private bool _isWarningEnabled = true;
        [SerializeField] private bool _isErrorEnabled = true;
        [SerializeField] private Color _logColor = Color.white;
        [SerializeField] private Color _warningColor = Color.yellow;
        [SerializeField] private Color _errorColor = Color.red;

        #endregion

        #region Override Methods

        protected override void OnAwake()
        {
            base.OnAwake();
            
            DebugUtils.UpdateLogStatus(_isLogEnabled);
            DebugUtils.UpdateWarningStatus(_isWarningEnabled);
            DebugUtils.UpdateErrorStatus(_isErrorEnabled);
            DebugUtils.UpdateLogColor(_logColor);
            DebugUtils.UpdateWarningColor(_warningColor);
            DebugUtils.UpdateErrorColor(_errorColor);
        }

        #endregion
    }
}