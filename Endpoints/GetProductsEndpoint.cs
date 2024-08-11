using ApiWithFastEndpoints.Data;
using ApiWithFastEndpoints.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApiWithFastEndpoints.Endpoints;

public class GetProductsEndpoint : Endpoint<GetProductsQuery, ProductResponse[]>
{
    private readonly AppDbContext _context;

    public GetProductsEndpoint(AppDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetProductsQuery query, CancellationToken ct)
    {
        var queryable = _context.Products.AsNoTracking()
                                         .AsQueryable();

        if (string.IsNullOrEmpty(query.Name) == false)
            queryable = queryable.Where(c => c.Name.Contains(query.Name));

        var result = await queryable.Select(c => new ProductResponse
        {
            Id = c.Id,
            Name = c.Name,
            Price = c.Price,
        }).ToArrayAsync(ct);

        await SendOkAsync(result, ct);
    }
}
