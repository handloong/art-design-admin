using Microsoft.Extensions.DependencyModel;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Loader;
using System.Security.Claims;

namespace ArtAdmin
{
    public class App
    {
        static App()
        {
            Assemblies = GetAssemblies();
            EffectiveTypes = Assemblies.SelectMany(GetTypes);
            LoadLanguageData();
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public static IConfiguration Configuration { get; internal set; }

        /// <summary>
        /// 已经加载的Json文件
        /// </summary>
        public static List<string> ConfigJsonFiles { get; internal set; } = new();

        /// <summary>
        /// ServiceProvider
        /// </summary>
        public static IServiceProvider ServiceProvider { get; internal set; }

        /// <summary>
        /// 当前HttpContext 对应的ClaimsPrincipal
        /// </summary>
        public static ClaimsPrincipal User => HttpContext?.User;

        /// <summary>
        /// IWebHostEnvironment是否是开发环境
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment { get; internal set; }

        /// <summary>
        /// 当前HttpContext
        /// </summary>
        public static HttpContext HttpContext => ServiceProvider?.GetService<IHttpContextAccessor>()?.HttpContext;

        /// <summary>
        /// 读取配置
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static TOptions Get<TOptions>(string path)
        {
            var options = Configuration.GetSection(path).Get<TOptions>();
            return options;
        }

        /// <summary>
        /// 应用有效程序集(类型为project,项目应用的,不包括包和手动应用)
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 有效程序集类型
        /// </summary>
        public static readonly IEnumerable<System.Type> EffectiveTypes;

        public static IEnumerable<Assembly> GetAssemblies()
        {
            var projects = DependencyContext
                                .Default
                                .RuntimeLibraries
                                .Where(u => u.Type == "project" || u.Type == "reference")
                                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)));

            return projects;
        }

        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        /// <summary>
        /// 加载程序集中的所有类型
        /// </summary>
        /// <param name="ass"></param>
        /// <returns></returns>
        private static IEnumerable<System.Type> GetTypes(Assembly ass)
        {
            var types = Array.Empty<System.Type>();

            try
            {
                types = ass.GetTypes();
            }
            catch
            {
                Console.WriteLine($"Error load `{ass.FullName}` assembly.");
            }

            return types.Where(u => u.IsPublic);
        }

        #region 多语言

        private static void LoadLanguageData()
        {
            var i18nDirectory = Path.Combine(Directory.GetCurrentDirectory(), "JsonConfig", "i18n");
            if (!Directory.Exists(i18nDirectory))
                Directory.CreateDirectory(i18nDirectory);

            var files = Directory.GetFiles(i18nDirectory);

            foreach (var item in files)
            {
                if (File.Exists(item))
                {
                    string jsonContent = File.ReadAllText(item);
                    var languageDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonContent);
                    LanguageData[Path.GetFileNameWithoutExtension(item)] = languageDict;
                }
            }
        }

        /// <summary>
        /// 语言配置信息
        /// </summary>
        private static readonly Dictionary<string, Dictionary<string, string>> LanguageData = new();

        public static string L(string errorCode)
        {
            var languageCode = Get<string>("SystemConfig:DefaultLanguageCode") ?? "zh-cn";

            var headers = HttpContext.Request.Headers;

            if (headers.ContainsKey("X-Language"))
            {
                if (headers.ContainsKey("X-Language"))
                    languageCode = headers["X-Language"].ToString();
            }

            if (LanguageData.ContainsKey(languageCode) && LanguageData[languageCode].ContainsKey(errorCode))
            {
                return LanguageData[languageCode][errorCode];
            }
            else
            {
                return errorCode;
            }
        }

        #endregion
    }
}