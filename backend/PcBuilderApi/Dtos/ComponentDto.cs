﻿using PcBuilderApi.Controllers;

namespace PcBuilderApi.Dtos
{
    public record struct ComponentGet
    (
        int Id,
        string Name,
        string Description,
        decimal Price
    );

    public record struct ComponentPut
    (

    );

    public record struct ComponentPost
    (
        string Name,
        string Description,
        decimal Price,
        List<ComponentCharactPostPut> Characts
    );
}
