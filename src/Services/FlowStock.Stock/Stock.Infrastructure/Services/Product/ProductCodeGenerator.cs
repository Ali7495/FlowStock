using System.CodeDom.Compiler;
using BuildingBlocks.Domain;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Stock.Application;

namespace Stock.Infrastructure;

public sealed class ProductCodeGenerator : ICodeGenerator<ProductCode>
{
    private readonly ISequenceGenerator _sequenceGenerator;
    private readonly ILogger<ProductCodeGenerator> _logger;

    public ProductCodeGenerator(ISequenceGenerator sequenceGenerator, ILogger<ProductCodeGenerator> logger)
    {
        _sequenceGenerator = sequenceGenerator;
        _logger = logger;
    }

    public async Task<ProductCode> GenerateCodeAsync(CancellationToken cancellationToken)
    {
        long nextSequence = await _sequenceGenerator.GetNextAsync(SequenceNames.ProductCode, cancellationToken);

        if (nextSequence <= 0)
        {
            _logger.LogError("The next sequence is {seq} and not valid!", nextSequence);
            throw new DomainExceptions("The next sequence is not valid!");
        }

        return ProductCode.CreateBySequence(nextSequence);
    }
}
