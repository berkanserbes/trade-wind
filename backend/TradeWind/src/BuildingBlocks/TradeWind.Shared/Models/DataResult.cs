namespace TradeWind.Shared.Models;

public class DataResult<T> where T : class
{
	public bool IsSuccess { get; set; } = false;

	public string Message { get; set; } = string.Empty;

	public T? Data { get; set; } = null;
}
