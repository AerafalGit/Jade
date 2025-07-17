// Copyright (c) AerafalGit 2025.
// Jade licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Jade/blob/main/LICENSE.

namespace Jade.Ecs.Components;

public readonly partial struct ComponentMask
{
    public static ComponentMask Create<T1>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4, T5>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4, T5, T6>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4, T5, T6, T7>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4, T5, T6, T7, T8>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id)
            .With(Component<T8>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id)
            .With(Component<T8>.Metadata.Id)
            .With(Component<T9>.Metadata.Id);
    }

    public static ComponentMask Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
    {
        return new ComponentMask()
            .With(Component<T1>.Metadata.Id)
            .With(Component<T2>.Metadata.Id)
            .With(Component<T3>.Metadata.Id)
            .With(Component<T4>.Metadata.Id)
            .With(Component<T5>.Metadata.Id)
            .With(Component<T6>.Metadata.Id)
            .With(Component<T7>.Metadata.Id)
            .With(Component<T8>.Metadata.Id)
            .With(Component<T9>.Metadata.Id)
            .With(Component<T10>.Metadata.Id);
    }
}
