//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SHEP_Platform
{
    using System;
    
    public partial class T_Users_GetList_Result
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<byte> Status { get; set; }
        public System.DateTime RegTime { get; set; }
        public int RoleId { get; set; }
        public Nullable<System.DateTime> LastTime { get; set; }
        public Nullable<System.DateTime> NowTime { get; set; }
        public string Remark { get; set; }
    }
}
