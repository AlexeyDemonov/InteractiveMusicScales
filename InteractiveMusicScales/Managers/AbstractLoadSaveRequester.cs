using System;

namespace InteractiveMusicScales
{
    internal abstract class AbstractLoadSaveRequester
    {
        public event Action<string, object> Request_Save;

        public event Func<string, Type, object> Request_Load;

        protected void InvokeSaveRequest(string filename, object instance) => Request_Save?.Invoke(filename, instance);

        protected object InvokeLoadRequest(string filename, Type loadtype) => Request_Load?.Invoke(filename, loadtype);
    }
}