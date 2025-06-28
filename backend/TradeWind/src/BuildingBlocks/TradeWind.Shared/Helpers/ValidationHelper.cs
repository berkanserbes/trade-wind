using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeWind.Shared.Helpers;

public static class ValidationHelper
{
	public static string GetErrorMessage(ValidationResult validationResult)
	{
		if (validationResult == null)
			return string.Empty;

		return string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage));
	}

}
