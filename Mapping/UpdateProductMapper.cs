using ApiWithFastEndpoints.DTOs;
using ApiWithFastEndpoints.Entities;

namespace ApiWithFastEndpoints.Mapping;

// /Mapping/UpdateProductMapper.cs
public class UpdateProductMapper : Mapper<UpdateProductRequest, ProductResponse, Product>
{
    public override Product UpdateEntity(UpdateProductRequest updateProductRequest, Product entity)
    {
        if (updateProductRequest.Name != null)
            entity.Name = updateProductRequest.Name;
        if (updateProductRequest.Price != null)
            entity.Price = updateProductRequest.Price.Value;
        return entity;
    }
}

