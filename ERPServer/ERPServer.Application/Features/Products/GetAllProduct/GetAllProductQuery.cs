﻿using MediatR;
using TS.Result;

namespace ERPServer.Application.Features.Products.GetAllProduct;
public sealed record GetAllProductQuery() : IRequest<Result<List<GetAllProductQueryResponse>>>;
