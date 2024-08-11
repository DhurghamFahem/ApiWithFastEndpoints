using ApiWithFastEndpoints.Data;
using ApiWithFastEndpoints.DTOs;
using ApiWithFastEndpoints.Mapping;

namespace ApiWithFastEndpoints.Endpoints;

// /Endpoints/UpdateProductEndpoint.cs
class UpdateProductEndpoint : Endpoint<UpdateProductRequest, ProductResponse, UpdateProductMapper>
{
    private readonly AppDbContext _context;

    public UpdateProductEndpoint(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Patch("products/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateProductRequest req, CancellationToken ct)
    {
        var id = Route<int>("id");
        var product = await _context.Products.FindAsync(id, ct);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        product = Map.UpdateEntity(req, product);

        _context.Products.Update(product);
        await _context.SaveChangesAsync(ct);

        var response = Map.FromEntity(product);
        await SendOkAsync(response, ct);
    }
}
