//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CourseWorkDB_DudasVI
{
    using System;
    using System.Collections.Generic;
    
    public partial class SALARY
    {
        public int SALARY_ID { get; set; }
        public decimal SALARY_VALUE { get; set; }
        public System.DateTime CHANGED_DATE { get; set; }
        public int POST_ID { get; set; }
    
        public virtual POST POST { get; set; }
    }
}
