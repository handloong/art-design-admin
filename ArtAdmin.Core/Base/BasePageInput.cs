using System.ComponentModel.DataAnnotations;

namespace ArtAdmin
{
    public class BasePageInput
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public virtual int Current { get; set; } = 1;

        /// <summary>
        /// 每页条数
        /// </summary>
        [Range(1, 2000, ErrorMessage = "页码容量超过最大限制")]
        public virtual int Size { get; set; } = 10;

        /// <summary>
        /// 排序字段
        /// </summary>
        public virtual string SortField { get; set; }

        /// <summary>
        /// 排序方式，升序：ascend；降序：descend"
        /// </summary>
        public virtual string SortOrder { get; set; } = "desc";

        /// <summary>
        /// 关键字
        /// </summary>
        public virtual string SearchKey { get; set; }
    }
}