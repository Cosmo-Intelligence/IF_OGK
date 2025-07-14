using System.Data;
using RISCommonLibrary.Lib.Utils;

namespace RISODPLibrary.Lib.Utils
{
	/// <summary>
	/// Commandユーティリティ
	/// </summary>
	public static class ODPCommandUtils
	{
		public static void InitCommandODP(this IDbCommand command)
		{
			const int DEFAULT_COMMAND_TIMEOUT = 30;
            ODPCommandUtils.InitCommandODP(command, DEFAULT_COMMAND_TIMEOUT);
		}

        public static void InitCommandODP(this IDbCommand command, int commandTimeout)
		{
			((Oracle.DataAccess.Client.OracleCommand)command).BindByName = true; //BindByNameを使いためにしょうがなく
			CommandUtils.InitCommand(command, commandTimeout);
		}
	}
}
