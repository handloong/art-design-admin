namespace ArtAdmin
{
    public class Unify
    {
        private static readonly string ExtDataKey = "__UnifyWithWriteInvoked";
        private static readonly string ErrorKey = "__UnifyWithErrorInvoked";

        public static void Write(object exts)
        {
            var items = App.HttpContext?.Items;
            if (items.ContainsKey(ExtDataKey))
                items[ExtDataKey] = exts;
            else
                items.Add(ExtDataKey, exts);
        }

        public static object Read()
        {
            if (App.HttpContext?.Items?.TryGetValue(ExtDataKey, out object exts) == true)
                return exts;
            else
                return null;
        }

        public static dynamic SetError(string errors, params object[] args)
        {
            var items = App.HttpContext?.Items;
            errors = string.Format(errors, args);

            if (items.ContainsKey(ErrorKey))
                items[ErrorKey] = errors;
            else
                items.Add(ErrorKey, errors);

            return default;
        }

        public static dynamic SetErrorL(string errorCode, params object[] args)
        {
            var error = App.L(errorCode);
            error = string.Format(error, args);
            return SetError(error);
        }

        public static string GetError()
        {
            if (App.HttpContext?.Items?.TryGetValue(ErrorKey, out object errors) == true)
                return errors.ToString();
            else
                return null;
        }
    }
}