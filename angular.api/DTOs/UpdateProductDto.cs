using System.ComponentModel.DataAnnotations;

namespace angular.api.DTOs;

public record UpdateProductDto(
  // [required] attribute is used in input validation to indicate that a property is required and cannot be null or empty.
  [Required][StringLength(100)] string Name,
  [Required][StringLength(100)] string Category,
  [Required][StringLength(500)] string Description,
  [Range(0, double.MaxValue)] decimal Price,
  [Range(0, double.MaxValue)] decimal? DiscountPrice,
  DateOnly ReleaseDate
);
