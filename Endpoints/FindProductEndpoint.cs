using ApiWithFastEndpoints.Data;
using ApiWithFastEndpoints.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiWithFastEndpoints.Endpoints;

public class FindProductEndpoint : EndpointWithoutRequest<ProductResponse>
{
    private readonly AppDbContext _context;

    public FindProductEndpoint(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var product = await _context.Products.AsNoTracking()
                                             .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new ProductResponse
        {
            Id = id,
            Name = product.Name,
            Price = product.Price,
        };

        await SendOkAsync(response, ct);
    }
}
