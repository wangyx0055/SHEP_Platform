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
    using System.Collections.Generic;
    
    public partial class T_AlarmType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public T_AlarmType()
        {
            this.T_Stats = new HashSet<T_Stats>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<double> DustRange { get; set; }
        public Nullable<double> DbRange { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<T_Stats> T_Stats { get; set; }
    }
}
