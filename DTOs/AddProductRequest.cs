namespace ApiWithFastEndpoints.DTOs;

// /DTOs/AddProductRequest.cs
public class AddProductRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
