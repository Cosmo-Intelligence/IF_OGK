
using System;
namespace RISCommonLibrary.Lib.Exceptions
{
    [global::System.Serializable]
    public class RISIfException : System.Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RISIfException() { }
        public RISIfException(string message) : base(message) { }
        public RISIfException(string message, System.Exception inner) : base(message, inner) { }
        protected RISIfException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    #region 致命的
    
    public class RISIfExceptionFatalException : RISIfException
    {
		public RISIfExceptionFatalException() : base()
		{
		}

		public RISIfExceptionFatalException(string message)
			: base(message)
		{
		}

		public RISIfExceptionFatalException(string message, Exception inner)
			: base(message, inner)
		{
		}
    }

    #endregion

    #region ユーザ例外
    
    public class RISIfExceptionUserException : RISIfException
    {
		public RISIfExceptionUserException() : base()
		{
		}

		public RISIfExceptionUserException(string message)
			: base(message)
		{
		}

		public RISIfExceptionUserException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}

    /// <summary>
    /// データなし例外
    /// </summary>
    public class RISIfExceptionDataNotFoundException : RISIfExceptionUserException
    {
        public RISIfExceptionDataNotFoundException(string message) : base(message) { }
    }
    
    #endregion

}
