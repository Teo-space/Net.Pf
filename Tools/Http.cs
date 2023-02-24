using Microsoft.AspNetCore.Http;

public static class Http
{
    //HttpContext Context

    public static string RemoteIp(this HttpContext Context) => RemoteIp(Context);
    public static string RemoteIp(this HttpResponse Response) => Response?.HttpContext?.Connection?.RemoteIpAddress?.ToString();


    public static string Header(this HttpContext Context, string key) => Header(Context.Request, key);
    public static string Header(this HttpRequest Request, string key)
	{
		Microsoft.Extensions.Primitives.StringValues result;

		if (Request.Headers.TryGetValue(key, out result) )
		{
			return result;
		}
		if (Request.Headers.TryGetValue(key.ToLower(), out result))
		{
			return result;
		}

		return string.Empty;
	}

	public static string UserAgent(this HttpRequest Request) => Header(Request, "User-Agent");
	public static string Forwarded(this HttpRequest Request) => Header(Request, "x-forwarded-for");



	


}