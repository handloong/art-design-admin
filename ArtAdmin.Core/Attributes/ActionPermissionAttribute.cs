namespace ArtAdmin
{
    public class ActionPermissionAttribute : Attribute
    {
        /// <summary>
        /// 接口权限标注
        /// </summary>
        /// <param name="type">类型:0查询接口,1按钮接口</param>
        /// <param name="name">接口名称,用于UI配置菜单和httpLog记录</param>
        public ActionPermissionAttribute(ActionType type, string name)
        {
            AuthType = type;
            Name = name;
        }

        public ActionType AuthType { get; }
        public string Name { get; }
    }

    public enum ActionType
    {
        /// <summary>
        /// 查询接口
        /// </summary>
        Query = 0,

        /// <summary>
        /// 按钮接口
        /// </summary>
        Button = 1
    }
}