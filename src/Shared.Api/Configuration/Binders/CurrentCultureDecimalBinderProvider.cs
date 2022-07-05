﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;

namespace Shared.Api.Configuration.Binders
{
    /// <summary>
    /// Provider that uses the binder model responsible for handling the decimal format for the current culture.
    /// </summary>
    public class CurrentCultureDecimalBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// Method responsible for returning an instance of model binder to decimal numbering.
        /// </summary>
        /// <param name="context">The ModelBinderProviderContext.</param>
        /// <returns>An IModelBinder.</returns>
        /// <example>
        /// In the host layer into configuration folder, create a new file that's name ***MvcOptionsConfigurations***.
        /// <para>Create a new method as below.</para>
        /// <code>
        /// public static class MvcOptionsConfigurations
        /// {
        ///     public static MvcOptions ConfigureModelBinders(this MvcOptions options)
        ///     {
        ///         options.ModelBinderProviders.Insert(1, new CurrentCultureDecimalBinderProvider());
        ///         return options;
        ///     }
        /// }
        /// </code>
        /// <note type="note">Do not forget to number the binders so that one does not replace the other.</note>
        /// </example>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(decimal) || context.Metadata.ModelType == typeof(decimal?))
                return new BinderTypeModelBinder(typeof(CurrentCultureDecimalBinder));

            return null;
        }
    }
}