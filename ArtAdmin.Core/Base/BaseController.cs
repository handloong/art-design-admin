namespace ArtAdmin.Core
{
    /// <summary>
    /// 原生Controller
    /// </summary>
    [ApiController]
    public class BaseControllerAspNet : ControllerBase
    {
    }

    /// <summary>
    /// 一个最普通的Api (规范化返回结果)
    /// </summary>
    [ApiController]
    public class BaseController : ControllerBase
    {
    }

    /// <summary>
    /// 需要jwt授权
    /// </summary>
    [ApiController]
    [Authorize]
    public class BaseControllerAuthorize : ControllerBase
    {
    }

    /// <summary>
    /// 需要接口授权
    /// </summary>
    [ApiController]
    [Authorize]
    [RolePermission]
    public class BaseControllerRoleAuthorize : ControllerBase
    {
    }

    /// <summary>
    /// 超级管理员可访问
    /// </summary>
    [ApiController]
    [Authorize]
    [SuperAdmin]
    public class BaseControllerSuperAdmin : ControllerBase
    {
    }
}