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
    
    public partial class PRODUCT_INFO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT_INFO()
        {
            this.INGREDIENTS = new HashSet<INGREDIENTS>();
            this.ORDER_PRODUCT = new HashSet<ORDER_PRODUCT>();
            this.SCHEDULE_PRODUCT_INFO = new HashSet<SCHEDULE_PRODUCT_INFO>();
            this.RELEASED_PRODUCT = new HashSet<RELEASED_PRODUCT>();
            this.PRODUCT_PRICE = new HashSet<PRODUCT_PRICE>();
        }
    
        public int CATEGORY_ID { get; set; }
        public int PRODUCT_INFO_ID { get; set; }
        public string PRODUCT_TITLE { get; set; }
        public string PRODUCT_DESCRIPTION { get; set; }
        public int QUANTITY_IN_PACKAGE { get; set; }
        public int PACK_DESCR_ID { get; set; }
        public decimal PRODUCTION_PRICE { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INGREDIENTS> INGREDIENTS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_PRODUCT> ORDER_PRODUCT { get; set; }
        public virtual PACKAGE_DESCRIPTION PACKAGE_DESCRIPTION { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SCHEDULE_PRODUCT_INFO> SCHEDULE_PRODUCT_INFO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RELEASED_PRODUCT> RELEASED_PRODUCT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT_PRICE> PRODUCT_PRICE { get; set; }
    }
}
