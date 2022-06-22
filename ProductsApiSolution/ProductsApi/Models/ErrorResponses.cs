namespace ProductsApi.Models;


public class ErrorResponseMessage
{
    public string? Message { get; set; }
    public int StatusCode { get; set; }

    public HelpDeskInfo? ForHelpContact { get; set; }
    
}

public class HelpDeskInfo
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}