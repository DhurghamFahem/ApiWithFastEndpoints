using ApiWithFastEndpoints.Data;
using ApiWithFastEndpoints.DTOs;
using ApiWithFastEndpoints.Mapping;

namespace ApiWithFastEndpoints.Endpoints;

// /Endpoints/AddProductEndpoint.cs
public class AddProductEndpoint : Endpoint<AddProductRequest, ProductResponse, AddProductMapper>
{
    private readonly AppDbContext _context;

    public AddProductEndpoint(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Post("products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(AddProductRequest req, CancellationToken ct)
    {
        var product = Map.ToEntity(req);
        await _context.Products.AddAsync(product, ct);
        await _context.SaveChangesAsync(ct);

        var response = Map.FromEntity(product);
        await SendCreatedAtAsync<AddProductEndpoint>(new { id = product.Id }, response, cancellation: ct);
    }
}
