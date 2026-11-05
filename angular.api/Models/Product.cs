namespace angular.api.Models;
// This is the data model for the Product entity. 
// It represents the structure of the Product table in the database and is used to map data between the application and the database.
public class Product
{
  public int Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public Category? Category { get; set; }
  // Category Id is used here so we don't have to load the entire Category object when we just need the Id. 
  // This is a common practice in database design to avoid unnecessary data loading and improve performance.
  public int CategoryId { get; set; }
  public decimal Price { get; set; }
  public decimal? DiscountPrice { get; set; }
  public DateOnly ReleaseDate { get; set; }
}
