﻿using ERPServer.Domain.Abstractions;

namespace ERPServer.Domain.Entities;
public sealed class StockMovement : Entity
{
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }

    public Guid DepotId { get; set; }
    public decimal NumberOfEntries { get; set; }
    public decimal NumberOfOutputs { get; set; }
    public decimal Price { get; set; }
    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public Guid? ProductionId { get; set; }
    public Production? Production { get; set; }
}
