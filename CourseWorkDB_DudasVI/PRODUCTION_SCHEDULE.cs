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
    
    public partial class PRODUCTION_SCHEDULE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCTION_SCHEDULE()
        {
            this.SCHEDULE_PRODUCT_INFO = new HashSet<SCHEDULE_PRODUCT_INFO>();
        }
    
        public int SCHEDULE_ID { get; set; }
        public System.DateTime REQUIRED_DATE { get; set; }
        public int STAFF_ID { get; set; }
        public System.DateTime CREATED_DATE { get; set; }
        public string SCHEDULE_STATE { get; set; }
        public decimal SCHEDULE_TOTAL { get; set; }
        public int WAREHOUSE_ID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCHEDULE_PRODUCT_INFO> SCHEDULE_PRODUCT_INFO { get; set; }
        public virtual WAREHOUSE WAREHOUSE { get; set; }
        public virtual STAFF STAFF { get; set; }
    }
}
