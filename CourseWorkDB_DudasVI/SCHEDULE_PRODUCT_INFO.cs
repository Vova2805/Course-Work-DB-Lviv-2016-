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
    
    public partial class SCHEDULE_PRODUCT_INFO
    {
        public int SCHEDULE_PRODUCT_INFO_ID { get; set; }
        public int SCHEDULE_ID { get; set; }
        public int PRODUCT_INFO_ID { get; set; }
        public int QUANTITY_IN_SCHEDULE { get; set; }
        public int RELEASED_QUANTITY { get; set; }
    
        public virtual PRODUCT_INFO PRODUCT_INFO { get; set; }
        public virtual PRODUCTION_SCHEDULE PRODUCTION_SCHEDULE { get; set; }
    }
}
