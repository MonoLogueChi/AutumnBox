﻿namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    public class ExecuteResult
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Message
        {
            get { return _message; }
            internal set { _message = value; _isHandled = true; }
        }
        /// <summary>
        /// 是否正确完成
        /// </summary>
        public bool IsSuccessful
        {
            get { return _isSuccessful; }
            internal set { _isSuccessful = value; _isHandled = true; }
        }
        /// <summary>
        /// 具体输出
        /// </summary>
        public OutputData OutputData
        {
            get { return _outputData; }
            internal set { _outputData = value; }
        }
        /// <summary>
        /// 是否已经进行处理
        /// </summary>
        public bool IsHandled { get { return _isHandled; } }

        public ExecuteResult()
        {
        }
        public ExecuteResult(OutputData o)
        {
            this.OutputData = o;
        }
        private bool _isHandled = false;
        private string _message = string.Empty;
        private bool _isSuccessful = true;
        private OutputData _outputData;
    }
}
