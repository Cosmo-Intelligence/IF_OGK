using System;

namespace RISCommonLibrary.Lib.LogCleaner
{
	public delegate void UpdatePropFunc(ILogCleaner target);

	public interface ILogCleaner
    {
        String GetName();
        void CleanUp();
        void UpdateProp();
    }
}
