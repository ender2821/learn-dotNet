namespace angular.api.DTOs;

public record CreateProductDto(
  string Name,
  string Description,
  decimal Price,
  decimal? DiscountPrice,
  DateOnly ReleaseDate
);
