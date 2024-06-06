namespace MvcRatings;

public class JwtCookieToHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public JwtCookieToHeaderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue("JwtToken", out var jwtToken))
        {
            context.Request.Headers.Append("Authorization", $"Bearer {jwtToken}");
        }

        await _next(context);
    }
}