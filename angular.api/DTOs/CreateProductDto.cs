using System.ComponentModel.DataAnnotations;

namespace angular.api.DTOs;

public record CreateProductDto(
  // [required] attribute is used in input validation to indicate that a property is required and cannot be null or empty.
  [Required][StringLength(100)] string Name,
  [Range(1, 100)] int CategoryId,
  [Required][StringLength(500)] string Description,
  [Range(0, double.MaxValue)] decimal Price,
  [Range(0, double.MaxValue)] decimal? DiscountPrice,
  DateOnly ReleaseDate
);
