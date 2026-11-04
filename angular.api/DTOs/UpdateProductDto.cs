namespace angular.api.DTOs;

public record UpdateProductDto(
  string Name,
  string Description,
  decimal Price,
  decimal? DiscountPrice,
  DateOnly ReleaseDate
);
