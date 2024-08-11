using ApiWithFastEndpoints.Data;

namespace ApiWithFastEndpoints.Endpoints;

public class DeleteProductEndpoint : EndpointWithoutRequest
{
    private readonly AppDbContext _context;

    public DeleteProductEndpoint(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Delete("products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<int>("id");
        var product = await _context.Products.FindAsync(id, ct);
        if (product is null)
        {
            await SendNoContentAsync(ct);
            return;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        await SendNoContentAsync();
    }
}
