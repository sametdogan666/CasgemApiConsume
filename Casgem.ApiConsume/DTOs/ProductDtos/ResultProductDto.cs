﻿namespace Casgem.ApiConsume.DTOs.ProductDtos;

public class ResultProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Brand { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}