// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Systems;

public abstract partial class SystemBase
{
    protected internal World World { get; set; } = null!;

    public virtual void Startup()
    {
    }

    public virtual void PreUpdate()
    {
    }

    public virtual void Update()
    {
    }

    public virtual void PostUpdate()
    {
    }

    public virtual void FixedUpdate()
    {
    }

    public virtual void Render()
    {
    }

    public virtual void Cleanup()
    {
    }
}
