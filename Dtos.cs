using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service
{
    public record ItemDto(Guid Id, [Required] string Name,
                  [Required] string Description, decimal Price,
                   DateTimeOffset CreatedDate);
    public record CreatedItemDto([Required] string Name, [Required] string Description, [Range(0, 1000)] decimal Price);
    public record UpdatedItemDto([Required] string Name, string Description, [Range(0, 1000)] decimal Price);
}
