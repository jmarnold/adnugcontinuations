using System;

namespace AdnugContinuations
{
    // All this means is that it has an Id, nothing else
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}