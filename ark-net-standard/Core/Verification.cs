using System.Collections.Generic;

namespace ArkNet.Core
{
	public class Verification
	{
		private readonly List<string> errors = new List<string>();

		public override string ToString()
		{
			var response = "";
			if (errors.Count > 0)
				response = string.Join(", ", errors.ToArray());
			else
				response = "Verified";

			return response;
		}

		public void AddError(string errorDescription)
		{
			errors.Add(errorDescription);
		}
	}
}