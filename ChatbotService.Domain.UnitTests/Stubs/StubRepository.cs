using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoFixture;
using ChatbotProject.Common.Infrastructure.Mongo;
using ChatbotProject.Common.Infrastructure.Mongo.Interfaces;

namespace ChatbotService.Domain.UnitTests.stubs;

[ExcludeFromCodeCoverage]
public class StubRepository<TDocument> : IRepository<TDocument> where TDocument : BaseEntity
{
    private readonly IFixture _fixture;
    private Task<TDocument>? _response;
    public StubRepository(IFixture fixture, TDocument? response)
    {
        _fixture = fixture;
        _response = Task.FromResult(response);
    }

    public async Task AddOrUpdateDocument(TDocument document)
    {
        _response = Task.FromResult(document);
       await Task.CompletedTask;
    }

    public async Task DeleteDocument(TDocument document)
    {
        await Task.CompletedTask;
    }

    public Task<TDocument> GetDocument(Expression<Func<TDocument, bool>> filter)
    {
        return _response;
    }

    public Task<List<TDocument>> GetDocuments(Expression<Func<TDocument, bool>> filter)
    {
        return Task.FromResult(_fixture.CreateMany<TDocument>().ToList());
    }
}