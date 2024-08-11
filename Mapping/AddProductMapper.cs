using ApiWithFastEndpoints.DTOs;
using ApiWithFastEndpoints.Entities;

namespace ApiWithFastEndpoints.Mapping;

// /Mapping/AddProductMapper.cs
public class AddProductMapper : Mapper<AddProductRequest, ProductResponse, Product>
{
    public override Product ToEntity(AddProductRequest addProductRequest) => new()
    {
        Name = addProductRequest.Name,
        Price = addProductRequest.Price,
    };
}
