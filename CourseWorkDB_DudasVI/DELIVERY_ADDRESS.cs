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
    
    public partial class DELIVERY_ADDRESS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DELIVERY_ADDRESS()
        {
            this.DELIVERY = new HashSet<DELIVERY>();
        }
    
        public int DEL_ADDRESS_ID { get; set; }
        public int DELIVERY_ADDRESS_FROM { get; set; }
        public int DELIVERY_ADDRESS_TO { get; set; }
        public decimal DISTANCE { get; set; }
    
        public virtual ADDRESS ADDRESS { get; set; }
        public virtual ADDRESS ADDRESS1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DELIVERY> DELIVERY { get; set; }
    }
}
