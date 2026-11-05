namespace angular.api.DTOs;

// A DTO (Data Transfer Object) is a simple object that is used as a contract to transfer data between the client and the server.
// It is typically used to encapsulate data and send it over the network or between different parts of an application. 
// In this case, the ProductDto class is used to represent a product in the application and transfer its data between the API the client.

// We use a Record type here because it is a simple data container that is immutable and has value-based equality semantics.
public record ProductDetailsDto(
  int Id,
  string Name,
  int CategoryId,
  string Description,
  decimal Price,
  decimal? DiscountPrice,
  DateOnly ReleaseDate
);
