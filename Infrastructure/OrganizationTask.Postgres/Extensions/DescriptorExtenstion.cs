using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;

namespace OrganizationTask.Postgres.Extensions;

/// <summary>
/// Добавляет описания полей в EF-конфигурацию
/// [Description] -> HasComment
/// </summary>
public static class DescriptorExtenstion
{
	/// <summary>
	/// Метод добавления описания полей в EF-конфигурацию.
	/// </summary>
	public static EntityTypeBuilder<T> HasDescription<T>(this EntityTypeBuilder<T> builder)
        where T : class
    {
        var props = typeof(T).GetProperties();

        foreach (var prop in props)
        {
            // Проверка необходима, чтобы игнорировать навигационные свойства
            if (prop.CustomAttributes.Any())
            {
                AttributeCollection? attributes = TypeDescriptor.GetProperties(typeof(T))[prop.Name]?.Attributes;

                var attribute = attributes?.OfType<DescriptionAttribute>().FirstOrDefault();

                if (attribute != null)
                {
                    builder
                        .Property(prop.Name)
                        .HasComment(attribute.Description);
                }
            }
        }

        // Описание сущности
        var entityAttribute = TypeDescriptor.GetAttributes(typeof(T)).OfType<DescriptionAttribute>().FirstOrDefault();
        builder.ToTable(t => t.HasComment(entityAttribute?.Description));

        return builder;
    }
}