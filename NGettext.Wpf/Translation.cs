﻿using System;
using System.Globalization;
using System.Linq;

namespace NGettext.Wpf
{
    public static class Translation
    {
        public static string _(string msgId, params object[] @params)
        {
            if (Localizer is null)
            {
                CompositionRoot.WriteMissingInitializationErrorMessage();
                return (@params.Any() ? string.Format(CultureInfo.InvariantCulture, msgId, @params) : msgId);
            }
            return @params.Any() ? Localizer.Catalog.GetString(msgId, @params) : Localizer.Catalog.GetString(msgId);
        }

        [Obsolete("This public property will be removed in 1.1")]
        public static ILocalizer Localizer { get; set; }

        public static string Noop(string msgId) => msgId;

        public static string PluralGettext(int n, string singularMsgId, string pluralMsgId, params object[] @params)
        {
            if (Localizer is null)
            {
                CompositionRoot.WriteMissingInitializationErrorMessage();
                return string.Format(CultureInfo.InvariantCulture, n == 1 ? singularMsgId : pluralMsgId, @params);
            }

            return @params.Any()
                ? Localizer.Catalog.GetPluralString(singularMsgId, pluralMsgId, n, @params)
                : Localizer.Catalog.GetPluralString(singularMsgId, pluralMsgId, n);
        }
    }
}